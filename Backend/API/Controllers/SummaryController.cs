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
        public async Task<ActionResult<object>> GetQuantiyEmployees()
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
    }
}