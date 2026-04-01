#nullable enable

using PhoneBookMapApp.Core;
using PhoneBookMapApp.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace phone_book_map_dot_net
{
    public partial class Form1 : Form
    {
        private readonly clsPhoneBookEngine _engine = new clsPhoneBookEngine();

        public Form1()
        {
            InitializeComponent();

           // LoadSampleData();

            // Load from file instead of Sample Data
            var data = clsFileHelper.LoadData();
            if (data != null) _engine.LoadContacts(data);

            RefreshContactsGrid();
        }

        //private void LoadSampleData()
        //{
        //    _engine.AddContact(new clsContact("Alice Johnson", "555-1234", "alice@example.com"));
        //    _engine.AddContact(new clsContact("Bob Smith", "555-5678", "bob@example.com"));
        //    _engine.AddContact(new clsContact("Charlie Brown", "555-8765", "charlie@example.com"));
        //}

        private void RefreshContactsGrid()
        {
            var contacts = _engine.GetAllContacts().ToList();
            dgvContacts.DataSource = null;
            dgvContacts.DataSource = contacts;
        }

        private void SearchContacts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                RefreshContactsGrid();
                return;
            }

            // Direct search using Map (O(log n)) - very fast!
            var foundContact = _engine.FindContact(searchTerm);

            if (foundContact != null)
            {
                dgvContacts.DataSource = new List<clsContact> { foundContact };
            }
            else
            {
                // If not found by exact name, we can fall back to a general search 
                // or just show an empty list
                dgvContacts.DataSource = null;
            }
        }

        private void ClearInputFields()
        {
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSearch.Text = string.Empty;
        }

        // Email validation using a simple regex
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Enforce that the email ends with .com
            string pattern = @"^[^@\s]+@[^@\s]+\.com$";
            return Regex.IsMatch(email, pattern);
        }

        // Phone validation: allows digits, dashes, spaces, plus, parentheses
        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Allow digits, dashes, spaces, plus, parentheses
            string pattern = @"^[\d\s\-\(\)\+]+$";
            return Regex.IsMatch(phone, pattern);
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Phone number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (!IsValidPhone(txtPhone.Text.Trim()))
            {
                MessageBox.Show("Phone number can only contain digits, dashes, spaces, plus signs, and parentheses.",
                    "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Please enter a valid email address (e.g., name@domain.com).",
                    "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            // Get the next valid ID from the engine before creating the object
            int nextId = _engine.GetNextID();
            var newContact = new clsContact(nextId, txtName.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim());

            if (_engine.AddContact(newContact))
            {
                RefreshContactsGrid();
                ClearInputFields();
                clsFileHelper.SaveData(_engine.GetAllContacts());
                MessageBox.Show("Contact added successfully!", "Success");
            }
            else
            {
                MessageBox.Show("A contact with this name already exists.", "Duplicate");
            }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (dgvContacts.CurrentRow == null)
            {
                MessageBox.Show("Please select a contact to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedContact = dgvContacts.CurrentRow.DataBoundItem as clsContact;
            if (selectedContact == null)
            {
                MessageBox.Show("Invalid contact data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var originalName = selectedContact.Name;

            if (!ValidateInputs()) return;

            // Check if name changed and new name already exists
            if (!originalName.Equals(txtName.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                if (_engine.FindContact(txtName.Text.Trim()) != null)
                {
                    MessageBox.Show("A contact with the new name already exists.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // If name changed, remove old and add new; otherwise just update properties
            if (!originalName.Equals(txtName.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                _engine.DeleteContact(originalName);

                var updatedContact = new clsContact(selectedContact.ID, txtName.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim());
                _engine.AddContact(updatedContact);
            }
            else
            {
                selectedContact.Name = txtName.Text.Trim();
                selectedContact.PhoneNumber = txtPhone.Text.Trim();
                selectedContact.Email = txtEmail.Text.Trim();
            }

            RefreshContactsGrid();
            ClearInputFields();

            clsFileHelper.SaveData(_engine.GetAllContacts());
            MessageBox.Show("Contact updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

       
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvContacts.CurrentRow == null)
            {
                MessageBox.Show("Please select a contact to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedContact = dgvContacts.CurrentRow.DataBoundItem as clsContact;
            if (selectedContact == null)
            {
                MessageBox.Show("Invalid contact data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete {selectedContact.Name}?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _engine.DeleteContact(selectedContact.Name);
                RefreshContactsGrid();
                ClearInputFields();
                clsFileHelper.SaveData(_engine.GetAllContacts());
                MessageBox.Show("Contact deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            SearchContacts(txtSearch.Text.Trim());
        }

        private void BtnShowAll_Click(object? sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            RefreshContactsGrid();
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void DgvContacts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvContacts.CurrentRow != null)
            {
                var contact = dgvContacts.CurrentRow.DataBoundItem as clsContact;
                if (contact != null)
                {
                    txtName.Text = contact.Name;
                    txtPhone.Text = contact.PhoneNumber;
                    txtEmail.Text = contact.Email;
                }
            }
        }
    }
}