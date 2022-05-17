using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RecipeHub.API.Attributes;

namespace RecipeHub.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Token"].FirstOrDefault()?.Split(" ").Last();
            var endpoint = context.GetEndpoint();
            var atr = endpoint.Metadata.OfType<SkipJwtMiddlewareAttribute>().FirstOrDefault();
            if (atr == null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                context.Items.Add("id", jwtSecurityToken.Claims.First(claim => claim.Type == "id").Value);
            }
            await _next(context);
        }
    }
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
