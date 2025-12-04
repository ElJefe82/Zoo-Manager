namespace Zoo_Manager.Models {
	internal class Tier {

		public int ID { get; set; } = 0;
		public string Name { get; set; } = string.Empty;
		public string Art { get; set; } = string.Empty;
		public int Alter { get; set; } = 0;
		public string Gehege { get; set; } = string.Empty;  // Verweis auf Gehege-Name

		public override string ToString() {
			return $"ID: {ID}, Name: {Name}, Art: {Art}, Alter: {Alter} Jahre, Gehege: {Gehege}";
		}
	}
}
