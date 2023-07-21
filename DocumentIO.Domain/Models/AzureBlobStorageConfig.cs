﻿namespace DocumentIO.Domain.Models
{
    public class AzureBlobStorageConfig
    {
        public required string ConnectionString { get; set; }
        public required string ContainerName { get; set; }
    }
}

