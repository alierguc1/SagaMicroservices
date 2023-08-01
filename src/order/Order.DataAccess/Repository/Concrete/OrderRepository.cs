using MassTransit;
using Order.DataAccess.Context;
using Order.DataAccess.Repository.Interface;
using Order.Entity.Dtos;
using Order.Entity.Enums;
using Order.Entity.Models;
using Shared.Events.order;
using Shared.Interfaces;
using Shared.Messages.order;
using Shared.Messages.payment;
using Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccess.Repository.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public OrderRepository(AppDbContext appDbContext, ISendEndpointProvider sendEndpointProvider)
        {
            _appDbContext = appDbContext;
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task CreateOrder(OrderCreateDto orderCreateDto)
        {
            var newOrder = new Orders
            {
                BuyerId = orderCreateDto.BuyerId,
                Status = OrderStatus.Suspend,
                FailMessage = "",
                Adress = new Adress
                {
                    Line = orderCreateDto.adress.Line,
                    Province = orderCreateDto.adress.Province,
                    District = orderCreateDto.adress.District,
                },
                CreatedDate = DateTime.Now
            };

            orderCreateDto.orderItems.ForEach(item =>
            {
                newOrder.Items.Add(new OrderItem()
                {
                    Price = item.Price,
                    ProductId = item.ProductId,
                    BuyerCount = item.Count 
                });
            });

            await _appDbContext.AddAsync(newOrder);
            await _appDbContext.SaveChangesAsync();
            var orderCreatedRequestEvent = new OrderCreatedRequestEvent
            {
                BuyerId = orderCreateDto.BuyerId,
                OrderId = newOrder.Id,
                Payment = new PaymentMessage
                {
                    CardName = orderCreateDto.payment.CardName,
                    CardNumber = orderCreateDto.payment.CardNumber,
                    CVV = orderCreateDto.payment.CVV,
                    Expiration = orderCreateDto.payment.Expiration,
                    TotalPrice = orderCreateDto.orderItems.Sum(x=>x.Price + x.Count),
                }
            };

            orderCreateDto.orderItems.ForEach(x =>
            {
                orderCreatedRequestEvent.OrderItems.Add(new OrderItemMessage
                {
                    Count = x.Count,
                    ProductId = x.ProductId,
                });
            });
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.ORDER_SAGA}"));

            await sendEndpoint.Send<IOrderCreatedRequestEvent>(orderCreatedRequestEvent);

        }
    }
}
