using System;
using System.Timers;

namespace LR2.Functions {
    public class TimerOperations {
        public static void ExampleTimer() {
            Console.WriteLine("===[ Example of using System.Timers ]===");

            System.Timers.Timer timer = new System.Timers.Timer(1000);

            timer.Elapsed += OnElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.WriteLine("Timer has been started. Press Enter if you want to stop it");

            static void OnElapsed(Object source, ElapsedEventArgs e) {
                    Console.WriteLine("Timer: {0:HH:mm:ss}", e.SignalTime);
            }
            Console.ReadLine();
            timer.Stop();

            Console.WriteLine();
        }
    }
}
