using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.Keycloak;

[assembly: OwinStartupAttribute(typeof(entryPoint.Startup))]
namespace entryPoint
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            const string persistentAuthType = "keycloak_cookies"; // Or name it whatever you want

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = persistentAuthType
            });

            // You may also use this method if you have multiple authentication methods below,
            // or if you just like it better:
            app.SetDefaultSignInAsAuthenticationType(persistentAuthType);

            app.UseKeycloakAuthentication(new KeycloakAuthenticationOptions
            {
                Realm = System.Environment.GetEnvironmentVariable("REALM", System.EnvironmentVariableTarget.Machine),
                ClientId = System.Environment.GetEnvironmentVariable("CLIENTID", System.EnvironmentVariableTarget.Machine),
                ClientSecret = System.Environment.GetEnvironmentVariable("SECRET", System.EnvironmentVariableTarget.Machine),
                KeycloakUrl = System.Environment.GetEnvironmentVariable("KEYCLOAK", System.EnvironmentVariableTarget.Machine),
                DisableAudienceValidation = true,
                SignInAsAuthenticationType = persistentAuthType // Not required with SetDefaultSignInAsAuthenticationType
            });
        }
    }
}