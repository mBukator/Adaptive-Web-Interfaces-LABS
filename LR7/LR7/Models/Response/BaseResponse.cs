namespace LR7.Models.Response {
    public class BaseResponse {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public BaseResponse() {
            Success = true;
            Message = string.Empty;
        }

        public BaseResponse(string message) {
            Success = false;
            Message = message;
        }

        public BaseResponse(object data) {
            Success = true;
            Data = data;
        }

        public BaseResponse(string message, object data) {
            Success = false;
            Message = message;
            Data = data;
        }
    }
}
