using System.ComponentModel.DataAnnotations;

namespace Demo2.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
