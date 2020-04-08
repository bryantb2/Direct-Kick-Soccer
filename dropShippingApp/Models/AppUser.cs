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
        private List<TeamCreationRequest> creationReqHistory = new List<TeamCreationRequest>();
        private List<CustomProduct> createdCustomProducts = new List<CustomProduct>();
        private List<RosterProduct> createdRosterProducts = new List<RosterProduct>();
        private List<Order> ordersFulfilled = new List<Order>();
        private List<TeamCreationRequest> approvedTeamRequests = new List<TeamCreationRequest>();
        private List<QuestionResponse> questionResponses = new List<QuestionResponse>();
        private List<QuestionMessage> askedQuestions = new List<QuestionMessage>();
        private List<ActivityLog> activityLog = new List<ActivityLog>();

        // universal properties
        public DateTime DateJoined { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        // user properties
        public List<TeamCreationRequest> CreationRequestHistory { get { return this.creationReqHistory; } }
        public bool hasApprovedRequest { get; set; }

        // team manager properties
        public Team ManagedTeam { get; set; }
        public List<CustomProduct> CreatedCustomProducts { get { return createdCustomProducts; } }

        // team manager AND user properties
        public List<QuestionMessage> AskedQuestions { get { return this.askedQuestions; } }

        // admin properties
        public List<QuestionResponse> AnsweredQuestions { get { return questionResponses; } }
        public List<RosterProduct> RosterProducts { get { return createdRosterProducts; } }
        public List<TeamCreationRequest> ApprovedTeamRequests { get { return approvedTeamRequests; } }
        public List<Order> ApprovedOrders { get { return ordersFulfilled; } }

        // team manager AND admin properties
        public List<ActivityLog> ActivityLog { get { return activityLog; } }


        // approved request methods
        public void AddApprovedRequest(TeamCreationRequest request) => approvedTeamRequests.Add(request);
        public TeamCreationRequest RemoveApprovedRequest(TeamCreationRequest request)
        {
            TeamCreationRequest removedRequest = null;
            foreach (TeamCreationRequest r in approvedTeamRequests)
            {
                if (r.TeamCreationRequestID == request.TeamCreationRequestID)
                {
                    removedRequest = r;
                    approvedTeamRequests.Remove(r);
                    return removedRequest;
                }
            }
            return removedRequest;
        }

        // question response methods
        public void AddQuestionResponse(QuestionResponse question) => questionResponses.Add(question);
        public QuestionResponse RemoveQuestionResponse(QuestionResponse question)
        {
            QuestionResponse removedQuestion = null;
            foreach (QuestionResponse qr in questionResponses)
            {
                if (qr.QuestionResponseID == question.QuestionResponseID)
                {
                    removedQuestion = qr;
                    questionResponses.Remove(qr);
                    return removedQuestion;
                }
            }
            return removedQuestion;
        }

        // approved order methods
        public void AddApprovedOrder(Order order) => ordersFulfilled.Add(order);
        public Order RemoveApprovedOrder(Order order)
        {
            Order removedOrder = null;
            foreach (Order o in ordersFulfilled)
            {
                if (order.OrderID == o.OrderID)
                {
                    removedOrder = o;
                    ordersFulfilled.Remove(o);
                    return removedOrder;
                }
            }
            return removedOrder;
        }

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

        // roster product methods
        public void AddRosterProduct(RosterProduct product) => createdRosterProducts.Add(product);
        public RosterProduct RemoveRosterProduct(RosterProduct product)
        {
            RosterProduct removedProduct = null;
            foreach (RosterProduct p in createdRosterProducts)
            {
                if (p.RosterProductID == product.RosterProductID)
                {
                    removedProduct = p;
                    createdRosterProducts.Remove(p);
                    return removedProduct;
                }
            }
            return removedProduct;
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

        // activity log methods
        public void AddLog(ActivityLog log) => this.activityLog.Add(log);
        public ActivityLog RemoveLog(ActivityLog log)
        {
            ActivityLog removedLog = null;
            foreach (ActivityLog l in this.activityLog)
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
