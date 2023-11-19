
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductLineController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public ProductLineController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductLineDto>>> Get()
        {
            var productoLine = await _unitOfWork.PLines.GetAllAsync();
            return _mapper.Map<List<ProductLineDto>>(productoLine);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductLineDto>> Get(int id)
        {
            var productoLine = await _unitOfWork.PLines.GetByIdAsync(id);
            if (productoLine == null) return NotFound();
            return _mapper.Map<ProductLineDto>(productoLine);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductLine>> Post(ProductLineDto productLineDto)
        {
            var productoLine = _mapper.Map<ProductLine>(productLineDto);
            _unitOfWork.PLines.Add(productoLine);
            await _unitOfWork.SaveAsync();
            if (productoLine == null) return BadRequest();
            productLineDto.Id = productoLine.Id;
            return CreatedAtAction(nameof(Post), new { id = productLineDto.Id }, productLineDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductLineDto>> Put(int id, [FromBody] ProductLineDto productLineDto)
        {
            if (productLineDto == null) return NotFound();
            if (productLineDto.Id == 0) productLineDto.Id = id;
            if (productLineDto.Id != id) return BadRequest();
            var productoLine = await _unitOfWork.PLines.GetByIdAsync(id);
            _mapper.Map(productLineDto, productoLine);
            //productoLine.FechaModificacion = DateTime.Now;
            _unitOfWork.PLines.Update(productoLine);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ProductLineDto>(productoLine);
        
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var productoLine = await _unitOfWork.PLines.GetByIdAsync(id);
            if (productoLine == null) return NotFound();
            _unitOfWork.PLines.Remove(productoLine);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}