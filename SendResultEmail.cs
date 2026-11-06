// SendResultEmail.cs
// (wie SendEmail.cs, aber mit einem Sportresultat in der Nachricht)
// Setup: dotnet new console -o MailDemo / dotnet add package MailKit

using MimeKit;
using MailKit.Net.Smtp;

string result = "FC A 2 - 1 FC B";

var msg = new MimeMessage();
msg.From.Add(new MailboxAddress("Notifier", "no-reply@myapp.com"));
msg.To.Add(new MailboxAddress("User", "user@example.com"));
msg.Subject = "Sportresultat";

var bb = new BodyBuilder();
bb.TextBody = $"Neues Resultat: {result}";
msg.Body = bb.ToMessageBody();

using var smtp = new SmtpClient();
await smtp.ConnectAsync("localhost", 1025, SecureSocketOptions.StartTlsWhenAvailable);
await smtp.SendAsync(msg);
await smtp.DisconnectAsync(true);

Console.WriteLine("Resultat-Email gesendet.");
