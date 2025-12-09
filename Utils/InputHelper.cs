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
	}
}
