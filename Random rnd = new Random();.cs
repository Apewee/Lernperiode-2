Random rnd = new Random();
int ziel = rnd.Next(1, 101);
int tipp;

do
{
    Console.Write("Rate eine Zahl (1-100): ");
    tipp = int.Parse(Console.ReadLine());

    if (tipp < ziel) Console.WriteLine("Zu klein!");
    else if (tipp > ziel) Console.WriteLine("Zu groß!");
    else Console.WriteLine("Richtig!");
} while (tipp != ziel);