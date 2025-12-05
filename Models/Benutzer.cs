namespace Zoo_Manager.Models {
	public record Benutzer {
		public string Benutzername { get; set; } = string.Empty;
		public string PasswortHash { get; set; } = string.Empty;
		public Rolle Rolle { get; set; } = Rolle.Gast;
	}
}
