using dropShippingApp.Data.Repositories;
using dropShippingApp.Data.Repositories.FakeRepos;
using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ActivityLogRepoTest :IDisposable
    {
        private IActivityLogRepo logRepo;


        //setup
        public ActivityLogRepoTest()
        {
            logRepo = new FakeActivityLogRepo();
        }

        public void Dispose()
        {
            logRepo = null;
        }
        [Fact]
        public async Task AddLog()
        {
            //arrant
            ActivityLog log = new ActivityLog
            {
                ActivityLogID = 1,
                Title = "Test Log"
            };
            //act
            logRepo.AddActivityLog(log);
            List<ActivityLog> results = logRepo.GetActivityLogs;

            //assert
            Assert.Equal(log, results.Find(l => l == log));
        }
        [Fact]
        public async Task TestRemoveLog()
        {
            //arrange
            ActivityLog log = new ActivityLog
            {
                ActivityLogID = 1,
                Title = "Test Log"
            };
            await logRepo.AddActivityLog(log);

            //act
            logRepo.RemoveActivityLog(log.ActivityLogID);
            //assert
            Assert.DoesNotContain(log, logRepo.GetActivityLogs);
        }
        [Fact]
        public async Task TestUpdateLog()
        {
            ActivityLog log = new ActivityLog
            {
                ActivityLogID = 1,
                Title = "Test Log"
            };
            ActivityLog log2 = new ActivityLog
            {
                ActivityLogID = 1,
                Title = "Updated Test Log"
            };
            await logRepo.AddActivityLog(log);
            //act
            await logRepo.UpdateActivityLog(log2);

            //assert
            Assert.DoesNotContain(log, logRepo.GetActivityLogs);
            Assert.Contains(log2, logRepo.GetActivityLogs);
        }
        [Fact]
        public async Task TestFindById()
        {
            //arrage
            ActivityLog log = new ActivityLog
            {
                ActivityLogID = 1,
                Title = "Test Log"
            };
            await logRepo.AddActivityLog(log);
            //act
            ActivityLog result = await logRepo.GetActivityLogById(log.ActivityLogID);
            //assert
            Assert.Equal(log, result);
        }
   



    }
}
