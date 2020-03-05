using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace nwc.Tarwya.Infra.Core
{
	public class nwcHttpClient
	{

		static readonly HttpClient _client;
		static nwcHttpClient()
		{
			_client = new HttpClient();
			//_client.DefaultRequestHeaders.Accept
			//.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
			_client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
		}
		public async Task<Response> PostAsync<Request, Response>(
			string url,
			Request input,
			string authorizationType = AuthorizationType.Basic,
			string token = null)
		{
			return await CreateRequest<Response>(url, HttpMethod.Post, input, authorizationType, token);
		}
		public async Task<Response> PutAsync<Request, Response>(
			string url,
			Request input,
			string authorizationType = AuthorizationType.Basic,
			string token = null)
		{
			return await CreateRequest<Response>(url, HttpMethod.Put, input, authorizationType, token);
		}
		public async Task<Response> GetAsync<Response>(
			string url,
			string authorizationType = AuthorizationType.Basic,
			string token = null)
		{
			return await CreateRequest<Response>(url, HttpMethod.Get, authorizationType, token);
		}
		public async Task<Response> DeleteAsync<Response>(
			string url,
			string authorizationType = AuthorizationType.Basic,
			string token = null)
		{
			return await CreateRequest<Response>(url, HttpMethod.Delete, authorizationType, token);
		}
		#region [ -- Private helper methods -- ]
		async Task<Response> CreateRequest<Response>(
			string url,
			HttpMethod method,
			string authorizationType,
			string token)
		{
			return await CreateRequestMessage(url, method, authorizationType, token, async (msg) =>
			 {
				 return await GetResult<Response>(msg);
			 });
		}
		async Task<Response> CreateRequest<Response>(
			string url,
			HttpMethod method,
			object input,
			string authorizationType,
			string token)
		{
			return await CreateRequestMessage(url, method, authorizationType, token, async (msg) =>
			 {
				 using (var content = new StringContent(JObject.FromObject(input).ToString(), Encoding.UTF8, "application/json"))
				 {
					 content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
					 msg.Content = content;
					 return await GetResult<Response>(msg);
				 }
			 });
		}
		async Task<Response> CreateRequestMessage<Response>(
			string url,
			HttpMethod method,
			string authorizationType,
			string token,
			Func<HttpRequestMessage, Task<Response>> functor)
		{
			using (var msg = new HttpRequestMessage())
			{
				msg.RequestUri = new Uri(url);
				msg.Method = method;
				if (token != null)
					msg.Headers.Authorization = new AuthenticationHeaderValue(authorizationType, token);
				msg.Headers.TryAddWithoutValidation("Content-Type", "application/json");

				return await functor(msg);
			}
		}
		async Task<Response> GetResult<Response>(HttpRequestMessage msg)
		{
			using (var response = await _client.SendAsync(msg))
			{
				using (var content = response.Content)
				{
					var responseContent = await content.ReadAsStringAsync();
					if (!response.IsSuccessStatusCode)
						throw new Exception(responseContent);
					if (typeof(IConvertible).IsAssignableFrom(typeof(Response)))
						return (Response)Convert.ChangeType(responseContent, typeof(Response));
					return JToken.Parse(responseContent).ToObject<Response>();
				}
			}
		}
		#endregion
	}

	public class AuthorizationType
	{
		public const string Basic = "Basic";
		public const string Bearer = "Bearer";
	}
}
