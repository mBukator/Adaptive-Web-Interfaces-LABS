using System;
using System.Xml.Linq;

namespace LR2.Functions {
    public class XmlOperations {
        public static void ExampleXml() {
            Console.WriteLine("===[ Example of using System.Xml.Linq]===");

            XDocument doc = new XDocument(
                new XElement("Cars",
                    new XElement("Car",
                        new XElement("Brand", "Porsche"),
                        new XElement("Model", "911 GT RS")
                    ),
                    new XElement("Car",
                        new XElement("Brand", "Mercedes"),
                        new XElement("Model", "CLS AMG 63")
                    )
                )
            );

            Console.WriteLine(doc);
        }
    }
}
