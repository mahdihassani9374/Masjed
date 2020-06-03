﻿using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.DTO.Project;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.Payment
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public long Price { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentType Type { get; set; }
        public int? RecordId { get; set; }
        public ProjectDto Project { get; set; }
    }
}