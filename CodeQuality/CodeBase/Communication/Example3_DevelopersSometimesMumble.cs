using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaughingMonkey.CodeQuality.Communication
{
    public class Example3_DevelopersSometimesMumble
    {
        //What is this going to print out?
        public string TrivialMumblingExample()
        {
            int temp0 = 1;
            int temp1 = 1000;
            int temp2 = 17;
            int temp3 = 23;
            int temp5 = 0;
            for(int temp4 = temp0; temp4 < temp1; temp4++)
            {
                if(temp4 % temp2 == temp5)
                {
                    return "Hello";
                }
                if(temp4 % temp2 != temp5 && temp4 % temp3 != temp5)
                {
                    return "World";
                }
            }
            return String.Empty;
        }
    }
}
