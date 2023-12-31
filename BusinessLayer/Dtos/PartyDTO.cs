﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class PartyDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PartyIdentifier { get; set; }
        public int Students { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
