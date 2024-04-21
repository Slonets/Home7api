﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CreateUnitDto
    {       
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
