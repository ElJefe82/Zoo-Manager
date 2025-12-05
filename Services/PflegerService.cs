using Zoo_Manager.Models;
using Zoo_Manager.Repository;

namespace Zoo_Manager.Services {
	public class PflegerService {

		public readonly string _filePath = "Daten/pfleger.json";
		private List<Pfleger> _pfleger;

		public PflegerService() {

			_pfleger = JsonRepository.LadeDaten<Pfleger>(_filePath);
		}

		public List<Pfleger> GetAll() => _pfleger;

		public void Hinzufügen(Pfleger pfleger) {

			pfleger.Id = _pfleger.Any() ? _pfleger.Max(p => p.Id) + 1 : 1;
			_pfleger.Add(pfleger);
			JsonRepository.SpeichereDaten(_pfleger, _filePath);
		}

		public void Löschen(int id) {

			var pfleger = _pfleger.FirstOrDefault(p => p.Id == id);
			if (pfleger != null) {
				_pfleger.Remove(pfleger);
				JsonRepository.SpeichereDaten(_pfleger, _filePath);
			}
		}
	}
}
