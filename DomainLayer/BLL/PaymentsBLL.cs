using AutoMapper;
using DataLayer;
using DataLayer.DAL;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.BLL
{
    public class PaymentsBLL : BaseBLL
    {
        public PaymentsBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        public async Task<IEnumerable<PaymentModel>> GetAllAsync()
        {
            var payments = await _unitOfWork
                .PaymentRepository
                .GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentModel>>(payments);
        }
        public async Task<PaymentModel> GetAsync(int id)
        {
            var payment = await _unitOfWork
                .PaymentRepository
                .GetAsync(x => x.ID == id);
            return _mapper.Map<PaymentModel>(payment);
        }

        public async Task<PaymentModel> UpdateAsync(PaymentModel model)
        {
            var payment = await _unitOfWork
                .PaymentRepository
                .GetAsync(x => x.ID == model.ID);
            if (payment == null)
            {
                return null;
            }
            _mapper.Map(model, payment);
            _unitOfWork.PaymentRepository.Update(payment);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PaymentModel>(payment);
        }
        public async Task<PaymentModel> AddAsync(PaymentModel model)
        {
            var payment = _mapper.Map<Payment>(model);
            _unitOfWork.PaymentRepository.Add(payment);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PaymentModel>(payment);
        }
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.PaymentRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
