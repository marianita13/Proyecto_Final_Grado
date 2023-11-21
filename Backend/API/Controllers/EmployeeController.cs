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

        /* Devuelve un listado con el nombre, apellidos y email de los empleados cuyo
jefe tiene un código de jefe igual a 7. */
        [HttpGet("GetEmployeesByBossCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByBossCode(int bossCode)
        {
            var office = await _unitOfWork.Employees.GetEmployeesByBossCode(bossCode);
            return Ok(office);
        }

        /* Devuelve un listado con el nombre, apellidos y puesto de aquellos
empleados que no sean representantes de ventas. */
        [HttpGet("GetNonSalesRepresentatives")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetNonSalesRepresentatives()
        {
            var office = await _unitOfWork.Employees.GetNonSalesRepresentatives();
            return Ok(office);
        }

        /* Devuelve el nombre del puesto, nombre, apellidos y email del jefe de la
        empresa. */
        [HttpGet("GetCEOInformation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetCEOInformation()
        {
            var office = await _unitOfWork.Employees.GetCEOInformation();
            return Ok(office);
        }

/* Devuelve un listado que muestre el nombre de cada empleados, el nombre
de su jefe y el nombre del jefe de sus jefe. */
        [HttpGet("GetEmployeeHierarchy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetEmployeeHierarchy()
        {
            try
            {
                var clientCodes = _unitOfWork.Employees.GetEmployeeHierarchy();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
        [HttpGet("GetEmployeesWithManagers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<int>> GetEmployeesWithManagers()
        {
            try
            {
                var clientCodes = _unitOfWork.Employees.GetEmployeesWithManagers();
                return Ok(clientCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener códigos de cliente: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
        
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
