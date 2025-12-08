namespace Zoo_Manager.Models {
	internal class Afrikasavanne : Gehege {
		public bool HatWasserstelle { get; set; }

		public override string ToString() {
			return base.ToString() + $", Wasserstelle: {(HatWasserstelle ? "Ja" : "Nein")}";
		}
	}
}
