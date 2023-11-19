using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SupplierController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public SupplierController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> Get()
        {
            var supplier = await _unitOfWork.Suppliers.GetAllAsync();
            return _mapper.Map<List<SupplierDto>>(supplier);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SupplierDto>> Get(int id)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            return _mapper.Map<SupplierDto>(supplier);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Supplier>> Post(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _unitOfWork.Suppliers.Add(supplier);
            await _unitOfWork.SaveAsync();
            if (supplier == null) return BadRequest();
            supplierDto.Id = supplier.Id;
            return CreatedAtAction(nameof(Post), new { id = supplierDto.Id }, supplierDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SupplierDto>> Put(int id, [FromBody] SupplierDto supplierDto)
        {
            if (supplierDto == null) return NotFound();
            if (supplierDto.Id == 0) supplierDto.Id = id;
            if (supplierDto.Id != id) return BadRequest();
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            _mapper.Map(supplierDto, supplier);
            //supplier.FechaModificacion = DateTime.Now;
            _unitOfWork.Suppliers.Update(supplier);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SupplierDto>(supplier);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            _unitOfWork.Suppliers.Remove(supplier);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
