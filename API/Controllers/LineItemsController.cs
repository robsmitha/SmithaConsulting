using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Data;
using DomainLayer.Models;
using DataLayer.DAL;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LineItemsController(DbArchitecture context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LineItemModel>> Get()
        {
            var lineItems = _unitOfWork
                .LineItemRepository
                .GetAll(includeProperties: "Item");
            try
            {
                return Ok(_mapper.Map<IEnumerable<LineItemModel>>(lineItems));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LineItemModel>> Get(int id)
        {
            var lineItem = await _unitOfWork.LineItemRepository
                .GetAsync(x => x.ID == id, includeProperties: "Item");

            if (lineItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LineItemModel>(lineItem));
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<LineItemModel>> Put(int id, LineItemModel model)
        {
            var lineItem = await _unitOfWork
                .LineItemRepository
                .GetAsync(x => x.ID == id, includeProperties: "Customer,Merchant,OrderStatusType,User");

            if (lineItem == null)
            {
                return NotFound();
            }

            _mapper.Map(model, lineItem);
            _unitOfWork.LineItemRepository.Update(lineItem);
            await _unitOfWork.SaveAsync();
            return Ok(_mapper.Map<LineItemModel>(lineItem));
        }


        [HttpPost]
        public async Task<ActionResult<LineItemModel>> Post(LineItemModel model)
        {
            try
            {
                var lineItem = _mapper.Map<LineItem>(model);
                _unitOfWork.LineItemRepository.Add(lineItem);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("Get", new { id = lineItem.ID });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _unitOfWork.LineItemRepository.Delete(id);
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
