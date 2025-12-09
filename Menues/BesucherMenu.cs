using Zoo_Manager.Services;

namespace Zoo_Manager.Menues {
	internal class BesucherMenu {
		private BesucherInfoMenu besucherInfoMenu;
		private SpielService spielService;

		public BesucherMenu(BesucherInfoMenu besucherInfoMenu, SpielService spielService) {
			this.besucherInfoMenu = besucherInfoMenu;
			this.spielService = spielService;
		}

		public void ZeigeBesucherMenue() {
			bool back = false;

			while (!back) {

				Console.Clear();
				Console.WriteLine("=== Besucherbereich ===");
				Console.WriteLine("1 - Besucherinfo");
				Console.WriteLine("2 - Tier.Ratespiel");
				Console.WriteLine("3 - Zurück");
				Console.Write("\nAuswahl:");

				switch (Console.ReadLine()!) {

					case "1":
						besucherInfoMenu.BesucherInfo();
						break;

					case "2":
						spielService.Start();
						break;

					case "3":
						back = true;
						break;

					default:
						Console.WriteLine("Ungültige Eingabe! Bitte versuchen Sie es erneut.");
						Console.ReadKey();
						break;
				}
			}
		}
	}
}
