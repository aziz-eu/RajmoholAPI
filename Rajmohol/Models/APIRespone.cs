using System.Net;

namespace Rajmohol.Models
{
    public class APIRespone
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = false;
        public List<String> ErrorMessage { get; set; }
        public object Result { get; set; }
    }

}
