using System;
using System.IO;

class Program {

    static void Main() {
        while (true) {
            Console.WriteLine("Select the option: " +
                "\n1. Number of words in the file LoremIpsum.txt" +
                "\n2. Perform a mathematical operation" +
                "\n3. Create LoremIpsum.txt" +
                "\n4. Exit"
            );


            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice) {
                case 1:
                    CountWordsInLoremIpsum();
                    break;
                case 2:
                    PerformMathOperation();
                    break;
                case 3:
                    CreateLoremIpsumFile();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong choice. Please try again.");
                    break;
            }

        }
    }


    static void CreateLoremIpsumFile() {
        string loremIpsumText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LoremIpsum.txt");
        File.WriteAllText(filePath, loremIpsumText);
        Console.WriteLine($"The file '{filePath}' was successfully created and filled with the text.\n");
    }


    static void CountWordsInLoremIpsum() {
        string loremIpsumText = File.ReadAllText("LoremIpsum.txt");
        string[] words = loremIpsumText.Split(' ');
        Console.WriteLine($"Number of words in the text: {words.Length}\n");
    }


    /* 
     * The PerformMathOperation method takes a user expression, 
     * calls the EvaluateMathExpression method to evaluate it,
     * and displays the result or an error message on the console.
    */
    static void PerformMathOperation() {
        Console.WriteLine("Enter the expression to calculate:");
        string expression = Console.ReadLine(); // Read the expression entered by the user and store it in a local variable

        try {
            double result = EvaluateMathExpression(expression);
            Console.WriteLine($"Result: {result} \n");
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message} \n");
        }
    }

    static double EvaluateMathExpression(string expression) {
        return Convert.ToDouble(new System.Data.DataTable().Compute(expression, ""));
    }
}
