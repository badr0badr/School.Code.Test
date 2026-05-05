﻿using System;
using System.Linq;

namespace Application.Core.Errors
{
    public class ApiValidationError : ApiError
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public ApiValidationError(IEnumerable<string> error) : base(400)
        {
            Errors = error;
        }
    }
}