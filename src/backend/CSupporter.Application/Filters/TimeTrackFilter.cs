﻿using CSupporter.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CSupporter.Application.Filters;

public class TimeTrackFilter : IActionFilter
{
    private readonly ILogger<TimeTrackFilter> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;
    private Stopwatch _stopwatch;

    public TimeTrackFilter(ILogger<TimeTrackFilter> logger, IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        
        var miliseconds = _stopwatch.ElapsedMilliseconds;
        var action = context.ActionDescriptor.DisplayName;

        _logger.LogInformation($"Action [{action}] stop at {_dateTimeProvider.CurrentDateTime} and executed in {miliseconds}ms.");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
        var action = context.ActionDescriptor.DisplayName;

        _logger.LogInformation($"Action [{action}] start at {_dateTimeProvider.CurrentDateTime}.");
    }
}
