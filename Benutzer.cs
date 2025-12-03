namespace Zoo_Manager {
	internal record Benutzer {
		public string Benutzername { get; set; } = "";
		public string Passwort { get; set; } = string.Empty;
		public Rolle Rolle { get; set; } = Rolle.Gast;
	}
}
