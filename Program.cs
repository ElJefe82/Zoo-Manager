using Zoo_Manager.Models;
using Zoo_Manager.Services;

namespace Zoo_Manager {
	internal class Program {
		static void Main(string[] args) {

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
						Console.Write("Programm wird beendet");
						for (int i = 0; i < 3; i++) {
							Thread.Sleep(wartezeitMs);
							Console.Write(".");
						}
						Thread.Sleep(wartezeitMs);
						Console.WriteLine();
						Console.WriteLine("\aAuf Wiedersehen!!!");
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
			}

			////====================PFLEGER-MENÜ (BESCHRÄNKTER ZUGRIFF)====================

			//void PflegerMenue() {

			//	bool logout = false;

			//	while (!logout) {

			//		Console.Clear();
			//		Console.WriteLine("=== Pfleger-Menü ===");
			//		Console.WriteLine("1 - Gehegeverwaltung");
			//		Console.WriteLine("2 - Besucherinfo");
			//		Console.WriteLine("3 - Logout");
			//		Console.Write("\nAuswahl:");

			//		switch (Console.ReadLine()!) {

			//			case "1":
			//				GehegeMenue();
			//				break;

			//			case "2":
			//				BesucherInfo();
			//				break;

			//			case "3":
			//				logout = true;
			//				break;

			//			default:
			//				Console.WriteLine("Ungültige Eingabe! Bitte versuchen Sie es erneut.");
			//				Console.ReadKey();
			//				break;
			//		}
			//	}
			//}

			////====================BESUCHER-MENÜ (OHNE LOGIN)====================

			//void BesucherMenue() {
			//	bool back = false;

			//	while (!back) {

			//		Console.Clear();
			//		Console.WriteLine("=== Besucherbereich ===");
			//		Console.WriteLine("1 - Besucherinfo");
			//		Console.WriteLine("2 - Tier.Ratespiel");
			//		Console.WriteLine("3 - Zurück");
			//		Console.Write("\nAuswahl:");

			//		switch (Console.ReadLine()!) {

			//			case "1":
			//				BesucherInfo();
			//				break;

			//			case "2":
			//				//spielService.Start();
			//				break;

			//			case "3":
			//				back = true;
			//				break;

			//			default:
			//				Console.WriteLine("Ungültige Eingabe! Bitte versuchen Sie es erneut.");
			//				Console.ReadKey();
			//				break;
			//		}
			//	}
			//}

			////====================VERWALTUNGSMENÜS====================
			void TierMenue() {
				bool back = false;
				while (!back) {
					Console.Clear();
					Console.WriteLine("=== Tierverwaltung ===");
					Console.WriteLine("1 - Alle Tiere anzeigen");
					Console.WriteLine("2 - Neues Tier anlegen");
					Console.WriteLine("3 - Tier löschen");
					Console.WriteLine("4 - Tier aktualisieren");
					Console.WriteLine("5 - Zurück");
					Console.Write("\nAuswahl:");

					try {
						input = Convert.ToInt32(Console.ReadLine());
					} catch (FormatException) {
						Console.WriteLine("Ungültige Eingabe!");
					}

					switch (input) {
						case 1:
							TiereAnzeigen();
							break;
						case 2:
							TierAnlegen();
							break;
						case 3:
							TierLoeschen();
							break;
						case 4:
							TierUpdate();
							break;
						case 5:
							back = true;
							break;
						default:
							Console.WriteLine("Bitte geben sie nur Zahlen von 1 bis 5 an!");
							Console.ReadKey();
							break;
					}
				}
			}

			void TiereAnzeigen() {
				Console.Clear();
				Console.WriteLine("=== Alle Tiere ===");
				if (tierService.GetAll().Any()) {
					foreach (var t in tierService.GetAll()) {
						Console.WriteLine(t);
					}
				} else {
					Console.WriteLine("Keine Tiere im Zoo vorhanden.");
				}
				Console.ReadLine();
			}

			void TierAnlegen() {
				bool back = false;
				while (!back) {
					Console.Clear();
					Console.WriteLine("=== Neues Tier anlegen ===");
					Console.WriteLine("Wähle die Tierart:");
					Console.WriteLine("1 - Löwe");
					Console.WriteLine("2 - Elefant");
					Console.WriteLine("3 - Zurück");
					Console.Write("\nAuswahl: ");

					try {
						input = Convert.ToInt32(Console.ReadLine());
					} catch (FormatException) {
						Console.WriteLine("Ungültige Eingabe!");
					}

					//int artWahl;
					//while (!int.TryParse(Console.ReadLine(), out artWahl) || artWahl < 1 || artWahl > 3) {
					//	Console.WriteLine("Ungültige Eingabe! Bitte 1, 2 oder 3 wählen.");  //weitere Tiere anlegen!
					//	Console.Write("Auswahl: ");
					//}

					Console.Write("Name: ");
					string name = Console.ReadLine();
					Console.Write("Alter: ");
					int alter = Convert.ToInt32(Console.ReadLine());
					Console.Write("Gehege ID: ");
					int gehegeId = Convert.ToInt32(Console.ReadLine());

					Tier neuesTier;
					switch (input) {
						case 1: // Löwe
							Console.Write("Ist Rudelfuehrer? (j/n): ");
							bool rudelfuehrer = Console.ReadLine()?.Trim().ToLower() == "j";
							neuesTier = new Loewe {
								Name = name,
								Art = "Loewe",
								Alter = alter,
								GehegeId = gehegeId,
								IstRudelfuehrer = rudelfuehrer
							};
							tierService.Hinzufuegen(neuesTier);
							Console.WriteLine("Neues Tier erfolgreich angelegt!");
							Console.ReadLine();
							break;
						case 2: // Elefant
							Console.Write("Ruessel-Länge (in Meter): ");
							double ruesselLaenge = Convert.ToDouble(Console.ReadLine());
							neuesTier = new Elefant {
								Name = name,
								Art = "Elefant",
								Alter = alter,
								GehegeId = gehegeId,
								RuesselLaenge = ruesselLaenge
							};
							tierService.Hinzufuegen(neuesTier);
							Console.WriteLine("Neues Tier erfolgreich angelegt!");
							Console.ReadLine();
							break;
						case 3:
							back = true;
							break;
						default:
							Console.WriteLine("Bitte geben sie nur Zahlen von 1 bis 3 an!");
							Console.ReadKey();
							break;
					}
					back = true;
				}
			}

			void TierLoeschen() {
				Console.Clear();
				Console.WriteLine("=== Tier löschen ===");
				Console.Write("Geben Sie die ID des zu löschenden Tiers ein: ");
				int id = Convert.ToInt32(Console.ReadLine());
				bool erfolg = tierService.Loeschen(id);
				if (erfolg)
					Console.WriteLine("Tier erfolgreich gelöscht!");
				else
					Console.WriteLine("Tier mit dieser ID nicht gefunden!");
				Console.ReadLine();
			}

			void TierUpdate() {
				Console.Clear();
				Console.WriteLine("=== Tier aktualisieren ===");
				TiereAnzeigen();
				Console.Write("Geben Sie die ID des zu aktualisierenden Tiers ein: ");
				int id = Convert.ToInt32(Console.ReadLine());

				var tier = tierService.Suche(id);
				if (tier == null) {
					Console.WriteLine("Tier mit dieser ID nicht gefunden!");
					Console.ReadLine();
					return;
				}
				var erfolgreich = tierService.Update(tier);
				Console.Write("Neuer Name (aktuell: {0}): ", tier.Name);
				tier.Name = Console.ReadLine();
				Console.Write("Neues Alter (aktuell: {0}): ", tier.Alter);
				tier.Alter = Convert.ToInt32(Console.ReadLine());
				Console.Write("Neue Gehege ID (aktuell: {0}): ", tier.GehegeId);
				tier.GehegeId = Convert.ToInt32(Console.ReadLine());
				tierService.Update(tier);
				Console.WriteLine("Tier erfolgreich aktualisiert!");
				Console.ReadLine();
			}

			void GehegeMenue() {
				Console.Clear();
				Console.WriteLine("=== Alle Gehege ===");
				foreach (var g in gehegeService.GetAll())
					Console.WriteLine(g);
				Console.ReadLine();
			}

			void PflegerMenueAdmin() {
				Console.Clear();
				Console.WriteLine("=== Alle Pfleger ===");
				foreach (var p in pflegerService.GetAll())
					Console.WriteLine(p);
				Console.ReadLine();
			}

			void BesucherInfo() {

				Console.Clear();
				Console.WriteLine("=== Besucherinfo ===");
				Console.WriteLine("Willkommen im Zoo! Hier finden Sie Informationen über unsere Tiere und Gehege.");
				foreach (var t in tierService.GetAll())
					Console.WriteLine(t);
				Console.ReadLine();
			}
		}
	}
}
