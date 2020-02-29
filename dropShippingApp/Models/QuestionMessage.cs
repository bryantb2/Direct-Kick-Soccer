using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class QuestionMessage
    {
        public int QuestionMessageID { get; set; }
        public String QuestionTitle { get; set; }
        public String QuestionBody { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsResolved { get; set; }
    }
}
