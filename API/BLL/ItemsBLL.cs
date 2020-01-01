using AutoMapper;
using DataLayer.Repositories;
using DomainLayer.Entities;
using DomainLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.BLL
{
    public class ItemsBLL : BaseBLL
    {
        public ItemsBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        public async Task<IEnumerable<ItemModel>> GetAllAsync()
        {
            var items = await _unitOfWork.ItemRepository.GetAllAsync(includeProperties: $"{nameof(Merchant)}");
            return _mapper.Map<IEnumerable<ItemModel>>(items);
        }
        public async Task<ItemModel> GetAsync(int itemId)
        {
            var item = await _unitOfWork.ItemRepository.GetAsync(i => i.ID == itemId, includeProperties: $"{nameof(Merchant)}");
            return _mapper.Map<ItemModel>(item);
        }
        public async Task<ItemModel> UpdateAsync(ItemModel model)
        {
            var item = _unitOfWork.ItemRepository.Get(x => x.ID == model.ID);

            if (item == null)
            {
                return null;
            }

            _mapper.Map(model, item);
            _unitOfWork.ItemRepository.Update(item);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ItemModel>(item);
        }
        public async Task<ItemModel> AddAsync(ItemModel model)
        {
            var item = _mapper.Map<Item>(model);
            _unitOfWork.ItemRepository.Add(item);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ItemModel>(item);
        }
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.ItemRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
