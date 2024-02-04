using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thweb.Model.Model.Pager
{
    public class PagedList<T> : IReadOnlyList<T>
    {
        private readonly IList<T> subset;
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize, int pagenationSize = 10)
        {
            subset = items as IList<T> ?? new List<T>(items);
            this.pagerOptions = new PagerOptions(count, pageNumber, pageSize, pagenationSize);
        }

        /// <summary>
        /// 페이지 옵션
        /// </summary>
        public PagerOptions pagerOptions { get; }

        /// <summary>
        // 열거자 구현
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int Count => subset.Count;


        public T this[int index] => subset[index];

        public IEnumerator<T> GetEnumerator() => subset.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => subset.GetEnumerator();
    


    }
}
