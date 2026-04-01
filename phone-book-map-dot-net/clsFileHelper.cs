using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using PhoneBookMapApp.Core;

namespace PhoneBookMapApp.Utilities
{
    public static class clsFileHelper
    {
        private static string _filePath = "contacts.json";

        // Save all contacts to a JSON file
        public static void SaveData(IEnumerable<clsContact> contacts)
        {
            string jsonString = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonString);
        }

        // Load contacts from the JSON file
        public static List<clsContact>? LoadData()
        {
            if (!File.Exists(_filePath)) return null;

            string jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<clsContact>>(jsonString);
        }
    }
}