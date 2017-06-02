using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Easom.Message
{
    [XmlRoot("returnsms")]
    public class Returnsms
    {
        [XmlElement("returnstatus")]
        public string Returnstatus { get; set; }

        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("remainpoint")]
        public int Remainpoint { get; set; }

        [XmlElement("taskID")]
        public int TaskID { get; set; }

        [XmlElement("successCounts")]
        public int SuccessCounts { get; set; }

    }
}
