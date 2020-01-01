using AutoMapper;
using DataLayer.Repositories;
using DomainLayer.Entities;
using DomainLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.BLL
{
    public class ThemesBLL : BaseBLL
    {
        public ThemesBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        public async Task<IEnumerable<ThemeModel>> GetAllAsync()
        {
            var themes = await _unitOfWork
                .ThemeRepository
                .GetAllAsync();
            return _mapper.Map<IEnumerable<ThemeModel>>(themes);
        }
        public async Task<ThemeModel> GetAsync(int id)
        {
            var theme = await _unitOfWork
                .ThemeRepository
                .GetAsync(x => x.ID == id);
            return _mapper.Map<ThemeModel>(theme);
        }

        public async Task<ThemeModel> UpdateAsync(ThemeModel model)
        {
            var theme = await _unitOfWork
                .ThemeRepository
                .GetAsync(x => x.ID == model.ID);
            if (theme == null)
            {
                return null;
            }
            _mapper.Map(model, theme);
            _unitOfWork.ThemeRepository.Update(theme);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ThemeModel>(theme);
        }
        public async Task<ThemeModel> AddAsync(ThemeModel model)
        {
            var theme = _mapper.Map<Theme>(model);
            _unitOfWork.ThemeRepository.Add(theme);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ThemeModel>(theme);
        }
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.ThemeRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
