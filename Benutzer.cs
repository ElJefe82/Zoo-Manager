using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Manager
{
	internal class Benutzer
	{
		public string Benutzername { get; set; } = "";
		public string Rolle { get; set; } = "";
		public string PasswortHash { get; set; } = "";  // ChatGPT nach Infos gefragt
		public string Salt { get; set; } = "";   // ChatGPT nach Infos gefragt
	}
}
