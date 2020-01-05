using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.BLL
{
    public class CustomersBLL : BaseBLL
    {
        public CustomersBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        public async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerModel>>(customers);
        }
        public async Task<CustomerModel> GetAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetAsync(c => c.ID == customerId);
            return _mapper.Map<CustomerModel>(customer);
        }
        public async Task<CustomerModel> UpdateAsync(CustomerModel model)
        {
            var customer = _unitOfWork.CustomerRepository.Get(x => x.ID == model.ID);
            
            if (customer == null)
            {
                return null;
            }

            _mapper.Map(model, customer);
            _unitOfWork.CustomerRepository.Update(customer);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CustomerModel>(customer);
        }
        public async Task<CustomerModel> AddAsync(CustomerModel model)
        {
            var customer = _mapper.Map<Customer>(model);
            _unitOfWork.CustomerRepository.Add(customer);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CustomerModel>(customer);
        }
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.CustomerRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
