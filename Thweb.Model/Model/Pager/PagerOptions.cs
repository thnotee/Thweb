using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thweb.Model.Model.Pager
{
    public class PagerOptions
    {
        /// <summary>
        /// PagerOptions 객체를 초기화 합니다.
        /// </summary>
        /// <param name="path">컨트롤러 경로</param>
        /// <param name="addQueryString">PageNo를 제외한 추가적인 쿼리 문자열</param>
        /// <param name="pageVarName">페이지 변수 명 </param>
        public PagerOptions(int totalCount, int pageNumber, int pageSize, int pagenationSize = 10)
        {
            PageNumber = pageNumber;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PagenationSize = pagenationSize;
            StartNavi = (pageNumber - 1) / pagenationSize * pageSize + 1;
            EndNavi = StartNavi + pagenationSize - 1;
            if (EndNavi > TotalPages) { EndNavi = TotalPages; }
        }
        /// <summary>
        /// 페이지번호
        /// </summary>
        public int PageNumber { get; }
        /// <summary>
        /// 천체 수
        /// </summary>
        public int TotalCount { get; }
        /// <summary>
        /// 전체 페이지 수
        /// </summary>

        public int TotalPages { get; }
        /// <summary>
        /// 첫 번째 페이지 여부
        /// </summary>

        public bool IsFirstPage => PageNumber == 1;
        /// <summary>
        /// 마지막 페이지 여부
        /// </summary>
        public bool IsLastPage => PageNumber == TotalPages;

        /// <summary>
        /// 페이지 네비 사이즈
        /// </summary>
        public int PagenationSize;
        /// <summary>
        /// 시작 네비 번호
        /// </summary>
        public int StartNavi;
        /// <summary>
        /// 종료 네비 번호
        /// </summary>
        public int EndNavi;

        /// <summary>
        /// 컨트롤러 경로
        /// </summary>
        public string Path { get; set; } 
        /// <summary>
        ///  PageNo를 제외한 추가적인 쿼리 문자열
        /// </summary>
        public string AddQueryString { get; set; }
        /// <summary>
        /// 페이지 변수 명
        /// </summary>
        public string PageVariableName { get; set; } = "page";


    }
}
