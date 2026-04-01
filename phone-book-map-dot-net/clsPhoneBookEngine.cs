#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace PhoneBookMapApp.Core
{
    public class clsPhoneBookEngine
    {
        private SortedDictionary<string, clsContact> _contacts = new SortedDictionary<string, clsContact>();

        // Logic to get the next unique ID based on existing data
        public int GetNextID()
        {
            if (_contacts.Count == 0) return 1;
            return _contacts.Values.Max(c => c.ID) + 1;
        }

        public void LoadContacts(List<clsContact> loadedContacts)
        {
            _contacts.Clear();
            foreach (var contact in loadedContacts)
            {
                if (!_contacts.ContainsKey(contact.Name))
                {
                    _contacts.Add(contact.Name, contact);
                }
            }
        }

        public bool AddContact(clsContact contact)
        {
            if (!_contacts.ContainsKey(contact.Name))
            {
                _contacts.Add(contact.Name, contact);
                return true;
            }
            return false;
        }

        public clsContact? FindContact(string name)
        {
            _contacts.TryGetValue(name, out clsContact? contact);
            return contact;
        }

        public IEnumerable<clsContact> GetAllContacts() => _contacts.Values;

        public bool DeleteContact(string name) => _contacts.Remove(name);
    }
}