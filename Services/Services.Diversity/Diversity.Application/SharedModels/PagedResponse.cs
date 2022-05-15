using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.SharedModels
{
    public class PagedResponse<T> : ResponseBase<T>
    {
        public virtual int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public PagedResponse(T data, string message, int pageNumber, int pageSize, int totalRecords, HttpStatusCode statusCode) : base(data, message, statusCode)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }
    }
}
