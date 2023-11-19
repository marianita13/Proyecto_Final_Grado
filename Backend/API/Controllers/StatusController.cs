using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StatusController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public StatusController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<StatusDto>>> Get()
        {
            var status = await _unitOfWork.Status.GetAllAsync();
            return _mapper.Map<List<StatusDto>>(status);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusDto>> Get(int id)
        {
            var status = await _unitOfWork.Status.GetByIdAsync(id);
            if (status == null) return NotFound();
            return _mapper.Map<StatusDto>(status);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Status>> Post(StatusDto statusDto)
        {
            var status = _mapper.Map<Status>(statusDto);
            _unitOfWork.Status.Add(status);
            await _unitOfWork.SaveAsync();
            if (status == null) return BadRequest();
            statusDto.Id = status.Id;
            return CreatedAtAction(nameof(Post), new { id = statusDto.Id }, statusDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StatusDto>> Put(int id, [FromBody] StatusDto statusDto)
        {
            if (statusDto == null) return NotFound();
            if (statusDto.Id == 0) statusDto.Id = id;
            if (statusDto.Id != id) return BadRequest();
            var status = await _unitOfWork.Status.GetByIdAsync(id);
            _mapper.Map(statusDto, status);
            //status.FechaModificacion = DateTime.Now;
            _unitOfWork.Status.Update(status);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<StatusDto>(status);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.Status.GetByIdAsync(id);
            if (status == null) return NotFound();
            _unitOfWork.Status.Remove(status);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
