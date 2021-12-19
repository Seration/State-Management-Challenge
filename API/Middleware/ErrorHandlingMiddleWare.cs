using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Middleware
{
    public class ErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleWare(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // ErrorLog
            }
        }        
    }
}
