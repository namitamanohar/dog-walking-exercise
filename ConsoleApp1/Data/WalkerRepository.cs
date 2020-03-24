using DogWalking.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalking.Data
{
 
    public class WalkerRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DogWalking;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }



        public List<Walker> GetAllWalkers()
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();


                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT w.Id, w.Name, w.NeighborhoodId, n.Id, n.Name AS NeighborhoodName
                    FROM Walker w
                    LEFT JOIN Neighborhood n
                    ON n.Id = w.NeighborhoodId";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> allWalkers = new List<Walker>();

                    while (reader.Read())
                    {

                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);

                        int neighborhoodIdColumn = reader.GetOrdinal("NeighborhoodId");
                        int neighborhoodValue = reader.GetInt32(neighborhoodIdColumn);


                        int NeighborhoodNameColumn= reader.GetOrdinal("NeighborhoodName");
                        string NeighborhoodNameValue = reader.GetString(NeighborhoodNameColumn);



                        var walker = new Walker
                        {
                            Id = idValue,
                            Name = NameValue,
                            NeighborhoodId = neighborhoodValue,
                            Neighborhood = new Neighborhood()
                            {
                                Id = neighborhoodValue,
                                Name = NeighborhoodNameValue
                            }
                        };

                        allWalkers.Add(walker);
                    }

                    reader.Close();

                    return allWalkers;
                }
            }
        }
        //change this to return a LIST of walkers by neighborhood 
        public List<Walker> GetWalkerByNeighborhood(int neighborhoodId)
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();


                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"
                    SELECT w.Id, w.Name, w.NeighborhoodId, n.Id, n.Name AS NeighborhoodName
                    FROM Walker w
                    LEFT JOIN Neighborhood n
                    ON n.Id = w.NeighborhoodId
                    WHERE w.NeighborhoodId = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", neighborhoodId));

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> AllWalkersByNeighborhood = new List<Walker>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);

                        int neighborhoodIdColumn = reader.GetOrdinal("NeighborhoodId");
                        int neighborhoodValue = reader.GetInt32(neighborhoodIdColumn);


                        int NeighborhoodNameColumn = reader.GetOrdinal("NeighborhoodName");
                        string NeighborhoodNameValue = reader.GetString(NeighborhoodNameColumn);

                        var walker = new Walker
                        {
                            Id = idValue,
                            Name = NameValue,
                            NeighborhoodId = neighborhoodValue,
                            Neighborhood = new Neighborhood()
                            {
                                Id = neighborhoodValue,
                                Name = NeighborhoodNameValue
                            }
                        };

                        AllWalkersByNeighborhood.Add(walker);
                    };
                    
                        reader.Close();

                        return AllWalkersByNeighborhood;
                }
            }
        }
        public Walker CreateNewWalker(Walker walkerToAdd)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Walker (Name, NeighborhoodId)
                        OUTPUT INSERTED.Id
                        VALUES (@Name, @NeighborhoodId)";

                    cmd.Parameters.Add(new SqlParameter("@Name", walkerToAdd.Name));
                    cmd.Parameters.Add(new SqlParameter("@NeighborhoodId", walkerToAdd.NeighborhoodId));

                    int id = (int)cmd.ExecuteScalar();

                    walkerToAdd.Id = id;

                    return walkerToAdd;
                }
            }
        }



    }


}
