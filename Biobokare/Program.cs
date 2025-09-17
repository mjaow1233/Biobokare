using System;
using System.Threading;

namespace biobokare
{
    internal class Program
    {
        const double tax_rate = 0.06;          // 6% moms
        const double student_discount = 0.15;  // 15% rabatt
        const string currency = "SEK";

        static double CalculatePrice(int tickets, double basePrice)
        {
            return tickets * basePrice;
        }

        static double CalculatePrice(int tickets, double basePrice, double discountPercent)
        {
            return tickets * basePrice * (1 - discountPercent);
        }

        static void PrintReceipt(string movie, string time, int tickets, double total, bool isStudent) //namngivna argument
        {
            Console.WriteLine("");
            Console.WriteLine($"Film: {movie}");
            Console.WriteLine($"Tid: {time}");
            Console.WriteLine($"Antal biljetter: {tickets}");
            Console.WriteLine($"Studentrabatt: {(isStudent ? "Ja" : "Nej")}");
            Console.WriteLine($"Totalt pris (inkl. {tax_rate * 100}% moms): {total:F2} {currency}");
            Console.WriteLine("-------------------");
        }
        static bool spinnerShown = false; //en gång räcker

        static void ShowMenu()
        {
            
            Console.WriteLine("---------------------------");

            if (!spinnerShown) 
            {
                Console.WriteLine();
                int spinnerRow = Console.CursorTop;

                using (var leftSpinner = new Spinner(left: 0, top: spinnerRow, delay: 150))
                using (var rightSpinner = new Spinner(left: 30, top: spinnerRow, delay: 150))
                {
                    leftSpinner.Start();
                    rightSpinner.Start();

                    int titleRow = spinnerRow + 0;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(10, titleRow);
                    Console.Write("SKRÄCKBION");
                    Console.ResetColor();

                    Thread.Sleep(1250);

                    leftSpinner.Stop();
                    rightSpinner.Stop();

                    Console.SetCursorPosition(0, titleRow + 2);
                }

                spinnerShown = true;
            }
            else
            {
                // Visa bara titeln utan spinner
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("          SKRÄCKBION");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Gör ett val");
            Console.WriteLine("1) Lista filmer & pris");
            Console.WriteLine("2) Välj film och ange antal biljetter");
            Console.WriteLine("3) Lägg på/ta bort studentrabatt");
            Console.WriteLine("4) Skriv ut kvitto");
            Console.WriteLine("5) Avsluta");
        }
        static void Main()
        {
            bool isStudent = false;
            int selectedMovie = -1;
            int tickets = 0;

            string[] movies = { "Introduktion till C#", "Lär dig använda arrayer", "Metodöverlagring" };
            string[] showTimes = { "18:00", "20:00", "22:00" };
            double[] basePrices = { 100, 120, 150 };

            while (true)
            {
                ShowMenu();

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice)) continue;

                if (choice == 5)
                {
                    Console.WriteLine("Hejdå!");
                    break;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Filmer:");
                        for (int i = 0; i < movies.Length; i++)
                        {
                            Console.WriteLine("-------------------");
                            Console.WriteLine($"{i + 1}) {movies[i]} - {showTimes[i]}");
                            Console.WriteLine($"Pris ex moms: {basePrices[i]} {currency}");
                        }
                        Console.WriteLine("");
                        Console.WriteLine(""); // Extra rad för bättre läsbarhet
                        break;

                    case 2:
                        Console.WriteLine("Ange filmnummer (1-3):");
                        string filmInput = Console.ReadLine();
                        if (!int.TryParse(filmInput, out int filmNumber) || filmNumber < 1 || filmNumber > 3)
                        {
                            Console.WriteLine("Ogiltigt filmnummer.");
                            break;
                        }
                        selectedMovie = filmNumber - 1;
                        Console.WriteLine($"Du valde: {movies[selectedMovie]} kl {showTimes[selectedMovie]}");

                        Console.WriteLine("Ange antal biljetter:");
                        string ticketsInput = Console.ReadLine();
                        if (!int.TryParse(ticketsInput, out tickets) || tickets <= 0)
                        {
                            Console.WriteLine("Ogiltigt antal biljetter.");
                            tickets = 0;
                            break;
                        }
                        Console.WriteLine($"Antal biljetter: {tickets}");
                        Console.WriteLine("");
                        Console.WriteLine(""); //extra rad för bättre läsbarhet
                        break;

                    case 3:
                        Console.WriteLine("Är du student? (ja/nej):");
                        string studentInput = Console.ReadLine();
                        isStudent = studentInput.Equals("ja", StringComparison.OrdinalIgnoreCase);
                        Console.WriteLine("");
                        break;

                    case 4:
                        if (selectedMovie == -1 || tickets == 0)
                        {
                            Console.WriteLine("Ingen film vald ännu.");
                            break;
                        }

                        double totalPrice = isStudent
                            ? CalculatePrice(tickets, basePrices[selectedMovie], student_discount)
                            : CalculatePrice(tickets, basePrices[selectedMovie]);

                        totalPrice *= (1 + tax_rate);

                        PrintReceipt(
                            movie: movies[selectedMovie],
                            time: showTimes[selectedMovie],
                            tickets: tickets,
                            total: totalPrice,
                            isStudent: isStudent
                        );

                        //Avsluta efter utskrift av kvitto
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }

               
            }
        }
    }
    public class Spinner : IDisposable
    {
        private const string Sequence = @"/-";
        private int counter = 0;
        private readonly int left;
        private readonly int top;
        private readonly int delay;
        private bool active;
        private readonly Thread thread;

        public Spinner(int left, int top, int delay = 150)
        {
            this.left = left;
            this.top = top;
            this.delay = delay;
            thread = new Thread(Spin);
        }

        public void Start()
        {
            active = true;
            if (!thread.IsAlive)
                thread.Start();
        }

        public void Stop()
        {
            active = false;
            Draw(' ');
        }

        private void Spin()
        {
            while (active)
            {
                Turn();
                Thread.Sleep(delay);
            }
        }

        private void Draw(char c)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(c);
            Console.ResetColor();
        }

        private void Turn()
        {
            Draw(Sequence[++counter % Sequence.Length]);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}