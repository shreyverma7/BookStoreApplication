using BookStoreCommon.CustomerDetail;
using BookStoreCommon.TypeModel;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BookStoreRepository.Repository
{
    public class CustomerDetailRepository : ICustomerDetailRepository
    {
        private readonly IConfiguration iconfiguration;
        public CustomerDetailRepository(IConfiguration iconfiguration)
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
        public CustomerDetails AddAddress(CustomerDetails customerDetails, int userId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddAddressDetails", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TypeId", customerDetails.TypeId);
                com.Parameters.AddWithValue("@UserId", userId);
                com.Parameters.AddWithValue("@FullName", customerDetails.FullName);
                com.Parameters.AddWithValue("@MobileNumber", customerDetails.MobileNumber);
                com.Parameters.AddWithValue("@Address", customerDetails.Address);
                com.Parameters.AddWithValue("@CityOrTown", customerDetails.CityOrTown);
                com.Parameters.AddWithValue("@State", customerDetails.State);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                nlog.LogDebug("Customer Details Added");
                return customerDetails;

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
        public bool DeleteAddress(int CustomerId, int UserId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spDeleteAddress", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CustomerId", CustomerId);
                com.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Customer Details Deleted");
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
        public IEnumerable<CustomerDetails> GetAllAddress(int UserId)
        {
            try
            {
                Connection();
                List<CustomerDetails> customerDetails = new List<CustomerDetails>();
                SqlCommand com = new SqlCommand("spGetAllAddress", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    customerDetails.Add(
                        new CustomerDetails()
                        {
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            UserId = Convert.ToInt32(dr["UserId"]),
                            TypeId = Convert.ToInt32(dr["TypeId"]),
                            FullName = Convert.ToString(dr["FullName"]),
                            MobileNumber = Convert.ToInt32(dr["MobileNumber"]),
                            Address = Convert.ToString(dr["Address"]),
                            CityOrTown = Convert.ToString(dr["CityOrTown"]),
                            State = Convert.ToString(dr["State"]),
                            TypeModel = new TypesModel()
                            {
                                TypeId = Convert.ToInt32(dr["TypeId"]),
                                TypeName = dr["TypeName"].ToString(),
                            },
                        }
                        );
                }
                nlog.LogDebug("Got all Customer Details");
                return customerDetails;
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
        public bool UpdateAddress(CustomerDetails obj, int userId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spUpdateAddress", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
                com.Parameters.AddWithValue("@UserId", userId);
                com.Parameters.AddWithValue("@TypeId", obj.TypeId);
                com.Parameters.AddWithValue("@FullName", obj.FullName);
                com.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
                com.Parameters.AddWithValue("@Address", obj.Address);
                com.Parameters.AddWithValue("@CityOrTown", obj.CityOrTown);
                com.Parameters.AddWithValue("@State", obj.State);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Customer Details Updated");
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
    }
}
