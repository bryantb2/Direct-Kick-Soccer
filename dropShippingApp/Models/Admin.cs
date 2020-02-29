using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Admin : IdentityUser
    {
        // private fields
        private List<QuestionResponse> questionResponses = new List<QuestionResponse>();
        private List<CustomProduct> createdCustomProducts = new List<CustomProduct>();
        private List<RosterProduct> createdRosterProducts = new List<RosterProduct>();
        private List<TeamCreationRequest> approvedTeamRequests = new List<TeamCreationRequest>();
        private List<ActivityLog> activityRecord = new List<ActivityLog>();
        private List<Order> ordersFulfilled = new List<Order>();

        // public properties
        public List<QuestionResponse> AnsweredQuestions { get { return questionResponses; } }
        public List<CustomProduct> CustomProducts { get { return createdCustomProducts; } }
        public List<RosterProduct> RosterProducts { get { return createdRosterProducts; } }
        public List<TeamCreationRequest> TeamRequests { get { return approvedTeamRequests; } }
        public List<ActivityLog> ActivityLogs { get { return activityRecord; } }
        public List<Order> ApprovedOrders { get { return ordersFulfilled; } }

        // methods
        public void AddQuestion(QuestionResponse question) => questionResponses.Add(question);
        public void AddCustomProduct(CustomProduct product) => createdCustomProducts.Add(product);
        public void AddRosterProduct(RosterProduct product) => createdRosterProducts.Add(product);
        public void AddApprovedRequest(TeamCreationRequest request) => approvedTeamRequests.Add(request);
        public void AddActivityRecord(ActivityLog log) => activityRecord.Add(log);
        public void AddApprovedOrder(Order order) => ordersFulfilled.Add(order);

        public QuestionResponse RemoveQuestionResponse(QuestionResponse question)
        {
            QuestionResponse removedQuestion = null;
            foreach(QuestionResponse qr in questionResponses)
            {
                if(qr.QuestionResponseID == question.QuestionResponseID)
                {
                    removedQuestion = qr;
                    questionResponses.Remove(qr);
                    return removedQuestion;
                }
            }
            return removedQuestion;
        }

        public CustomProduct RemoveCustomProducts(CustomProduct product)
        {
            CustomProduct removedProduct = null;
            foreach (CustomProduct p in createdCustomProducts)
            {
                if(p.CustomProductID == product.CustomProductID)
                {
                    removedProduct = p;
                    createdCustomProducts.Remove(p);
                    return removedProduct;
                }
            }
            return removedProduct;
        }

        public RosterProduct RemoveRosterProduct(RosterProduct product)
        {
            RosterProduct removedProduct = null;
            foreach (RosterProduct p in createdRosterProducts)
            {
                if(p.RosterProductID == product.RosterProductID)
                {
                    removedProduct = p;
                    createdRosterProducts.Remove(p);
                    return removedProduct;
                }
            }
            return removedProduct;
        }

        public TeamCreationRequest RemoveApprovedRequest(TeamCreationRequest request)
        {
            TeamCreationRequest removedRequest = null;
            foreach (TeamCreationRequest r in approvedTeamRequests)
            {
                if(r.TeamCreationRequestID == request.TeamCreationRequestID)
                {
                    removedRequest = r;
                    approvedTeamRequests.Remove(r);
                    return removedRequest;
                }
            }
            return removedRequest;
        }

        public ActivityLog RemoveActivityLog(ActivityLog log)
        {
            ActivityLog removedLog = null;
            foreach (ActivityLog l in activityRecord)
            {
                if(l.ActivityLogID == log.ActivityLogID)
                {
                    removedLog = l;
                    activityRecord.Remove(l);
                    return removedLog;
                }
            }
            return removedLog;
        }

        public Order RemoveApprovedOrder(Order order)
        {
            Order removedOrder = null;
            foreach (Order o in ordersFulfilled)
            {
                if(order.OrderID == o.OrderID)
                {
                    removedOrder = o;
                    ordersFulfilled.Remove(o);
                    return removedOrder;
                }
            }
            return removedOrder;
        }
    }
}
