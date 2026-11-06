// ParseResult.cs
// Parst ein simples Resultat aus einem HTML-String mit Regex.

using System.Text.RegularExpressions;

string html = "<div class=\"result\">FC A 2 - 1 FC B</div>";

var match = Regex.Match(html, @"(.+?)\s+(\d+)\s*-\s*(\d+)\s+(.+)");
if (match.Success)
{
    string home = match.Groups[1].Value.Trim();
    string homeGoals = match.Groups[2].Value;
    string awayGoals = match.Groups[3].Value;
    string away = match.Groups[4].Value.Trim();

    Console.WriteLine($"Heim: {home} {homeGoals}");
    Console.WriteLine($"Gast: {away} {awayGoals}");
}
else
{
    Console.WriteLine("Kein Resultat gefunden.");
}
