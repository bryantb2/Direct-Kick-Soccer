using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface IQuestionMsgRepo
    {
       
        List<QuestionMessage> GetQuestionMessages {get;}
        // CRUD operations for QuestionMessages
        Task AddQuestionMessage(QuestionMessage newMsg);
        Task<QuestionMessage> GetQuestionMessageById(int questionMessageId);
        Task UpdateQuestionMessage(QuestionMessage updatedMsg);
        Task<QuestionMessage> RemoveQuestionMessage(int questionMessageId);
    }
}
