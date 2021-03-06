﻿using System;
using System.Net;

namespace ServiceConfigurator.Entities
{
    [Serializable]
    public enum ServiceType
    {
        Master,
        Slave
    }

    [Serializable]
    public class ServiceConfiguration
    {
        public ServiceType Type { get; set; }

        public string Name { get; set; }

        public IPEndPoint IpEndPoint { get; set; }

        public bool LoggingEnabled { get; set; }

        public string FilePath { get; set; } // pretend that we have only one file in app config ... and this is *.xml                                   
    }
}
