using RPTestingExample_PlainXunit.Interfaces;
using System;
using System.Collections.Generic;

namespace RPTestingExample_PlainXunit.Services
{
    public class StorageService : IStorageService
    {
        private List<string> storedString;

        public StorageService()
        {
            storedString = new List<string>();
        }
        public void AddToStorage(string stringToAdd)
        {
            Console.WriteLine("This had added to the real store");
            // this is the real call out to azure storage;
            storedString.Add(stringToAdd);
        }
        public void ClearStorage()
        {
            storedString.Clear();
        }

        public bool isInList(string check)
        {
            return storedString.Contains(check);
        }
    }
}
