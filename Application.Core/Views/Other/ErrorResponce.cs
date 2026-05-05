using System;
using System.Linq;

namespace Application.Core.Views.Other
{
    public class ErrorResponce
    {
        public ErrorResponce(int status, string message, string data = "")
        {
            Status = status;
            Message = message;
            Data = data;
        }
        public int Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
    public class AdvErrorResponce<T>
    {
        public AdvErrorResponce(int status, string message, T? data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
        public int Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
