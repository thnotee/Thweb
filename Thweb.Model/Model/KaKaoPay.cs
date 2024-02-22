using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thweb.Model.Model
{
    public class KaKaoPay
    {
        /// <summary>
        /// 주문번호
        /// </summary>
        public string imp_uid { get; set; }
        /// <summary>
        /// 우편번호
        /// </summary>
        public string buyer_postcode { get; set; }
        /// <summary>
        /// 주소+상세주소
        /// </summary>
        public string buyer_addr { get; set; }
        /// <summary>
        /// 구매자 이메일
        /// </summary>
        public string buyer_email { get; set; }
        /// <summary>
        /// 주문자 
        /// </summary>
        public string buyer_name { get; set; }
        /// <summary>
        /// 전화번호
        /// </summary>
        public string buyer_tel { get; set; }
        /// <summary>
        /// 통화 ex)KRW
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 가맹점코드
        /// </summary>
        public string merchant_uid { get; set; }
        /// <summary>
        /// 상품명
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 결제금액
        /// </summary>
        public int paid_amount { get; set; }
        /// <summary>
        /// 결제시간
        /// </summary>
        public int paid_at { get; set; }

        /// <summary>
        /// pg_id
        /// </summary>
        public int pg_tid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pg_provider { get; set; }

        /// <summary>
        /// 상태 
        ///       "ready" = 미결제(결제창을 띄우는 순간 ready 생성됨)
        ///       "paid"  = 결제완료
        ///       "cancelled" = 결제취소
        ///       "failed" = "결제 실패"
        /// </summary>
        public string status { get; set; }
        
        /*
         


card_quota
: 
0
currency
: 
"KRW"
custom_data
: 
null
imp_uid
: 
"imp_307320429563"
merchant_uid
: 
"IMP16156489"
name
: 
"상품명"
paid_amount
: 
100
paid_at
: 
1708585344
pay_method
: 
"point"
pg_provider
: 
"kakaopay"
pg_tid
: 
"T5d6f1681c025b0f4185"
pg_type
: 
"payment"
receipt_url
: 
"https://mockup-pg-web.kakao.com/v1/confirmation/p/T5d6f1681c025b0f4185/0d6ce3794e533775274f39408537faadb3a4dd3c9dbc43d65652bfbfefc1443b"
status
: 
"paid"
success
: 
true
         */
    }
}
