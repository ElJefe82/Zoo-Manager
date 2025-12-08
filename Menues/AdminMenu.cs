using Zoo_Manager.Services;

namespace Zoo_Manager.Menues {
	internal class AdminMenu {

		private TierMenu tierMenu;
		private GehegeMenu gehegeMenu;
		private PflegerMenu pflegerMenu;
		private TierService tierService;
		private PflegerVerwaltungMenu pflegerVerwaltungMenu;
		private SpielService spielService;

		public AdminMenu(TierMenu tierMenu, GehegeMenu gehegeMenu, PflegerMenu pflegerMenu, TierService tierService, PflegerVerwaltungMenu pflegerVerwaltungMenu, SpielService spielService) {
			this.tierMenu = tierMenu;
			this.gehegeMenu = gehegeMenu;
			this.pflegerMenu = pflegerMenu;
			this.tierService = tierService;
			this.pflegerVerwaltungMenu = pflegerVerwaltungMenu;
			this.spielService = spielService;
		}

		public void ZeigeAdminMenue() {
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
				var input = 0;
				try {
					input = Convert.ToInt32(Console.ReadLine());
				} catch (FormatException) {
					Console.WriteLine("Ungültige Eingabe!");
				}
				switch (input) {
					case 1:
						tierMenu.TierMenue();
						break;
					case 2:
						gehegeMenu.GehegeMenue();
						break;
					case 3:
						pflegerVerwaltungMenu.PflegerMenueAdmin();
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
