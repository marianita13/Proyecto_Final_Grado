using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderDetailController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public OrderDetailController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDetailDto>>> Get()
        {
            var orderDetail = await _unitOfWork.Details.GetAllAsync();
            return _mapper.Map<List<OrderDetailDto>>(orderDetail);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailDto>> Get(int id)
        {
            var orderDetail = await _unitOfWork.Details.GetByIdAsync(id);
            if (orderDetail == null) return NotFound();
            return _mapper.Map<OrderDetailDto>(orderDetail);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetail>> Post(OrderDetailDto orderDetailDto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
            _unitOfWork.Details.Add(orderDetail);
            await _unitOfWork.SaveAsync();
            if (orderDetail == null) return BadRequest();
            orderDetailDto.Id = orderDetail.Id;
            return CreatedAtAction(nameof(Post), new { id = orderDetailDto.Id }, orderDetailDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailDto>> Put(int id, [FromBody] OrderDetailDto orderDetailDto)
        {
            if (orderDetailDto == null) return NotFound();
            if (orderDetailDto.Id == 0) orderDetailDto.Id = id;
            if (orderDetailDto.Id != id) return BadRequest();
            var orderDetail = await _unitOfWork.Details.GetByIdAsync(id);
            _mapper.Map(orderDetailDto, orderDetail);
            //orderDetail.FechaModificacion = DateTime.Now;
            _unitOfWork.Details.Update(orderDetail);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderDetailDto>(orderDetail);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetail = await _unitOfWork.Details.GetByIdAsync(id);
            if (orderDetail == null) return NotFound();
            _unitOfWork.Details.Remove(orderDetail);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}