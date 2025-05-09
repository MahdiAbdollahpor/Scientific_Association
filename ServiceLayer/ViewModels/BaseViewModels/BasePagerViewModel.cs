using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.BaseViewModels
{
    public class BasePagerViewModel
    {
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int PageCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
