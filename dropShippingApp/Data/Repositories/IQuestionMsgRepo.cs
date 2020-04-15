using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface IQuestionMsgRepo
    {
        List<QuestionMessage> QuestionMessages { get; }
        // CRUD operations for QuestionMessages
        Task AddQuestionMessage(QuestionMessage newMsg);
        Task<QuestionMessage> GetQuestionMessageById(int questionMessageId);
        Task UpdateQuestionMessage(QuestionMessage updatedMsg);
        Task RemoveQuestionMessage(QuestionMessage message);
    }
}
