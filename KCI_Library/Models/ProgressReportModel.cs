using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library.Models
{
    public class ProgressReportModel
    {
        public string ProgressMessage { get; set; }
        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                LastProgressValue = _progressValue;
                _progressValue = value;
            }
        }
        public int LastProgressValue { get; private set; }
        private int _progressValue = 0;

        public ProgressReportModel(string progressMessage = "", int progressValue = 0, int lastProgressValue = 0)
        {
            ProgressMessage = progressMessage;
            ProgressValue = progressValue;
            LastProgressValue = lastProgressValue;
        }

        public ProgressReportModel Set(string progressMessage, int progressValue)
        {
            ProgressMessage = progressMessage;
            ProgressValue = progressValue;
            return this;
        }
    }
}
