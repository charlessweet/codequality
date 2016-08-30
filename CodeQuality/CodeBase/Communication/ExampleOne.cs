using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaughingMonkey.CodeQuality.Communication
{
    public class ExampleOne
    {
        public string MachineOnly()
        {
            byte[] h = new byte[11];
            h[0] = 72;
            h[1] = 101;
            for(int i = 1; i < 3; i++)
            {
                h[1 + i] = 108;
            }
            h[4] = h[7] = 111;
            h[5] = 32;
            h[6] = 87;
            h[8] = 114;
            h[9] = 108;
            h[10] = 100;

            ASCIIEncoding asc = new ASCIIEncoding();
            return asc.GetString(h);
        }

        public string HumanReadable()
        {
            string helloWorld = "Hello World";
            return helloWorld;
        }
    }
}
