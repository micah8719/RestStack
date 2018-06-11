using System;
using System.Net;

namespace RestStack
{
	public class RestResponse
	{
		public RestResponse(bool success, HttpStatusCode statusCode, Exception error)
		{
			Success = success;
			StatusCode = statusCode;
			Error = error;
		}

		public RestResponse(HttpStatusCode statusCode)
			: this(true, statusCode, null)
		{
		}

		public bool Success { get; }
		public HttpStatusCode StatusCode { get; }
		public Exception Error { get; }
	}

	public class RestResponse<T> : RestResponse
	{
		public static implicit operator T(RestResponse<T> response)
		{
			return response.Data;
		}

		public RestResponse(T data, bool success, HttpStatusCode statusCode, Exception error)
			: base(success, statusCode, error)
		{
			Data = data;
		}

		public RestResponse(T data, HttpStatusCode statusCode)
			: this(data, true, statusCode, null)
		{
		}

		public T Data { get; }
	}
}