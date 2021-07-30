﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiInvoices.Messaging
{
    public class RabbitMQConfiguration
    {
        public string HostName { set; get; }
        public string QueueName { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}
