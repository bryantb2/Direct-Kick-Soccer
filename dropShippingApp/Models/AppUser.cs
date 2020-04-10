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
        private List<TeamCreationRequest> userCreationReqHistory = new List<TeamCreationRequest>();
        private List<Order> userOrderHistory = new List<Order>();
        private List<Order> adminOrdersFulfilled = new List<Order>();
        private List<TeamCreationRequest> adminApprovedTeamRequests = new List<TeamCreationRequest>();
        private List<QuestionResponse> adminQuestionResponses = new List<QuestionResponse>();
        private List<CustomProduct> createdCustomProducts = new List<CustomProduct>();
        private List<RosterProduct> adminCreatedRosterProducts = new List<RosterProduct>();
        private List<QuestionMessage> askedQuestions = new List<QuestionMessage>();
        private List<ActivityLog> activityLog = new List<ActivityLog>();

        // universal properties
        public Int64 DateJoined { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        // user properties
        public List<TeamCreationRequest> CreationRequestHistory { get { return this.userCreationReqHistory; } }
        public bool hasApprovedRequest { get; set; }
        public List<Order> UserOrderHistory { get { return this.userOrderHistory; } }

        // team manager properties
        public Team ManagedTeam { get; set; }
        public List<CustomProduct> CreatedCustomProducts { get { return createdCustomProducts; } }

        // team manager AND user properties
        public List<QuestionMessage> AskedQuestions { get { return this.askedQuestions; } }
        public Cart Cart { get; set; }

        // admin properties
        public List<QuestionResponse> AnsweredQuestions { get { return adminQuestionResponses; } }
        public List<RosterProduct> RosterProducts { get { return adminCreatedRosterProducts; } }
        public List<TeamCreationRequest> ApprovedTeamRequests { get { return adminApprovedTeamRequests; } }
        public List<Order> ApprovedOrders { get { return adminOrdersFulfilled; } }

        // team manager AND admin properties
        public List<ActivityLog> ActivityLog { get { return activityLog; } }


        // approved request methods
        public void AddApprovedRequest(TeamCreationRequest request) => adminApprovedTeamRequests.Add(request);
        public TeamCreationRequest RemoveApprovedRequest(TeamCreationRequest request)
        {
            TeamCreationRequest removedRequest = null;
            foreach (TeamCreationRequest r in adminApprovedTeamRequests)
            {
                if (r.TeamCreationRequestID == request.TeamCreationRequestID)
                {
                    removedRequest = r;
                    adminApprovedTeamRequests.Remove(r);
                    return removedRequest;
                }
            }
            return removedRequest;
        }

        // question response methods
        public void AddQuestionResponse(QuestionResponse question) => adminQuestionResponses.Add(question);
        public QuestionResponse RemoveQuestionResponse(QuestionResponse question)
        {
            QuestionResponse removedQuestion = null;
            foreach (QuestionResponse qr in adminQuestionResponses)
            {
                if (qr.QuestionResponseID == question.QuestionResponseID)
                {
                    removedQuestion = qr;
                    adminQuestionResponses.Remove(qr);
                    return removedQuestion;
                }
            }
            return removedQuestion;
        }

        // approved order methods
        public void AddApprovedOrder(Order order) => adminOrdersFulfilled.Add(order);
        public Order RemoveApprovedOrder(Order order)
        {
            Order removedOrder = null;
            foreach (Order o in adminOrdersFulfilled)
            {
                if (order.OrderID == o.OrderID)
                {
                    removedOrder = o;
                    adminOrdersFulfilled.Remove(o);
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
        public void AddRosterProduct(RosterProduct product) => adminCreatedRosterProducts.Add(product);
        public RosterProduct RemoveRosterProduct(RosterProduct product)
        {
            RosterProduct removedProduct = null;
            foreach (RosterProduct p in adminCreatedRosterProducts)
            {
                if (p.RosterProductID == product.RosterProductID)
                {
                    removedProduct = p;
                    adminCreatedRosterProducts.Remove(p);
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
        public void AddCreationRequest(TeamCreationRequest request) => userCreationReqHistory.Add(request);
        public TeamCreationRequest RemoveCreationRequest(TeamCreationRequest creationReq)
        {
            TeamCreationRequest removedRequest = null;
            foreach (TeamCreationRequest req in userCreationReqHistory)
            {
                if(req.TeamCreationRequestID == creationReq.TeamCreationRequestID)
                {
                    removedRequest = req;
                    userCreationReqHistory.Remove(req);
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
