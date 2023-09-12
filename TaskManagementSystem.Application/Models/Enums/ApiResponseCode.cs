using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Models.Enums
{
    public enum ApiResponseCode
    {
        EXCEPTION = 1,

        [Description("Unauthorized Access")]
        UNAUTHORIZED,

        [Description("Not Found")]
        NOT_FOUND,

        [Description("Invalid Request")]
        INVALID_REQUEST,

        [Description("Server error occured, please try again.")]
        ERROR,

        [Description("FAILED")]
        FAILED, 

        [Description("SUCCESS")]
        OK,

        [Description("CREATED")]
        CREATED,

        [Description("NO CONTENT")]
        NO_CONTENT


    }
}
