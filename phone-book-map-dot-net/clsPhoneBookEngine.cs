using System.Collections.Generic;

namespace PhoneBookMapApp.Core
{
    public class clsPhoneBookEngine
    {
        // Using SortedDictionary to keep contacts automatically sorted alphabetically by Name.
        // This gives us a highly efficient O(log n) time complexity for searching.
        private SortedDictionary<string, clsContact> _contacts = new SortedDictionary<string, clsContact>();

        // Adds a new contact if the name does not already exist
        public bool AddContact(clsContact contact)
        {
            if (!_contacts.ContainsKey(contact.Name))
            {
                _contacts.Add(contact.Name, contact);
                return true; // Added successfully
            }
            return false; // Contact already exists
        }

        // Fast search using the Map key (Name)
        public clsContact FindContact(string name)
        {
            if (_contacts.TryGetValue(name, out clsContact contact))
            {
                return contact; // Found
            }
            return null; // Not found
        }

        // Retrieves all contacts to display in the UI
        public IEnumerable<clsContact> GetAllContacts()
        {
            return _contacts.Values;
        }

        // Deletes a contact by name
        public bool DeleteContact(string name)
        {
            return _contacts.Remove(name);
        }
    }
}