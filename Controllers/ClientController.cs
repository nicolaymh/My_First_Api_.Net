using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using My_First_Api_.Net.Models;

namespace My_First_Api_.Net.Controllers
{
    [ApiController]
    [Route("/client")]
    public class ClientController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string ClientCacheKey = "clientList";

        public ClientController(IMemoryCache cache)
        {
            _cache = cache;
            Console.WriteLine(_cache);

            _cache.CreateEntry(ClientCacheKey).Value ??= new List<Client>();
            Console.WriteLine(_cache);
        }


    }
}
