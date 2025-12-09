using Zoo_Manager.Models;
using Zoo_Manager.Services;
using Zoo_Manager.Utils;

namespace Zoo_Manager.Menues {
	internal class TierMenu {
		private TierService tierService;
		public TierMenu(TierService tierService) {
			this.tierService = tierService;
		}

		public void TierMenue() {
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
				var input = 0;
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
			string name = InputHelper.ReadNonEmptyString("Name: ");
			int alter = InputHelper.ReadInt("Alter: ");
			string art = InputHelper.ReadNonEmptyString("Art: ");
			string gehegename = InputHelper.ReadNonEmptyString("Gehegename: ");

			Tier neuesTier = new Tier {
				Name = name,
				Art = art,
				Alter = alter,
				GehegeName = gehegename,
			};
			tierService.Hinzufuegen(neuesTier);
			Console.WriteLine("Neues Tier erfolgreich angelegt!");
			Console.ReadKey();
		}

		void TierLoeschen() {
			Console.Clear();
			Console.WriteLine("=== Tier löschen ===");
			TiereAnzeigen();
			int id = InputHelper.ReadInt("Geben Sie die ID des zu löschenden Tiers ein: ");
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
			int id = InputHelper.ReadInt("Geben Sie die ID des zu aktualisierenden Tiers ein: ");

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
			Console.Write("Neue Gehege ID (aktuell: {0}): ", tier.GehegeName);
			tier.GehegeName = Console.ReadLine();
			tierService.Update(tier);
			Console.WriteLine("Tier erfolgreich aktualisiert!");
			Console.ReadLine();
		}
	}
}
