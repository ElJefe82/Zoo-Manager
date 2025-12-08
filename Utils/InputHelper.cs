namespace Zoo_Manager.Utils {
	public static class InputHelper {
		public static string ReadNonEmptyString(string prompt) {
			while (true) {
				Console.Write(prompt);
				var eingabe = Console.ReadLine();
				if (!string.IsNullOrWhiteSpace(eingabe))
					return eingabe.Trim();
				Console.WriteLine("Eingabe darf nicht leer sein.");
			}
		}

		public static int ReadInt(string prompt) {
			while (true) {
				Console.Write(prompt);
				var eingabe = Console.ReadLine();
				if (int.TryParse(eingabe, out var zahl))
					return zahl;
				Console.WriteLine("Bitte eine gültige ganze Zahl eingeben.");
			}
		}

		public static double ReadDouble(string prompt) {
			while (true) {
				Console.Write(prompt);
				var eingabe = Console.ReadLine();
				if (double.TryParse(eingabe, out var zahl))
					return zahl;
				Console.WriteLine("Bitte eine gültige Zahl eingeben (z.B. 12.5).");
			}
		}

		public static bool WaehleAus(string prompt) {
			Console.Write($"{prompt} (j/n): ");
			var auswahl = Console.ReadLine()?.Trim().ToLower();
			return auswahl == "j" || auswahl == "y";
		}
	}
}
