using Zoo_Manager.Services;

namespace Zoo_Manager.Menues {
	internal class BesucherInfoMenu {
		private TierService tierService;
		private GehegeService gehegeService;

		public BesucherInfoMenu(TierService tierService, GehegeService gehegeService) {
			this.tierService = tierService;
			this.gehegeService = gehegeService;
		}

		public void BesucherInfo() {
			Console.Clear();
			Console.WriteLine("=== Besucherinfo ===");
			Console.WriteLine("Willkommen im Zoo! Hier finden Sie Informationen über unsere Tiere und Gehege.");
			Console.WriteLine("\nDas sind unsere Tiere:\n");
			foreach (var t in tierService.GetAll()) {
				Console.WriteLine(t);
			}

			Console.WriteLine("\nDiese Gehege gibt es:\n");
			foreach (var t in gehegeService.GetAll()) {
				Console.WriteLine(t);
			}
			Console.ReadLine();
		}
	}
}
