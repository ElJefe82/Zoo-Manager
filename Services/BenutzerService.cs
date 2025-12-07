using System.Security.Cryptography;
using System.Text;
using Zoo_Manager.Models;
using Zoo_Manager.Repository;

namespace Zoo_Manager.Services {
	public class BenutzerService {
		private readonly string _filePath = "benutzer.json";
		private List<Benutzer> _benutzer;
		public Benutzer LoggedInBenutzer { get; private set; }

		public BenutzerService() {
			_benutzer = JsonRepository.LadeDaten<Benutzer>(_filePath);

			// Standard-Admin-Benutzer erstellen, wenn keine Benutzer vorhanden sind
			if (!_benutzer.Any()) {
				_benutzer.Add(new Benutzer {
					Benutzername = "admin",
					PasswortHash = HashPasswort("admin"),
					Rolle = Rolle.Admin,
				});
				JsonRepository.SpeichereDaten(_benutzer, _filePath);
			}
		}

		public bool Login(string benutzername, string passwort) {
			string hash = HashPasswort(passwort);
			var benutzer = _benutzer.FirstOrDefault(b => b.Benutzername == benutzername);

			if (benutzer != null) {
				LoggedInBenutzer = benutzer;
				return true;
			}
			return false;
		}

		private string HashPasswort(string passwort) {
			using var sha256 = SHA256.Create();
			return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(passwort)));
		}
	}
}
