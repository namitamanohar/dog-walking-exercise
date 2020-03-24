using DogWalking.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalking.Data
{

    public class OwnerRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DogWalking;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }



        public List<Owner> GetAllOwners()
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();


                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT o.Id, o.Name, o.Address, o.NeighborhoodId, n.Id, n.Name
                    FROM Owner o
                    LEFT JOIN Neighborhood n
                    ON n.Id = o.NeighborhoodId";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Owner> allOwners= new List<Owner>();

                    while (reader.Read())
                    {

                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);

                        int AddressColumnPosition = reader.GetOrdinal("Address");
                        string AddressValue = reader.GetString(AddressColumnPosition);

                        int PhoneColumnPosition = reader.GetOrdinal("Phone");
                        string PhoneValue = reader.GetString(PhoneColumnPosition);

                        int neighborhoodIdColumn = reader.GetOrdinal("NeighborhoodId");
                        int neighborhoodValue = reader.GetInt32(neighborhoodIdColumn);


                        int NeighborhoodNameColumn = reader.GetOrdinal("Name");
                        string NeighborhoodNameValue = reader.GetString(NeighborhoodNameColumn);



                        var owner = new Owner
                        {
                            Id = idValue,
                            Name = NameValue,
                            Address = AddressValue,
                            Phone = PhoneValue, 
                            NeighborhoodId = neighborhoodValue,
                            Neighborhood = new Neighborhood()
                            {
                                Id = neighborhoodValue,
                                Name = NeighborhoodNameValue
                            }
                        };

                        allOwners.Add(owner);
                    }

                    reader.Close();

                    return allOwners;
                }
            }
        }

       
        public Owner AddNewOwner(Owner ownerToAdd)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Owner (Name, Address, Phone, NeighborhoodId)
                        OUTPUT INSERTED.Id
                        VALUES (@Name, @Address, @Phone, @NeighborhoodId)";

                    cmd.Parameters.Add(new SqlParameter("@Name", ownerToAdd.Name));
                    cmd.Parameters.Add(new SqlParameter("@Address", ownerToAdd.Name));
                    cmd.Parameters.Add(new SqlParameter("@Phone", ownerToAdd.Name));
                    cmd.Parameters.Add(new SqlParameter("@NeighborhoodId", ownerToAdd.NeighborhoodId));

                    int id = (int)cmd.ExecuteScalar();

                    ownerToAdd.Id = id;

                    return ownerToAdd;
                }
            }
        }



    }


}
