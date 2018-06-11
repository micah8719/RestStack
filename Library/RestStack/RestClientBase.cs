using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestStack
{
	public abstract class RestClientBase : IDisposable
	{
		private readonly HttpClient _client;

		protected RestClientBase(Uri endpointUri, HttpMessageHandler messageHandler)
		{
			if (messageHandler != null)
			{
				_client = new HttpClient(messageHandler, true)
				{
					BaseAddress = endpointUri
				};
			}
			else
			{
				_client = new HttpClient
				{
					BaseAddress = endpointUri
				};
			}
		}

		protected RestClientBase(Uri endpointUri)
			: this(endpointUri, null)
		{
		}

		protected RestClientBase(string endpointUri, HttpMessageHandler messageHandler)
			: this(new Uri(endpointUri, UriKind.Absolute), messageHandler)
		{
		}

		protected RestClientBase(string endpointUri)
			: this(endpointUri, null)
		{
		}

		protected async Task<RestResponse<T>> GetAsync<T>(Uri requestUri, SerializerBase<T> responseSerializer)
		{
			try
			{
				var response = await _client.GetAsync(requestUri).ConfigureAwait(false);

				if (!response.IsSuccessStatusCode)
				{
					return new RestResponse<T>
					(
						default(T),
						false,
						response.StatusCode,
						new Exception(response.ReasonPhrase)
					);
				}

				var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				var responseData = responseSerializer.Deserialize(responseContent);

				return new RestResponse<T>(responseData, response.StatusCode);
			}
			catch (Exception e)
			{
				return new RestResponse<T>
				(
					default(T),
					false,
					HttpStatusCode.InternalServerError,
					e
				);
			}
		}

		protected async Task<RestResponse<T>> GetAsync<T>(string requestUri, SerializerBase<T> responseSerializer)
		{
			return await GetAsync(new Uri(requestUri, UriKind.Relative), responseSerializer).ConfigureAwait(false);
		}

		protected async Task<RestResponse<TResponse>> PutAsync<TRequest, TResponse>(Uri requestUri, TRequest requestData, string mediaType, SerializerBase<TRequest> requestSerializer, SerializerBase<TResponse> responseSerializer)
		{
			try
			{
				var requestContent = requestSerializer.Serialize(requestData);
				var response = await _client
					.PutAsync
					(
						requestUri,
						new StringContent(requestContent, responseSerializer.Encoding, mediaType)
					)
					.ConfigureAwait(false);

				if (!response.IsSuccessStatusCode)
				{
					return new RestResponse<TResponse>
					(
						default(TResponse),
						false,
						response.StatusCode,
						new Exception(response.ReasonPhrase)
					);
				}

				var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				var responseData = responseSerializer.Deserialize(responseContent);

				return new RestResponse<TResponse>(responseData, response.StatusCode);
			}
			catch (Exception e)
			{
				return new RestResponse<TResponse>
				(
					default(TResponse),
					false,
					HttpStatusCode.InternalServerError,
					e
				);
			}
		}

		protected async Task<RestResponse<TResponse>> PutAsync<TRequest, TResponse>(string requestUri, TRequest requestData, string mediaType, SerializerBase<TRequest> requestSerializer, SerializerBase<TResponse> responseSerializer)
		{
			return await PutAsync
				(
					new Uri(requestUri, UriKind.Relative),
					requestData,
					mediaType,
					requestSerializer,
					responseSerializer
				)
				.ConfigureAwait(false);
		}

		protected async Task<RestResponse<T>> PutAsync<T>(Uri requestUri, T requestData, string mediaType, SerializerBase<T> serializer)
		{
			return await PutAsync
				(
					requestUri,
					requestData,
					mediaType,
					serializer,
					serializer
				)
				.ConfigureAwait(false);
		}

		protected async Task<RestResponse<T>> PutAsync<T>(string requestUri, T requestData, string mediaType, SerializerBase<T> serializer)
		{
			return await PutAsync
				(
					requestUri,
					requestData,
					mediaType,
					serializer,
					serializer
				)
				.ConfigureAwait(false);
		}

		protected async Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(Uri requestUri, TRequest requestData, string mediaType, SerializerBase<TRequest> requestSerializer, SerializerBase<TResponse> responseSerializer)
		{
			try
			{
				var requestContent = requestSerializer.Serialize(requestData);
				var response = await _client
					.PostAsync
					(
						requestUri,
						new StringContent(requestContent, responseSerializer.Encoding, mediaType)
					)
					.ConfigureAwait(false);

				if (!response.IsSuccessStatusCode)
				{
					return new RestResponse<TResponse>
					(
						default(TResponse),
						false,
						response.StatusCode,
						new Exception(response.ReasonPhrase)
					);
				}

				var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				var responseData = responseSerializer.Deserialize(responseContent);

				return new RestResponse<TResponse>(responseData, response.StatusCode);
			}
			catch (Exception e)
			{
				return new RestResponse<TResponse>
				(
					default(TResponse),
					false,
					HttpStatusCode.InternalServerError,
					e
				);
			}
		}

		protected async Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(string requestUri, TRequest requestData, string mediaType, SerializerBase<TRequest> requestSerializer, SerializerBase<TResponse> responseSerializer)
		{
			return await PostAsync
				(
					new Uri(requestUri, UriKind.Relative),
					requestData,
					mediaType,
					requestSerializer,
					responseSerializer
				)
				.ConfigureAwait(false);
		}

		protected async Task<RestResponse<T>> PostAsync<T>(Uri requestUri, T requestData, string mediaType, SerializerBase<T> serializer)
		{
			return await PostAsync
				(
					requestUri,
					requestData,
					mediaType,
					serializer,
					serializer
				)
				.ConfigureAwait(false);
		}

		protected async Task<RestResponse<T>> PostAsync<T>(string requestUri, T requestData, string mediaType, SerializerBase<T> serializer)
		{
			return await PostAsync
				(
					requestUri,
					requestData,
					mediaType,
					serializer,
					serializer
				)
				.ConfigureAwait(false);
		}

		protected async Task<RestResponse> DeleteAsync(Uri requestUri)
		{
			try
			{
				var response = await _client.DeleteAsync(requestUri).ConfigureAwait(false);

				if (!response.IsSuccessStatusCode)
				{
					return new RestResponse
					(
						false,
						response.StatusCode,
						new Exception(response.ReasonPhrase)
					);
				}

				return new RestResponse(response.StatusCode);
			}
			catch (Exception e)
			{
				return new RestResponse
				(
					false,
					HttpStatusCode.InternalServerError,
					e
				);
			}
		}

		protected async Task<RestResponse> DeleteAsync(string requestUri)
		{
			return await DeleteAsync(new Uri(requestUri, UriKind.Relative)).ConfigureAwait(false);
		}

		protected RestResponse<T> Get<T>(Uri requestUri, SerializerBase<T> responseSerializer)
		{
			return GetAsync(requestUri, responseSerializer)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		protected RestResponse<T> Get<T>(string requestUri, SerializerBase<T> responseSerializer)
		{
			return Get
			(
				new Uri(requestUri, UriKind.Relative),
				responseSerializer
			);
		}

		protected RestResponse<TResponse> Put<TRequest, TResponse>(Uri requestUri, TRequest requestData, string mediaType, SerializerBase<TRequest> requestSerializer, SerializerBase<TResponse> responseSerializer)
		{
			return PutAsync
				(
					requestUri,
					requestData,
					mediaType,
					requestSerializer,
					responseSerializer
				)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		protected RestResponse<TResponse> Put<TRequest, TResponse>(string requestUri, TRequest requestData, string mediaType, SerializerBase<TRequest> requestSerializer, SerializerBase<TResponse> responseSerializer)
		{
			return Put
			(
				new Uri(requestUri, UriKind.Relative),
				requestData,
				mediaType,
				requestSerializer,
				responseSerializer
			);
		}

		protected RestResponse<T> Put<T>(Uri requestUri, T requestData, string mediaType, SerializerBase<T> serializer)
		{
			return Put
			(
				requestUri,
				requestData,
				mediaType,
				serializer,
				serializer
			);
		}

		protected RestResponse<T> Put<T>(string requestUri, T requestData, string mediaType, SerializerBase<T> serializer)
		{
			return Put
			(
				new Uri(requestUri, UriKind.Relative),
				requestData,
				mediaType,
				serializer
			);
		}

		protected RestResponse<TResponse> Post<TRequest, TResponse>(Uri requestUri, TRequest requestData, string mediaType, SerializerBase<TRequest> requestSerializer, SerializerBase<TResponse> responseSerializer)
		{
			return PostAsync
				(
					requestUri,
					requestData,
					mediaType,
					requestSerializer,
					responseSerializer
				)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		protected RestResponse<TResponse> Post<TRequest, TResponse>(string requestUri, TRequest requestData, string mediaType, SerializerBase<TRequest> requestSerializer, SerializerBase<TResponse> responseSerializer)
		{
			return Post
			(
				new Uri(requestUri, UriKind.Relative),
				requestData,
				mediaType,
				requestSerializer,
				responseSerializer
			);
		}

		protected RestResponse<T> Post<T>(Uri requestUri, T requestData, string mediaType, SerializerBase<T> serializer)
		{
			return Post
			(
				requestUri,
				requestData,
				mediaType,
				serializer,
				serializer
			);
		}

		protected RestResponse<T> Post<T>(string requestUri, T requestData, string mediaType, SerializerBase<T> serializer)
		{
			return Post
			(
				new Uri(requestUri, UriKind.Relative),
				requestData,
				mediaType,
				serializer
			);
		}

		protected RestResponse Delete(Uri requestUri)
		{
			return DeleteAsync(requestUri)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		protected RestResponse Delete(string requestUri)
		{
			return Delete(new Uri(requestUri, UriKind.Relative));
		}

		protected HttpRequestHeaders Headers => _client.DefaultRequestHeaders;

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_client?.Dispose();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}