using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface IQuestionResponseRepo
    {
        List<QuestionResponse> GetQuestionResponses { get; }
        // CRUD operations for QuestionResponses
        Task AddQuestionResponse(QuestionResponse newResponse);
        Task<QuestionResponse> GetQuestionResponseById(int questionResponseId);
        Task UpdateQuestionResponse(QuestionResponse updatedResponse);
        Task<QuestionResponse> RemoveQuestionResponse(int messageID);
    }
}
