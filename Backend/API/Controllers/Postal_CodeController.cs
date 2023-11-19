
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PostalCodeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public PostalCodeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PostalCodeDto>>> Get()
        {
            var postalCode = await _unitOfWork.PCodes.GetAllAsync();
            return _mapper.Map<List<PostalCodeDto>>(postalCode);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostalCodeDto>> Get(int id)
        {
            var postalCode = await _unitOfWork.PCodes.GetByIdAsync(id);
            if (postalCode == null) return NotFound();
            return _mapper.Map<PostalCodeDto>(postalCode);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostalCode>> Post(PostalCodeDto postalCodeDto)
        {
            var postalCode = _mapper.Map<PostalCode>(postalCodeDto);
            _unitOfWork.PCodes.Add(postalCode);
            await _unitOfWork.SaveAsync();
            if (postalCode == null) return BadRequest();
            postalCodeDto.Id = postalCode.Id;
            return CreatedAtAction(nameof(Post), new { id = postalCodeDto.Id }, postalCodeDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostalCodeDto>> Put(int id, [FromBody] PostalCodeDto postalCodeDto)
        {
            if (postalCodeDto == null) return NotFound();
            if (postalCodeDto.Id == 0) postalCodeDto.Id = id;
            if (postalCodeDto.Id != id) return BadRequest();
            var postalCode = await _unitOfWork.PCodes.GetByIdAsync(id);
            _mapper.Map(postalCodeDto, postalCode);
            //postalCode.FechaModificacion = DateTime.Now;
            _unitOfWork.PCodes.Update(postalCode);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PostalCodeDto>(postalCode);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var postalCode = await _unitOfWork.PCodes.GetByIdAsync(id);
            if (postalCode == null) return NotFound();
            _unitOfWork.PCodes.Remove(postalCode);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}