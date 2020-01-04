using AutoMapper;
using DataLayer.Repositories;
using DomainLayer.Entities;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.BLL
{
    public class UsersBLL : BaseBLL
    {
        public UsersBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var collection = await _unitOfWork.UsersRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserModel>>(collection);
        }
        public async Task<UserModel> GetAsync(int id)
        {
            var entity = await _unitOfWork.UsersRepository
                .GetAsync(x => x.ID == id);
            return _mapper.Map<UserModel>(entity);
        }
        public async Task<UserModel> GetByUsernameAsync(string username)
        {
            var entity = await _unitOfWork.UsersRepository
                .GetAsync(x => x.Username.ToLower() == username.ToLower());
            return _mapper.Map<UserModel>(entity);
        }
        public async Task<UserModel> UpdateAsync(UserModel model)
        {
            var entity = await _unitOfWork.UsersRepository
                .GetAsync(x => x.ID == model.ID);
            if (entity == null)
            {
                return null;
            }
            _mapper.Map(model, entity);
            _unitOfWork.UsersRepository.Update(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<UserModel>(entity);
        }
        public async Task<UserModel> AddAsync(UserModel model)
        {
            var entity = _mapper.Map<User>(model);
            _unitOfWork.UsersRepository.Add(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<UserModel>(entity);
        }
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.UsersRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
