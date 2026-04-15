using AutoMapper;
using Dto;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _r;
        IMapper _mapper;
        ILogger<OrderService> _logger;

        public OrderService(IOrderRepository i, IMapper mapperr, ILogger<OrderService> logger)
        {
            _mapper = mapperr;
            _r = i;
            _logger = logger;
        }
        public async Task<List<DtoOrder_Id_UserId_Date_Sum_OrderItems?>> GetOrdersUser(int id)
        {

           var o = await _r.GetOrdersUser(id);
           var r = _mapper.Map<List<Order>, List<DtoOrder_Id_UserId_Date_Sum_OrderItems>>(o);
           return r;
        }
        public async Task<DtoOrder_Id_UserId_Date_Sum_OrderItems?> GetOrderById(int id)
        {
            Order o = await _r.GetOrderById(id);
            return _mapper.Map<Order, DtoOrder_Id_UserId_Date_Sum_OrderItems>(o);

        }

        public async Task<DtoOrder_Id_UserId_Date_Sum_OrderItems> AddNewOrder(DtoOrder_Id_UserId_Date_Sum_OrderItems order)
        {
            var calculatedTotal = order.OrderItems.Sum(item => item.PriceAtPurchase * item.Quantity);
            if (order.TotalPrice != calculatedTotal)
            {
                _logger.LogWarning("Order sum mismatch. UserId: {UserId}, SentTotalPrice: {SentTotalPrice}, CalculatedTotalPrice: {CalculatedTotalPrice}",
                    order.UserId,
                    order.TotalPrice,
                    calculatedTotal);
            }

            var ooo = _mapper.Map<DtoOrder_Id_UserId_Date_Sum_OrderItems, Order>(order);
            Order o = await _r.AddNewOrder(ooo);
            var oo = _mapper.Map<Order, DtoOrder_Id_UserId_Date_Sum_OrderItems>(o);
            return oo;
        }
    }
}