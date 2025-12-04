using Zoo_Manager.Ablage;
using Zoo_Manager.Models;

namespace Zoo_Manager.Services {
	public class TierService {

		private readonly string _filePath = "Daten/tiere.json";
		private List<Tier> _tiere;

		public TierService() {

			_tiere = JsonRepository.LadeDaten<Tier>(_filePath);
		}

		public List<Tier> GetAll() => _tiere;

		public void TierHinzufügen(Tier tier) {

			tier.ID = _tiere.Any() ? _tiere.Max(t => t.ID) + 1 : 1;
			_tiere.Add(tier);
			JsonRepository.SpeichereDaten(_tiere, _filePath);
		}

		public void TierLöschen(int id) {
			var tier = _tiere.FirstOrDefault(t => t.ID == id);
			if (tier != null) {
				_tiere.Remove(tier);
				JsonRepository.SpeichereDaten(_tiere, _filePath);
			}
		}

		public void UpdateTier(Tier updatedTier) {
			var tier = _tiere.FirstOrDefault(t => t.ID == updatedTier.ID);
			if (tier != null) {

				tier.Name = updatedTier.Name;
				tier.Art = updatedTier.Art;
				tier.Alter = updatedTier.Alter;
				tier.Gehege = updatedTier.Gehege;
				JsonRepository.SpeichereDaten(_tiere, _filePath);
			}
		}
	}
}