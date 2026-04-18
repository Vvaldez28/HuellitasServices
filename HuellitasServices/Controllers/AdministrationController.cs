using Microsoft.AspNetCore.Mvc;

namespace HuellitasServices.Controllers
{
    using Common.Services;
    using Huellitas.Services.Administration;
    using Microsoft.EntityFrameworkCore.Query;

    public class AdministrationController(AdministrationService administrationService) : ControllerBase
    {
        [HttpGet("Authentication")]
        public async Task<ActionResult<AuthenticationData>> GetAuthentication(string email, string password)
        {
            try
            {
                return await administrationService.GetAuthentication(email,password);
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("RestartPassword")]
        public async Task<ActionResult<string>> RestartPassword(string email)
        {

            try
            {
                return await administrationService.RestartPassword(email);
             
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPut("ChangePassword")]
        public async Task<ActionResult<bool>> ChangePassword(string email, string password)
        {

            try
            {
                return await administrationService.ChangePassword(email, password);

            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpGet("PetsCount")]
        public async Task<ActionResult<int>> PetsCount()
        {
            try
            {
                return await administrationService.PetsCount();
            }

            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("PendingAdoption")]
        public async Task<ActionResult<int>> PendingAdoption()
        {
            try
            {
                return await administrationService.PendingAdoption();
            }

            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("User")]
        public async Task <ActionResult<UserData>> GetUser(int userId)
        {
            try
            {
                return await administrationService.GetUser(userId);
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("Userlist")]
        public async Task<ActionResult<List<User>>> GetListUser()
        {
            try
            {
                var pets = await administrationService.GetListUser();
                return Ok(pets);
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("Money2")]
        public async Task<ActionResult> PutMoney2([FromBody]MoneyTransactionData data)
        {

            try
            {
                await administrationService.PutMoney2(data);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPut("Money")]
        public async Task<ActionResult> PutMoney([FromBody] MoneyTransactionData data)
        {

            try
            {
                await administrationService.PutMoney(data);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpDelete("Money")]
        public async Task<ActionResult> DeleteMoney( int MoneyTransactionId)
        {

            try
            {
                await administrationService.DeleteMoney(MoneyTransactionId);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("UserDonation")]
        public async Task<ActionResult<List< UserDonationData>>> GetDonation(int userId)
        {
            try
            {
                return await administrationService.GetDonation(userId);
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("NewUser")]
        public async Task<ActionResult> PutNewUser([FromBody] UserData data)
        {

            try
            {
                await administrationService.PutNewUser(data);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPut("PutUser")]
        public async Task<ActionResult> PutUser([FromBody] UserData data)
        {

            try
            {
                await administrationService.PutUser(data);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(int id)
        {

            try
            {
                await administrationService.DeleteUser(id);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        //__________________________________________________________________________ pet
        [HttpGet("Petslist")]
        public async Task<ActionResult<List<Pet>>> GetListPets(int statusId)
        {
            try
            {
                var pets = await administrationService.GetListPets(statusId);
                return Ok(pets);
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("Pet")]
        public async Task<ActionResult<Pet>> GetPet(int petId)
        {
            try
            {
                return await administrationService.GetPet(petId);
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("Pet1")]
        public async Task<ActionResult> PostPet([FromBody] PetData data)
        {

            try
            {
                await administrationService.PostPet(data);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("GetPetPhoto")]
        public async Task<ActionResult<PhotoData>> GetPetPhoto(string PetId)
        {
            try
            {
                return await administrationService.GetPetPhoto(PetId);
            }
            catch (CustomException ex)
            {
                return StatusCode(217, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("Pet")]
        public async Task<ActionResult> DeletePet(int petId)
        {

            try
            {
                await administrationService.DeletePet( petId);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPut("Pet")]
        public async Task<ActionResult> PutPet([FromBody] PetData data)
        {

            try
            {
                await administrationService.PutPet(data);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPut("ChangePutStatus")]
        public async Task<ActionResult> ChangePutStatus([FromBody] ChangePutStatusData request)
        {
            try
            {
                await administrationService.ChangePutStatus(request);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //-------------------------------------

        [HttpGet("UserRoles")]
        public async Task<ActionResult<List<RolData>>> GetRolUser(int userId)
        {
            try
            {
                var pets = await administrationService.GetRolUser(userId);
                return Ok(pets);
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("PutUserRoles")]
        public async Task<ActionResult> PutRolUser([FromBody] UserRolData data)
        {

            try
            {
                await administrationService.PutRolUser(data);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpDelete("DeleteRolUser")]
        public async Task<ActionResult> DeleteRolUser(int userId,int rolId)
        {

            try
            {
                await administrationService.DeleteRolUser(userId,rolId);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        //__________________________________________________________________________ Event
        [HttpPut("Event")]
        public async Task<ActionResult> PutEvent([FromBody] Eventdata data)
        {

            try
            {
                await administrationService.PutEvent(data);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("EventList")]
        public async Task<ActionResult<List<EventResponse>>> GetListEvent()
        {
            try
            {
                var events = await administrationService.GetListEvent();
                return Ok(events);
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete("DeleteEvent")]
        public async Task<ActionResult> DeleteEvent(int id)
        {

            try
            {
                await administrationService.DeleteEvent(id);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("GetListMoney")]

        public async Task<ActionResult<List<MoneyTransaction>>> GetListMoney()
        {
            try
            {
                var events = await administrationService.GetListMoney();
                return Ok(events);
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("totalingresos")]
        public ActionResult<string> GetTotalIngresos()
        {
            try
            {
                var total = administrationService.GetTotalIngresos();
                return Ok($"${total:N2}"); 
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("totalegresos")]
        public ActionResult<string> GetTotalEgresos()
        {
            try
            {
                var total = administrationService.GetTotalEgresos();
                return Ok($"${total:N2}"); 
            }
            catch (CustomException ex)
            {
                return StatusCode(207, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }


}
