namespace Zoo_Manager.Models {
	internal class Gehege {

		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;  //z.B. "Savane"
		public string Lage { get; set; } = string.Empty;  //Wegbeschreibung z.B. "Links vom Eingang"
		public int AnzahlTiere { get; set; } = 0;         //Anzahl der Tiere im Gehege

		public override string ToString() {
			return $"{Id}) Name: {Name}, Lage: {Lage}, AnzahlTiere: {AnzahlTiere} Tiere";
		}
	}
}
