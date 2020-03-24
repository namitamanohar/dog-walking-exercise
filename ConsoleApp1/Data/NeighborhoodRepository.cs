using DogWalking.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalking.Data
{

    public class NeighborhoodRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DogWalking;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }



        public List<Neighborhood> GetAllNeighborhoods()
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();


                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT n.Id, n.Name
                    FROM Neighborhood n";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Neighborhood> allNeighborhoods = new List<Neighborhood>();

                    while (reader.Read())
                    {

                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);

                 



                        var neighborhood = new Neighborhood
                        {
                            Id = idValue,
                            Name = NameValue,
                         
                        };

                        allNeighborhoods.Add(neighborhood);
                    }

                    reader.Close();

                    return allNeighborhoods;
                }
            }
        }

    }
}