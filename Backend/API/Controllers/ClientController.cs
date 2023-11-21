using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClientController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> Get()
        {
            var client = await _unitOfWork.Clients.GetAllAsync();
            return _mapper.Map<List<ClientDto>>(client);
        }

        /* Devuelve un listado con el nombre de los todos los clientes españoles. */
        [HttpGet("GetSpanishClients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetSpanishClients()
        {
            var office = await _unitOfWork.Clients.GetSpanishClients();
            return Ok(office);
        }

        /*  Devuelve un listado con el código de cliente de aquellos clientes que
realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar
aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta */
        [HttpGet("GetUniqueClientCodesWithPaymentsIn2008")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetUniqueClientCodesWithPaymentsIn2008()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetUniqueClientCodesWithPaymentsIn2008();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

/*         Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y
cuyo representante de ventas tenga el código de empleado 11 o 30. */
        [HttpGet("GetClientsFromMadridWithSpecificEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientsFromMadridWithSpecificEmployees()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientsFromMadridWithSpecificEmployees();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /* Obtén un listado con el nombre de cada cliente y el nombre y apellido de su
representante de ventas. */
        [HttpGet("GetClientAndSalesRepresentativeInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientAndSalesRepresentativeInfo()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientAndSalesRepresentativeInfo();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("GetClientsWithPaymentsAndSalesRepresentatives")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientsWithPaymentsAndSalesRepresentatives()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientsWithPaymentsAndSalesRepresentatives();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }



        /* Devuelve un listado que muestre solamente los clientes que no han
realizado ningún pago. */
        [HttpGet("GetClientsWithoutPayments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientsWithoutPayments()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientsWithoutPayments();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

       /*  Devuelve el nombre de los clientes a los que no se les ha entregado a
tiempo un pedido. */
        [HttpGet("GetClientsWithLateDeliveries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientsWithLateDeliveries()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientsWithLateDeliveries();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

       /*  Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre
de sus representantes junto con la ciudad de la oficina a la que pertenece el
representante. */
        [HttpGet("GetClientsWithoutPaymentsAndRepresentativesWithCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientsWithoutPaymentsAndRepresentativesWithCity()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientsWithoutPaymentsAndRepresentativesWithCity();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /* Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus
representantes junto con la ciudad de la oficina a la que pertenece el
representante. */
        [HttpGet("GetClientsWithPaymentsAndRepresentativesWithCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientsWithPaymentsAndRepresentativesWithCity()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientsWithPaymentsAndRepresentativesWithCity();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        /* Muestra el nombre de los clientes que no hayan realizado pagos junto con
el nombre de sus representantes de ventas. */
        [HttpGet("GetClientsWithoutPaymentsAndRepresentatives")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientsWithoutPaymentsAndRepresentatives()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientsWithoutPaymentsAndRepresentatives();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /* Muestra el nombre de los clientes que hayan realizado pagos junto con el
nombre de sus representantes de ventas. */
        [HttpGet("GetClientsWithPaymentsAndRepresentatives")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientsWithPaymentsAndRepresentatives()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientsWithPaymentsAndRepresentatives();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /* Devuelve el nombre de los clientes y el nombre de sus representantes junto
con la ciudad de la oficina a la que pertenece el representante. */
        [HttpGet("GetClientsWithRepresentativesAndOfficeCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetClientsWithRepresentativesAndOfficeCity()
        {
            try
            {
                var clientCodes = _unitOfWork.Clients.GetClientsWithRepresentativesAndOfficeCity();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
        

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientDto>> Get(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            return _mapper.Map<ClientDto>(client);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> Post(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            _unitOfWork.Clients.Add(client);
            await _unitOfWork.SaveAsync();
            if (client == null)
                return BadRequest();
            clientDto.Id = client.Id;
            return CreatedAtAction(nameof(Post), new { id = clientDto.Id }, clientDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientDto>> Put(int id, [FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
                return NotFound();
            if (clientDto.Id == 0)
                clientDto.Id = id;
            if (clientDto.Id != id)
                return BadRequest();
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            _mapper.Map(clientDto, client);
            //client.FechaModificacion = DateTime.Now;
            _unitOfWork.Clients.Update(client);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ClientDto>(client);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            _unitOfWork.Clients.Remove(client);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
