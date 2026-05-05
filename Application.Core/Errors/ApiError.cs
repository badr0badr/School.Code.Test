﻿using System;
using System.Linq;

namespace Application.Core.Errors
{
    public class ApiError
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiError(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefultMessage(statusCode);
        }
        private string GetDefultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "An error occurred"
            };
        }
    }
}