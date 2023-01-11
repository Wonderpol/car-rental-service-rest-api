using System.Net;

namespace CarRentalRestApi.Models.Responses
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}