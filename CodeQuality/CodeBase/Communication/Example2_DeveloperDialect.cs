using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaughingMonkey.CodeQuality.Communication
{
    /// <summary>
    /// This is an abstraction of a nodejs based sensor system I built
    /// to stream EEG data over UDP sockets in a previous life, combined
    /// with an adapter-based approach to http packet building I've used
    /// at DocuSign.  This is much, much simpler than what I was doing 
    /// (in the latter case, supporting multi-part binary packet building, and
    /// in the former, managing the udp connections).
    /// 
    /// However, I used a similar strategy for both, and this is the high-level
    /// of what I did.
    /// </summary>
    public class Example2_DeveloperDialect
    {
        public class FirstDialect_SensorXmlPacket
        {
            private List<string> Headers { get; set; }
            private string Payload { get; set; }

            public FirstDialect_SensorXmlPacket()
            {
                Headers = new List<string>();
            }

            public void AddHeader(string key,string value)
            {
                Headers.Add(key + "\t" + value);
            }

            public void AddPayload(string payload)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<data>");
                sb.Append(payload);
                sb.Append("</data>");
                this.Payload = sb.ToString();
            }

            public string RenderPacket()
            {
                StringBuilder sb = new StringBuilder();
                foreach(String s in Headers)
                {
                    sb.Append(s);
                }
                sb.Append(this.Payload);
                return sb.ToString();
            }
        }

        /// <summary>
        /// This is inspired by Java development I've been getting into recently.
        /// Notice the variable naming conventions here, and the way I do the
        /// setter for the member variables.  Very java-like.  The big run-time
        /// difference between this one and the one above is that this one operates
        /// more like a state machine (first we're in header mode, then we're in 
        /// render mode).
        /// </summary>
        /// <remarks>
        /// I prefer the above approach, because I have the freedom to add to the 
        /// packet at any point.  I can even write/re-write the packet if I want.
        /// However, there's nothing wrong with having the object below turn immutable
        /// after we configure it.  In replay scenarios,this might be desirable, because
        /// then we have a guarantee that the packet didn't change from when it was
        /// originally built.
        /// </remarks>
        public class SecondDialect_SensorXmlPacket
        {
            private const string HEADER_FORMAT = "{0}\t{1}";
            private const string PAYLOAD_FORMAT = "<data>{0}</data>";
            private bool readyForPayload = false;
            private bool readyForRender = false;
            StringBuilder packet = new StringBuilder();

            public bool addPayload(string payload)
            {
                if (readyForPayload && !readyForRender)
                {
                    packet.AppendFormat(PAYLOAD_FORMAT, payload);
                    return true;
                }
                return false;
            }

            public bool addHeader(string key, string value)
            {
                if (!readyForPayload && !readyForRender)
                {
                    packet.AppendFormat(HEADER_FORMAT, key, value);
                    return true;
                }
                return false;
            }

            public void setReadyForPayload()
            {
                readyForPayload = true;
            }

            public void setReadyForRender()
            {
                readyForRender = true;
            }

            public string render()
            {
                if (readyForRender)
                    return packet.ToString();
                else
                    return null;
            }
        }
    }
}
