using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace CSupporter.API.Application.Filters;

public class TimeTrackFilter : Attribute, IActionFilter
{
    private Stopwatch _stopwatch;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        
        var miliseconds = _stopwatch.ElapsedMilliseconds;
        var action = context.ActionDescriptor.DisplayName;

        Debug.WriteLine($"Action [{action}] executed in {miliseconds}ms.");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
    }
}
