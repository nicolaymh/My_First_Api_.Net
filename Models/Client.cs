using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace My_First_Api_.Net.Models
{
    public class Client
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}
