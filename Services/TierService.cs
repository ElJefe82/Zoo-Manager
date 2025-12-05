using Zoo_Manager.Models;
using Zoo_Manager.Repository;

namespace Zoo_Manager.Services {
	public class TierService {

		private readonly string _filePath = "Daten/tiere.json";
		private List<Tier> _tiere;

		public TierService() {

			_tiere = JsonRepository.LadeDaten<Tier>(_filePath);
		}

		public List<Tier> GetAll() => _tiere;

		public void Hinzufügen(Tier tier) {

			tier.Id = _tiere.Any() ? _tiere.Max(t => t.Id) + 1 : 1;
			_tiere.Add(tier);
			JsonRepository.SpeichereDaten(_tiere, _filePath);
		}

		public void Löschen(int id) {

			var tier = _tiere.FirstOrDefault(t => t.Id == id);
			if (tier != null) {
				_tiere.Remove(tier);
				JsonRepository.SpeichereDaten(_tiere, _filePath);
			}
		}

		public void Update(Tier updatedTier) {

			var tier = _tiere.FirstOrDefault(t => t.Id == updatedTier.Id);
			if (tier != null) {

				tier.Name = updatedTier.Name;
				tier.Art = updatedTier.Art;
				tier.Alter = updatedTier.Alter;
				tier.GehegeId = updatedTier.GehegeId;
				JsonRepository.SpeichereDaten(_tiere, _filePath);
			}
		}
	}
}