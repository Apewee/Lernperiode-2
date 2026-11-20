// SportsNotifier.cs
// Verbesseder Sportresultats-Notifier mit Unterst├╝tzung f├╝r mehrere Spiele
// und geplanter Ausf├╝hrung via Timer/Cron.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using MimeKit;
using MailKit.Net.Smtp;

namespace SportsNotifier;

/// <summary>
/// Configuration for a single sport/game to monitor.
/// </summary>
record SportConfig(string Name, string Url, string RegexPattern);

/// <summary>
/// Main sports notifier that fetches, parses, and emails sports results.
/// </summary>
public class Program
{
    private static readonly List<SportConfig> Sports = new()
    {
        new SportConfig("Football", "https://www.example.com/sports/results1", @".+?\s+(\d+)\s*-\s*(\d+)\s+.+?"),
        new SportConfig("Basketball", "https://www.example.com/sports/results2", @".+?\s+(\d+)\s*-\s*(\d+)\s+.+?"),
        // Add more sports as needed
    };

    public static async Task Main(string[] args)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm}] SportsNotifier gestartet");

        var results = new Dictionary<string, string>();

        using var client = new HttpClient();

        foreach (var sport in Sports)
        {
            try
            {
                Console.WriteLine($"Hole Daten von {sport.Name}...");
                string html = await client.GetStringAsync(sport.Url);

                var match = Regex.Match(html, sport.RegexPattern);
                string result = match.Success
                    ? $"{sport.Name}: {match.Groups[1].Value} - {match.Groups[2].Value}"
                    : $"{sport.Name}: Kein Resultat gefunden";

                results[sport.Name] = result;
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler bei {sport.Name}: {ex.Message}");
                results[sport.Name] = $"{sport.Name}: Fehler - {ex.Message}";
            }
        }

        // Send email with all results
        await SendEmailAsync(results);

        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm}] SportsNotifier fertig");
    }

    private static async Task SendEmailAsync(Dictionary<string, string> results)
    {
        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress("Notifier", "no-reply@myapp.com"));
        msg.To.Add(new MailboxAddress("User", "user@example.com"));
        msg.Subject = $"Sportresultate - {DateTime.Now:yyyy-MM-dd}";

        var bb = new BodyBuilder();
        var resultLines = new List<string>();
        foreach (var kvp in results)
        {
            resultLines.Add(kvp.Value);
        }
        bb.TextBody = string.Join(Environment.NewLine, resultLines);
        msg.Body = bb.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("localhost", 1025, SecureSocketOptions.StartTlsWhenAvailable);
        await smtp.SendAsync(msg);
        await smtp.DisconnectAsync(true);

        Console.WriteLine("Resultat-Email gesendet.");
    }
}
