using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using My_First_Api_.Net.Models;
using System.Diagnostics.Contracts;

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
        /// Retrieves a list of all clients.
        /// </summary>
        /// <returns>A list of all clients.</returns>
        /// <response code="200">Returns the list of clients.</response>
        /// <response code="404">If the client list is empty or does not exist.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpGet]
        [Route("list")]
        public ActionResult<List<Client>> ListAllClients()
        {
            try
            {
                var clients = _cache.Get<List<Client>>(ClientCacheKey);

                if (clients == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "The client list is empty or does not exist."
                    });
                }

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="client">The client information to create.</param>
        /// <returns>The newly created client.</returns>
        /// <response code="200">Client created successfully.</response>
        /// <response code="500">If an unexpected error occurs during creation.</response>
        [HttpPost]
        [Route("create")]
        public IActionResult CreateClient([FromBody] Client client)
        {
            try
            {
                var clients = _cache.Get<List<Client>>(ClientCacheKey) ?? new List<Client>();

                string uniqueId = Guid.NewGuid().ToString();

                client.Id = uniqueId;
                clients.Add(client);

                _cache.Set(ClientCacheKey, clients);

                return Ok(new
                {
                    success = true,
                    message = "Client created successfully.",
                    result = client
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieves a client by ID.
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <response code="200">Client found.</response>
        /// <response code="400">ID not provided.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [Route("getClient/{id}")]
        public IActionResult GetClientById(string id)
        {
            try
            {
                if (id is null)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            message = "Client id is required."

                        });
                }

                var clients = _cache.Get<List<Client>>(ClientCacheKey) ?? new List<Client>();
                var client = clients.FirstOrDefault(c => c.Id == id);

                if (client is null)
                {
                    return NotFound(
                        new
                        {
                            success = false,
                            message = "Client not found."

                        });
                }

                return Ok(new
                {
                    success = true,
                    result = client
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Updates a client's information.
        /// </summary>
        /// <param name="id">Client ID.</param>
        /// <param name="updateClient">Updated client data.</param>
        /// <response code="200">Client updated successfully.</response>
        /// <response code="400">Invalid client data or ID.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">Unexpected error during update.</response>
        [HttpPut]
        [Route("updateClient/{id}")]
        public IActionResult UpdateClient(string id, [FromBody] Client updateClient)
        {
            try
            {
                if (updateClient == null || updateClient.Id != id)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            message = "Invalid client data."
                        });
                }

                var clients = _cache.Get<List<Client>>(ClientCacheKey) ?? new List<Client>();
                var clientIndex = clients.FindIndex(c => c.Id == id);

                if (clientIndex == -1)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Client not found."
                    });
                }

                clients[clientIndex].FirstName = updateClient.FirstName;
                clients[clientIndex].LastName = updateClient.LastName;
                clients[clientIndex].Age = updateClient.Age;
                clients[clientIndex].Email = updateClient.Email;
                clients[clientIndex].Phone = updateClient.Phone;

                _cache.Set(ClientCacheKey, clients);

                return Ok(new
                {
                    succes = true,
                    message = "Client successfully updated",
                    result = updateClient
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Deletes a client by ID.
        /// </summary>
        /// <param name="id">The ID of the client to delete.</param>
        /// <response code="200">Client successfully deleted.</response>
        /// <response code="400">If the ID parameter is null.</response>
        /// <response code="404">If no client is found with the specified ID.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpDelete]
        [Route("deleteClient/{id}")]
        public IActionResult DeleteClient(string id)
        {
            try
            {
                if (id is null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Id is required."
                    });
                }

                var clients = _cache.Get<List<Client>>(ClientCacheKey) ?? new List<Client>();
                var client = clients.FirstOrDefault(c => c.Id == id);

                if (client is null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Client not found."
                    });
                }

                clients.Remove(client);
                _cache.Set(ClientCacheKey, clients);

                return Ok(
                    new
                    {
                        success = true,
                        message = "Client successfully deleted"
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
