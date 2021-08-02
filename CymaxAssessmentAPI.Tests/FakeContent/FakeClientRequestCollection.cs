using CymaxAssessmentAPI.Models;

namespace CymaxAssessmentAPI.Tests.FakeContent
{
    public static class FakeClientRequestCollection
    {
        public static ClientRequestAPI1 GetFakeRequestAPI1()
        {
            var clientRequest = new ClientRequestAPI1()
            {
                ContactAddress = "ContactAddress",
                WarehouseAddress = "WarehouseAddress",
                PackageDimensions = new int[] { 1, 2, 3 }
            };

            return clientRequest;
        }

        public static ClientRequestAPI1 GetFakeEmptyRequestAPI1()
        {
            return new ClientRequestAPI1();
        }

        public static ClientRequestAPI2 GetFakeRequestAPI2()
        {
            var clientRequest = new ClientRequestAPI2()
            {
                Consignee = "Consignee",
                Consignor = "Consignor",
                Cartons = new int[] { 1, 2, 3 }
            };

            return clientRequest;
        }

        public static ClientRequestAPI3 GetFakeRequestAPI3()
        {
            var clientRequest = new ClientRequestAPI3()
            {
                Source = "Source",
                Destination = "Destination",
                Packages = new Packages[] { new Packages() { Package = 1 }, new Packages() { Package = 2 }, new Packages() { Package = 3 } }
            };

            return clientRequest;
        }
    }
}
