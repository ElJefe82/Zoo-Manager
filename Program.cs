using System.Text;
using Newtonsoft.Json;
using Zoo_Manager.Models;
﻿using Zoo_Manager.Models;
using Zoo_Manager.Services;

namespace Zoo_Manager {
	internal class Program {
		static void Main(string[] args) {
			LadeBenutzer();
			ErstelleAdmin();

			var tierService = new TierService();
			var gehegeService = new GehegeService();
			var pflegerService = new PflegerService();
			var benutzerService = new BenutzerService();
			var spielService = new SpielService(tierService);

			var input = 0;
			while (input != 3) {
				Console.Clear();
				Console.WriteLine("======= ZooManager =======");
				Console.WriteLine("1 - Login");
				Console.WriteLine("2 - Besucher");
				Console.WriteLine("3 - Beenden");
				Console.Write("\nAuswahl:");

				try {
					input = Convert.ToInt32(Console.ReadLine());
				} catch (FormatException) {
					Console.WriteLine("Ungültige Eingabe!");
				}

				switch (input) {
					case 1:
						LoginMenue();
						break;
					case 2:
						//BesucherMenue();
						break;
					case 3:
						const int wartezeitMs = 1000;
						Console.Write("\aProgramm wird beendet");
						for (int i = 0; i < 3; i++) {
							Thread.Sleep(wartezeitMs);
							Console.Write(".");
						}
						Thread.Sleep(wartezeitMs);
						Console.WriteLine();
						Console.WriteLine("Auf Wiedersehen!!!");
						break;
					default:
						Console.WriteLine("Bitte geben sie nur Zahlen von 1 bis 3 an!");
					Console.ReadKey();
						break;
			}
		}

		//====================LOGIN====================
			void LoginMenue() {
				Console.Clear();
				Console.WriteLine("=== Login ===");
				Console.Write("Benutzername: ");
				string benutzername = Console.ReadLine();

				while (string.IsNullOrWhiteSpace(benutzername)) {
					Console.WriteLine("Benutzername darf nicht leer sein. Bitte erneut eingeben.");
					Console.ReadKey();
			Console.Clear();
					Console.WriteLine("=== Login ===");
			Console.Write("Benutzername: ");
					benutzername = Console.ReadLine();
				}

			Console.Write("Passwort: ");
				string passwort = Console.ReadLine();

				while (string.IsNullOrWhiteSpace(passwort)) {
					Console.WriteLine("Passwort darf nicht leer sein. Bitte erneut eingeben.");
					Console.Write("Passwort: ");
					passwort = Console.ReadLine();
				}

				if (!benutzerService.Login(benutzername, passwort)) {
					Console.WriteLine("Login fehlgeschlagen!");
				Console.ReadKey();
				return;
			}

				Console.WriteLine($"Erfolgreich eingeloggt als {benutzerService.LoggedInBenutzer.Rolle}!");
				Console.ReadKey();

				switch (benutzerService.LoggedInBenutzer.Rolle) {
					case Rolle.Admin:
						AdminMenue();
						break;

					case Rolle.Pfleger:
						//PflegerMenue();
						break;

					default:
						//BesucherMenue();
						break;
			}
		}

			////====================ADMIN-MENÜ (VOLLZUGRIFF)====================

			void AdminMenue() {
				bool logout = false;

				while (!logout) {
					Console.Clear();
					Console.WriteLine("=== Admin-Menü ===");
					Console.WriteLine("1 - Tierverwaltung");
					Console.WriteLine("2 - Gehegeverwaltung");
					Console.WriteLine("3 - Pflegerverwaltung");
					Console.WriteLine("4 - Besucherinfo");
					Console.WriteLine("5 - Tier-Ratespiel");
					Console.WriteLine("6 - Logout");
					Console.Write("\nAuswahl:");

					try {
						input = Convert.ToInt32(Console.ReadLine());
					} catch (FormatException) {
						Console.WriteLine("Ungültige Eingabe!");
					}
					switch (input) {
						case 1:
							TierMenue();
					break;
						case 2:
							GehegeMenue();
							break;
						case 3:
							PflegerMenueAdmin();
							break;
						case 4:
							BesucherInfo();
							break;
						case 5:
							spielService.Start();
							break;
						case 6:
							logout = true;
							break;
						default:
							Console.WriteLine("Bitte geben sie nur Zahlen von 1 bis 6 an!");
							Console.ReadKey();
							break;
				}
			}
			Console.WriteLine();
			return sb.ToString();
		}

		//====================BENUTZERVERWALTUNG====================

		static void BenutzerVerwaltung() {
			while (true) {
				Console.Clear();
				Console.WriteLine("=== Benutzerverwaltung ===");
				Console.WriteLine("1) Benutzer anzeigen");
				Console.WriteLine("2) Benutzer anlegen");
				Console.WriteLine("3) Benutzer löschen");
				Console.WriteLine("4) Logout");

				var w = Console.ReadLine();

				if (w == "1")
					BenutzerAnzeigen();
				else if (w == "2")
					BenutzerAnlegen();
				else if (w == "3")
					BenutzerLöschen();
				else if (w == "4") { AktiverBenutzer = null; return; }
			}
		}

		static void BenutzerAnzeigen() {
			Console.Clear();
			foreach (var b in Benutzer)
				Console.WriteLine($"{b.Benutzername} ({b.Rolle})");
			Console.ReadKey();
		}

		static void BenutzerAnlegen() {
			Console.Clear();
			Console.Write("Benutzername: ");
			var name = Console.ReadLine();

			if (Benutzer.Any(b => b.Benutzername == name)) {
				Console.WriteLine("Benutzer existiert bereits.");
				Console.ReadKey();
				return;
			}

			Console.Write("Passwort: ");
			var pw = PasswortLesen();

			Console.Write("Rolle (Admin/Pfleger): ");
			var rolle = Console.ReadLine();

			if (rolle != "Admin" && rolle != "Pfleger") {
				Console.WriteLine("Ungültige Rolle.");
				Console.ReadKey();
				return;
			}

			Benutzer.Add(new Benutzer {
				Benutzername = name!,
				Passwort = pw,
				Rolle = rolle == "Admin" ? Rolle.Admin : Rolle.Pfleger
			});

			SpeichernBenutzer();
			Console.WriteLine("Benutzer angelegt.");
			Console.ReadKey();
		}

		static void BenutzerLöschen() {
			Console.Clear();
			Console.Write("Benutzername löschen: ");
			var name = Console.ReadLine();
			var benutzer = Benutzer.FirstOrDefault(b => b.Benutzername == name);

			if (benutzer == null)
				Console.WriteLine("Nicht gefunden.");
			else {
				Benutzer.Remove(benutzer);
				SpeichernBenutzer();
				Console.WriteLine("Benutzer gelöscht.");
			}
			Console.ReadKey();
		}

		static void LadeBenutzer() {
			if (!File.Exists(BenutzerDatei))
				return;
			Benutzer = JsonConvert.DeserializeObject<List<Benutzer>>(File.ReadAllText(BenutzerDatei))
			?? [];
		}

		static void SpeichernBenutzer() {
			File.WriteAllText(BenutzerDatei, JsonConvert.SerializeObject(Benutzer, Newtonsoft.Json.Formatting.Indented));
		}

		static void ErstelleAdmin() {
			if (!Benutzer.Any(b => b.Rolle == Rolle.Admin)) {

				Benutzer.Add(new Benutzer {
					Benutzername = "admin",
					Passwort = "admin123!?",
					Rolle = Rolle.Admin
				});
				SpeichernBenutzer();
			}
		}
	}
}
