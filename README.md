# Lernperiode-2
----

**16.10.2026**
Habe vs code mit vc cod**ium** ersezt weil ich meistens auf linux arbeite nacher auch noch kurzen number generator gemacht (ungefähr 20 minuten gebraucht bis es optimal ausgesehen hat)
Angefangen mit brainstorming für ein gutes projekt für diese Lernperidode

Lernpackete für nächstes mal:
- Complete Projekt idee mit genauen anweisungen erstellen und notieren
- Erste recherche zu dem Projekt erstellen und wie man es umsetzen kann

**22.10.2026**
Ich habe mich informiert was für eine App ich machen will und ich werde einen Sportresultats notifier machen , er soll das web nach bestimmten spielen suchen und diese mir automatisch via email schicken
Ich habe mich auch informiert wie ich das umsetzten soll und in der Dokumentation von c# webcrawlern nachgeschaut
Ich denke das dieses programm realistisch ist und das ich es schaffen sollte

**23.10.2026**
Ich habe herausgefunden wie man ? anstatt "if else if" benutzt.
Ich habe mich dazu informiert wie man Emails mit C# versendet und dabei die Ganze anleitung von https://dometrain.com/blog/sending-email-with-dotnet/ befolgt.

Lernpacket hausaufgabe:
- simples .NET script schreiben welche eine simple Email an eine bestimmte addresse schickt

**26.10.2026** (Uploaded am 30.10.2026 weil ich vergessen habe changes zu commiting)
Ich habe genau dieses script geschrieben welches eine simple email macht mit einer simplen nachricht man muss jedoch immer noch einen privaten email host server auf dem eigenen laptop einrichten welches ich am nächsten mache
(File name ist : SendEmail.cs)

**30.10.2026**
Ich habe angefangen den Sportresultats-Notifier umzusetzen. Zuerst habe ich einen einfachen HTTP-Request geschrieben, der eine Sportresultats-Seite holt und den HTML-Text ausgibt, damit ich sehe was ich bekomme.

Lernpackete für nächstesmal:
- HTML der Sportseite analysieren und das richtige Resultat finden
- Ergebnis aus dem HTML herausparsen

Lernpacket hausaufgabe:
- simples .NET script schreiben welches eine Sportseite per HTTP holt und den Text ausgibt
(File name ist : FetchSportsPage.cs)

**02.11.2026**
Ich habe mit Regex versucht das Resultat aus dem HTML herauszuholen. Funktioniert noch nicht perfekt aber ich verstehe jetzt wie man mit Regex Gruppen arbeitet.

Lernpackete für nächstesmal:
- Email mit dem geparsten Resultat versenden
- Fehlerbehandlung wenn die Seite nicht erreichbar ist

Lernpacket hausaufgabe:
- simples .NET script schreiben welches ein Resultat aus einem HTML-String parses
(File name ist : ParseResult.cs)

**05.11.2026**
Ich habe das Email-Script vom 26.10 erweitert, so dass jetzt ein Sportresultat in der Nachricht steht. Benutzt wieder MailKit und den lokalen SMTP-Server.

Lernpackete für nächstesmal:
- HTTP-Request, Parsen und Email in einem Programm verbinden

Lernpacket hausaufgabe:
- simples .NET script schreiben welche eine Email mit einem Sportresultat verschickt
(File name ist : SendResultEmail.cs)

**09.11.2026**
Ich habe alles zusammengefügt: Seite holen, Resultat parsen, Email versenden. Es ist noch sehr simpel und nicht robust aber der Notifier läuft einmal durch.

Lernpackete für nächstesmal:
- Regelmässig automatisch laufen lassen (Timer/Cron)
- Mehrere Spiele unterstützen

Lernpacket hausaufgabe:
- simples .NET script schreiben welches den kompletten Sportresultats-Notifier einmal durchlauft
(File name ist : SportsNotifier.cs)

---

## Projektstatus

Die folgenden Verbesserungen wurden vorgenommen:

### SportsNotifier.cs — Große Refaktion
- **Mehrere Spiele**: `SportConfig`-Record mit Name, URL, Regex-Muster; vorconfigured Football und Basketball (erweiterbar)
- **Fehlerbehandlung**: Try/catch pro Sport-Fetch — individuelle Fehler stürzen den ganzen Lauf nicht
- **Strukturiertes Output**: Results dictionary gesammelt vor senden einer Email
- **Modulares Design**: `SendEmailAsync` als wiederverwendbare Methode extrahiert
- **Timestamped Logging**: Start- und End-Zeitstempel in Console-Output
- **Namespace**: `SportsNotifier` um `Program` Klasse

### Architektur
Der Notifier folgt nun einem sauberen Ablauf:
1. Über konfigurierte Sports iterieren (`SportConfig` Records mit Name, URL, Regex-Muster)
2. Jede Seite per `HttpClient` laden
3. Ergebnisse mit individuellen Regex-Mustern parsen
4. Alle Results sammeln, eine Email mit allen Results senden
5. Fehlerbehandlung pro Sport verhindert totalen Absturz

### Offene Punkte aus den Lernnotizen
- [ ] Timer/Cron für regelmäßige Ausführung hinzufügen (`Regelmässig automatisch laufen lassen`)
- [ ] Regex-Muster für spezifische Sport-Websites erweitern (`Mehrere Spiele unterstützen`)

Die individuellen Lernfiles (`FetchSportsPage.cs`, `ParseResult.cs`, `SendEmail.cs`, `SendResultEmail.cs`) bleiben als Referenz für den schrittweisen Lernweg erhalten.
