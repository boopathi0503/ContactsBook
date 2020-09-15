using ContactsBook.BusinessLayer.Models;
using ContactsBook.BusinessLayer.Repository;
using GalaSoft.MvvmLight.Command;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ContactBooks.UI.ViewModels
{
    public class ContactListViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        #region Constants
        const string SAVEMESSAGE = "Saved successfully";
        const string UPDATEMESSAGE = "Updated successfully";
        const string DELETEMESSAGE = "Deleted successfully";
        const string CONFIRMMESSAGE = "Are you sure?";
        const string APPNAME = "Contact Books";
        #endregion

        #region Properties
        private int gridSelectedIndex = -1;
        private string errorMessage = string.Empty;
        private Contact selectedContact = new Contact();
        private ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();
        private IContactRepository repository = new ContactRepository();
        public ICommand AddContact { get; set; }
        public ICommand NavigateToMail { get; set; }
        public ICommand DeleteContact { get; set; }
        public ICommand ClearContact { get; set; }
        
        public Contact SelectedContact
        {
            get
            {
                return selectedContact;
            }
            set
            {
                selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }
        public ObservableCollection<Contact> Contacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }
        public int GridSelectedIndex
        {
            get
            {
                return gridSelectedIndex;
            }
            set
            {
                gridSelectedIndex = value;
                OnPropertyChanged(nameof(GridSelectedIndex));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        #endregion

        #region Constructor
        public ContactListViewModel()
        {
            InitializeCommands();
            AddContactsForTest();
            LoadContacts();
        }
        #endregion

        #region Methods & Events
        void InitializeCommands()
        {
            AddContact = new DelegateCommand(AddContactEvent);
            ClearContact = new DelegateCommand(ClearContactEvent);
            NavigateToMail = new RelayCommand<object>(NavigateToMailEvent);
            DeleteContact = new RelayCommand<object>(DeleteContactEvent);
        }

        private void AddContactEvent()
        {
            if (selectedContact.ID == 0)
            {
                repository.AddContact(selectedContact);
                MessageBox.Show(SAVEMESSAGE, APPNAME);
            }
            else
            {
                repository.UpdateContact(selectedContact);
                MessageBox.Show(UPDATEMESSAGE, APPNAME);
            }
            LoadContacts();
            SelectedContact = new Contact();
        }
        private void DeleteContactEvent(object obj)
        {
            int ContactID = int.Parse(obj.ToString());
            if (ContactID > 0 && MessageBox.Show(CONFIRMMESSAGE, APPNAME, MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                repository.DeleteContact(ContactID);
                MessageBox.Show(DELETEMESSAGE, APPNAME);
            }
            LoadContacts();
            SelectedContact = new Contact();
        }
        private void ClearContactEvent()
        {
            GridSelectedIndex = -1;
            SelectedContact = new Contact();
            LoadContacts();
        }
        private void NavigateToMailEvent(object obj)
        {
            string mailto = String.Format("mailto:{0}", obj.ToString());
            Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
        }

        void LoadContacts()
        {
            Contacts = repository.GetAllContacts().ToObservableCollection();
        }


        void AddContactsForTest()
        {
            Contact cnt = new Contact() { 
                Name="Ram krishnan",
                EmailID="ram@company.com",
                PhoneNumber="1234567890",
                NickName="Ram"
            };
            repository.AddContact(cnt);
            cnt = new Contact()
            {
                Name = "SivaRaman",
                EmailID = "siva@company.com",
                PhoneNumber = "4545454545",
                NickName = "Siva"
            };
            repository.AddContact(cnt);
            cnt = new Contact()
            {
                Name = "Preetha",
                EmailID = "preetha@company.com",
                PhoneNumber = "1212121212",
                NickName = "Preetha"
            };
            repository.AddContact(cnt);
        }
        #endregion
    }
}
