
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Thweb.Model.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 제목
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 설명
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 가격
        /// </summary>
        [Required]
        public int Price { get; set; }

        /// <summary>
        /// 사용여부 
        ///         0 : 미사용
        ///         1 : 사용
        /// </summary>
        [Required]
        public int ProductTF { get; set; }

        /// <summary>
        /// 등록일
        /// </summary>
        [Required]
        public DateTime RegDate { get; set; }


        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }


    }
}
