using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CityController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CityDto>>> Get()
        {
            var city = await _unitOfWork.Cities.GetAllAsync();
            return _mapper.Map<List<CityDto>>(city);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            var city = await _unitOfWork.Cities.GetByIdAsync(id);
            if (city == null) return NotFound();
            return _mapper.Map<CityDto>(city);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<City>> Post(CityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            _unitOfWork.Cities.Add(city);
            await _unitOfWork.SaveAsync();
            if (city == null) return BadRequest();
            cityDto.Id = city.Id;
            return CreatedAtAction(nameof(Post), new { id = cityDto.Id }, cityDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CityDto>> Put(int id, [FromBody] CityDto cityDto)
        {
            if (cityDto == null) return NotFound();
            if (cityDto.Id == 0) cityDto.Id = id;
            if (cityDto.Id != id) return BadRequest();
            var city = await _unitOfWork.Cities.GetByIdAsync(id);
            _mapper.Map(cityDto, city);
            //city.FechaModificacion = DateTime.Now;
            _unitOfWork.Cities.Update(city);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CityDto>(city);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await _unitOfWork.Cities.GetByIdAsync(id);
            if (city == null) return NotFound();
            _unitOfWork.Cities.Remove(city);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
