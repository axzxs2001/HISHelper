using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.ProductRelease
{
    public partial class BugQuestionTable
    {
        public int ID { get; set; }
        public string BugName { get; set; }
        public string BugDescription { get; set; }
        public int ImplementerID { get; set; }
        public string UserName { get; set; }
        public string AskQuestions { get; set; }
        public string State { get; set; }
        public DateTime QuestionTime { get; set; }
    }
}
