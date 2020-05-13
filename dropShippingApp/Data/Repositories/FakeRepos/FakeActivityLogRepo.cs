using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
    public class FakeActivityLogRepo : IActivityLogRepo
    {
        private List<ActivityLog> logs = new List<ActivityLog>();

        public List<ActivityLog> GetActivityLogs
        {
            get { return logs; }
        }

        public async Task AddActivityLog(ActivityLog newActivityLog)
        {
            logs.Add(newActivityLog);
        }

        public async Task<ActivityLog> GetActivityLogById(int activityLogID)
        {
            ActivityLog log = (from l in logs
                               where l.ActivityLogID == activityLogID
                               select l).FirstOrDefault();
            if (log != null)
            {
               
                return await Task.FromResult<ActivityLog>(log);
            }
            else
                return await Task.FromResult<ActivityLog>(null);
        }

        public async Task<ActivityLog> RemoveActivityLog(int activityLogID)
        {
            ActivityLog log = (from l in logs
                               where l.ActivityLogID == activityLogID
                               select l).FirstOrDefault();
            if (log != null)
            {
                logs.Remove(log);
                return await Task.FromResult<ActivityLog>(log);
            }
            else
                return await Task.FromResult<ActivityLog>(null);
        }

        public async Task UpdateActivityLog(ActivityLog updatedActivityLog)
        {
            ActivityLog log = (from l in logs
                               where l.ActivityLogID == updatedActivityLog.ActivityLogID
                               select l).FirstOrDefault();
            logs.Remove(log);
            logs.Add(log);
        }
    }
}
