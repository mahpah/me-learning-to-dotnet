using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace superweb.Filters
{
	public class ExecuteTimeFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var start = DateTime.UtcNow;
			await next();
			var time = DateTime.UtcNow.Subtract(start).TotalMilliseconds;
			context.HttpContext.Response.Headers.Add("X-execute-time", $"{time}");
		}
	}
}
