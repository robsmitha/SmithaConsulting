using AutoMapper;
using DataLayer.Repositories;
using DomainLayer.Entities;
using DomainLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.BLL
{
    public class BlogBLL : BaseBLL
    {
        public BlogBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        public async Task<IEnumerable<BlogModel>> GetAllAsync()
        {
            var blogs = await _unitOfWork
                .BlogRepository
                .GetAllAsync(includeProperties: $"{nameof(BlogStatusType)},{nameof(User)}");
            return _mapper.Map<IEnumerable<BlogModel>>(blogs);
        }
        public async Task<BlogModel> GetAsync(int blogId)
        {
            var blog = await _unitOfWork
                .BlogRepository
                .GetAsync(filter: c => c.ID == blogId, includeProperties: $"{nameof(BlogStatusType)},{nameof(User)}");
            return _mapper.Map<BlogModel>(blog);
        }
        public async Task<BlogModel> UpdateAsync(BlogModel model)
        {
            var blog = _unitOfWork.BlogRepository.Get(x => x.ID == model.ID);

            if (blog == null)
            {
                return null;
            }

            _mapper.Map(model, blog);
            _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<BlogModel>(blog);
        }
        public async Task<BlogModel> AddAsync(BlogModel model)
        {
            var blog = _mapper.Map<Blog>(model);
            _unitOfWork.BlogRepository.Add(blog);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<BlogModel>(blog);
        }
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.BlogRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
