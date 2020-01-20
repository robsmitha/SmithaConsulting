using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.BLL
{
    public class ItemCategoryTypesBLL : BaseBLL
    {
        public ItemCategoryTypesBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public async Task<IEnumerable<ItemCategoryTypeModel>> GetAllAsync()
        {
            var collection = await _unitOfWork.ItemCategoryTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemCategoryTypeModel>>(collection);
        }
        public async Task<ItemCategoryTypeModel> GetAsync(int id)
        {
            var item = await _unitOfWork.ItemCategoryTypeRepository
                .GetAsync(x => x.ID == id);
            return _mapper.Map<ItemCategoryTypeModel>(item);
        }
    }
}
