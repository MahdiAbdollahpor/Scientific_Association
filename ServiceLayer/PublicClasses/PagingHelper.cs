using ServiceLayer.ViewModels.BaseViewModels;
using System.Linq;

namespace ServiceLayer.PublicClasses
{
    public static class PagingHelper
    {
        public static BasePagerViewModel Pager(int pageIndex, int entitiesCount, int take, int howManyPageShow)
        {
            int pageCount = (int)Math.Ceiling(entitiesCount / (double)take);

            // محدود کردن howManyPageShow اگر تعداد صفحات کمتر از مقدار درخواستی باشد
            howManyPageShow = Math.Min(howManyPageShow, pageCount - 1);

            int startPage = (pageIndex - howManyPageShow) <= 0 ? 1 : (pageIndex - howManyPageShow);
            int endPage = (pageIndex + howManyPageShow) > pageCount ? pageCount : (pageIndex + howManyPageShow);

            // اطمینان حاصل کنید که startPage از 1 کمتر نباشد و endPage از pageCount بیشتر نباشد
            startPage = Math.Max(startPage, 1);
            endPage = Math.Min(endPage, pageCount);

            int skip = (pageIndex - 1) * take;

            var BasePage = new BasePagerViewModel
            {
                EndPage = endPage,
                PageCount = pageCount,
                StartPage = startPage,
                Skip = skip,
                Take = take,
            };

            return BasePage;
        }


        public static IEnumerable<T> Pagination<T>(IEnumerable<T> entities, BasePagerViewModel pager)
        {
            return entities.Skip(pager.Skip).Take(pager.Take);
        }
    }
}
