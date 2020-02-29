using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class AppUser
    {
        // private fields
        private List<QuestionMessage> askedQuestions = new List<QuestionMessage>();
        private List<Invoice> invoiceHistory = new List<Invoice>();
        private List<TeamCreationRequest> creationReqHistory = new List<TeamCreationRequest>();
        
        // public properties
        public int UserID { get; set; }
        public DateTime DateJoined { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public List<QuestionMessage> AskedQuestions { get { return this.askedQuestions; } }
        public List<Invoice> InvoiceHistory { get { return this.invoiceHistory; } }
        public List<TeamCreationRequest> CreationRequestHistory { get { return this.creationReqHistory; }  }
        public bool hasApprovedRequest { get; set; }

        // methods
        public void AddQuestion(QuestionMessage question) => askedQuestions.Add(question);
        public QuestionMessage RemoveQuestion(QuestionMessage question)
        {
            QuestionMessage removedQuestion = null;
            foreach(QuestionMessage q in askedQuestions)
            {
                if(q.)
            }
        }
        public void AddInvoice(Invoice inv) => invoiceHistory.Add(inv);
        public Invoice RemoveInvoice(Invoice inv)
        {
            Invoice removedInv = null;
            foreach(Invoice inv in invoiceHistory)
            {

            }
        }
        public void AddCreationRequest(TeamCreationRequest request) => creationReqHistory.Add(request);
        public TeamCreationRequest RemoveCreationRequest(TeamCreationRequest creationReq)
        {
            Invoice removedRequest = null;
            foreach (TeamCreationRequest req in creationReqHistory)
            {

            }
        }
        public decimal CalculateTotalSpending()
        {

        }
    }
}
