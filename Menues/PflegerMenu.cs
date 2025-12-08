namespace Zoo_Manager.Menues {
	internal class PflegerMenu {
		private GehegeMenu gehegemenu;
		private BesucherInfoMenu besucherInfoMenu;

		public PflegerMenu(GehegeMenu gehegemenu, BesucherInfoMenu besucherInfoMenu) {
			this.gehegemenu = gehegemenu;
			this.besucherInfoMenu = besucherInfoMenu;
		}

		public void PflegerMenue() {

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
						gehegemenu.GehegeMenue();
						break;

					case "2":
						besucherInfoMenu.BesucherInfo();
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
	}
}
