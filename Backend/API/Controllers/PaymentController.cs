
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

        //PAGOS TOTALES POR YEAR
        [HttpGet("GetPaymentsForYear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsForYear()
        {
            var payment = await _unitOfWork.Payments.GetPaymentsForYear();
            return Ok(payment);
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
            paymentDto.ClienteId = payment.Id;
            return CreatedAtAction(nameof(Post), new { id = paymentDto.ClienteId }, paymentDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentDto>> Put(int ClienteId, [FromBody] PaymentDto paymentDto)
        {
            if (paymentDto == null) return NotFound();
            if (paymentDto.ClienteId == 0) paymentDto.ClienteId = ClienteId;
            if (paymentDto.ClienteId != ClienteId) return BadRequest();
            var payment = await _unitOfWork.Payments.GetByIdAsync(ClienteId);
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