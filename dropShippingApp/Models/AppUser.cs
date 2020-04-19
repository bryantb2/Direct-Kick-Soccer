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
        public TeamCreationRequest RemoveApprovedRequest(int requestId)
        {
            var foundRequest = adminApprovedTeamRequests.Find(request => request.TeamCreationRequestID == requestId);
            adminApprovedTeamRequests.Remove(foundRequest);
            return foundRequest;
        }

        // question response methods
        public void AddQuestionResponse(QuestionResponse question) => adminQuestionResponses.Add(question);
        public QuestionResponse RemoveQuestionResponse(int questionId)
        {
            var foundReponse = adminQuestionResponses.Find(response => response.QuestionResponseID == questionId);
            adminQuestionResponses.Remove(foundReponse);
            return foundReponse;
        }

        // approved order methods
        public void AddApprovedOrder(Order order) => adminOrdersFulfilled.Add(order);
        public Order RemoveApprovedOrder(int orderId)
        {
            var foundOrder = adminOrdersFulfilled.Find(order => order.OrderID == orderId);
            adminOrdersFulfilled.Remove(foundOrder);
            return foundOrder;
        }

        // question methods
        public void AddQuestion(QuestionMessage question) => askedQuestions.Add(question);
        public QuestionMessage RemoveQuestion(int questionId)
        {
            var foundMsg = askedQuestions.Find(question => question.QuestionMessageID == questionId);
            askedQuestions.Remove(foundMsg);
            return foundMsg;
        }

        // roster product methods
        public void AddRosterProduct(RosterProduct product) => adminCreatedRosterProducts.Add(product);
        public RosterProduct RemoveRosterProduct(int productId)
        {
            var foundProduct = adminCreatedRosterProducts.Find(product => product.RosterProductID == productId);
            adminCreatedRosterProducts.Remove(foundProduct);
            return foundProduct;
        }

        // custom product methods
        public void AddCustomProduct(CustomProduct product) => createdCustomProducts.Add(product);
        public CustomProduct RemoveCustomProduct(int productId)
        {
            var foundProduct = createdCustomProducts.Find(product => product.CustomProductID == productId);
            createdCustomProducts.Remove(foundProduct);
            return foundProduct;
        }

        // creation request methods
        public void AddCreationRequest(TeamCreationRequest request) => userCreationReqHistory.Add(request);
        public TeamCreationRequest RemoveCreationRequest(int creationReqId)
        {
            var foundRequest = userCreationReqHistory.Find(product => product.TeamCreationRequestID == creationReqId);
            userCreationReqHistory.Remove(foundRequest);
            return foundRequest;
        }

        // activity log methods
        public void AddLog(ActivityLog log) => this.activityLog.Add(log);
        public ActivityLog RemoveLog(int logId)
        {
            var foundLog = activityLog.Find(log => log.ActivityLogID == logId);
            activityLog.Remove(foundLog);
            return foundLog;
        }
    }
}
