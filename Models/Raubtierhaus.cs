namespace Zoo_Manager.Models {
	internal class Raubtierhaus : Gehege {
		public bool HatZaun { get; set; }

		public override string ToString() {
			return base.ToString() + $", Zaun: {(HatZaun ? "Ja" : "Nein")}";
		}
	}
}
