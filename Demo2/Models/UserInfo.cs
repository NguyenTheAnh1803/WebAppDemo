using System.ComponentModel.DataAnnotations;

namespace Demo2.Models
{
    public class UserInfo
    {

        public int Id { get; set; }
        public string? Name { get; set; }  // nullable
        public int? Age { get; set; }      // nullable
        public string? Email { get; set; } // nullable
    }
}
