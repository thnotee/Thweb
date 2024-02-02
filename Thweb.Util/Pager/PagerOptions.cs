using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thweb.Util.Pager
{
    public class PagerOptions
    {
        /// <summary>
        /// PagerOptions 객체를 초기화 합니다.
        /// </summary>
        /// <param name="path">컨트롤러 경로</param>
        public PagerOptions(string path)
        {
            this.path = path;
            this.addQueryString = string.Empty;
            this.displayPrevNextButton = true;
            this.displayPrevNextPaginationSize = true;
        }

        /// <summary>
        /// PagerOptions 객체를 초기화 합니다.
        /// </summary>
        /// <param name="path">컨트롤러 경로</param>
        /// <param name="addQueryString">PageNo를 제외한 추가적인 쿼리 문자열</param>
        public PagerOptions(string path, string addQueryString)
        {
            this.path = path;
            this.addQueryString = addQueryString;
            this.displayPrevNextButton = true;
            this.displayPrevNextPaginationSize = true;
        }

        /// <summary>
        /// PagerOptions 객체를 초기화 합니다.
        /// </summary>
        /// <param name="path">컨트롤러 경로</param>
        /// <param name="addQueryString">PageNo를 제외한 추가적인 쿼리 문자열</param>
        /// <param name="displayPrevNextButton">이전 다음 페이지 표시여부</param>
        /// <param name="displayPrevNextPaginationSize">이전 x 페이지, 다음 x 페이지 표시여부</param>
        public PagerOptions(string path, string addQueryString, bool displayPrevNextButton, bool displayPrevNextPaginationSize)
        {
            this.path = path;
            this.addQueryString = addQueryString;
            this.displayPrevNextButton = displayPrevNextButton;
            this.displayPrevNextPaginationSize = displayPrevNextPaginationSize;
        }

        /// <summary>
        /// 컨트롤러 경로
        /// </summary>
        public string path { get; set; }
        /// <summary>
        ///  PageNo를 제외한 추가적인 쿼리 문자열
        /// </summary>
        public string addQueryString { get; set; }
        /// <summary>
        /// 이전 다음 버튼 표시여부
        /// </summary>
        public bool displayPrevNextButton { get; set; }
        /// <summary>
        /// 다음 x 페이지, 이전 x 페이지 버튼 표시여부
        /// </summary>
        public bool displayPrevNextPaginationSize { get; set; }

    }
}
