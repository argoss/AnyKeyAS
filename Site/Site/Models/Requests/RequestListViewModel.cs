﻿using System;
using System.ComponentModel.DataAnnotations;
using Servicing.Data;
using Servicing.Requests;
using Site.Models.Clients;

namespace Site.Models.Requests
{
    public class RequestListViewModel
    {
        public RequestListViewModel()
        {
            List = new RequestViewModel[0];
        }

        public RequestViewModel[] List { get; set; }
    }

    public class RequestViewModel
    {
        public int Id { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public string Status { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }
    }
}