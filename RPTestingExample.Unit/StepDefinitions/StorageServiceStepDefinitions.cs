using RPTestingExample.Interfaces;
using RPTestingExample.Services;

namespace RPTestingExample.Unit.StepDefinitions
{
    [Binding]
    public sealed class StorageServiceStepDefinitions
    {
        private IStorageService ?storageServiceToTest;

        [Given(@"I have an instance of the storage service")]
        public void GivenIHaveAnInstanceOfTheStorageService()
        {
            storageServiceToTest = new StorageService();
        }

        [Given(@"I start with an empty storage")]
        public void GivenIStartWithAnEmptyStorage()
        {
            storageServiceToTest?.ClearStorage();
        }

        [Given(@"I add '([^']*)' to the list")]
        public void GivenThenIAddToTheList(string stringToAdd)
        {
            storageServiceToTest?.AddToStorage(stringToAdd);
        }

        [Then(@"the string '([^']*)' should be in the list")]
        public void ThenTheStringShouldBeInTheList(string stringToCheck)
        {
            Assert.True(storageServiceToTest?.isInList(stringToCheck));
        }

    }
}