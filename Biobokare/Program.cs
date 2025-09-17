using System;

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

        static void Main()
        {
            bool isStudent = false;
            int selectedMovie = -1;
            int tickets = 0;
            while (true)
            {
                Console.WriteLine("Välkommen till SKRÄCKBION");
                Console.WriteLine("---------------------------");
                Console.WriteLine("Gör ett val");
                Console.WriteLine("1) Lista filmer & pris");
                Console.WriteLine("2) Välj film och ange antal biljetter");
                Console.WriteLine("3) Lägg på/ta bort studentrabatt");
                Console.WriteLine("4) Skriv ut kvitto");
                Console.WriteLine("5) Avsluta");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice)) continue;
                if (choice == 5) { Console.WriteLine("Hejdå"); break; }

                string[] movies = { "Introduktion till C#", "Lär dig använda arrayer", "Metodöverlagring" };
                string[] showTimes = { "18:00", "20:00", "22:00" };
                double[] basePrices = { 100, 120, 150 };
              
              

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Tillgängliga filmer:");
                        for (int i = 0; i < movies.Length; i++)
                        {
                            Console.WriteLine("-------------------");
                            Console.WriteLine($"{i + 1}) {movies[i]} - {showTimes[i]}");
                            Console.WriteLine($"Pris ex moms: {basePrices[i]} {currency}");


                         
                        }
                        Console.WriteLine(""); //mellanrum för när loopen laddas om
                        Console.WriteLine("");
                        break;

                    case 2:
                        Console.WriteLine("Ange filmnummer (1-3):");
                        string filmInput = Console.ReadLine();
                        int filmNumber;
                        if (!int.TryParse(filmInput, out filmNumber) || filmNumber < 1 || filmNumber > 3)
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
                        Console.WriteLine(""); //mellanrum för när loopen laddas om
                        Console.WriteLine("");
                        break;

                    case 3:
                        Console.WriteLine("Är du student? (ja/nej):");
                        string studentInput = Console.ReadLine();
                        isStudent = studentInput.Equals("ja", StringComparison.OrdinalIgnoreCase);
                        Console.WriteLine(""); //mellanrum för.. ja du vet vad.
                        Console.WriteLine("");

                        break;

                    case 4:
                        if (selectedMovie == -1 || tickets == 0)
                        {
                            Console.WriteLine("Ingen film vald ännu.");
                            break;
                        }
                        double totalPrice;
                        if (isStudent)
                        {
                            totalPrice = CalculatePrice(tickets, basePrices[selectedMovie], student_discount);
                        }
                        else
                        {
                            totalPrice = CalculatePrice(tickets, basePrices[selectedMovie]);
                        }
                        totalPrice *= (1 + tax_rate);
                        Console.WriteLine("-------------------");
                        Console.WriteLine($"Film: {movies[selectedMovie]}"); Console.WriteLine();
                        Console.WriteLine($"Tid: {showTimes[selectedMovie]}");
                        Console.WriteLine($"Antal biljetter: {tickets}");
                        Console.WriteLine($"Studentrabatt: {(isStudent ? "Ja" : "Nej")}");
                        Console.WriteLine($"Totalt pris (inkl. {tax_rate * 100}% moms): {totalPrice:F2} {currency}");
                        Console.WriteLine("-------------------");
                        return;
                        
                        

                        

                }

               
            }

        }
    }
}