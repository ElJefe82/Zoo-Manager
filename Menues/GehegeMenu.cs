using Zoo_Manager.Models;
using Zoo_Manager.Services;
using Zoo_Manager.Utils;

namespace Zoo_Manager.Menues {
	internal class GehegeMenu {
		private GehegeService gehegeService;

		public GehegeMenu(GehegeService gehegeService) {
			this.gehegeService = gehegeService;
		}

		public void GehegeMenue() {
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
				var input = 0;
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
			string gehegename = InputHelper.ReadNonEmptyString("Name des Geheges: ");
			string lage = InputHelper.ReadNonEmptyString("Lage: ");
			int anzahlTiere = InputHelper.ReadInt("Anzahl der Tiere: ");

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
			GehegeAnzeigen();
			int id = InputHelper.ReadInt("Geben Sie die ID des zu löschenden Geheges ein:");
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
			int id = InputHelper.ReadInt("Geben Sie die ID des zu aktualisierenden Geheges ein: ");
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
	}
}
