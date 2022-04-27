namespace RPTestingExample.Interfaces
{
    public interface IStorageService
    {
        public void AddToStorage(string stringToAdd);
        public void ClearStorage();
        public bool isInList(string check);

    }
}
