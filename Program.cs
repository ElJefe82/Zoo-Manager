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

			//====================PFLEGER-MENÜ (BESCHRÄNKTER ZUGRIFF)====================

			void PflegerMenue() {

				bool logout = false;

				while (!logout) {

					Console.Clear();
					Console.WriteLine("=== Pfleger-Menü ===");
					Console.WriteLine("1 - Gehegeverwaltung");
					Console.WriteLine("2 - Besucherinfo");
					Console.WriteLine("3 - Logout");
					Console.Write("\nAuswahl:");

					switch (Console.ReadLine()!) {

						case "1":
							GehegeMenue();
							break;

						case "2":
							BesucherInfo();
							break;

						case "3":
							logout = true;
							break;

						default:
							Console.WriteLine("Ungültige Eingabe! Bitte versuchen Sie es erneut.");
							Console.ReadKey();
							break;
					}
				}
			}

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
				Console.Clear();
				Console.WriteLine("=== Neues Tier anlegen ===");
				Console.Write("Name: ");
				string name = Console.ReadLine();
				Console.Write("Alter: ");
				int alter = Convert.ToInt32(Console.ReadLine());
				Console.Write("Art: ");
				string art = Console.ReadLine();
				Console.Write("Gehege ID: ");
				int gehegeId = Convert.ToInt32(Console.ReadLine());

				Tier neuesTier = new Tier {
					Name = name,
					Art = art,
					Alter = alter,
					GehegeId = gehegeId,
				};
				tierService.Hinzufuegen(neuesTier);
				Console.WriteLine("Neues Tier erfolgreich angelegt!");
				Console.ReadKey();
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

			//Gehege

			void GehegeMenue() {
				bool back = false;
				while (!back) {
					Console.Clear();
					Console.WriteLine("=== Gehegeverwaltung ===");
					Console.WriteLine("1 - Alle Gehege anzeigen");
					Console.WriteLine("2 - Neues Gehege anlegen");
					Console.WriteLine("3 - Gehege löschen");
					Console.WriteLine("4 - Gehege aktualisieren");
					Console.WriteLine("5 - Zurück");
					Console.Write("\nAuswahl:");
					try {
						input = Convert.ToInt32(Console.ReadLine());
					} catch (FormatException) {
						Console.WriteLine("Ungültige Eingabe!");
					}
					switch (input) {
						case 1:
							GehegeAnzeigen();
							break;
						case 2:
							GehegeAnlegen();
							break;
						case 3:
							GehegeLoeschen();
							break;
						case 4:
							GehegeUpdate();
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

			void GehegeAnzeigen() {
				Console.Clear();
				Console.WriteLine("=== Alle Gehege ===");
				if (gehegeService.GetAll().Any()) {
					foreach (var g in gehegeService.GetAll()) {
						Console.WriteLine(g);
					}
				} else {
					Console.WriteLine("Keine Gehege im Zoo vorhanden.");
				}
				Console.ReadLine();
			}

			void GehegeAnlegen() {
				Console.Clear();
				Console.WriteLine("=== Neues Gehege anlegen ===");
				Console.Write("Name des Geheges: ");
				string gehegename = Console.ReadLine();
				Console.Write("Lage: ");
				string lage = Console.ReadLine();
				Console.Write("Anzahl der Tiere: ");
				int anzahlTiere = Convert.ToInt32(Console.ReadLine());

				Gehege neuesGehege = new Gehege {
					Name = gehegename,
					Lage = lage,
					AnzahlTiere = anzahlTiere,
				};
				gehegeService.Hinzufuegen(neuesGehege);
				Console.WriteLine("Neues Gehege erfolgreich angelegt!");
				Console.ReadKey();
			}

			void GehegeLoeschen() {
				Console.Clear();
				Console.WriteLine("=== Gehege löschen ===");
				Console.Write("Geben Sie die ID des zu löschenden Geheges ein: ");
				int id = Convert.ToInt32(Console.ReadLine());
				bool erfolg = gehegeService.Loeschen(id);
				if (erfolg)
					Console.WriteLine("Gehege erfolgreich gelöscht!");
				else
					Console.WriteLine("Gehege mit dieser ID nicht gefunden!");
				Console.ReadLine();
			}

			void GehegeUpdate() {
				Console.Clear();
				Console.WriteLine("=== Gehege aktualisieren ===");
				GehegeAnzeigen();
				Console.Write("Geben Sie die ID des zu aktualisierenden Geheges ein: ");
				int id = Convert.ToInt32(Console.ReadLine());
				var gehege = gehegeService.Suche(id);
				if (gehege == null) {
					Console.WriteLine("Gehege mit dieser ID nicht gefunden!");
					Console.ReadLine();
					return;
				}
				var erfolgreich = gehegeService.Update(gehege);
				Console.Write("Neue Lage (aktuell: {0}): ", gehege.Lage);
				gehege.Lage = Console.ReadLine();
				Console.Write("Neue Anzahl der Tiere (aktuell: {0}): ", gehege.AnzahlTiere);
				gehege.AnzahlTiere = Convert.ToInt32(Console.ReadLine());
				gehegeService.Update(gehege);
				Console.WriteLine("Gehege erfolgreich aktualisiert!");
				Console.ReadLine();
			}

			//Pfleger
			void PflegerMenueAdmin() {
				bool back = false;
				while (!back) {
					Console.Clear();
					Console.WriteLine("=== Pflegerverwaltung ===");
					Console.WriteLine("1 - Alle Pfleger anzeigen");
					Console.WriteLine("2 - Neuen Pfleger anlegen");
					Console.WriteLine("3 - Pfleger löschen");
					Console.WriteLine("4 - Pfleger aktualisieren");
					Console.WriteLine("5 - Zurück");
					Console.Write("\nAuswahl:");

					try {
						input = Convert.ToInt32(Console.ReadLine());
					} catch (FormatException) {
						Console.WriteLine("Ungültige Eingabe!");
					}
					switch (input) {
						case 1:
							PflegerAnzeigen();
							break;
						case 2:
							PflegerAnlegen();
							break;
						case 3:
							PflegerLoeschen();
							break;
						case 4:
							PflegerUpdate();
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

			void PflegerAnzeigen() {
				Console.Clear();
				Console.WriteLine("=== Alle Pfleger ===");
				if (pflegerService.GetAll().Any()) {
					foreach (var p in pflegerService.GetAll())
						Console.WriteLine(p);
				} else {
					Console.WriteLine("Keine Pfleger im Zoo vorhanden.");
				}
				Console.ReadLine();
			}

			void PflegerAnlegen() {
				Console.Clear();
				Console.WriteLine("=== Neuen Pfleger anlegen ===");
				Console.Write("Name: ");
				string name = Console.ReadLine();
				Console.Write("Einsatzort: ");
				string einsatzort = Console.ReadLine();
				Pfleger neuerPfleger = new Pfleger {
					Name = name,
					Einsatzort = einsatzort
				};
				pflegerService.Hinzufuegen(neuerPfleger);
				Console.WriteLine("Neuer Pfleger erfolgreich angelegt!");
				Console.ReadLine();
			}

			void PflegerLoeschen() {
				Console.Clear();
				Console.WriteLine("=== Pfleger löschen ===");
				Console.Write("Geben Sie die ID des zu löschenden Pflegers ein: ");
				int id = Convert.ToInt32(Console.ReadLine());
				bool erfolg = pflegerService.Loeschen(id);
				if (erfolg)
					Console.WriteLine("Pfleger erfolgreich gelöscht!");
				else
					Console.WriteLine("Pfleger mit dieser ID nicht gefunden!");
				Console.ReadLine();
			}

			void PflegerUpdate() {
				Console.Clear();
				Console.WriteLine("=== Pfleger aktualisieren ===");
				PflegerAnzeigen();
				Console.Write("Geben Sie die ID des zu aktualisierenden Pflegers ein: ");
				int id = Convert.ToInt32(Console.ReadLine());
				var pfleger = pflegerService.Suche(id);
				if (pfleger == null) {
					Console.WriteLine("Pfleger mit dieser ID nicht gefunden!");
					Console.ReadLine();
					return;
				}
				var erfolgreich = pflegerService.Update(pfleger);
				Console.Write("Neuer Name (aktuell: {0}): ", pfleger.Name);
				pfleger.Name = Console.ReadLine();
				Console.Write("Neuer Einsatzort (aktuell: {0}): ", pfleger.Einsatzort);
				pfleger.Einsatzort = Console.ReadLine();
				pflegerService.Update(pfleger);
				Console.WriteLine("Pfleger erfolgreich aktualisiert!");
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
