using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace LR7.SwaggerConfig
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {

        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var text = new StringBuilder("An example application with OpenAPI, Swashbuckle, and API versioning.");
            var info = new OpenApiInfo()
            {
                Title = "Parking API",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact() { Name = "Max Bukator", Email = "m.bukator03@gmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
            {
                text.Append("This API version has been deprecated.");
            }

            info.Description = text.ToString();

            return info;
        }
    }
}
