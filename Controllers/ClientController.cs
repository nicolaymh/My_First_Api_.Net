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
            _cache.CreateEntry(ClientCacheKey).Value ??= new List<Client>();
        }

        /// <summary>
        /// Retrieves the list of all clients.
        /// </summary>
        /// <returns>The list of clients if available; otherwise, a 404 Not Found response.</returns>
        [HttpGet]
        [Route("list")]
        public ActionResult<List<Client>> ListAllClients()
        {
            try
            {
                var clients = _cache.Get<List<Client>>(ClientCacheKey);

                if (clients == null)
                {
                    return NotFound("The client list is empty or does not exist.");
                }

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
