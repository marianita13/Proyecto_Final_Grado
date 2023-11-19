
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PersonController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public PersonController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PersonDto>>> Get()
        {
            var person = await _unitOfWork.Persons.GetAllAsync();
            return _mapper.Map<List<PersonDto>>(person);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null) return NotFound();
            return _mapper.Map<PersonDto>(person);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Person>> Post(PersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            _unitOfWork.Persons.Add(person);
            await _unitOfWork.SaveAsync();
            if (person == null) return BadRequest();
            personDto.Id = person.Id;
            return CreatedAtAction(nameof(Post), new { id = personDto.Id }, personDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonDto>> Put(int id, [FromBody] PersonDto personDto)
        {
            if (personDto == null) return NotFound();
            if (personDto.Id == 0) personDto.Id = id;
            if (personDto.Id != id) return BadRequest();
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            _mapper.Map(personDto, person);
            //person.FechaModificacion = DateTime.Now;
            _unitOfWork.Persons.Update(person);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PersonDto>(person);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null) return NotFound();
            _unitOfWork.Persons.Remove(person);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}