﻿namespace UpdateService.Core.Entities.Settings
{
	public abstract class FileTransferSettingsBase
	{
		public string BinariesBasePath { get; set; }
		public string GlobalVersionManifestPath { get; set; }
	}
}
