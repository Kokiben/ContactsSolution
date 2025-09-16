using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class ClsContactsData
    {
        public static bool GetContactByInfoById(int Id, ref string FirstName, ref string LastName, ref string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth, ref int CountryId, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString );
            string query = "Select * from Contacts WHERE ContactId = @ContactId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactId", Id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    IsFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryId = (int)reader["CountryId"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
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


        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryId, string ImagePath)
        {
            int ContactId = -1;

            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = @"Insert Into Contacts (FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryId, ImagePath)
                           VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryId, @ImagePath)
                            Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryId", CountryId);
            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString(), out int Inserted))
                {
                    ContactId = Inserted;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return ContactId;
            
        }

        public static bool UpDateContact(int Id, string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryId, string ImagePath)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Contacts
                           SET  FirstName=@FirstName,
                                LastName=@LastName,
                                Email=@Email,
                                Phone=@Phone,
                                Address=@Address,
                                DateOfBirth=@DateOfBirth,
                                CountryId=@CountryId,
                                ImagePath=@ImagePath
                                Where ContactId=@ContactId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactId", Id);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryId", CountryId);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);

        }

        public static bool DeleteContact(int Id)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = @"Delete Contacts 
                             Where ContactId = @ContactId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactId", Id);
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

        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = "Select * from Contacts";

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
        public static bool IsContactExist(int Id)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ClsDataAccessSettings.ConnectionString);
            string query = "Select Found=1  From Contacts Where ContactId = @ContactId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactId", Id);
            
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

    }
}
