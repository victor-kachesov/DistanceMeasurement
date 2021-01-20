using DistanceMeasurement.BusinessLogic.PlacesCTeleport.Dto;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DistanceMeasurement.BusinessLogic.PlacesCTeleport
{
    public class PlacesCTeleportClient : IPlacesCTeleportClient
    {
        public PlacesCTeleportClient(string apiUrl, IHttpClientFactory httpClientFactory)
        {
            _apiUrl = apiUrl;
            _httpClientFactory = httpClientFactory;
        }

        private readonly string _apiUrl;
        private readonly IHttpClientFactory _httpClientFactory;

        public async Task<AirportInfoDto> GetAirportInfoAsync(string code)
        {
            var uri = new Uri(_apiUrl + $"/{code}");

            var policy = GetPolicy();

            using (var client = GetHttpClient())
            {
                var response = await policy.ExecuteAsync(async (ct) =>
                {
                    // It is needed to create HttpRequestMessage every time
                    // because it can't be used twice during retry

                    var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

                    return await client.SendAsync(requestMessage, ct);

                }, CancellationToken.None);

                string responseContent = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                var responseResult = JsonConvert.DeserializeObject<AirportInfoDto>(responseContent);

                return responseResult;
            }
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_apiUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        private IAsyncPolicy<HttpResponseMessage> GetPolicy()
        {
            IAsyncPolicy<HttpResponseMessage> timeoutPolicy = Policy
                .TimeoutAsync(300, (contex, timespan, task) =>
                {
                    return Task.CompletedTask;
                })
                .AsAsyncPolicy<HttpResponseMessage>();

            IAsyncPolicy<HttpResponseMessage> retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(message => {
                    return !message.IsSuccessStatusCode;
                })
                .Or<HttpRequestException>()
                .Or<OperationCanceledException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(5)
                });

            var policy = timeoutPolicy.WrapAsync(retryPolicy);

            return policy;
        }
    }
}
