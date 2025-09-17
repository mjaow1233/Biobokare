using System;

namespace biobokare
{
    internal class Program
    {
        static int Addera(int a, int b)
        {
            return a + b;
        }

        static int Subtrahera(int a, int b)
        {
            return a - b;
        }

        static int Multiplicera(int a, int b)
        {
            return a * b;
        }

        static double Dividera(int a, int b)
        {
            return (double)a / b;
        }

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Välkommen till SKRÄCKbion);
                Console.WriteLine("---------------------------");
                Console.WriteLine("Gör ett val");
                Console.WriteLine("1) Lista filmer");
                Console.WriteLine("2) Välj film & tid, ange biljetter");
                Console.WriteLine("3) Lägg på/ta bort studentrabatt");
                Console.WriteLine("4) Skriv ut kvitto");
                Console.WriteLine("5) Avsluta");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice)) continue;
                if (choice == 5) { Console.WriteLine("Hejdå"); break; }

                Console.WriteLine("Ange tal 1:");
                int.TryParse(Console.ReadLine(), out int tal1);
                Console.WriteLine("Ange tal 2:");
                int.TryParse(Console.ReadLine(), out int tal2);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Summan är: " + Addera(tal1, tal2));
                        break;
                    case 2:
                        Console.WriteLine("Differensen är: " + Subtrahera(tal1, tal2));
                        break;
                    case 3:
                        Console.WriteLine("Produkten är: " + Multiplicera(tal1, tal2));
                        break;
                    case 4:
                        Console.WriteLine("Kvoten är: " + Dividera(tal1, tal2));
                        break;
                    default:
                        Console.WriteLine("Fel bakom spakarna");
                        break;
                }

                Console.WriteLine();
            }

        }
    }
}