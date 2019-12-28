using AutoMapper;
using DataLayer;
using DataLayer.Repositories;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.BLL
{
    public class MerchantsBLL : BaseBLL
    {
        public MerchantsBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        public async Task<IEnumerable<MerchantModel>> GetAllAsync()
        {
            var merchants = await _unitOfWork
                .MerchantRepository
                .GetAllAsync(includeProperties: $"{nameof(MerchantType)}");
            return _mapper.Map<IEnumerable<MerchantModel>>(merchants);
        }
        public async Task<MerchantModel> GetAsync(int id)
        {
            var merchant = await _unitOfWork
                .MerchantRepository
                .GetAsync(x => x.ID == id, includeProperties: $"{nameof(MerchantType)}");
            return _mapper.Map<MerchantModel>(merchant);
        }
        public async Task<IEnumerable<ItemModel>> GetMerchantItems(int id)
        {
            var items = await _unitOfWork
                .MerchantRepository
                .GetMerchantItemsAsync(id);
            return _mapper.Map<IEnumerable<ItemModel>>(items);
        }
        public async Task<MerchantModel> UpdateAsync(MerchantModel model)
        {
            var merchant = await _unitOfWork
                .MerchantRepository
                .GetAsync(x => x.ID == model.ID);
            if (merchant == null)
            {
                return null;
            }
            _mapper.Map(model, merchant);
            _unitOfWork.MerchantRepository.Update(merchant);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MerchantModel>(merchant);
        }
        public async Task<MerchantModel> AddAsync(MerchantModel model)
        {
            var merchant = _mapper.Map<Merchant>(model);
            _unitOfWork.MerchantRepository.Add(merchant);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MerchantModel>(merchant);
        }
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.MerchantRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
