using System;

namespace delegatesAndEvents
{
    // create a delegate
    public delegate void Notify(int n);

    public class Race
    {
        // create a delegate event object
        public event Notify RaceEnd;

        public void Racing(int contestants, int laps)
        {
            Console.WriteLine("Ready\nSet\nGo!");
            Random r = new Random();
            int[] participants = new int[contestants];
            bool done = false;
            int champ = -1;
            // first to finish specified number of laps wins
            while (!done)
            {
                for (int i = 0; i < contestants; i++)
                {

                    if (participants[i] <= laps)
                    {
                        participants[i] += r.Next(1, 5);
                    }
                    else
                    {
                        champ = i;
                        done = true;
                        continue;
                    }
                }

            }

            TheWinner(champ);
        }
        private void TheWinner(int champ)
        {
            Console.WriteLine("We have a winner!");
            // invoke the delegate event object and pass champ to the method
            RaceEnd(champ);

        }
    }
    class Program
    {
        public static void Main()
        {
            // create a class object
            Race round1 = new Race();
            // register with the footRace event
            round1.RaceEnd += footRace_RaceEnd;
            // trigger the event
            round1.Racing(5, 3);
            Console.WriteLine();

            // register with the carRace event
            round1.RaceEnd -= footRace_RaceEnd;
            round1.RaceEnd += carRace_RaceEnd;
            //trigger the event
            round1.Racing(12, 20);
            Console.WriteLine();

            // register a bike race event using a lambda expression
            round1.RaceEnd -= carRace_RaceEnd;
            round1.RaceEnd += (winner) => Console.WriteLine($"The bike race competition winner is {winner}");
            // trigger the event
            round1.Racing(8,1);
        }

        // event handlers
        public static void carRace_RaceEnd(int winner)
        {
            Console.WriteLine($"Car number {winner} is the winner.");
        }
        public static void footRace_RaceEnd(int winner)
        {
            Console.WriteLine($"Racer number {winner} is the winner.");
        }
    }
}