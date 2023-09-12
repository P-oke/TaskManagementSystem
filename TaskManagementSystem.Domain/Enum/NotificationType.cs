using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Enum
{
    public enum NotificationType
    {
        [Description("TASK DUE DATE")]
        Task_Due_Date,

        [Description("TASK STATUS DATE")]
        Task_Status_Update,

        [Description("ASSIGNED A NEW TASK")]
        New_Task


    }
}
