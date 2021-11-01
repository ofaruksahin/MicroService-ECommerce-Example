using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ECommerce.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        public List<string> Errors { get; set; }

        public static ResponseDto<T> Success(T data, int statusCode) => new ResponseDto<T>()
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccessful = statusCode >= 200 && statusCode < 400
        };

        public static ResponseDto<T> Success(int statusCode) => new ResponseDto<T>()
        {
            Data = default(T),
            StatusCode = statusCode,
            IsSuccessful = statusCode >= 200 && statusCode < 400
        };

        public static ResponseDto<T> Fail(List<string> errors, int statusCode) => new ResponseDto<T>()
        {
            Data = default(T),
            Errors = errors,
            StatusCode = statusCode,
            IsSuccessful = statusCode >= 200 && statusCode < 400
        };

        public static ResponseDto<T> Fail(string error, int statusCode) => new ResponseDto<T>()
        {
            Data = default(T),
            Errors = new List<string>() { error },
            StatusCode = statusCode,
            IsSuccessful = statusCode >= 200 && statusCode < 400
        };
    }
}
