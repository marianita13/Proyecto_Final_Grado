using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PersonTypeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public PersonTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PersonTypeDto>>> Get()
        {
            var personType = await _unitOfWork.PTypes.GetAllAsync();
            return _mapper.Map<List<PersonTypeDto>>(personType);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonTypeDto>> Get(int id)
        {
            var personType = await _unitOfWork.PTypes.GetByIdAsync(id);
            if (personType == null) return NotFound();
            return _mapper.Map<PersonTypeDto>(personType);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonType>> Post(PersonTypeDto personTypeDto)
        {
            var personType = _mapper.Map<PersonType>(personTypeDto);
            _unitOfWork.PTypes.Add(personType);
            await _unitOfWork.SaveAsync();
            if (personType == null) return BadRequest();
            personTypeDto.Id = personType.Id;
            return CreatedAtAction(nameof(Post), new { id = personTypeDto.Id }, personTypeDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonTypeDto>> Put(int id, [FromBody] PersonTypeDto personTypeDto)
        {
            if (personTypeDto == null) return NotFound();
            if (personTypeDto.Id == 0) personTypeDto.Id = id;
            if (personTypeDto.Id != id) return BadRequest();
            var personType = await _unitOfWork.PTypes.GetByIdAsync(id);
            _mapper.Map(personTypeDto, personType);
            //personType.FechaModificacion = DateTime.Now;
            _unitOfWork.PTypes.Update(personType);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PersonTypeDto>(personType);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var personType = await _unitOfWork.PTypes.GetByIdAsync(id);
            if (personType == null) return NotFound();
            _unitOfWork.PTypes.Remove(personType);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
