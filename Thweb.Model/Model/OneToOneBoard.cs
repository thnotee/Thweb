using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thweb.Model.Model
{
    public class OneToOneBoard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Contents { get; set; }

        [Required]
        public DateTime RegDate{ get; set; }

        [Required]
        public string Reply { get; set; }

        [Required]
        public DateTime ReplyDate { get; set; }

        [Required]
        [ForeignKey("ThwebUser")]
        public string UserId { get; set; }

        

        public ThwebUser ThwebUser { get; set; }

    }
   
}
