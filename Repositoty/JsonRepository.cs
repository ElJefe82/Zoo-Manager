using System.Text.Json;

namespace Zoo_Manager.Repository {
	public static class JsonRepository {

		public static List<T> LadeDaten<T>(string filePath) {
			if (!File.Exists(filePath)) {
				return new List<T>();
			}
			string json = File.ReadAllText(filePath);
			return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
		}

		public static void SpeichereDaten<T>(List<T> daten, string filePath) {
			string json = JsonSerializer.Serialize(daten, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(filePath, json);
		}
	}
}
