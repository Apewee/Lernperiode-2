// SendEmail.cs
// (as described in https://dometrain.com/blog/sending-email-with-dotnet/)
//
// Setup:
//   dotnet new console -o MailDemo
//   cd MailDemo
//   dotnet add package MailKit



using MimeKit;
using MailKit.Net.Smtp; // ⚠️ MailKit, NOT the obsolete System.Net.Mail

var msg = new MimeMessage();
msg.From.Add(new MailboxAddress("App", "no-reply@myapp.com"));
msg.To.Add(new MailboxAddress("User", "user@example.com"));
msg.Subject = "Hello from .NET!";

// Plain text + HTML body via MimeKit's BodyBuilder
var bb = new BodyBuilder();
bb.TextBody = "This is a test email.";
bb.HtmlBody = "<h1>Hello World</h1><p>This is <b>HTML</b> email.</p>";
// bb.Attachments.Add("Dometrain.png");   // uncomment to attach a file
msg.Body = bb.ToMessageBody();

using var smtp = new SmtpClient();
await smtp.ConnectAsync("localhost", 1025, SecureSocketOptions.StartTlsWhenAvailable);
await smtp.SendAsync(msg);
await smtp.DisconnectAsync(true);

Console.WriteLine("Email sent.");
