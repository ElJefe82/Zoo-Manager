namespace Zoo_Manager.Models {
	internal class Loewe : Tier {
		public bool IstRudelfuehrer { get; set; }

		public override string ToString() {
			return base.ToString() + $", Rudelfuehrer: {(IstRudelfuehrer ? "Ja" : "Nein")}";
		}
	}
}