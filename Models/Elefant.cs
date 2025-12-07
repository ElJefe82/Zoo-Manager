namespace Zoo_Manager.Models {
	internal class Elefant : Tier {
		public double RuesselLaenge { get; set; }

		public override string ToString() {
			return base.ToString() + $", Rüssel-Länge: {RuesselLaenge} m";
		}
	}
}