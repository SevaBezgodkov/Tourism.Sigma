using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IPublisherServiceBus
    {
        void PublishMessage<T>(T model, string exchangeName, string queueName, string routingKey);
    }
}
