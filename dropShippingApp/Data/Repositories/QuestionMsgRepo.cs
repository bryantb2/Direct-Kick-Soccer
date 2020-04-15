using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public class QuestionMsgRepo : IQuestionMsgRepo
    {

        private List<QuestionMessage> questionMessages = new List<QuestionMessage>();
        public List<QuestionMessage> QuestionMessages { get { return questionMessages; } }

        // methods
        public async Task AddQuestionMessage(QuestionMessage newMsg)
        {
            QuestionMessages.Add(newMsg);
        }

        public async Task<QuestionMessage> GetQuestionMessageById(int questionMessageId)
        {
            QuestionMessage foundMsg = QuestionMessages.Find(product => product.QuestionMessageID == questionMessageId);
            if (foundMsg != null)
            {
                return await Task.FromResult<QuestionMessage>(foundMsg);
            }
            // Return the Roster product as null if not found
            return await Task.FromResult<QuestionMessage>(null);
        }

        public async Task UpdateQuestionMessage(QuestionMessage updatedMsg)
        {
            QuestionMessage oldMsg = QuestionMessages.Find(cp => cp.QuestionMessageID == updatedMsg.QuestionMessageID);
            QuestionMessages.Remove(oldMsg);
            QuestionMessages.Add(updatedMsg);
        }

        public async Task RemoveQuestionMessage(QuestionMessage message)
        {
            if (message != null)
            {
                QuestionMessages.Remove(message);
            }
        }
    }
}
