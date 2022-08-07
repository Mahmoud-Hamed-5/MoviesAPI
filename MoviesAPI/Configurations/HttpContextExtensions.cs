using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Configurations
{
    public static class HttpContextExtensions
    {
        public static async Task InsertPaginationParametersInResponse<T>(this HttpContext httpContext, IQueryable<T> queryable, int recordsPerPage)
        {
            if(httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / recordsPerPage);

            
            //var p = httpContext.Request.Query.ContainsKey("Page");
            Microsoft.Extensions.Primitives.StringValues value = "1";
            if (httpContext.Request.Query.ContainsKey("Page")) {               
                httpContext.Request.Query.TryGetValue("Page", out value);              
            }           

            httpContext.Response.Headers.Add("TotalPages", totalPages.ToString());
            httpContext.Response.Headers.Add("Page", value.ToString());

        }
    }
}
