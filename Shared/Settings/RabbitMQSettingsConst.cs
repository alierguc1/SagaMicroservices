using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Settings
{
    public class RabbitMQSettingsConst
    {
        public const string STOCK_ORDER_CREATED_EVENT_QUEUE_NAME = "stock-order-created-queue";
        public const string STOCK_RESERVED_EVENT_QUEUE_NAME = "stock-reserved-queue";
        public const string STOCK_NOT_RESERVED_EVENT_QUEUE_NAME = "stock-not-reserved-queue";
        public const string ORDER_PAYMENT_COMPLETED_QUEUE_NAME = "order-payment-completed-queue";
        public const string STOCK_PAYMENT_FAILED_EVENT_QUEUE_NAME = "stock-payment-failed-queue";
        public const string ORDER_PAYMENT_FAILED_EVENT_QUEUE_NAME = "order-payment-failed-queue";
    }
}
