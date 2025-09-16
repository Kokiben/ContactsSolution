using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;

namespace ContactsDataAccessLayer
{
    public class ClsCountryData
    {
        public static bool GetCountryById(int Id, ref string CountryName, ref string Code, ref string PhoneCode)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = "Select * from Countries Where CountryId=@CountryId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryId", Id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    CountryName = (string)reader["CountryName"];

                    if (reader["Code"] != DBNull.Value)
                    {
                        Code = (string)reader["Code"];
                    }
                    else
                    {
                        Code = "";
                    }
                    if (reader["PhoneCode"] != DBNull.Value)
                    {
                        PhoneCode = (string)reader["PhoneCode"];
                    }
                    else
                    {
                        PhoneCode = "";
                    }
                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static bool GetCountryByName(ref int Id, string CountryName, ref string Code, ref string PhoneCode)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);

            string query = "Select * from Countries Where CountryName=@CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    Id = (int)reader["CountryId"];

                    if (reader["Code"] != DBNull.Value)
                    {
                        Code = (string)reader["Code"];
                    }
                    else
                    {
                        Code = "";
                    }
                    if (reader["PhoneCode"] != DBNull.Value)
                    {
                        PhoneCode = (string)reader["PhoneCode"];
                    }
                    else
                    {
                        PhoneCode = "";
                    }
                    
                }
                else
                {
                    IsFound = false;
                }
                reader.Close();

            }
            catch(Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;

        }

        public static int AddNewCountry(string CountryName,string Code, string PhoneCode)
        {
            int CountryId = -1;
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = @"Insert Into Countries (CountryName, Code, PhoneCode)
                           VALUES (@CountryName, @Code, @PhoneCode);
                           Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            if (Code != "")
                command.Parameters.AddWithValue("@Code", Code);
            else
                command.Parameters.AddWithValue("@Code", DBNull.Value);
            if (PhoneCode != "")
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            else
                command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);


                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedId))
                    {
                        CountryId = insertedId;
                    }


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            return CountryId;
        }

        public static bool UpdateCountry(int Id, string CountryName, string Code, string PhoneCode)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = @"Update Countries
                           SET CountryName =@CountryName,
                               Code=@Code,
                               PhoneCode=@PhoneCode
                              Where CountryId = @CountryId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@CountryId", Id);
            command.Parameters.AddWithValue("@Code", Code);
            command.Parameters.AddWithValue("@PhoneCode", Code);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);

                 
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = "SELECT * from Countries Order by CountryName";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();

            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool DeleteCountry(int Id)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = @"delete Countries WHERE CountryId=@CountryId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryId", Id);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }


        public static bool IsCountryExistById(int Id)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = "Select found=1 from Countries Where CountryId=@CountryId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryId", Id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                IsFound = reader.HasRows;
                reader.Close();

            }
            catch(Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();

            }
            return IsFound;
        }
        public static bool IsCountryExistByName(string CountryName)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = "Select found=1 from Countries Where CountryName=@CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;     
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        
    }
}
