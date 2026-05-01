using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHook.Assessment.Application.Models
{
    public class ServerResponse<T> : BasicResponse
    {
        public ServerResponse(bool success = false)
        {
            IsSuccessful = success;
        }
        public T Data { get; set; }
        public string SuccessMessage { get; set; }
    }
}
