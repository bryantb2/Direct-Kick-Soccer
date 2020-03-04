using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class AppUser : IdentityUser
    {
        // private fields
        private List<QuestionMessage> askedQuestions = new List<QuestionMessage>();
        private List<Invoice> invoiceHistory = new List<Invoice>();
        private List<TeamCreationRequest> creationReqHistory = new List<TeamCreationRequest>();
        private List<CustomProduct> createdCustomProducts = new List<CustomProduct>();
        private List<ActivityLog> activityLog = new List<ActivityLog>();

        // universal properties
        public DateTime DateJoined { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        // user properties
        public List<QuestionMessage> AskedQuestions { get { return this.askedQuestions; } }
        public List<Invoice> InvoiceHistory { get { return this.invoiceHistory; } }
        public List<TeamCreationRequest> CreationRequestHistory { get { return this.creationReqHistory; } }
        public bool hasApprovedRequest { get; set; }

        // team manager properties
        public Team ManagedTeam { get; set; }
        public List<CustomProduct> CreatedCustomProducts { get { return createdCustomProducts; } }
        public List<ActivityLog> ActivityLog { get { return activityLog; } }

        // question methods
        public void AddQuestion(QuestionMessage question) => askedQuestions.Add(question);
        public QuestionMessage RemoveQuestion(QuestionMessage question)
        {
            QuestionMessage removedQuestion = null;
            foreach(QuestionMessage q in askedQuestions)
            {
                if(q.QuestionMessageID == question.QuestionMessageID)
                {
                    removedQuestion = q;
                    askedQuestions.Remove(q);
                    return removedQuestion;
                }
            }
            return removedQuestion;
        }

        // custom product methods
        public void AddCustomProduct(CustomProduct product) => createdCustomProducts.Add(product);
        public CustomProduct RemoveCustomProduct(CustomProduct product)
        {
            CustomProduct removedProduct = null;
            foreach (CustomProduct p in createdCustomProducts)
            {
                if (p.CustomProductID == product.CustomProductID)
                {
                    removedProduct = p;
                    createdCustomProducts.Remove(p);
                    return removedProduct;
                }
            }
            return removedProduct;
        }

        // invoicing methods
        public void AddInvoice(Invoice inv) => invoiceHistory.Add(inv);
        public Invoice RemoveInvoice(Invoice inv)
        {
            Invoice removedInv = null;
            foreach(Invoice i in invoiceHistory)
            {
                if (inv.InvoiceID == i.InvoiceID)
                {
                    removedInv = i;
                    invoiceHistory.Remove(i);
                    return removedInv;
                }
            }
            return removedInv;
        }

        // creation request methods
        public void AddCreationRequest(TeamCreationRequest request) => creationReqHistory.Add(request);
        public TeamCreationRequest RemoveCreationRequest(TeamCreationRequest creationReq)
        {
            TeamCreationRequest removedRequest = null;
            foreach (TeamCreationRequest req in creationReqHistory)
            {
                if(req.TeamCreationRequestID == creationReq.TeamCreationRequestID)
                {
                    removedRequest = req;
                    creationReqHistory.Remove(req);
                    return removedRequest;
                }
            }
            return removedRequest;
        }
        public decimal CalculateTotalSpending()
        {
            // loop through invoice history
            decimal totalPrice = 0.00m;
            foreach(Invoice inv in invoiceHistory)
            {
                totalPrice += inv.CalculateGrandTotal();
            }
            return totalPrice;
        }

        // activity log methods
        public void AddLog(ActivityLog log) => activityLog.Add(log);
        public ActivityLog RemoveLog(ActivityLog log)
        {
            ActivityLog removedLog = null;
            foreach (ActivityLog l in activityLog)
            {
                if (l.ActivityLogID == log.ActivityLogID)
                {
                    removedLog = l;
                    activityLog.Remove(l);
                    return removedLog;
                }
            }
            return removedLog;
        }
    }
}
