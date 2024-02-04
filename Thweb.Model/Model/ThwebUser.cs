using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Thweb.Model.Model
{
    public class ThwebUser : IdentityUser
    { 
        [Required]
        public string Nickname { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string PostName { get; set; }
        [Required]
        public string PostDetail { get; set; }
        [Required]
        public DateTime RegDate { get; set; }
        [Required]
        public DateTime UpdDate { get; set; }
    }
}
