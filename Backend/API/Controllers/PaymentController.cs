
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public PaymentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> Get()
        {
            var payment = await _unitOfWork.Payments.GetAllAsync();
            return _mapper.Map<List<PaymentDto>>(payment);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentDto>> Get(int id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null) return NotFound();
            return _mapper.Map<PaymentDto>(payment);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Payment>> Post(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            _unitOfWork.Payments.Add(payment);
            await _unitOfWork.SaveAsync();
            if (payment == null) return BadRequest();
            paymentDto.ClientCode = payment.Id;
            return CreatedAtAction(nameof(Post), new { id = paymentDto.ClientCode }, paymentDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentDto>> Put(int ClientCode, [FromBody] PaymentDto paymentDto)
        {
            if (paymentDto == null) return NotFound();
            if (paymentDto.ClientCode == 0) paymentDto.ClientCode = ClientCode;
            if (paymentDto.ClientCode != ClientCode) return BadRequest();
            var payment = await _unitOfWork.Payments.GetByIdAsync(ClientCode);
            _mapper.Map(paymentDto, payment);
            //payment.FechaModificacion = DateTime.Now;
            _unitOfWork.Payments.Update(payment);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PaymentDto>(payment);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null) return NotFound();
            _unitOfWork.Payments.Remove(payment);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}