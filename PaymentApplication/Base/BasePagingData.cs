using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Base
{
    public class BasePagingData<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public int TotalItems { get; set; }
    }
}
