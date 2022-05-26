using JsonWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class JsonTestClass : JsonClass
    {
        public string Nachname = string.Empty;
        public string Vorname = string.Empty;

        // Die Konstruktoren müssen nicht gesetzt werden, ist aber empfehlenswert
        public JsonTestClass()
        {
        }

        // Die Konstruktoren müssen nicht gesetzt werden, ist aber empfehlenswert
        public JsonTestClass(string fileName) : base(fileName)
        {
        }
    }
}
