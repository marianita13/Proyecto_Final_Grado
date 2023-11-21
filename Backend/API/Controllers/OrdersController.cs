using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            var orders = await _unitOfWork.Orderse.GetAllAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        /* Devuelve un listado con los distintos estados por los que puede pasar un
pedido. */
        [HttpGet("GetOrderStatusList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderStatusList()
        {
            var office = await _unitOfWork.Orderse.GetOrderStatusList();
            return Ok(office);
        }

        /* Devuelve un listado con el código de pedido, código de cliente, fecha
esperada y fecha de entrega de los pedidos que no han sido entregados a
tiempo. */
        [HttpGet("GetDelayedOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetDelayedOrders()
        {
            var office = await _unitOfWork.Orderse.GetDelayedOrders();
            return Ok(office);
        }

        /* Devuelve un listado con el código de pedido, código de cliente, fecha
esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al
menos dos días antes de la fecha esperada. */
        [HttpGet("GetOrdersDeliveredEarly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersDeliveredEarly()
        {
            var office = await _unitOfWork.Orderse.GetOrdersDeliveredEarly();
            return Ok(office);
        }

        /* Devuelve un listado de todos los pedidos que fueron rechazados en 2009. */
        [HttpGet("GetRejectedOrdersIn2009")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetRejectedOrdersIn2009()
        {
            var office = await _unitOfWork.Orderse.GetRejectedOrdersIn2009();
            return Ok(office);
        }
        
        /* Devuelve un listado de todos los pedidos que han sido entregados en el
mes de enero de cualquier año. */
        [HttpGet("GetOrdersDeliveredInJanuary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersDeliveredInJanuary()
        {
            var office = await _unitOfWork.Orderse.GetOrdersDeliveredInJanuary();
            return Ok(office);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var orders = await _unitOfWork.Orderse.GetByIdAsync(id);
            if (orders == null) return NotFound();
            return _mapper.Map<OrderDto>(orders);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> Post(OrderDto ordersDto)
        {
            var orders = _mapper.Map<Order>(ordersDto);
            _unitOfWork.Orderse.Add(orders);
            await _unitOfWork.SaveAsync();
            if (orders == null) return BadRequest();
            ordersDto.Id = orders.Id;
            return CreatedAtAction(nameof(Post), new { id = ordersDto.Id }, ordersDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDto>> Put(int id, [FromBody] OrderDto ordersDto)
        {
            if (ordersDto == null) return NotFound();
            if (ordersDto.Id == 0) ordersDto.Id = id;
            if (ordersDto.Id != id) return BadRequest();
            var orders = await _unitOfWork.Orderse.GetByIdAsync(id);
            _mapper.Map(ordersDto, orders);
            //orders.FechaModificacion = DateTime.Now;
            _unitOfWork.Orderse.Update(orders);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderDto>(orders);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var orders = await _unitOfWork.Orderse.GetByIdAsync(id);
            if (orders == null) return NotFound();
            _unitOfWork.Orderse.Remove(orders);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}