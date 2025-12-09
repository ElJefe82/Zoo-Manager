namespace Zoo_Manager.Models {
	internal class Pfleger {

		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Einsatzort { get; set; } = string.Empty;  //z.B. Reptilienhaus

		public override string ToString() {
			return $"{Id}) Name: {Name}, Einsatzort: {Einsatzort}";
		}
	}
}
