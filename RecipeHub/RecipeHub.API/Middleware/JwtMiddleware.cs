﻿using System;
using System.CodeDom;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
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
            var atr = endpoint.Metadata.OfType<JwtMiddlewareAttribute>().FirstOrDefault();
            if (atr == null)
            {
                await _next(context);
                return;
            }
            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (atr.GetType() == typeof(JwtAdminAuthorizationAttribute))
                    if (await HandleAdminAuthorization(context, handler, token)) return;
                if (atr.GetType() == typeof(JwtUserAuthorizationAttribute))
                    HandleUserAuthorization(context, handler, token);
               // if (atr.GetType() == typeof(JwtAdminOrSameUserIdAuthorization))
               //     if (await HandleAdminOrSameUserIdAuthorization(context, handler, token)) return;
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Invalid token");
                return;
            }
            await _next(context);
        }

        private static async Task<bool> HandleAdminAuthorization(HttpContext context, JwtSecurityTokenHandler handler, string token)
        {
            var jwtSecurityToken = handler.ReadJwtToken(token);
            context.Items.Add("id", jwtSecurityToken.Claims.First(claim => claim.Type == "id").Value);
            if (jwtSecurityToken.Claims.First(claim => claim.Type == "isAdmin").Value.Equals("False"))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Forbidden");
                return true;
            }

            context.Items.Add("isAdmin", jwtSecurityToken.Claims.First(claim => claim.Type == "isAdmin").Value);
            return false;
        }

        private static void HandleUserAuthorization(HttpContext context, JwtSecurityTokenHandler handler, string token)
        {
            var jwtSecurityToken = handler.ReadJwtToken(token);
            context.Items.Add("id", jwtSecurityToken.Claims.First(claim => claim.Type == "id").Value);
        }

        private static async Task<bool> HandleAdminOrSameUserIdAuthorization(HttpContext context, JwtSecurityTokenHandler handler, string token)
        {
            string body;
            int userId;
            using (StreamReader stream = new StreamReader(context.Request.Body))
            {
                body = await stream.ReadToEndAsync();
            }

            using (var reader = new JsonTextReader(new StringReader(body)))
            {
                GetUserIdFromBody(reader);
                if (reader.Value == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync("UserId not found in body");
                    return true;
                }

                userId = Convert.ToInt32(reader.Value);
            }

            var jwtSecurityToken = handler.ReadJwtToken(token);
            var id = int.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "id").Value);
            context.Items.Add("id", id);
            var isAdmin = jwtSecurityToken.Claims.First(claim => claim.Type == "isAdmin").Value;
            if (isAdmin.Equals("False") && userId != id)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Forbidden");
                return true;
            }

            return false;
        }

        private static void GetUserIdFromBody(JsonTextReader reader)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "userId")
                {
                    reader.Read();
                    break;
                }
            }
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
    


