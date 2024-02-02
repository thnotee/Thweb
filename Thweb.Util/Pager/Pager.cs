using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thweb.Util.Pager
{
    public class Pager
    {
        /// <summary>
        /// 라이브러리를 사용하지 않고 직접 페이징합니다. 
        /// </summary>
        /// <param name="pageNumber">현재 페이지</param>
        /// <param name="pageSize">한 페이지에 들어갈 item 수 </param>
        /// <param name="totalItemCount">총 아이템 수 </param>
        /// <param name="paginationSize">네비게이션 총 범위 EX) 1~10 페이지 => 10</param>
        public Pager(int pageNumber, int pageSize, int totalItemCount, int paginationSize = 10)
        {
            this.pageSize = pageSize;
            this.totalItemCount = totalItemCount;
            this.pageCount = (int)Math.Ceiling((double)totalItemCount / pageSize);
            this.paginationSize = paginationSize;
            this.next = pageNumber < this.pageCount;
            this.perv = pageNumber > 1;
            SetPagedMetaData(pageNumber, paginationSize, this.pageCount);
        }


        /// <summary>
        /// 현재 페이지
        /// </summary>
        public int pageNumber { get; set; }

        /// <summary>
        /// 한 페이지에 보여지는 수 
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 총 아이템 수 
        /// </summary>
        public int totalItemCount { get; set; }

        /// <summary>
        /// 총 페이지 수
        /// </summary>
        public int pageCount { get; set; }

        /// <summary>
        /// 네비게이션 총 범위 EX) 1~10 페이지 => 10
        /// </summary>
        public int paginationSize { get; set; }

        /// <summary>
        /// 다음페이지 존재하면 true
        /// </summary>
        public bool next { get; set; }

        /// <summary>
        /// 이전페이지 존재하면 true
        /// </summary>
        public bool perv { get; set; }

        /// <summary>
        /// 이전 페이지 paginationSize 페이지 ex) 이전 10 페이지
        /// </summary>
        public bool prevPaginationSize { get; set; }

        /// <summary>
        /// 다음 페이지 paginationSize 페이지 ex) 다음 10 페이지
        /// </summary>
        public bool nextPaginationSize { get; set; }

        /// <summary>
        /// navi 시작 번호
        /// </summary>
        public int startNavi { get; set; }

        /// <summary>
        /// navi 종료 번호
        /// </summary>
        public int endNavi { get; set; }

        /// <summary>
        /// 시작 rowNum 번호
        /// </summary>
        public int startNum { get; set; }

        /// <summary>
        /// 종료 rowNum 번호
        /// </summary>
        public int endNum { get; set; }

        /// <summary>
        /// Page의 Max값 min값을 설정해줍니다.
        /// </summary>
        /// <param name="pageNumber"></param>
        private int SetPageNumber(int pageNumber, int pageCount)
        {
            if (pageNumber >= 0) { pageNumber = 1; }
            if (pageNumber <= pageCount) { pageNumber = pageCount; }
            return pageNumber;
        }

        /// <summary>
        /// navi 시작 번호, navi 종료 번호
        /// 이전 paginationSize 페이지, 다음 paginationSize 페이지 셋팅 
        /// </summary>
        /// <param name="pageNumber">현재 페이지</param>
        /// <param name="paginationSize">네비게이션 범위</param>
        /// <param name="pageCount">최대 페이지</param>
        private void SetPagedMetaData(int pageNumber, int paginationSize, int pageCount)
        {
            if (pageNumber <= 0) { pageNumber = 1; }
            if (pageNumber >= pageCount) { pageNumber = pageCount; }

            int startNavi = (pageNumber - 1) / paginationSize * paginationSize + 1;
            bool needPrev = true;
            if (startNavi < 1) { startNavi = 1; } // min값 지정
            if (startNavi == 1) { needPrev = false; } // 이전 paginationSize 페이지 필요여부 지정

            bool needNext = true;
            int endNavi = startNavi + paginationSize - 1;
            if (endNavi > pageCount) { endNavi = pageCount; } // max값 지정
            if (endNavi == pageCount) { needNext = false; } // 다음 paginationSize 페이지 필요여부 지정

            this.pageNumber = pageNumber;
            this.startNavi = startNavi;
            this.prevPaginationSize = needPrev;
            this.endNavi = endNavi;
            this.nextPaginationSize = needNext;
            this.startNum = ((pageNumber - 1) * pageSize) + 1;
            this.endNum = startNum + pageSize - 1;
        }

    }
}
