using System;
using System.Linq;

namespace Application.Core.Views.Other
{
    public class RequestView<TEntity>
    {
        public RequestView(string token, TEntity request)
        {
            Token = token;
            Request = request;
        }
        public string Token { get; set; }
        public TEntity Request { get; set; }
    }
}
