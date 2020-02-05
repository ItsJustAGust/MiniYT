using Microsoft.Azure.Documents.Client;
using System;

namespace MiniYT.Infra.Data.Context
{
    public class MiniYTContext : IDisposable
    {
        public DocumentClient client;

        public MiniYTContext()
        {
            client = new DocumentClient(new Uri("https://localhost:8081"),
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
        }
        public void Dispose()
        {
            client.Dispose();
        }
    }
}
