using System;
using System.Data;
using ContactsBusinessLayer;

namespace ContactsConsolApp_PresentationLayer
{
    public class Program
    {


        static void TestFindContact(int ID)
        {
            ClsContact contact = ClsContact.Find(ID);
            if(contact != null)

            {
                Console.WriteLine(contact.FirstName + " " + contact.LastName);
                Console.WriteLine(contact.Email);
                Console.WriteLine(contact.Phone);
                Console.WriteLine(contact.Address);
                Console.WriteLine(contact.DateOfBirht);
                Console.WriteLine(contact.CountryId);
                Console.WriteLine(contact.ImagePath);

            }
            else
            {
                Console.WriteLine("Contact with Id [" + ID + "] not found!");
            }

        }
        static void TestAddNewContact()
        {
            ClsContact contact = new ClsContact();

            contact.FirstName = "Soso";
            contact.LastName = "JIJI";
            contact.Email = "Jiji@gmail.com";
            contact.Phone = "0112548";
            contact.Address = "5 O Street";
            contact.DateOfBirht = new DateTime(2004, 05, 10, 10, 30, 00);

            contact.CountryId = 1;
            contact.ImagePath = "C:\\Pictures\\laila.JPG";

            if(contact.Save())
            {
                Console.WriteLine("Contact Added Succefully id " + contact.ID);
            }
        }
        static void TestUpDateContact(int Id)
        {
            ClsContact contact = ClsContact.Find(Id);

            if(contact != null)
            {
                contact.FirstName = "kiko";
                contact.LastName = "fandi";
                contact.Email = "Fandi@gmail.com";
                contact.Address = "9 Z Street";
                contact.DateOfBirht = new DateTime(2007, 06, 05, 10, 30, 00);
                contact.CountryId = 1;
                contact.ImagePath = "";

                if (contact.Save())
                {
                    Console.WriteLine("Contact Updated Successfully");
                    
                }
            }
        }
        static void TestDeleteContact(int Id)
        {
            if (ClsContact.IsContactExist(Id))
           
            if (ClsContact.DeleteContact(Id))
                Console.WriteLine("Contact deleted Successfully!");
            else
                Console.WriteLine("Faild to delete Contact.");
            else
                Console.WriteLine("No, Contact is not there.");
        }
        static void ListContacts()
        {
            DataTable dt = ClsContact.GetAllContacts();

            Console.WriteLine("Contact Data : ");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["ContactId"]}, {row["FirstName"]}, {row["LastName"]}");
                
            }
        }
        static void TestIsContactExist(int Id)
        {
            if (ClsContact.IsContactExist(Id))
                Console.WriteLine("Yes, contact is there.");
            else
                Console.WriteLine("No, Contact is not there.");
        }

        static void TestFindCountryById(int Id)
        {
            ClsCountry Country = ClsCountry.Find(Id);
            if(Country != null)
            { 
                Console.WriteLine(Country.CountryName);
            }
            else
            {
                Console.WriteLine("Country [" + Id + "] Not found!");
            }
        }
        static void TestFindCountryByName(string CountryName)
        {
            ClsCountry country = ClsCountry.Find(CountryName);
            if(country != null)
            {
                Console.WriteLine("Country [" + CountryName+ "] isfound with id = " + country.Id);
                
            }
            else
            {
                Console.WriteLine("Country [" + CountryName + "] Not found!");
            }
        }
        static void TestAddNewCountry()
        {
            ClsCountry country = new ClsCountry();
            country.CountryName = "Morocco";
            if(country.Save())
            {
                Console.WriteLine("Country added successfully  with Id = " + country.Id);
            }


        }
        static void TestUpDateCountry(int Id)
        {
            ClsCountry country = ClsCountry.Find(Id);

            if(country != null)
            {
                country.CountryName = "Morocco1";

                if(country.Save())
                {
                    Console.WriteLine("Country UpDate SuccessFully");
                }
            }
            else
            {
                Console.WriteLine("Country is you want to update is not found!");
            }

           
        }
        static void ListCountries()
        {
            DataTable dt = ClsCountry.GetAllCountries();
            Console.WriteLine("Countries Data : ");

            foreach(DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["CountryId"]}, {row["CountryName"]}");
            }
        }
        static void DeleteCountry(int Id)
        {
            if (ClsCountry.DeleteCountry(Id))
                Console.WriteLine("Country Deleted Successfully!");
            else
                Console.WriteLine("faild to delete : the country With id : [" + Id + "] not found!");
        }

        static void TestCountryExist(int Id)
        {
            if (ClsCountry.IsCountryExist(Id))
                Console.WriteLine("Yes, Country Is there.");
            else
                Console.WriteLine("No, Country Is not there.");
        }
        static void TestCountryExsit(string CountryName)
        {
            if (ClsCountry.IsCountryExist(CountryName))
                Console.WriteLine("Yes, Country is there.");
            else
                Console.WriteLine("No, Counrty is not there.");
        }



        static void Main(string[] args)
        {
            //TestFindContact(1);
            // TestAddNewContact();
            // TestUpDateContact(5);
            // TestDeleteContact(10);
            // ListContacts();
            // TestIsContactExist(2);
            //TestIsContactExist(100);


            // TestFindCountryById(1);
            //TestFindCountryByName("Canada");
            // TestAddNewCountry();
            // TestUpDateCountry(7);
            // ListCountries();
            // DeleteCountry(8);

            TestCountryExist(100);
            TestCountryExsit("Morocco");
        }
    }
}
