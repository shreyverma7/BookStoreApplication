using BookStoreCommon.Book;
using BookStoreCommon.Wishlist;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly IConfiguration iconfiguration;
        public WishlistRepository(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        NlogOperation nlog = new NlogOperation();
        public async Task<int> AddWishlist(Wishlist wishlist ,int userId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddWishlist", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", wishlist.BookId);
                com.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("Added to Wishlist");
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public bool DeleteWishlist(int UserId, int BookId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spDeleteWishList", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", BookId);
                com.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Deleted from Wishlist");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                nlog.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public IEnumerable<Wishlist> GetAllWishList(int UserId)
        {
            try
            {
                Connection();
                List<Wishlist> wishlist = new List<Wishlist>();
                SqlCommand com = new SqlCommand("spGetAllWishList", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    wishlist.Add(
                        new Wishlist()
                        {
                            BookId = Convert.ToInt32(dr["BookId"]),
                            //UserId = Convert.ToInt32(dr["UserId"]),
                            // WishlistId = Convert.ToInt32(dr["WishlistId"]),
                            book = new Books()
                            {
                                BookId = Convert.ToInt32(dr["BookId"]),
                                BookName = dr["BookName"].ToString(),
                                BookDescription = dr["BookDescription"].ToString(),
                                BookAuthor = dr["BookAuthor"].ToString(),
                                BookImage = Convert.ToString(dr["BookImage"]),
                                BookPrice = Convert.ToInt32(dr["BookPrice"]),
                                Rating = Convert.ToInt32(dr["Rating"])
                            },
                            IsAvailable = Convert.ToBoolean(dr["IsAvailable"])
                        }
                        );
                }
                nlog.LogDebug("Got all in Wishlist");
                return wishlist;
            }
            catch (Exception ex)
            {
                nlog.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
