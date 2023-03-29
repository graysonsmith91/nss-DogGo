using DogGo.Interfaces;
using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _config;

        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walk> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT * FROM Walks;
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walk> walks = new List<Walk>();
                        while (reader.Read())
                        {
                            Walk walk = new Walk
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                            };

                            walks.Add(walk);
                        }

                        return walks;
                    }
                }
            }
        }

        public List<Walk> GetWalksByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT *, d.Name AS DogName FROM Walks w
                        LEFT JOIN Walker wa ON wa.Id = w.WalkerId
                        LEFT JOIN Dog d ON d.Id = w.DogId
                        WHERE wa.Id = @walkerId
            ";

                    cmd.Parameters.AddWithValue("@walkerId", walkerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walk> walks = new List<Walk>();

                        while (reader.Read())
                        {
                            Walk walk = new Walk()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Dog = new Dog()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("DogName"))
                                }
                            };

                            walks.Add(walk);
                        }

                        return walks;
                    }
                }
            }
        }


        //public Dog GetDogById(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                SELECT * FROM Dog
        //                WHERE Id = @id";

        //            cmd.Parameters.AddWithValue("@id", id);

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    Dog dog = new Dog
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                        Name = reader.GetString(reader.GetOrdinal("Name")),
        //                        OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
        //                        Breed = reader.GetString(reader.GetOrdinal("Breed")),
        //                        //Notes
        //                        //ImageUrl
        //                    };

        //                    return dog;
        //                }

        //                return null;
        //            }
        //        }
        //    }
        //}


        //public void AddDog(Dog dog)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //            INSERT INTO Dog ([Name], OwnerId, Breed)
        //            OUTPUT INSERTED.ID
        //            VALUES (@name, @ownerId, @breed);
        //        ";

        //            cmd.Parameters.AddWithValue("@name", dog.Name);
        //            cmd.Parameters.AddWithValue("@ownerId", dog.OwnerId);
        //            cmd.Parameters.AddWithValue("@breed", dog.Breed);
        //            int id = (int)cmd.ExecuteScalar();

        //            dog.Id = id;
        //        }
        //    }
        //}

        //public void UpdateDog(Dog dog)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                    UPDATE Dog
        //                    SET 
        //                        [Name] = @name, 
        //                        OwnerId = @ownerId, 
        //                        Breed = @breed,
        //                        Notes = @notes,
        //                        ImageUrl = @imageUrl
        //                    WHERE Id = @id";

        //            cmd.Parameters.AddWithValue("@name", dog.Name);
        //            cmd.Parameters.AddWithValue("@ownerId", dog.OwnerId);
        //            cmd.Parameters.AddWithValue("@breed", dog.Breed);
        //            cmd.Parameters.AddWithValue("@id", dog.Id);
        //            if (dog.Notes == null)
        //            {
        //                cmd.Parameters.AddWithValue("@notes", DBNull.Value);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@notes", dog.Notes);
        //            }
        //            if (dog.ImageUrl == null)
        //            {
        //                cmd.Parameters.AddWithValue("@imageUrl", DBNull.Value);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@imageUrl", dog.ImageUrl);
        //            }


        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public void DeleteDog(int dogId)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                    DELETE FROM Dog
        //                    WHERE Id = @id
        //                ";

        //            cmd.Parameters.AddWithValue("@id", dogId);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}