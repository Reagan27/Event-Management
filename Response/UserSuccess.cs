using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Response
{
    public class UserSuccess
    {
        public string Message { get; set; } = "";
        public int Code { get; set; }

        public UserSuccess(string message, int code)
        {
            Message = message;
            Code = code;
        }

    }
}