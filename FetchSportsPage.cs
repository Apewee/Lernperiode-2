// FetchSportsPage.cs
// Holt eine Sportresultats-Seite per HTTP und gibt den Text aus.

using System.Net.Http;

using HttpClient client = new HttpClient();
string url = "https://www.example.com/sports/results";

string html = await client.GetStringAsync(url);

// Nur die ersten 500 Zeichen ausgeben, damit es übersichtlich bleibt
Console.WriteLine(html.Substring(0, Math.Min(500, html.Length)));
