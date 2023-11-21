using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var employee = await _unitOfWork.Employees.GetAllAsync();
            return _mapper.Map<List<EmployeeDto>>(employee);
        }

        //EMPLEADOS A CARGO DE ALBERTOSORIA
        [HttpGet("AlbertoSoriaEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> AlbertoSoriaEmployees()
        {
            var employee = await _unitOfWork.Employees.AlbertoSoriaEmployees();
            return Ok(employee);
        }


        //EMPLEADOS A CARGO DE ALBERTOSORIA
        // [HttpGet("Employees_Dont_Represent_Client")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<ActionResult<IEnumerable<EmployeeDto>>> Employees_Dont_Represent_Client()
        // {
        //     var employee = await _unitOfWork.Employees.Employees_Dont_Represent_Client();
        //     return Ok(employee);
        // }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return _mapper.Map<EmployeeDto>(employee);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> Post(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.Employees.Add(employee);
            await _unitOfWork.SaveAsync();
            if (employee == null) return BadRequest();
            employeeDto.Id = employee.Id;
            return CreatedAtAction(nameof(Post), new { id = employeeDto.Id }, employeeDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeDto>> Put(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null) return NotFound();
            if (employeeDto.Id == 0) employeeDto.Id = id;
            if (employeeDto.Id != id) return BadRequest();
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            _mapper.Map(employeeDto, employee);
            //employee.FechaModificacion = DateTime.Now;
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<EmployeeDto>(employee);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null) return NotFound();
            _unitOfWork.Employees.Remove(employee);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
