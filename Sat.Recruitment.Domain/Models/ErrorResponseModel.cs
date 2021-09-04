﻿using Newtonsoft.Json;

namespace Sat.Recruitment.Domain.Models
{
    public class ErrorResponseModel
    {

        public int Code { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

        // other fields

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
