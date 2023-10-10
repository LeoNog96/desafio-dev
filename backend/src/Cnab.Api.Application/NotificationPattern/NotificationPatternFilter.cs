using Cnab.Api.Domain.NotificationPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Cnab.Api.Application.NotificationPattern
{
    public class NotificationPatternFilter : IAsyncResultFilter
    {
        private readonly INotificationContext _notificacaoContext;
        public NotificationPatternFilter(INotificationContext notificationContext)
        {
            _notificacaoContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificacaoContext.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = _notificacaoContext.StatusCode;
                context.HttpContext.Response.ContentType = "application/json";

                var notifications = JsonConvert.SerializeObject(new NotificationResponse("Falha na Operação", _notificacaoContext.Notifications));
                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}
