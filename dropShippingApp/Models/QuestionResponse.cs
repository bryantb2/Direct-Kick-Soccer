using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class QuestionResponse
    {
        public int QuestionResponseID { get; set; }
        public QuestionMessage ParentMessage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
