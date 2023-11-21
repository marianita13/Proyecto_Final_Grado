using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SummaryController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SummaryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetQuantiyEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> GetQuantiyEmployees()
        {
            var employee = await _unitOfWork.Summaries.GetQuantiyEmployees();
            return Ok(employee);
        }
        
        [HttpGet("ClientQuantityForCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> ClientQuantityForCountry()
        {
            var employee = await _unitOfWork.Summaries.ClientQuantityForCountry();
            return Ok(employee);
        }

        [HttpGet("PaymentAverage2009")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> PaymentAverage2009()
        {
            var employee = await _unitOfWork.Summaries.PaymentAverage2009();
            return Ok(employee);
        }

        [HttpGet("OrderQuantityForStates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> OrderQuantityForStates()
        {
            var employee = await _unitOfWork.Summaries.OrderQuantityForStates();
            return Ok(employee);
        }

        [HttpGet("ProductMoreExpensiveAndCheap")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> ProductMoreExpensiveAndCheap()
        {
            var employee = await _unitOfWork.Summaries.ProductMoreExpensiveAndCheap();
            return Ok(employee);
        }

        [HttpGet("GetQuantiyClients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> GetQuantiyClients()
        {
            var employee = await _unitOfWork.Summaries.GetQuantiyClients();
            return Ok(employee);
        }

        [HttpGet("ClientInMadrid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> ClientInMadrid()
        {
            var employee = await _unitOfWork.Summaries.ClientInMadrid();
            return Ok(employee);
        }

        [HttpGet("ClientsInCityWithM")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> ClientsInCityWithM()
        {
            var employee = await _unitOfWork.Summaries.ClientsInCityWithM();
            return Ok(employee);
        }

        [HttpGet("quantityClientsForEmployeeVents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> quantityClientsForEmployeeVents()
        {
            var employee = await _unitOfWork.Summaries.quantityClientsForEmployeeVents();
            return Ok(employee);
        }

        [HttpGet("ClientNoEmployeeVents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> ClientNoEmployeeVents()
        {
            var employee = await _unitOfWork.Summaries.ClientNoEmployeeVents();
            return Ok(employee);
        }







        
    }
}