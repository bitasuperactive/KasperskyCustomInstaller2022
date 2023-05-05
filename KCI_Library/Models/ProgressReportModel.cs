using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library.Models
{
    public class ProgressReportModel
    {
        public int Percentage { get; set; }
        public string Description { get; set; }

        public ProgressReportModel(int percentage, string description)
        {
            Percentage = percentage;
            Description = description;
        }
    }
}
