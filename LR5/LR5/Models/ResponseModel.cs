using System.Net;

namespace LR5.Models {
    public class ResponseModel<T> {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}
