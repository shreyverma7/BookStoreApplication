using BookStoreCommon.User;
using BookStoreCommon.UserRegister;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NlogImplementation;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration iconfiguration;
        public UserRepository(IConfiguration iconfiguration)
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
        public async Task<int> UserRegistration(UserRegister obj)
        {
            var password = EncryptPassword(obj.Password);
            obj.Password = password;
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spUserRegistration", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@FullName", obj.FullName);
                com.Parameters.AddWithValue("@EmailId", obj.EmailId);
                com.Parameters.AddWithValue("@password", password);
                com.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
                com.Parameters.AddWithValue("@IsAdmin", obj.IsAdmin);
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("User Registered");
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
        public string UserLogin(string email, string password)
        {
            UserRegister userregister = GetUser(email);
            try
            {
                var decryptPassword = DecryptPassword(userregister.Password);
                if (userregister != null && decryptPassword.Equals(password))
                {
                    var token = GenerateSecurityToken(userregister.EmailId, userregister.UserId, userregister.IsAdmin);
                    return token;
                }
                nlog.LogDebug("User Logged In");
                return null;
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
        public UserRegister GetUser(string email)
        {
            var obj = new UserRegister();
            try
            {
                Connection();
                con.Open();
                SqlCommand com = new SqlCommand("spGetUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmailId", email);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    obj = new UserRegister
                    {
                        UserId = (int)reader["UserId"],
                        FullName = (string)reader["FullName"],
                        EmailId = (string)reader["EmailId"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"],
                        IsAdmin = (string)reader["IsAdmin"]
                    };
                }
                con.Close();
                nlog.LogDebug("Got User Details");
                return obj;
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
        public string ForgetPassword(string email)
        {
            try
            {
                var emailcheck = GetUser(email);
                if (emailcheck != null)
                {
                    var token = GenerateSecurityToken(emailcheck.EmailId, emailcheck.UserId, emailcheck.IsAdmin);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token, email);
                    nlog.LogDebug("Reset Email Send");
                    return token;
                }
                else
                {
                    return null;
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
        public UserRegister ResetPassword(string email, string newpassword, string confirmpassword)
        {
            try
            {
                var userregister = GetUser(email);
                if (newpassword.Equals(confirmpassword))
                {
                    var input = userregister;
                    var password = EncryptPassword(newpassword);
                    input.Password = password;
                    if (input != null)
                    {
                        Connection();
                        SqlCommand com = new SqlCommand("spResetPassword", con);
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@FullName", input.FullName);
                        com.Parameters.AddWithValue("@EmailId", input.EmailId);
                        com.Parameters.AddWithValue("@password", password);
                        com.Parameters.AddWithValue("@MobileNumber", input.MobileNumber);
                        com.Parameters.AddWithValue("@IsAdmin", input.IsAdmin);
                        con.Open();
                        int i = com.ExecuteNonQuery();
                        con.Close();
                        if (i != 0)
                        {
                            return input;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return null;
                }
                nlog.LogDebug("Password reset");
                return null;
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

        public string GenerateSecurityToken(string email, int userId, string role)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.iconfiguration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id",userId.ToString()),
                    new Claim(ClaimTypes.Role,role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
        public string EncryptPassword(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public string DecryptPassword(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new string(decoded_char);
            return decryptpwd;
        }
    }
}
