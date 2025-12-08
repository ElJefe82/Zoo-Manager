using Zoo_Manager.Models;
using Zoo_Manager.Services;

namespace Zoo_Manager.Menues {
	internal class PflegerVerwaltungMenu {

		private PflegerService pflegerService;

		public PflegerVerwaltungMenu(PflegerService pflegerService) {
			this.pflegerService = pflegerService;
		}

		public void PflegerMenueAdmin() {
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
				var input = 0;
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
	}
}
