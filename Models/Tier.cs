namespace Zoo_Manager.Models {
	internal class Tier {
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Art { get; set; } = string.Empty;
		public int Alter { get; set; }
		public string GehegeName { get; set; } = string.Empty; // Verweis auf Gehege

		public override string ToString() {
			return $"{Id}) Name: {Name}, Art: {Art}, Alter: {Alter} Jahre, Gehege: {GehegeName}";
		}
	}
}
