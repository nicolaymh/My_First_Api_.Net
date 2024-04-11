using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace My_First_Api_.Net.Models
{
    public class Client
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("firstname")]
        [Required(ErrorMessage = "The firstname field is required.")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        [Required(ErrorMessage = "The lastname field is required.")]
        public string LastName { get; set; }

        [JsonPropertyName("age")]
        [Required(ErrorMessage = "The age field is required.")]
        public int Age { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "The email field is required.")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        [Required(ErrorMessage = "The phone field is required.")]
        public string Phone { get; set; }
    }
}
