﻿using System;
using System.Linq;

namespace Application.Core.Errors
{
    public class ApiExceptionError : ApiError
    {
        public string? Details { get; set; }
        public ApiExceptionError(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
        }
    }
}