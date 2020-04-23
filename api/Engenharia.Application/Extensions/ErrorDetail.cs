using Engenharia.Domain.Interfaces.Error;
using Newtonsoft.Json;

namespace Engenharia.Application.ExceptionMiddleware
{
    public class ErrorDetail : IErrorDetail
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
