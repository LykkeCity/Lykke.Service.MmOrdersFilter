﻿using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.MmOrdersFilter.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
