// SportsNotifier.cs
// Kompletter (sehr simpler) Sportresultats-Notifier: holen, parsen, email senden.

using System.Net.Http;
using System.Text.RegularExpressions;
using MimeKit;
using MailKit.Net.Smtp;

using HttpClient client = new HttpClient();
string url = "https://www.example.com/sports/results";
string html = await client.GetStringAsync(url);

var match = Regex.Match(html, @"(.+?)\s+(\d+)\s*-\s*(\d+)\s+(.+)");
string result = match.Success
    ? $"{match.Groups[1].Value.Trim()} {match.Groups[2].Value} - {match.Groups[3].Value} {match.Groups[4].Value.Trim()}"
    : "Kein Resultat gefunden";

var msg = new MimeMessage();
msg.From.Add(new MailboxAddress("Notifier", "no-reply@myapp.com"));
msg.To.Add(new MailboxAddress("User", "user@example.com"));
msg.Subject = "Sportresultat-Notifier";

var bb = new BodyBuilder();
bb.TextBody = result;
msg.Body = bb.ToMessageBody();

using var smtp = new SmtpClient();
await smtp.ConnectAsync("localhost", 1025, SecureSocketOptions.StartTlsWhenAvailable);
await smtp.SendAsync(msg);
await smtp.DisconnectAsync(true);

Console.WriteLine("Notifier fertig.");
