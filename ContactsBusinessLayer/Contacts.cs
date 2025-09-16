using System;
using System.Data;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class ClsContact
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirht { get; set; }
        public int CountryId { get; set; }
     
        public string ImagePath { get; set; }


        public ClsContact()
            {
            ID = -1;
            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
            Address = "";
            DateOfBirht = DateTime.Now;
            CountryId = -1;
            ImagePath = "";

            Mode = enMode.AddNew;
            }
        private ClsContact(int ID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryId,string ImagePath)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirht = DateOfBirth;
            this.CountryId = CountryId;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;

        }


        private bool _AddNewContact()
        {
            this.ID = ClsContactsData.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirht, this.CountryId, this.ImagePath);

            return (this.ID != -1);

        }
        private bool _UpDateContact()
        {
            return ClsContactsData.UpDateContact(this.ID, this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirht, this.CountryId, this.ImagePath);

        }

        public static ClsContact Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryId = -1;

            if (ClsContactsData.GetContactByInfoById(ID, ref FirstName, ref LastName, ref Email, ref Phone, ref Address,  ref DateOfBirth,ref CountryId, ref ImagePath))
                return new ClsContact(ID, FirstName, LastName, Email, Phone, Address,DateOfBirth, CountryId,ImagePath);
            else
                return null;

        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewContact())
                    {
                         Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                     return _UpDateContact();

            }
            return false;
        }
        public static DataTable GetAllContacts()
        {
            return ClsContactsData.GetAllContacts();
        }
        public static bool DeleteContact(int Id)
        {
            return ClsContactsData.DeleteContact(Id);
        }
        public static bool IsContactExist(int Id)
        {
            return ClsContactsData.IsContactExist(Id);
        }
    }
}
