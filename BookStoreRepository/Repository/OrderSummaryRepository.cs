using BookStoreCommon.Book;
using BookStoreCommon.Cart;
using BookStoreCommon.OrderPlaced;
using BookStoreCommon.OrderSummary;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BookStoreRepository.Repository
{
    public class OrderSummaryRepository : IOrderSummaryRepository
    {
        private readonly IConfiguration iconfiguration;
        public OrderSummaryRepository(IConfiguration iconfiguration)
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
        public IEnumerable<SummaryOrder> GetOrderSummary(int UserId, int OrderId)
        {
            try
            {
                Connection();
                List<SummaryOrder> summaryOrder = new List<SummaryOrder>();
                SqlCommand com = new SqlCommand("spOrderSummary", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                com.Parameters.AddWithValue("@OrderId", OrderId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    summaryOrder.Add(
                        new SummaryOrder()
                        {
                            SummaryId = Convert.ToInt32(dr["SummaryId"]),
                            OrderId = Convert.ToInt32(dr["OrderId"]),
                            OrderPlaced = new PlaceOrder()
                            {
                                OrderId = Convert.ToInt32(dr["OrderId"]),
                                CustomerId = Convert.ToInt32(dr["CustomerId"]),
                                CartId = Convert.ToInt32(dr["CartId"]),
                                Cart = new Carts()
                                {
                                    //UserId = Convert.ToInt32(dr["UserId"]),
                                    Count = Convert.ToInt32(dr["Count"]),
                                    Book = new Books()
                                    {
                                        BookId = Convert.ToInt32(dr["BookId"]),
                                        BookName = Convert.ToString(dr["BookName"]),
                                        BookDescription = Convert.ToString(dr["BookDescription"]),
                                        BookAuthor = Convert.ToString(dr["BookAuthor"]),
                                        BookImage = Convert.ToString(dr["BookImage"]),
                                        BookCount = Convert.ToInt32(dr["BookCount"]),
                                        BookPrice = Convert.ToInt32(dr["BookPrice"]),
                                        Rating = Convert.ToInt32(dr["Rating"])

                                    },

                                }

                            }

                        }
                        );
                }
                nlog.LogDebug("Got Order Summary");
                return summaryOrder;
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

