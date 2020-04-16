using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface IActivityLogRepo
    {
        List<ActivityLog> GetActivityLogs { get; }
        // CRUD operations for ActivityLogs
        Task AddActivityLog(ActivityLog newActivityLog);
        Task<ActivityLog> GetActivityLogById(int activityLogID);
        Task UpdateActivityLog(ActivityLog updatedActivityLog);
        Task<ActivityLog> RemoveActivityLog(int activityLogID);
    }
}
