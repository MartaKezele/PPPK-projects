using Azure.Storage.Blobs;
using System;
using System.Configuration;

namespace RemoteFileStorage.Dao
{
    static class Repository
    {
        private const string ContainerName = "blobcontainer";
        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        private static Lazy<BlobContainerClient> container = new Lazy<BlobContainerClient>(
            () => new BlobServiceClient(cs).GetBlobContainerClient(ContainerName));

        public static BlobContainerClient Container => container.Value;
    }
}
