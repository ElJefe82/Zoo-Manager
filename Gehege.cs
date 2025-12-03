namespace Zoo_Manager {
	internal class Gehege {

		public string Name { get; set; } = string.Empty;
		public string Lage { get; set; } = string.Empty;
		public int Kapazität { get; set; } = 0;

		public override string ToString() {
			return $"Name: {Name}, Lage: {Lage}, Kapazität: {Kapazität} Tiere";
		}
	}
}
