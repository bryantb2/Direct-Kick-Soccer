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

        private List<QuestionResponse> questionResponses = new List<QuestionResponse>();
        public List<QuestionResponse> QuestionResponses { get { return questionResponses; } }

        // methods
        public async Task AddQuestionResponse(QuestionResponse newResponse)
        {
            QuestionResponses.Add(newResponse);
        }

        public async Task<QuestionResponse> GetQuestionResponseById(int questionResponseId)
        {
            QuestionResponse foundResponse = QuestionResponses.Find(product => product.QuestionResponseID == questionResponseId);
            if (foundResponse != null)
            {
                return await Task.FromResult<QuestionResponse>(foundResponse);
            }
            // Return the Roster product as null if not found
            return await Task.FromResult<QuestionResponse>(null);
        }

        public async Task UpdateQuestionResponse(QuestionResponse updatedResponse)
        {
            QuestionResponse oldResponse = QuestionResponses.Find(cp => cp.QuestionResponseID == updatedResponse.QuestionResponseID);
            QuestionResponses.Remove(oldResponse);
            QuestionResponses.Add(updatedResponse);
        }

        public async Task RemoveQuestionResponse(QuestionResponse response)
        {
            if (response != null)
            {
                QuestionResponses.Remove(response);
            }
        }
    }
}
