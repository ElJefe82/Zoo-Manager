using Zoo_Manager.Models;
using Zoo_Manager.Repository;

namespace Zoo_Manager.Services {
	internal class TierService : IService {
		private readonly string _filePath = "tiere.json";
		private List<Tier> _tiere;

		public TierService() {
			_tiere = JsonRepository.LadeDaten<Tier>(_filePath);
		}

		public List<Tier> GetAll() => _tiere;
		public void Hinzufuegen(Tier tier) {
			if (_tiere.Any()) {
				tier.Id = _tiere.Max(t => t.Id) + 1;
			} else {
				tier.Id = 1;
			}
			_tiere.Add(tier);
			JsonRepository.SpeichereDaten(_tiere, _filePath);
		}

		public bool Loeschen(int id) {
			var tier = _tiere.FirstOrDefault(t => t.Id == id);
			if (tier != null) {
				_tiere.Remove(tier);
				JsonRepository.SpeichereDaten(_tiere, _filePath);
				return true;
			}
			return false;
		}

		public bool Update(Tier updatedTier) {
			var tier = _tiere.FirstOrDefault(t => t.Id == updatedTier.Id);
			if (tier != null) {
				tier.Name = tier.Name;
				tier.Art = updatedTier.Art;
				tier.Alter = updatedTier.Alter;
				tier.GehegeName = updatedTier.GehegeName;
				JsonRepository.SpeichereDaten(_tiere, _filePath);
				return true;
			}
			return false;
		}

		public Tier? Suche(int tierId) {
			return _tiere.FirstOrDefault(t => t.Id == tierId);
		}
	}
}