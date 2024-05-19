using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace LR7.Services.HealthCheck {
    public class UserHealthCheck : IHealthCheck {
        private readonly string _param;

        public UserHealthCheck(string param) {
            _param = param;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) {
            return Task.FromResult(HealthCheckResult.Healthy(description: _param));
        }

        private static Task WriteResponse(HttpContext context, HealthReport report) {
            context.Response.ContentType = "application/json; charset=utf-8";
            var json = JsonConvert.SerializeObject(new {
                status = report.Status.ToString(),
                results = report.Entries.Select(entry => new {
                    key = entry.Key,
                    status = entry.Value.Status.ToString(),
                    description = entry.Value.Description,
                    data = entry.Value.Data
                })
            }, Formatting.Indented);
            return context.Response.WriteAsync(json);
        }
    }
}
