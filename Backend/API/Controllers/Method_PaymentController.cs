using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MethodPaymentController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public MethodPaymentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MethodPaymentDto>>> Get()
        {
            var methodPayment = await _unitOfWork.Methods.GetAllAsync();
            return _mapper.Map<List<MethodPaymentDto>>(methodPayment);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MethodPaymentDto>> Get(int id)
        {
            var methodPayment = await _unitOfWork.Methods.GetByIdAsync(id);
            if (methodPayment == null) return NotFound();
            return _mapper.Map<MethodPaymentDto>(methodPayment);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MethodPayment>> Post(MethodPaymentDto methodPaymentDto)
        {
            var methodPayment = _mapper.Map<MethodPayment>(methodPaymentDto);
            _unitOfWork.Methods.Add(methodPayment);
            await _unitOfWork.SaveAsync();
            if (methodPayment == null) return BadRequest();
            methodPaymentDto.Id = methodPayment.Id;
            return CreatedAtAction(nameof(Post), new { id = methodPaymentDto.Id }, methodPaymentDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MethodPaymentDto>> Put(int id, [FromBody] MethodPaymentDto methodPaymentDto)
        {
            if (methodPaymentDto == null) return NotFound();
            if (methodPaymentDto.Id == 0) methodPaymentDto.Id = id;
            if (methodPaymentDto.Id != id) return BadRequest();
            var methodPayment = await _unitOfWork.Methods.GetByIdAsync(id);
            _mapper.Map(methodPaymentDto, methodPayment);
            //methodPayment.FechaModificacion = DateTime.Now;
            _unitOfWork.Methods.Update(methodPayment);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MethodPaymentDto>(methodPayment);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var methodPayment = await _unitOfWork.Methods.GetByIdAsync(id);
            if (methodPayment == null) return NotFound();
            _unitOfWork.Methods.Remove(methodPayment);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
