using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace nettbutikk_api.Extensions
{
    public static class WebAppExtensions
    {
        public static void AddSwaggerWithJWTAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }
    }
}
