﻿namespace InstallationManager.Domain.IisConfiguration
{
	public class PortBinding
	{
		public string Port { get; set; }
		public bool Https { get; set; }
		public string CertificatePath { get; set; }
	}
}
