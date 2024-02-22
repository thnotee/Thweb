using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thweb.Model.Model
{
    public class OrderHeader
    {
        [Key]
        public string OrderNo { get; set; }
        public string ThwebUserId { get; set; }

        [ForeignKey("ThwebUserId")]
        [ValidateNever]
        public ThwebUser ThwebUser { get; set; }

        /// <summary>
        /// 주문일자
        /// </summary>
        public DateTime OrderDate { get; set; }
        
        /// <summary>
        /// 우편번호
        /// </summary>
        [Required]
        public string BuyerPostCode { get; set; }

        /// <summary>
        /// 주소 + 상세주소
        /// </summary>
        [Required]
        public string BuyerPostAddr { get; set; }

        /// <summary>
        /// 주문자 이름
        /// </summary>
        [Required]
        public string BuyerName { get; set; }

        /// <summary>
        /// 주문자 명
        /// </summary>
        [Required]
        public string BuyerPhoneNumber { get; set; }

        /// <summary>
        /// 상태 
        ///       "ready" = 미결제(결제창을 띄우는 순간 ready 생성됨)
        ///       "paid"  = 결제완료
        ///       "cancelled" = 결제취소
        ///       "failed" = "결제 실패"
        /// </summary>
        [Required]
        public string State { get; set; }

        /// <summary>
        /// 상품명
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 결제시간
        /// </summary>
        [Required]
        public string PaidAt { get; set; }
        /// <summary>
        /// 결제금액
        /// </summary>
        [Required]
        public int PaidAmount { get; set; }

        /// <summary>
        /// 운송장번호
        /// </summary>
        public string? TrackingNumber { get; set; }
        /// <summary>
        /// 배송택배사
        /// </summary>
        public string? Carrier { get; set; }

        /// <summary>
        /// 배송시작일
        /// </summary>
        public DateTime? ShippingDate { get; set; }
    }
}
