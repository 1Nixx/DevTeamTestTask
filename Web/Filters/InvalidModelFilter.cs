using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters
{
	public class InvalidModelFilter : ActionFilterAttribute
	{
		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (!context.ModelState.IsValid)
			{
				context.Result = new ViewResult
				{
					ViewName = context.ActionDescriptor.RouteValues["action"]
				};
			}
			else
				await next();
		}
	}
}
