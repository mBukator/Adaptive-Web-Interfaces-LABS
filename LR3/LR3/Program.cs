using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;

class LR3 {
    static async Task Main() {
        UsingThread();
        await UsingAsyncAwait();
        await GetAsync();
     }




    static void UsingThread() {
        Console.WriteLine("=====[ UsingThread() ]=====");

        Thread thread1 = new Thread(new ThreadStart(generateRandomNumber));

        //  Console.WriteLine("First Thread State: " + thread1.ThreadState);

        Thread thread2 = new Thread(new ThreadStart(generateRandomString));

        thread1.Start();

        //  Console.WriteLine("First Thread State: " + thread1.ThreadState);

        // Thread.Sleep(1000);
        //  Console.WriteLine("First Thread State: " + thread1.ThreadState);

        thread2.Start();


        Console.WriteLine("====[ END ]====");
    }

    static void generateRandomNumber() {
        Random rnd = new Random();
        Console.WriteLine("Random Number: " + rnd.Next(1, 25));     // generates random number that > 1 and < 25
    }

    static void generateRandomString() {
        Random rnd = new Random();

        int stringlen = rnd.Next(4, 10);
        int randValue;
        string str = "";
        char letter;

        for (int i = 0; i < stringlen; i++) {
            randValue = rnd.Next(0, 26);
            
            letter = Convert.ToChar(randValue + 65);    // Generating random character by converting the random number into character.
            
            str = str + letter;     // Appending the letter to string. 
        }
        Console.Write("Random String:" + str + "\n");
    }




    public static async Task UsingAsyncAwait() {

        Console.WriteLine("\n====[ Async/Await ]====");

        var boiling = BoilWaterAsync();

        Console.WriteLine("Take the cup");
        Console.WriteLine("Put the tea bags in cup");
        var water = await boiling;

        Console.WriteLine( $"Pour {water} in cups with tea bags");
    }

    public static async Task<string> BoilWaterAsync() {
        Console.WriteLine("0. Get the kettle");
        Console.WriteLine("1. Pour the water in kettle");
        Console.WriteLine("2. Boiling water...");
        await Task.Delay(2000);
        Console.WriteLine("3. Water has been boiled in 2 seconds");

        return "boiled water";
    }




    public static async Task GetAsync() {

        Console.WriteLine("\n====[ API call from GetAsync() ]====");

        string url = "https://api.polygon.io/v2/aggs/ticker/TSLA/range/1/day/2023-01-09/2023-01-09?adjusted=true&sort=asc&limit=120&apiKey=lO_Avyrnt0MmYAWxgaffHW5l11ySBjFH";

        Console.WriteLine("Receiving data from API...");
        using (HttpClient client = new HttpClient()) {
            try {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode) {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Received data:");
                    Console.WriteLine(responseData);
                } else {
                    Console.WriteLine($"Error: {response.ReasonPhrase}");
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}