using BookHaven.Domain.Enum;

namespace BookHaven.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; }  = string.Empty;
        public Gender Gender { get; set; } 
    }
}