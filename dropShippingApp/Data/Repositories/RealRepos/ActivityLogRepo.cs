using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public class ActivityLogRepo : IActivityLogRepo
    {
        private ApplicationDbContext context;

        public ActivityLogRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<ActivityLog> GetActivityLogs
        {
            get
            {
                return this.context.ActivityLogs.ToList();
            }
        }



        // methods
        public async Task AddActivityLog(ActivityLog newActivityLog)
        {
            this.context.ActivityLogs.Add(newActivityLog);
            await this.context.SaveChangesAsync();

        }

        public async Task<ActivityLog> GetActivityLogById(int ActivityLogId)
        {
            return this.context.ActivityLogs
                .Include(p => p.Title)
                .Include(p => p.ChangeDescription)
                .Include(p => p.TimeStamp)
                .ToList().Find(id => id.ActivityLogID == ActivityLogId);
        }

        public async Task UpdateActivityLog(ActivityLog updatedActivityLog)
        {
            this.context.ActivityLogs.Update(updatedActivityLog);
            await this.context.SaveChangesAsync();

        }

        public async Task<ActivityLog> RemoveActivityLog(int ActivityLogID)
        {

            var foundActivityLog = this.context.ActivityLogs.ToList()
                .Find(ActivityLog => ActivityLog.ActivityLogID == ActivityLogID);
            this.context.ActivityLogs.Remove(foundActivityLog);
            await this.context.SaveChangesAsync();
            return foundActivityLog;


        }

    }
}
