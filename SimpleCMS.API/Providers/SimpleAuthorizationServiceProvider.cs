using Cerberus.Core.Business;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace SimpleCMS.API
{
	public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override async Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			context.Properties.Dictionary.ToList().ForEach(property => context.AdditionalResponseParameters.Add(property.Key, property.Value));
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			if (string.IsNullOrWhiteSpace(context.UserName) || string.IsNullOrWhiteSpace(context.Password))
			{
				context.SetError("badarguments", "The user name or password is incorrect.");
				return;
			}

			var user = CoreManager.UserService.GetUser(context.UserName, context.Password);

			if (user == null)
			{
				context.SetError("invalidarguments", "The user name or password is incorrect.");
				return;
			}

			var properties = new AuthenticationProperties(new Dictionary<string, string>
				{
					{
						"UserName", user.UserName
					},
					{
						"Id", user.Id.ToString()
					},
					{
						"FirstName", user.FirstName
					},
					{
						"LastName", user.LastName
					},
					{
						"Roles", string.Join(",", user.Roles.GetNames())
					}
				});

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);

			identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));

			user.Roles.GetNames().ToList().ForEach(role => identity.AddClaim(new Claim(ClaimTypes.Role, role)));

			var ticket = new AuthenticationTicket(identity, properties);
			context.Validated(ticket);
			//context.Validated(identity);
		}
	}
}