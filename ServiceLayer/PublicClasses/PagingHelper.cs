using ServiceLayer.ViewModels.BaseViewModels;
using System.Linq;

namespace ServiceLayer.PublicClasses
{
    public static class PagingHelper
    {
        public static BasePagerViewModel Pager(int pageIndex, int entitiesCount, int take, int howManyPageShow)
        {
            //pageCount = کل صفحات , entitiesCount کل موجودیت ها , take = تعداد شی های که در یک صفحه نمایش داده می شود, Ceiling = گرد کردن به سمت بالا
            int pageCount = (int)Math.Ceiling(entitiesCount / (double)take);
            int startPage = (pageIndex - howManyPageShow) <= 0 ? 1 : (pageIndex - howManyPageShow);
            int endPage = (pageIndex + howManyPageShow) <= pageCount ? pageCount : (pageIndex + howManyPageShow);
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
