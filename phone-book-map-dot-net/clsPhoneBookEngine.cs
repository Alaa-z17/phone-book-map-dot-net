#nullable enable

using System.Collections.Generic;

namespace PhoneBookMapApp.Core
{
    public class clsPhoneBookEngine
    {
        private SortedDictionary<string, clsContact> _contacts = new SortedDictionary<string, clsContact>();

        public bool AddContact(clsContact contact)
        {
            if (!_contacts.ContainsKey(contact.Name))
            {
                _contacts.Add(contact.Name, contact);
                return true;
            }
            return false;
        }

        // Return type is nullable because a contact may not be found.
        public clsContact? FindContact(string name)
        {
            _contacts.TryGetValue(name, out clsContact? contact);
            return contact; // contact may be null
        }

        public IEnumerable<clsContact> GetAllContacts()
        {
            return _contacts.Values;
        }

        public bool DeleteContact(string name)
        {
            return _contacts.Remove(name);
        }
    }
}