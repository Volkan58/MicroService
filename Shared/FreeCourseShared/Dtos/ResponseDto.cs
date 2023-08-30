using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeCourseShared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccess { get; private set; }

        public List<string> Errors { get; set; }

        public static ResponseDto<T> Success(T data, int StatusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = StatusCode, IsSuccess = true };
        }

        public static ResponseDto<T> Success(int StatusCode)
        {

            return new ResponseDto<T> { Data = default(T), StatusCode = StatusCode, IsSuccess = true };
        }

        public static ResponseDto<T> Fail(List<string> Errors, int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = Errors,
                StatusCode = statusCode,
                IsSuccess = false
            };
        }

        public static ResponseDto<T> Fail(string error, int statusCode)
        {

            return new ResponseDto<T>
            {
                Errors = new List<string>() { error },
                StatusCode = statusCode,
                IsSuccess = false
            };
        }
    }
}
