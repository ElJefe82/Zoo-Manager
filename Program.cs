using Zoo_Manager.Menues;
using Zoo_Manager.Models;
using Zoo_Manager.Services;

namespace Zoo_Manager {
	internal class Program {
		static void Main(string[] args) {

			//SERVICES INITIALISIEREN
			var tierService = new TierService();
			var gehegeService = new GehegeService();
			var pflegerService = new PflegerService();
			var benutzerService = new BenutzerService();
			var spielService = new SpielService(tierService);

			//MENÜS INITIALISIEREN
			var tierMenu = new TierMenu(tierService);
			var gehegeMenu = new GehegeMenu(gehegeService);
			var besucherInfoMenu = new BesucherInfoMenu(tierService, gehegeService);
			var pflegermenu = new PflegerMenu(gehegeMenu, besucherInfoMenu);
			var pflegerVerwaltungMenu = new PflegerVerwaltungMenu(pflegerService);
			var adminMenu = new AdminMenu(tierMenu, gehegeMenu, pflegermenu, tierService, pflegerVerwaltungMenu, spielService);
			var besucherMenu = new BesucherMenu(besucherInfoMenu, spielService);

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
						besucherMenu.ZeigeBesucherMenue();
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
						adminMenu.ZeigeAdminMenue();
						break;

					case Rolle.Pfleger:
						pflegermenu.PflegerMenue();
						break;

					default:
						besucherMenu.ZeigeBesucherMenue();
						break;
				}
			}
		}
	}
}