using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Zoo_Manager {
	internal class Program {
		static readonly string BenutzerDatei = "mitarbeiter.json";
		static List<Benutzer> Benutzer = new();
		static Benutzer? AktiverBenutzer = null;

		static void Main(string[] args) {
			LadeBenutzer();
			ErstelleAdmin();

			while (true) // bis Benutzer 4 drückt
			{
				Console.Clear();
				Console.WriteLine("=======ZooManager=======");
				Console.WriteLine("1 - Admin anmelden");
				Console.WriteLine("2 - Pfleger anmelden");
				Console.WriteLine("3 - Besucher");
				Console.WriteLine("4 - Beenden");
				Console.Write("\nAuswahl:");

				var wahl = Console.ReadLine();

				if (wahl == "1") {
					Login("Admin");
					Console.WriteLine("sdkfjadklflsjdf");
				} else if (wahl == "2") { Login("Pfleger"); } else if (wahl == "3") {
					Console.WriteLine("Besucherbereich ohne Anmeldung kommt vorraussichtlich in Tag 5");
					Console.ReadKey();
				} else if (wahl == "4")
					return;
			}
		}

		//====================LOGIN====================

		static void Login(string rolle) {
			Console.Clear();
			Console.Write("Benutzername: ");
			var name = Console.ReadLine();
			Console.Write("Passwort: ");
			var passwort = PasswortLesen();

			var user = Benutzer.FirstOrDefault(b => b.Benutzername == name && b.Rolle == rolle);
			if (user == null) {
				Console.WriteLine("Benutzer nicht gefunden");
				Console.ReadKey();
				return;
			}

			var salt = Convert.FromBase64String(user.Salt);
			var hash = HashPasswort(passwort!, salt);

			if (Convert.ToBase64String(hash) == user.PasswortHash) {
				AktiverBenutzer = user;
				BenutzerVerwaltung();
			} else {
				Console.WriteLine("Falsches Passwort");
				Console.ReadKey();
			}
		}

		static string PasswortLesen() {
			var sb = new StringBuilder();
			while (true) {
				var key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
					break;
				if (key.Key == ConsoleKey.Backspace && sb.Length > 0) {
					sb.Remove(sb.Length - 1, 1);
					Console.Write("\b \b");
				} else {
					sb.Append(key.KeyChar);
					Console.Write("*");
				}
			}
			Console.WriteLine();
			return sb.ToString();
		}

		static byte[] ErzeugeSalt() {
			byte[] s = new byte[16];
			RandomNumberGenerator.Fill(s);
			return s;
		}

		static byte[] HashPasswort(string passwort, byte[] salt) {
			return SHA256.HashData(Encoding.UTF8.GetBytes(passwort).Concat(salt).ToArray());
		}

		//====================BENUTZERVERWALTUNG====================

		static void BenutzerVerwaltung() {
			while (true) {
				Console.Clear();
				Console.WriteLine("=== Benutzerverwaltung ===");
				Console.WriteLine("1) Benutzer anzeigen");
				Console.WriteLine("2) Benutzer anlegen");
				Console.WriteLine("3) Benutzer löschen");
				Console.WriteLine("4) Logout");

				var w = Console.ReadLine();

				if (w == "1")
					BenutzerAnzeigen();
				else if (w == "2")
					BenutzerAnlegen();
				else if (w == "3")
					BenutzerLöschen();
				else if (w == "4") { AktiverBenutzer = null; return; }
			}
		}

		static void BenutzerAnzeigen() {
			Console.Clear();
			foreach (var b in Benutzer)
				Console.WriteLine($"{b.Benutzername} ({b.Rolle})");
			Console.ReadKey();
		}

		static void BenutzerAnlegen() {
			Console.Clear();
			Console.Write("Benutzername: ");
			var name = Console.ReadLine();

			if (Benutzer.Any(b => b.Benutzername == name)) {
				Console.WriteLine("Benutzer existiert bereits.");
				Console.ReadKey();
				return;
			}

			Console.Write("Passwort: ");
			var pw = PasswortLesen();

			Console.Write("Rolle (Admin/Pfleger): ");
			var rolle = Console.ReadLine();

			if (rolle != "Admin" && rolle != "Pfleger") {
				Console.WriteLine("Ungültige Rolle.");
				Console.ReadKey();
				return;
			}

			var salt = ErzeugeSalt();
			var hash = HashPasswort(pw, salt);

			Benutzer.Add(new Benutzer {
				Benutzername = name!,
				Rolle = rolle!,
				Salt = Convert.ToBase64String(salt),
				PasswortHash = Convert.ToBase64String(hash)
			});

			SpeichernBenutzer();
			Console.WriteLine("Benutzer angelegt.");
			Console.ReadKey();
		}

		static void BenutzerLöschen() {
			Console.Clear();
			Console.Write("Benutzername löschen: ");
			var name = Console.ReadLine();
			var benutzer = Benutzer.FirstOrDefault(b => b.Benutzername == name);

			if (benutzer == null)
				Console.WriteLine("Nicht gefunden.");
			else {
				Benutzer.Remove(benutzer);
				SpeichernBenutzer();
				Console.WriteLine("Benutzer gelöscht.");
			}
			Console.ReadKey();
		}

		static void LadeBenutzer() {
			if (!File.Exists(BenutzerDatei))
				return;
			Benutzer = JsonConvert.DeserializeObject<List<Benutzer>>(File.ReadAllText(BenutzerDatei))
			?? [];
		}

		static void SpeichernBenutzer() {
			File.WriteAllText(BenutzerDatei, JsonConvert.SerializeObject(Benutzer, Newtonsoft.Json.Formatting.Indented));
		}

		static void ErstelleAdmin() {
			if (!Benutzer.Any(b => b.Rolle == "Admin")) {
				var salt = ErzeugeSalt();
				Benutzer.Add(new Benutzer {
					Benutzername = "admin",
					Rolle = "Admin",
					Salt = Convert.ToBase64String(salt),
					PasswortHash = Convert.ToBase64String(HashPasswort("admin", salt))
				});
				SpeichernBenutzer();
			}
		}
	}
}
