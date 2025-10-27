
namespace Huellitas.Services.Administration
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FullName { get; set;} = null!;

        public bool Enabled { get; set; }
        public Credential? credential { get; set; } 
    }
    public class TransactionType
    {
        public int TransactionTypeId { get; set; }

        public string TransactionTypeEn { get; set; } = null!;

        public string TransactionTypeSP { get; set; } =null!;
    }
    public class PaymentType
    { 
        public int PaymentTypeId { get; set; }

        public string PaymentTypeNameEn { get; set; } = null!;

        public string PaymentTypeNameEs { get; set;} =null!;

    }
    public class MoneyTransaction
    {
        public int MoneyTransactionId { get; set; }

        public int TransactionTypeId { get; set; }

        public int PaymentTypeId { get; set;} 

        public int UserId { get; set;} 

        public decimal Amount { get; set; } 

        public string Comments { get; set; } = null!;

        public DateTime CreateDate { get; set; }

    }
    public class CredentialType
    {
        public int CredentialTypeId { get;set; }

        public string CredentialTypeName { get; set; }= null!;

    }
    public class Credential
    {
        public int UserId { get; set; }

        public int CredentialTypeId { get; set; }

        public string CredentialData { get; set; } = null!;
    }
    public class Pet
    {
        public int PetId { get; set; }
        public string PetName { get; set;} = null!;
        public string PetYears { get; set;} = null!;
        public string PetBreed { get; set; } = null!;
        public int PetSizeId {  get; set; }
        public int PetStatusId { get; set; }
        public string PetDescription { get; set; } = null!;
        public string Photo {  get; set; } = null!;
        public DateOnly CreateDate {  get; set; }
        public string UserName { get; set; } = null!;
        public DateOnly? CloseDate { get; set; }
      public PetStatus? Status { get; set; }
        public PetSize? Size { get; set; }

    }
    public class PetStatus
    {
        public int PetStatusId { get; set; }
        public string PetStatusName { get; set; } = null!;
    }
    public class PetSize
    { 
        public int PetSizeId { get;set; }
        public string PetSizeldName { get;set; } = null!;

    }
    public class Rol
    {
        public int RolId { get; set; }
        public string RolName { get; set; } = null!;
    }
    public class UserRol
    {
        public int UserId { get; set; }
        public int RolId { get; set; }
    }
    public class Event
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public string? Image { get; set; }

    }
}
