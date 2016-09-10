using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaughingMonkey.CodeQuality.Design
{
    public class Example6_DevelopersRepeatWhatYouSay
    {

        public class Document
        {
            public System.Data.DataTable documents { get; set; }
            public Document(int documentId)
            {
                //validates that the object exists in the database
                //this takes k ms
            }

            public bool LoadSingleDocument(int documentId)
            {
                //loads a single document into the data table
                return true;
            }

            public bool LoadMultipleDocuments(int documentId)
            {
                //loads many documents into the data table
                return true;
            }

            public bool CacheData(int documentId)
            {
                //stores the object in the http session
                //this creates a dependency on the web class
                //and prevents us from moving this object to a 
                //middle tier server
                return true;
            }
        }
    }
}
