using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Services.Administration
{
    public class AuthenticationData
    {
        public bool IsAuthenticated { get; set; }
        public bool IsEnabled { get; set; }

        public UserData? UserData { get; set; }
    }

    public class UserData
    {
        public int? UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;
        public bool? NewPassword { get; set; }


    }
    public class MoneyTransactionData
    {
        public int? MoneyTransactionId { get; set; }
        public int TransactionTypeId { get; set; }

        public int PaymentTypeId { get; set; }

        public int UserId { get; set; }

        public decimal Amount { get; set; }

        public string Comments { get; set; } = null!;

    }
    public class UserDonationData
    {
        public string UserName { get; set; } = null!;
        public decimal Amount { get; set; }

        public DateTime CreateDate { get; set; }

    }
    public class PetData
    {
        public int? PetId { get; set; }
        public string PetName { get; set; } = null!;

        public string PetYears { get; set; } = null!;

        public string PetBreed { get; set; } = null!;

        public int PetSizeId { get; set; }
        public int? PetStatusId { get; set; }
        public string PetDescription { get; set; } = null!;
        public DateOnly? CreateDate { get; set; }

        public string? Photo { get; set; }

        public string? UserName { get; set; }
        public DateOnly? CloseDate { get; set; }

    }
    public class ChangePutStatusData
    {
        public int PetId { get; set; }
        public int StatusId { get; set; }
    }
    public class RolData
    {
        public int RolId { get; set; }
        public string RolName { get; set; } = null!;

    }
    public class UserRolData
    {
        public int UserId { get; set; }
        public int RolId { get; set; }
    }
    public class Eventdata
    {
       public int? IdEvent { get; set; }
        public string EventTitle { get; set; } = null;
        public string Description { get; set; } = null;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null;
        public string Image { get; set; } = null;

    }
}
