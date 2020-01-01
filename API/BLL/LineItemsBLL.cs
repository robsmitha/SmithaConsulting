using AutoMapper;
using DataLayer.Repositories;
using DomainLayer.Entities;
using DomainLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.BLL
{
    public class LineItemsBLL : BaseBLL
    {
        public LineItemsBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        public async Task<IEnumerable<LineItemModel>> GetAllAsync()
        {
            var lineItems = await _unitOfWork.LineItemRepository
                .GetAllAsync(includeProperties: $"{nameof(Item)}");
            return _mapper.Map<IEnumerable<LineItemModel>>(lineItems);
        }
        public async Task<LineItemModel> GetAsync(int id)
        {
            var lineItem = await _unitOfWork.LineItemRepository
                .GetAsync(x => x.ID == id, includeProperties: $"{nameof(Item)}");
            return _mapper.Map<LineItemModel>(lineItem);
        }
        public async Task<LineItemModel> UpdateAsync(LineItemModel model)
        {
            var lineItem = await _unitOfWork
                .LineItemRepository
                .GetAsync(x => x.ID == model.ID);
            if (lineItem == null)
            {
                return null;
            }
            _mapper.Map(model, lineItem);
            _unitOfWork.LineItemRepository.Update(lineItem);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<LineItemModel>(lineItem);
        }
        public async Task<LineItemModel> AddAsync(LineItemModel model)
        {
            var lineItem = _mapper.Map<LineItem>(model);
            _unitOfWork.LineItemRepository.Add(lineItem);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<LineItemModel>(lineItem);
        }
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.LineItemRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
