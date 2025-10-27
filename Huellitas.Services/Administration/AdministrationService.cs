using Common.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BCrypt.Net;
namespace Huellitas.Services.Administration
{
    public class AdministrationService(AdministrationContext context) : DbService<AdministrationContext>(context)
    {

        public async Task<AuthenticationData> GetAuthentication(string email, string password)
        {
            var user = await Context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user == null)
                return new AuthenticationData { IsAuthenticated = false, IsEnabled = true };

            if (!user.Enabled)
                return new AuthenticationData { IsAuthenticated = false, IsEnabled = false };

            var credentials = await Context.Credentials
                .Where(x => x.UserId == user.UserId).ToListAsync();

            Credential matchingCredential = null;
            foreach (var cred in credentials)
            {
                if (BCrypt.Net.BCrypt.Verify(password, cred.CredentialData))
                {
                    matchingCredential = cred;
                    break;
                }
            }

            if (matchingCredential == null)
            {
                return new AuthenticationData { IsAuthenticated = false, IsEnabled = true };
            }
            var newcred = false;
            if (matchingCredential.CredentialTypeId == 1)
            {
                newcred = true;
            }
            var U = new UserData
            {
                Email = user.Email,
                FullName = user.FullName,
                UserId = user.UserId,
                UserName = user.UserName,
                NewPassword = newcred
            };
            return new AuthenticationData { IsAuthenticated = true,IsEnabled = true, UserData = U};
        }
        public async Task<string> RestartPassword(string email)
        {
            var userId= await (from U in Context.Users
                where U.Email == email
                select U.UserId).FirstOrDefaultAsync();

            if (userId == 0)
                throw new CustomException("Usuario no existe");

            string tempPassword = CryptoUtils.getHashPassword(DateTime.Now.ToString());

            await DeleteCredentials (userId, tempPassword,1);

            return tempPassword;
        }
        public async Task DeleteCredentials(int userId, string tempPassword,int type)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(tempPassword);

                var cred = Context.Credentials.Where(x => x.UserId == userId).ToList();

                Context.Credentials.RemoveRange(cred);
                await Context.SaveChangesAsync();

                var newcred = new Credential
                {
                    UserId = userId,
                    CredentialTypeId = type,
                    CredentialData = hashedPassword
                };

                Context.Credentials.Add(newcred);
                await Context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> ChangePassword(string email, string password)
        {
            var userId = await (from U in Context.Users
                                where U.Email == email
                                select U.UserId).FirstOrDefaultAsync();


            string hashedPasswordDefault = BCrypt.Net.BCrypt.HashPassword("L45Grull45");


           await DeleteCredentials(userId, password,2);
            var cred = new Credential
            {
                UserId = userId,
                CredentialTypeId = 3,
                CredentialData = hashedPasswordDefault
            };
            Context.Credentials.Add(cred);
            await Context.SaveChangesAsync();
            return true;


        }
    
        public async Task<int> PetsCount()
        {
            return await Context.Pets.CountAsync();
        }
        public async Task<int> PendingAdoption()
        {
            return await Context.Pets.CountAsync(x => x.PetStatusId < 3);
        }
        public async Task<UserData> GetUser(int userId)
        {
            var result = await (from U in Context.Users
                                where U.UserId == userId
                                select new UserData
                                {
                                    Email = U.Email,
                                    //Enabled = U.Enabled,
                                    FullName = U.FullName,
                                    UserName = U.UserName,

                                }).FirstOrDefaultAsync();
            if (result == null)
                throw new CustomException("El Usuario no existes");


            return result;



        }
        public async Task<List<User>> GetListUser()
        {
            return await Context.Users.ToListAsync();
           // return await Context.Users.Where(x => x.UserId >2 ).ToListAsync();
        }

        public async Task DeletePet(int petId)
        {


            var result = await Context.Pets.Where(x => x.PetId == petId).FirstOrDefaultAsync();
            if (result == null)
                throw new CustomException("El Perro que quieres eliminar no existe");

            Context.Pets.Remove(result);
            await Context.SaveChangesAsync();

        }
        public async Task<Pet> GetPet(int petId)
        {
            var result = await Context.Pets.Include(x=>x.Status).Include(x=>x.Size).FirstOrDefaultAsync(x=>x.PetId==petId);
            if (result == null)
                throw new CustomException("La mascota no existe");

            return result;
        }

        public async Task<List<Pet>> GetListPets(int statusId)
        {
            return await Context.Pets.ToListAsync();
        }
        public async Task  PostPet(PetData data)
        {
            var result = new Pet
            {

                PetName = data.PetName,
                PetYears = data.PetYears,
                PetBreed = data.PetBreed,
                PetSizeId = data.PetSizeId,
                PetStatusId = 1,
                CreateDate = DateOnly.FromDateTime(DateTime.Now),
                PetDescription = data.PetDescription,
                Photo = data.Photo,
                UserName = data.UserName,
                

            };
            Context .Pets.Add(result);
            await Context.SaveChangesAsync();
        }
        public async Task PutPet(PetData data)
        {
            var Pet = await Context.Pets.Where(x => x.PetId == data.PetId).FirstOrDefaultAsync();
            if (Pet == null)
                throw new CustomException("El usuario que quieres editar no existe");

           Pet.PetName = data.PetName;
            Pet.PetYears = data.PetYears;
            Pet.PetBreed = data.PetBreed;
            Pet.PetSizeId = data.PetSizeId;
            Pet.Photo = data.Photo;
            Pet.PetDescription = data.PetDescription;
           //Pet.CloseDate = data.CloseDate;
            //Pet.UserName = data.UserName;

            await Context.SaveChangesAsync();
        }
        public async Task PutNewUser(UserData data)
        {

            var result = new User
            {
                Email = data.Email,
                Enabled = true,
                FullName = data.FullName,
                UserName = data.UserName,
                credential = new Credential
                {
                    CredentialTypeId = 1,
                    CredentialData = BCrypt.Net.BCrypt.HashPassword("Huellitas")
                }

            };
            Context.Users.Add(result);
            await Context.SaveChangesAsync();
            
        }
        public async Task PutUser(UserData data)
        {
            var User = await Context.Users.Where(x => x.UserId == data.UserId).FirstOrDefaultAsync();
            if (User == null)
                throw new CustomException("El usuario que quieres editar no existe");
            User.Email = data.Email;
            //User.Enabled = data.Enabled;
            User.FullName = data.FullName;
            User.UserName = data.UserName;
            await Context.SaveChangesAsync();
        }
        public async Task DeleteUser(int id)
        {
            
           
            var result=await Context.Users.Where(x=>x.UserId == id).FirstOrDefaultAsync();
             if (result == null)
                throw new CustomException("El usuario que quieres eliminar no existe");
            
                Context.Users.Remove(result);
               await Context.SaveChangesAsync();
            
        }

        public async Task PutMoney2(MoneyTransactionData data)
        {
            var user = await Context.Users.Where(x => x.UserId == data.UserId).FirstOrDefaultAsync();
            if (user == null)
                throw new CustomException("El usuario no existe ");

            var Money = new MoneyTransaction
            {
                TransactionTypeId = data.TransactionTypeId,
                Amount = data.Amount,
                Comments = data.Comments,
                CreateDate = DateTime.Now,
                PaymentTypeId = data.PaymentTypeId,
                UserId = data.UserId,
            };
            Context.MoneyTransactions.Add(Money);
            await Context.SaveChangesAsync();
        }


        public async Task PutMoney(MoneyTransactionData data)
        {
            var Money = await Context.MoneyTransactions.Where(x => x.MoneyTransactionId == data.MoneyTransactionId).FirstOrDefaultAsync();
            if (Money == null)
                throw new CustomException("El movimiento que quieres editar no existe");


            Money.TransactionTypeId = data.TransactionTypeId;
            Money.PaymentTypeId = data.PaymentTypeId;
            Money.UserId = data.UserId;
            Money.Amount = data.Amount;
            Money.Comments = data.Comments;

            await Context.SaveChangesAsync();
        }

        public async Task DeleteMoney(int id)
        {
            var money = await Context.MoneyTransactions.Where(x => x.MoneyTransactionId == id).FirstOrDefaultAsync();

            if (money != null)
            {
                Context.MoneyTransactions.Remove(money);
                await Context.SaveChangesAsync();
            }
        }
        public async Task<List<UserDonationData>> GetDonation(int userId)
        {
            var user =await Context.Users.Where(x=>x.UserId==userId).FirstOrDefaultAsync();
            if (user == null)
                throw new CustomException("El usuario no existe");
            
            var result = await (from U in Context.Users
                                join M in Context.MoneyTransactions on U.UserId equals M.UserId
                                where U.UserId == userId
                                select new UserDonationData
                                {
                                    UserName = U.UserName,
                                    Amount = M.Amount,
                                    CreateDate = M.CreateDate

                                }).ToListAsync();
            return result;

        }

        public async Task ChangePutStatus(ChangePutStatusData data)
        {
            var pet = await Context.Pets
                .FirstOrDefaultAsync(x => x.PetId == data.PetId);

            if (pet == null)
                throw new CustomException("La mascota no existe");

            pet.PetStatusId = data.StatusId;
            await Context.SaveChangesAsync();
        }
       
        public async Task<List<RolData>> GetRolUser(int userId)
        {
            var result = await (from UR in Context.UserRoles
                                join R in Context.Roles on UR.RolId equals R.RolId
                                where UR.UserId == userId
                                select new RolData
                                {
                                    RolId = UR.RolId,
                                    RolName = R.RolName.Trim()
                                    
                                }).ToListAsync();
            return result;

        }

        public async Task PutRolUser(UserRolData data)
        {
            var existingUserRole = await Context.UserRoles
                .Where(x => x.UserId == data.UserId && x.RolId == data.RolId)
                .FirstOrDefaultAsync();

            if (existingUserRole != null)
                throw new CustomException("El rol para este usuario ya existe.");

            var newUserRole = new UserRol
            {
                UserId = data.UserId,
                RolId = data.RolId
            };

            Context.UserRoles.Add(newUserRole);
            await Context.SaveChangesAsync();
        }
        public async Task DeleteRolUser(int userId, int rolId)
        {
            var userRole = await Context.UserRoles
                .Where(x => x.UserId == userId && x.RolId == rolId)
                .FirstOrDefaultAsync();

            if (userRole == null)
                throw new CustomException("El rol para este usuario no existe.");

            Context.UserRoles.Remove(userRole);
            await Context.SaveChangesAsync();
        }


        public async Task PutEvent(Eventdata data)
        {
            var ev = await Context.Events
                .Where(x => x.EventId == data.IdEvent)
                .FirstOrDefaultAsync();

            if (ev == null)
            {
                
                ev = new Event
                {
                    EventTitle = data.EventTitle,
                    Description = data.Description,
                    Date = data.Date,
                    Location = data.Location,
                    Image = data.Image
                };

                Context.Events.Add(ev);
            }
            else
            {
              
                ev.EventTitle = data.EventTitle;
                ev.Description = data.Description;
                ev.Date = data.Date;
                ev.Location = data.Location;
                ev.Image = data.Image;
            }

            await Context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetListEvent()
        {
            return await Context.Events.ToListAsync();
        }

        public async Task DeleteEvent(int id)
        {
            var money = await Context.Events.Where(x => x.EventId == id).FirstOrDefaultAsync();

            if (money != null)
            {
                Context.Events.Remove(money);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<List<MoneyTransaction>> GetListMoney()
        {
            return await Context.MoneyTransactions.ToListAsync();
        }


        public decimal GetTotalIngresos()
        {
            return context.MoneyTransactions
                           .Where(t => t.TransactionTypeId == 1)
                           .Sum(t => t.Amount);
        }
        public decimal GetTotalEgresos()
        {
            return context.MoneyTransactions
                           .Where(t => t.TransactionTypeId == 2)
                           .Sum(t => t.Amount);
        }






    }
}
