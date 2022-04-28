using RPTestingExample_PlainXunit.Services;
using Xunit;

namespace RPTestingExample_PlainUnit.Tests
{
    public class StorageServiceTest
    {
        private readonly StorageService _storageServ = new StorageService();


        [Fact]
        public void Test1()
        {
            //Arrange

            // Act
            _storageServ.AddToStorage("addthis");

            // Assert
            Assert.True(_storageServ?.isInList("addthis"));


        }
    }
}