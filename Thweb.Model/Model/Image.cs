using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thweb.Model.Model
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DirectoryPath { get; set; }
        [Required]
        public string SaveFileName { get; set; }
        [Required]
        public string OriginFileName { get; set; }
        [Required]
        public string TableName { get; set; }  // 연결된 테이블 이름
        [Required]
        public int TableId { get; set; }  // 연결된 테이블의 Id
    }
}
