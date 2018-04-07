using Gildemeister.Cliente360.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Gildemeister.Cliente360.Application
{
    public class ServiceResult
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public dynamic Error { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public dynamic Paging { get; set; }
        public ServiceResult()
        {
            Code = 200;
            Success = true;
        }
        public void Paginate(PageLinkBuilder linkBuilder)
        {
            Paging = new
            {
                First = linkBuilder.FirstPage.ToString(),
                Previous = linkBuilder.PreviousPage == null ? null : linkBuilder.PreviousPage.ToString(),
                Next = linkBuilder.NextPage == null ? null : linkBuilder.NextPage.ToString(),
                Last = linkBuilder.LastPage.ToString(),
            };

        }

        public void Errors(Exception exception)
        {
            Error = new
            {
                Code = HttpStatusCode.InternalServerError,
                Message = exception.ToString()
            };
        }        

    }
}

