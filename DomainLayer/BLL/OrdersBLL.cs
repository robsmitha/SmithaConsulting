﻿using AutoMapper;
using DataLayer;
using DataLayer.DAL;
using DataLayer.Data;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.BLL
{
    public class OrdersBLL : BaseBLL
    {
        public OrdersBLL(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public async Task<List<OrderModel>> GetOrderModelsAsync(string includeProperties = null)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync(includeProperties: includeProperties);
            return _mapper.Map<List<OrderModel>>(orders);
        }

        public async Task<OrderModel> GetOrderModelAsync(int id, string includeProperties = null)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(filter: o => o.ID == id, includeProperties: includeProperties);
            return _mapper.Map<OrderModel>(order);
        }

        public async Task<IEnumerable<LineItemModel>> GetLineItemModelsAsync(int orderId)
        {
            var lineItems = await Task.Run(() => _unitOfWork.OrderRepository.GetLineItems(orderId));
            return _mapper.Map<IEnumerable<LineItemModel>>(lineItems);
        }
        public async Task<IEnumerable<PaymentModel>> GetPaymentsAsync(int orderId)
        {
            var payments = await Task.Run(() => _unitOfWork.OrderRepository.GetPayments(orderId));
            return _mapper.Map<IEnumerable<PaymentModel>>(payments);
        }
        public async Task<OrderModel> AddOrderAsync(OrderModel model)
        {
            var order = _mapper.Map<Order>(model);
            _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderModel>(order);
        }
        public async Task<OrderModel> UpdateOrderAsync(OrderModel model)
        {

            var order = await _unitOfWork.OrderRepository .GetAsync(x => x.ID == model.ID);
            if (order == null)
            {
                return null;
            }
            _mapper.Map(model, order);
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderModel>(order);
        }
        public async void DeleteOrder(int id)
        {
            _unitOfWork.OrderRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
        public async void DeleteLineItemsByItemId(int id, int itemId)
        {
            var entities = _unitOfWork.LineItemRepository.GetAll(filter: x => x.OrderID == id && x.ItemID == itemId);
            _unitOfWork.LineItemRepository.DeleteRange(entities);
            await _unitOfWork.SaveAsync();
        }

        //public OrderModel GetOrderModel(int id)
        //{
        //    var orderRows = _unitOfWork.OrderRepository.GetOrderRows(id);
        //    return GetOrderModels(orderRows.ToList()).FirstOrDefault();
        //}

        public List<OrderModel> GetCustomerOrderModels(int customerId)
        {
            var orderRows = _unitOfWork.OrderRepository.GetCustomerOrderRows(customerId);
            return GetOrderModels(orderRows.ToList());
        }

        private List<OrderModel> GetOrderModels(List<OrdersRepository.OrderRow> orderRows)
        {
            var orders = new Dictionary<int, OrderModel>();

            try
            {
                foreach (var orderRow in orderRows)
                {
                    if (orderRow?.order as Order == null) continue;

                    if (!orders.TryGetValue(orderRow.order.ID, out OrderModel orderModel))
                    {
                        orderModel = _mapper.Map<OrderModel>(orderRow.order);
                        orders.Add(orderRow.order.ID, orderModel);
                        orderModel.LineItems = new List<LineItemModel>();
                        orderModel.Payments = new List<PaymentModel>();
                    }
                    orderRow.lineItem.Item = orderRow.item;
                    orderModel.LineItems.Add(_mapper.Map<LineItemModel>(orderRow.lineItem));
                    orderModel.Payments.Add(_mapper.Map<PaymentModel>(orderRow.payment));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return orders.Select(o => o.Value).ToList();
        }
    }
}
