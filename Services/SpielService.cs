namespace Zoo_Manager.Services {
	internal class SpielService {

		private readonly TierService _tierService;
		private const int MaxVersuche = 5;

		public SpielService(TierService tierService) {
			_tierService = tierService;
		}

		public void Start() {

			Console.WriteLine("===== Tier-Ratespiel =====");
			Console.WriteLine($"Errate das Zootier! Du hast {MaxVersuche} Versuche.");

			var tiere = _tierService.GetAll();
			if (!tiere.Any()) {

				Console.WriteLine("Dieses Tier gibt es nicht im Zoo!");
				Console.ReadKey();
				return;
			}

			var rnd = new Random();
			var wahlTier = tiere[rnd.Next(tiere.Count)].Name.ToLower();
			var tip = new HashSet<char>();
			int versuche = MaxVersuche;

			while (versuche > 0) {

				Console.Clear();
				Console.WriteLine("===== Tier-Ratespiel =====");
				Console.WriteLine($"Versuche übrig: {versuche}");
				Console.WriteLine();

				//vorhandene Buchstaben anzeigen
				string display = new string(wahlTier.Select(c => tip.Contains(c) ? c : '_').ToArray());
				Console.WriteLine($"Tier: {display}");
				Console.Write("Buchstabe eingeben oder Tier erraten: ");
				string eingabe = Console.ReadLine()!.ToLower();

				//Ganzes Wort raten				
				if (eingabe == wahlTier) {

					Console.WriteLine($"Glückwunsch! Du hast das Tier '{wahlTier}' erraten!");
					Console.ReadKey();
					return;
				}

				//Einzelnen Buchstaben raten
				if (eingabe.Length == 1) {

					char buchstabe = eingabe[0];

					if (tip.Contains(buchstabe)) {

						Console.WriteLine("Buchstabe wurde bereits versucht.");
						Console.ReadKey();
						continue;
					}

					tip.Add(buchstabe);
					if (!wahlTier.Contains(buchstabe)) {

						versuche--;
					}
				} else {

					Console.WriteLine("Ungültige Eingabe!");
					Console.ReadKey();
				}

				//Prüfen ob gewonnen
				if (wahlTier.All(c => tip.Contains(c))) {

					Console.WriteLine($"Glückwunsch! Du hast das Tier '{wahlTier}' erraten!");
					Console.ReadKey();
					return;
				}
			}

			//Spiel verloren
			Console.WriteLine("Keine Versuche mehr!");
			Console.WriteLine($"Leider verloren! Das gesuchte Tier war '{wahlTier}'.");
			Console.ReadKey();
		}
	}
}
