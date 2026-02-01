using CSupporter.Application.Interfaces;
using CSupporter.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CSupporter.Infrastructure.Data.Interceptors;

public sealed class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICurrentUserService _currentUserService;

    public AuditSaveChangesInterceptor(IDateTimeProvider dateTimeProvider, ICurrentUserService currentUserService)
    {
        _dateTimeProvider = dateTimeProvider;
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAudit(DbContext? context)
    {
        if (context is null) return;

        var now = _dateTimeProvider.Now;
        var user = _currentUserService.UserEmail ?? "tempUser";

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State != EntityState.Added && entry.State != EntityState.Modified)
                continue;

            switch (entry.Entity)
            {
                case IAuditableEntity auditable:
                    if (entry.State == EntityState.Added)
                    {
                        auditable.InsertDate = now;
                        auditable.InsertUser = user;
                    }

                    auditable.UpdateDate = now;
                    auditable.UpdateUser = user;
                    break;

                case IEntity entity:
                    if (entry.State == EntityState.Added)
                    {
                        entity.InsertDate = now;
                        entity.InsertUser = user;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}