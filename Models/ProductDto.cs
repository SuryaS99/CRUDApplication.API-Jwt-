﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApplication.API.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }


        public string Name { get; set; }

        public string CategoryName { get; set; }
    }
}
