using System;
using System.Data;
using ContactsDataAccessLayer;
using Microsoft.SqlServer.Server;


namespace ContactsBusinessLayer
{
    public class ClsCountry
    {
        public enum enMode { AddNew=0, Update=1};
        public enMode Mode = enMode.AddNew;

        public int Id { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }

        public ClsCountry()
        {
            this.Id = -1;
            this.CountryName = "";
            Mode = enMode.AddNew;
        }
        private ClsCountry(int Id, string CountryName, string code, string PhoneCode)
        {
            this.Id = Id;
            this.CountryName = CountryName;
            this.Code = code;
            this.PhoneCode = PhoneCode;
            Mode = enMode.Update;
        }

        private bool _AddNewCountry()
        {
            this.Id = ClsCountryData.AddNewCountry(this.CountryName, this.Code, this.PhoneCode);

            return (this.Id != -1);
        }
        private bool _UpDateCountry()
        {
            return ClsCountryData.UpdateCountry(this.Id, this.CountryName, this.Code, this.PhoneCode);
        }
        public static ClsCountry Find(int Id)
        {
            string CountryName = "";
            string Code = "";
            string PhoneCode = "";
            int CountryId = -1;

            if (ClsCountryData.GetCountryById(Id, ref CountryName, ref Code, ref PhoneCode))
                return new ClsCountry(Id, CountryName, Code, PhoneCode);
            else
                return null;
        }
        public static ClsCountry Find(string CountryName)
        {
            int Id = -1;
            string Code = "";
            string PhoneCode = "";
            if (ClsCountryData.GetCountryByName(ref Id, CountryName, ref Code, ref PhoneCode))
                return new ClsCountry(Id, CountryName, Code, PhoneCode);
            else
                return null;
        }
        
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
               case enMode.Update:
                    return _UpDateCountry();
            }
            return false;
        }

        public static DataTable GetAllCountries()
        {
            return ClsCountryData.GetAllCountries();
        }
        public static bool DeleteCountry(int Id)
        {
            return ClsCountryData.DeleteCountry(Id);
        }

        public static bool IsCountryExist(int Id)
        {
            return ClsCountryData.IsCountryExistById(Id);
        }
        public static bool IsCountryExist(string CountryName)
        {
            return ClsCountryData.IsCountryExistByName(CountryName);
        }




    }
}
