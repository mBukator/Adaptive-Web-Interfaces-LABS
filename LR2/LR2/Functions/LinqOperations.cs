using System;
using System.Linq;

namespace LR2.Functions {
    public class LinqOperations {
        class Car {
            public string Brand { get; set; }
            public string Model { get; set; }
        }
        class Person {
            public string Name { get; set; }
            public Car[] Cars { get; set; }
        }


        public static void ExampleLinq() {
            Console.WriteLine("===[ Example of using System.Linq ]===");
            List<Person> people = new List<Person> {
            new Person {Name = "Max Bukator",
                Cars = new Car[] { new Car { Brand = "Mercedes", Model = "CLS AMG 63" },
                                   new Car { Brand = "Ferrari", Model = "f40" }
                }
            },
            new Person {Name = "Andrew Tate",
                Cars = new Car[] { new Car { Brand = "Bugatti", Model = "Veyron" },
                                   new Car { Brand = "Porsche", Model = "911 GT RS" },
                                   new Car { Brand = "Audi", Model = "R8" }
                }
            },
            new Person {Name = "Vasya Pupkin",
                Cars = new Car[] { }
            },
            new Person {Name = "Ann D'Arc",
                Cars = new Car[] { }
            }
        };


            IEnumerable<string> names = from person in people
                                        where person.Cars.Any()
                                        select person.Name;


            foreach (string name in names) {
                Console.WriteLine(name);
            }

            Console.WriteLine("\n");
        }
    }
}
