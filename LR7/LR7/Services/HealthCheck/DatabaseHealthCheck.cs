using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace LR7.Services.HealthCheck {
    public class DatabaseHealthCheck : IHealthCheck {
        private readonly string _connectionString;

        public DatabaseHealthCheck(string connectionString) {
            _connectionString = connectionString;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) {
            using (var connection = new SqlConnection(_connectionString)) {
                try {
                    await connection.OpenAsync(cancellationToken);
                    return HealthCheckResult.Healthy("Database connection is healthy.");
                } catch (Exception ex) {
                    return HealthCheckResult.Unhealthy("Database connection is unhealthy.", ex);
                }
            }
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
