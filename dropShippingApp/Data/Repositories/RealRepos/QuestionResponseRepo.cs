using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public class QuestionResponseRepo : IQuestionResponseRepo
    {

        private ApplicationDbContext context;

        public QuestionResponseRepo(ApplicationDbContext c) => this.context = c ?? throw new ArgumentNullException(nameof(c));

        public List<QuestionResponse> GetQuestionResponses
        {
            get
            {
                return this.context.QuestionResponses.ToList();
            }
        }



        // methods
        public async Task AddQuestionResponse(QuestionResponse newMsg)
        {
            this.context.QuestionResponses.Add(newMsg);
            await this.context.SaveChangesAsync();

        }

        public async Task<QuestionResponse> GetQuestionResponseById(int questionResponseId)
        {
            return this.context.QuestionResponses
                .Include(q => q.ParentMessage)
                .ThenInclude(r => r.QuestionTitle)
                .Include(q => q.ParentMessage)
                .ThenInclude(r => r.QuestionBody)
                .Include(q => q.ParentMessage)
                .ThenInclude(r => r.TimeStamp)
                .Include(q => q.ParentMessage)
                .ThenInclude(r => r.IsResolved)
                .Include(q => q.TimeStamp)
                .ToList().Find(id => id.QuestionResponseID == questionResponseId);
        }

        public async Task UpdateQuestionResponse(QuestionResponse updatedMsg)
        {
            this.context.QuestionResponses.Update(updatedMsg);
            await this.context.SaveChangesAsync();

        }

        public async Task<QuestionResponse> RemoveQuestionResponse(int questionResponseId)
        {

            var foundQuestionResponse = this.context.QuestionResponses.ToList()
                .Find(questionResponse => questionResponse.QuestionResponseID == questionResponseId);
            this.context.QuestionResponses.Remove(foundQuestionResponse);
            await this.context.SaveChangesAsync();
            return foundQuestionResponse;


        }
    }
}
