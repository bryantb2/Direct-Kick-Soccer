using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace dropShippingApp.HelperUtilities
{
    public class MyWordFilter
    {
        private List<string> badWords = new List<string>();
        
        public MyWordFilter()
        {
 
            using (StreamReader reader = new StreamReader("badwords.json"))
            {
                string result = reader.ReadToEnd();
                badWords = JsonConvert.DeserializeObject<List<string>>(result);
            }
        }
        public bool BadWords(string sentence)
        {
            sentence = sentence.ToLower();
            return badWords.Exists(word => sentence.Contains(word));
        }
    }
}
