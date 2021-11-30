﻿using System.Collections.Generic;

namespace ECommerce.Shared.Dtos
{
    public class ErrorDto
    {
        public List<string> Errors { get; set; }

        public ErrorDto()
        {
            Errors = new List<string>();
        }
    }
}
