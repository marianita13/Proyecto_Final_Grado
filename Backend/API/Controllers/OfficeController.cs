using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OfficeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public OfficeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OfficeDto>>> Get()
        {
            var office = await _unitOfWork.Offices.GetAllAsync();
            return _mapper.Map<List<OfficeDto>>(office);
        }

        
        /* Devuelve un listado con el código de oficina y la ciudad donde hay oficinas. */
        [HttpGet("GetCitiesWithOffices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OfficeDto>>> GetCitiesWithOffices()
        {
            var office = await _unitOfWork.Offices.GetCitiesWithOffices();
            return Ok(office);
        }

        /* Devuelve un listado con la ciudad y el telé fono de las oficinas de España. */
        [HttpGet("GetCitiesWithOfficesInSpain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OfficeDto>>> GetCitiesWithOfficesInSpain()
        {
            var office = await _unitOfWork.Offices.GetCitiesWithOfficesInSpain();
            return Ok(office);
        }
        
        [HttpGet("GetOfficeNoSellFruits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OfficeDto>>> GetOfficeNoSellFruits()
        {
            var office = await _unitOfWork.Offices.GetOfficeNoSellFruits();
            return _mapper.Map<List<OfficeDto>>(office);
        }

        /* Lista la dirección de las oficinas que tengan clientes en Fuenlabrada. */
        [HttpGet("GetOfficesWithClientsInFuenlabrada")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetOfficesWithClientsInFuenlabrada()
        {
            try
            {
                var clientCodes = _unitOfWork.Offices.GetOfficesWithClientsInFuenlabrada();
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
        public async Task<ActionResult<OfficeDto>> Get(string id)
        {
            var office = await _unitOfWork.Offices.GetByIdAsync(id);
            if (office == null) return NotFound();
            return _mapper.Map<OfficeDto>(office);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Office>> Post(OfficeDto officeDto)
        {
            var office = _mapper.Map<Office>(officeDto);
            _unitOfWork.Offices.Add(office);
            await _unitOfWork.SaveAsync();
            if (office == null) return BadRequest();
            officeDto.Id = office.Id;
            return CreatedAtAction(nameof(Post), new { id = officeDto.Id }, officeDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OfficeDto>> Put(string id, [FromBody] OfficeDto officeDto)
        {
            if (officeDto == null) return NotFound();
            if (officeDto.Id == null) officeDto.Id = id;
            if (officeDto.Id != id) return BadRequest();
            var office = await _unitOfWork.Offices.GetByIdAsync(id);
            _mapper.Map(officeDto, office);
            //office.FechaModificacion = DateTime.Now;
            _unitOfWork.Offices.Update(office);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OfficeDto>(office);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var office = await _unitOfWork.Offices.GetByIdAsync(id);
            if (office == null) return NotFound();
            _unitOfWork.Offices.Remove(office);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
