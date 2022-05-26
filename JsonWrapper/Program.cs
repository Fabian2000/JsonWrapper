using JsonWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Neue Json Instance erstellen und Dateinamen Festlegen oder FileName property zum Dateinamen ändern benutzen
            JsonTestClass jsonTestClass = new JsonTestClass("example.json");

            // Speichern der Datei
            jsonTestClass.Save();

            Console.WriteLine("Die json Datei kann jetzt manipuliert werden, dann irgendeine Taste drücken");
            Console.ReadKey();

            // Laden der Datei
            jsonTestClass = jsonTestClass.FromFile<JsonTestClass>();

            Console.WriteLine(jsonTestClass.Vorname + " " + jsonTestClass.Nachname);

            Console.ReadKey();
        }
    }
}
