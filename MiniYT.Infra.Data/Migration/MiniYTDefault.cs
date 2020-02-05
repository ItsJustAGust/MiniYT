using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Net;

namespace MiniYT.Infra.Data.Migration
{
    public class MiniYTDefault
    {
        private static string nameCollection;
        private static string Collection = "DataBase";


        private static DocumentClient Client()
        {
            DocumentClient client = new DocumentClient(new Uri("https://localhost:8081"),
                                                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            return client;
        }

        public async static void CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                var retorno = await Client().ReadDatabaseAsync(UriFactory.CreateDatabaseUri("MiniYTDB"));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await Client().CreateDatabaseAsync(new Database { Id = "MiniYTDB" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async static void CreateDocumentCollectionIfNotExistsAsync()
        {
            try
            {
                nameCollection = Collection;
                await Client().ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri("MiniYTDB", Collection));

            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    DocumentCollection collectionInfo2 = new DocumentCollection();
                    collectionInfo2.Id = Collection;

                    collectionInfo2.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });

                    await Client().CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri("MiniYTDB"),
                        new DocumentCollection { Id = Collection },
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
