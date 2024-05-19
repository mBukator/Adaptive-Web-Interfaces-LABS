using System.ComponentModel.DataAnnotations;

namespace LR7.Models.Auth {
    public class AuthPayload {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
