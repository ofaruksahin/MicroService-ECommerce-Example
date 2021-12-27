using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Gateway.DelegateHandlers
{
    public class TokenExchangeDelegateHandler : DelegatingHandler
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private string _accessToken;

        public TokenExchangeDelegateHandler(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private async Task<string> GetToken(string requestToken)
        {
            if (!string.IsNullOrEmpty(_accessToken))
            {
                return _accessToken;
            }


            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _configuration["IdentityServerURL"],
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            TokenExchangeTokenRequest tokenExchangeTokenRequest = new TokenExchangeTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _configuration["ClientId"],
                ClientSecret = _configuration["ClientSecret"],
                GrantType = "urn:ietf:params:oauth:grant-type:token-exchange",
                SubjectToken = requestToken,
                SubjectTokenType = "urn:ietf:params:oauth:token-type:access_token",
                Scope = "openid discount_fullpermission payment_fullpermission"
            };

            var tokenResponse = await _httpClient.RequestTokenExchangeTokenAsync(tokenExchangeTokenRequest);

            if (tokenResponse.IsError)
            {
                throw tokenResponse.Exception;
            }

            _accessToken = tokenResponse.AccessToken;

            return _accessToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await GetToken(request.Headers.Authorization.Parameter);

            request.SetBearerToken(token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
