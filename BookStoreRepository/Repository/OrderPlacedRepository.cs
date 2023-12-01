using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class OrderPlacedRepository : IOrderPlacedRepository
    {
        private readonly IConfiguration iconfiguration;
        public OrderPlacedRepository(IConfiguration iconfiguration)
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
        public async Task<int> PlaceOrder(int UserId, int CartId, int CustomerId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spPlaceOrder", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CustomerId", CustomerId);
                com.Parameters.AddWithValue("@CartId", CartId);
                com.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("Order Placed");
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
    }
}
