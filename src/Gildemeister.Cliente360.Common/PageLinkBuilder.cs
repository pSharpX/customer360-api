
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace Gildemeister.Cliente360.Common
{
    public class PageLinkBuilder
    {
        public Uri FirstPage { get; private set; }
        public Uri LastPage { get; private set; }
        public Uri NextPage { get; private set; }
        public Uri PreviousPage { get; private set; }
        public int PageSize { get; set; }
        public long Count { get; set; }

        public PageLinkBuilder(IUrlHelper urlHelper, string routeName, 
            object routeValues, int pageNo, int pageSize, long totalRecordCount)
        {

            var pageCount = totalRecordCount > 0 ? (int)Math.Ceiling(totalRecordCount / (double)pageSize) : 0;
            PageSize = pageSize;
            Count = totalRecordCount;

            FirstPage = new Uri(urlHelper.Link(routeName, new HttpRouteValueDictionary(routeValues){
                {"pageNo", 1},
                {"pageSize", pageSize}
            }));

            LastPage = new Uri(urlHelper.Link(routeName, new HttpRouteValueDictionary(routeValues)
            {
                {"pageNo", pageCount},
                {"pageSize", pageSize}
            }));

            if (pageNo > 1)
            {
                PreviousPage = new Uri(urlHelper.Link(routeName, new HttpRouteValueDictionary(routeValues){
                    {"pageNo", pageNo - 1},
                    {"pageSize", pageSize}
                }));
            }
            if (pageNo < pageCount)
            {
                NextPage = new Uri(urlHelper.Link(routeName, new HttpRouteValueDictionary(routeValues){
                    {"pageNo", pageNo + 1},
                    {"pageSize", pageSize}
                }));
            }
        }
    }
}
