using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(WebAPIOAuth2.Startup))]

namespace WebAPIOAuth2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureAuthentication(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        private static void ConfigureAuthentication(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer
            (
                new OAuthAuthorizationServerOptions()
                {
                    AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/issuer/token"),
                    Provider = new OAuthAuthorizationServerProvider()
                    {
                        OnValidateClientAuthentication = async ctx =>
                        {
                            await Task.Run(() => ctx.Validated());
                        },
                        OnGrantResourceOwnerCredentials = async ctx =>
                        {
                            await Task.Run(() =>
                            {
                                if (ctx.UserName != "Israel" || ctx.Password != "12345")
                                {
                                    ctx.Rejected();
                                    return;
                                }

                                var identity = new ClaimsIdentity(
                                    new[] {
                                        new Claim(ClaimTypes.Name, ctx.UserName),
                                        new Claim(ClaimTypes.Role, "Admin")},
                                    ctx.Options.AuthenticationType);

                                ctx.Validated(identity);
                            });
                        }
                    }
                }
            );

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
