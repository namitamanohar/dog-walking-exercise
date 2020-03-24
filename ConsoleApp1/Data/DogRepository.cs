using DogWalking.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalking.Data
{
    /// <summary>
    ///  An object to contain all database interactions.
    /// </summary>
    public class DogRepository
    {
        /// <summary>
        ///  Represents a connection to the database.
        ///   This is a "tunnel" to connect the application to the database.
        ///   All communication between the application and database passes through this connection.
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DogWalking;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }




        /// <summary>
        ///  Returns a list of all departments in the database
        /// </summary>
        public List<Dog> GetAllDogs()
        {
          
            using (SqlConnection conn = Connection)
            {
               
                conn.Open();

             
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT d.Id, d.Name, d.OwnerId, o.Id, o.Name, d.Breed, d.Notes 
                    FROM Dog d
                    LEFT JOIN Owner o
                    ON o.Id = d.OwnerId";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Dog> allDogs = new List<Dog>();

                    // Read() will return true if there's more data to read
                    while (reader.Read())
                    {
                    
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);

                        int ownerIdColumn = reader.GetOrdinal("OwnerId");
                        int ownerValue = reader.GetInt32(ownerIdColumn);

                        int BreedColumnPosition = reader.GetOrdinal("Breed");
                        string BreedValue = reader.GetString(BreedColumnPosition);

                        int NotesColumnPosition = reader.GetOrdinal("Notes");
                        string NotesValue = reader.GetString(NotesColumnPosition);

                        int OwnerNameColumnPosition = reader.GetOrdinal("Name");
                        string OwnerNameValue = reader.GetString(OwnerNameColumnPosition);



                        // Now let's create a new department object using the data from the database.
                        var dog = new Dog
                        {
                            Id = idValue,
                            Name = NameValue, 
                            Breed = BreedValue, 
                            Notes = NotesValue, 
                            OwnerId = ownerValue, 
                            Owner = new Owner()
                            {
                                Id = ownerValue,
                                Name = OwnerNameValue
                            }
                        };

                        // ...and add that department object to our list.
                        allDogs.Add(dog);
                    }

                    // We should Close() the reader. Unfortunately, a "using" block won't work here.
                    reader.Close();

                    // Return the list of departments who whomever called this method.
                    return allDogs;
                }
            }
        }

        public Dog GetDogById(int dogId)
        {
          
            using (SqlConnection conn = Connection)
            {
              
                conn.Open();

              
                using (SqlCommand cmd = conn.CreateCommand())
                {
                
                    cmd.CommandText = @"
                    SELECT d.Id, d.Name, d.OwnerId, o.Id, o.Name, d.Breed, d.Notes 
                    FROM Dog d
                    LEFT JOIN Owner o
                    ON o.Id = d.OwnerId
                    WHERE d.Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", dogId));

                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);

                        int ownerIdColumn = reader.GetOrdinal("OwnerId");
                        int ownerValue = reader.GetInt32(ownerIdColumn);

                        int BreedColumnPosition = reader.GetOrdinal("Breed");
                        string BreedValue = reader.GetString(BreedColumnPosition);

                        int NotesColumnPosition = reader.GetOrdinal("Notes");
                        string NotesValue = reader.GetString(NotesColumnPosition);

                        int OwnerNameColumnPosition = reader.GetOrdinal("Name");
                        string OwnerNameValue = reader.GetString(OwnerNameColumnPosition);

                        // Now that all the data is parsed, we create a new C# object
                        var dog = new Dog
                        {
                            Id = idValue,
                            Name = NameValue,
                            Breed = BreedValue,
                            Notes = NotesValue,
                            OwnerId = ownerValue,
                            Owner = new Owner()
                            {
                                Id = ownerValue,
                                Name = OwnerNameValue
                            }
                        };
                        // Now we can close the reader
                        reader.Close();

                        return dog;
                    }
                    else
                    {
                        // We didn't find the employee with that ID in the database. return null
                        return null;
                    }
                }
            }
        }


    }


}
