using Zoo_Manager.Models;
using Zoo_Manager.Repository;

namespace Zoo_Manager.Services {
	internal class GehegeService : IService {
		public readonly string _filePath = "gehege.json";
		private List<Gehege> _gehege;

		public GehegeService() {
			_gehege = JsonRepository.LadeDaten<Gehege>(_filePath);
		}

		public List<Gehege> GetAll() => _gehege;
		public void Hinzufuegen(Gehege gehege) {
			gehege.Id = _gehege.Any() ? _gehege.Max(g => g.Id) + 1 : 1;
			_gehege.Add(gehege);
			JsonRepository.SpeichereDaten(_gehege, _filePath);
		}

		public bool Loeschen(int id) {
			var gehege = _gehege.FirstOrDefault(g => g.Id == id);
			if (gehege != null) {
				_gehege.Remove(gehege);
				JsonRepository.SpeichereDaten(_gehege, _filePath);
				return true;
			}
			return false;
		}

		public bool Update(Gehege updatedGehege) {
			var gehege = _gehege.FirstOrDefault(g => g.Id == updatedGehege.Id);
			if (gehege != null) {
				gehege.Name = gehege.Name;
				gehege.Lage = updatedGehege.Lage;
				gehege.AnzahlTiere = updatedGehege.AnzahlTiere;
				JsonRepository.SpeichereDaten(_gehege, _filePath);
				return true;
			}
			return false;
		}

		public Gehege? Suche(int gehegeId) {
			return _gehege.FirstOrDefault(g => g.Id == gehegeId);
		}
	}
}
