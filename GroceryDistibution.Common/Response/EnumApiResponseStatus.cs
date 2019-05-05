using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryDistibution.Common.Response
{
    public enum EnumApiResponseStatus
    {
        Error,          // 0
        Success,        // 1
        Unauthorized,   // 2
        Info,           // 3
        BadRequest,     // 4    if token is not valid   
        InlineError     // 5
    }
}
