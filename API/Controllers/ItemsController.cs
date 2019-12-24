using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.DAL;
using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemsController(DbArchitecture context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemModel>>> Get()
        {
            var items = await _unitOfWork.ItemRepository.GetAllAsync(includeProperties: "Merchant");
            return Ok(_mapper.Map<IEnumerable<ItemModel>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemModel>> Get(int id)
        {
            var item = await _unitOfWork.ItemRepository.GetAsync(i => i.ID == id, includeProperties: "Merchant");
            if (item == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ItemModel>(item));
        }

        [HttpPost]
        public async Task<ActionResult<ItemModel>> Post([FromBody] ItemModel model)
        {
            try
            {
                var item = _mapper.Map<Item>(model);
                _unitOfWork.ItemRepository.Add(item);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("Get", new { id = item.ID });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemModel>> Put(int id, [FromBody] ItemModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            var item = _unitOfWork.ItemRepository.Get(x => x.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            _mapper.Map(model, item);

            _unitOfWork.ItemRepository.Update(item);

            try
            {
                await _unitOfWork.SaveAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _unitOfWork.ItemRepository.Delete(id);
                await _unitOfWork.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}