using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryDistibution.Common.Response
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public EnumApiResponseStatus Status { get; set; }
        public string Message { get; set; }
        //public string TechnicalMessage { get; set; }
        public string NewId { get; set; }

    }
}
