// See https://aka.ms/new-console-template for more information
using System.Security.AccessControl;

namespace dHondtCalculator
{
    class Program
    {

        private static void Main(string[] args)
        {
            Console.WriteLine("***** *** i ************!");
            (int mandaty, List<Komitet> komitety) = ReadInputFile();
            var wyniki = ProcessData(mandaty, komitety);
            ShowResults(wyniki);
        }

        private static (int mandaty, List<Komitet> komitety) ReadInputFile()
        {
            var komitety = new List<Komitet>();
            var content = File.ReadAllLines("input.txt");
            int.TryParse(content[0], out int mandaty);
            for (int i = 1; i < content.Length; i++)
            {
                var komitet = content[i].Split(';');
                int.TryParse(komitet[1], out int glosy);
                komitety.Add(new Komitet
                {
                    Nazwa = komitet[0],
                    LiczbaGlosow = glosy
                });
            }
            return (mandaty, komitety);
        }

        private static List<Komitet1> ProcessData(int mandaty, List<Komitet> komitety)
        {
            var wyniki = new List<Komitet1>();
            int limit = komitety.OrderByDescending(a => a.LiczbaGlosow).FirstOrDefault().LiczbaGlosow / mandaty;
            foreach (var komitet in komitety.OrderByDescending(a => a.LiczbaGlosow))
            {
                for (int i = 1; i <= mandaty; i++)
                {
                    int liczbaGlosowIteracja = komitet.LiczbaGlosow / i;
                    if (liczbaGlosowIteracja < limit) break;
                    else wyniki.Add(new Komitet1
                    {
                        Nazwa = komitet.Nazwa,
                        LiczbaGlosow = komitet.LiczbaGlosow,
                        Iteracja = i,
                        LiczbaGlosowIteracja = liczbaGlosowIteracja
                    });
                }
            }
            
            Console.WriteLine($"Łącznie wyników: {wyniki.Count}");
            return wyniki.OrderByDescending(q => q.LiczbaGlosowIteracja).Take(mandaty).ToList();
        }

        private static int ShowResults(List<Komitet1> wyniki)
        {
            int i = 0;
            foreach (var item in wyniki)
            {
                Console.WriteLine($"Mandat {++i}: {item.Nazwa} ({item.Iteracja})");
            }
            var partie = wyniki.GroupBy(a => a.Nazwa);
            Console.WriteLine("Podsumowanie:");
            foreach (var item in partie)
            {
                Console.WriteLine($"{item.Key}: {item.Count()}");
            }
            return 0;
        }

    }
}