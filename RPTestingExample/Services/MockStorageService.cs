using RPTestingExample.Interfaces;
using System;
using System.Collections.Generic;

namespace RPTestingExample.Services
{
    public class MockStorageService : IStorageService
    {
        private List<string> storedString;
        public MockStorageService()
        {
            storedString = new List<string>();
        }
        public void AddToStorage(string stringToAdd)
        {
            Console.WriteLine("This had added to the mock store");
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
