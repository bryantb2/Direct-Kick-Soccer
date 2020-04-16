using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class QuestionMessageRepo : IQuestionMsgRepo
    {
        private  ApplicationDbContext context;

        public QuestionMessageRepo(ApplicationDbContext c) => this.context = c ?? throw new ArgumentNullException(nameof(c));

        public List<QuestionMessage> GetQuestionMessages
        {
            get
            {
                return this.context.QuestionMessages.ToList();
            }
        }

        // methods
        public async Task AddQuestionMessage(QuestionMessage newMsg)
        {
            this.context.QuestionMessages.Add(newMsg);
            await this.context.SaveChangesAsync();

        }

        public async Task<QuestionMessage> GetQuestionMessageById(int questionMessageId)
        {
            return this.context.QuestionMessages
                .ToList().Find(id => id.QuestionMessageID == questionMessageId);
        }

        public async Task UpdateQuestionMessage(QuestionMessage updatedMsg)
        {
            this.context.QuestionMessages.Update(updatedMsg);
            await this.context.SaveChangesAsync();

        }

        public async Task<QuestionMessage> RemoveQuestionMessage(int questionMessageId)
        {

            var foundQuestionMessage = this.context.QuestionMessages.ToList()
                .Find(questionMessage => questionMessage.QuestionMessageID == questionMessageId);
            this.context.QuestionMessages.Remove(foundQuestionMessage);
            await this.context.SaveChangesAsync();
            return foundQuestionMessage;


        }
    }
}
