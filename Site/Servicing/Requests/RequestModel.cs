﻿using System;

namespace Servicing.Requests
{
    public class RequestModel
    {
        public int Id { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public RequestStatus Status { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }
    }
}
