using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChecker
{
    public class Applications : List<ApplicationModel>
    {

    }
    public class ApplicationModel
    {
        public string name { get; set; }
        public string process { get; set; }
        public State State { get; set; }
    }
    public enum State
    {
        Running = 0,
        Stopped = 1
    }
}
