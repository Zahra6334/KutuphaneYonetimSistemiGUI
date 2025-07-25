using KutuphaneYonetimSistemiGUI.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
namespace KutuphaneYonetimSistemiGUI.sqlbaglantest { 
public class DatabaseDataProvider
{
    private readonly string _connectionString;

    public DatabaseDataProvider()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["KutuphaneDB"].ConnectionString;
    }

    public void TestConnection()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                MessageBox.Show("✅ Bağlantı başarılı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Bağlantı hatası: " + ex.Message);
            }
        }
    }

        public void AddBook(Book book)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Books (Title, Author, Year, ISBN, Status) VALUES (@Title, @Author, @PublishYear, @ISBN, @Status)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@PublishYear", book.PublishYear);
                cmd.Parameters.AddWithValue("@ISBN", book.ISBN ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", book.IsAvailable);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.IsAdmin);
                

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteBookByIsbn(string isbn)
        {
            

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Books WHERE ISBN = @ISBN";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ISBN", isbn);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Belirtilen ISBN numarasına sahip kitap bulunamadı.");
                    }
                    else
                    {
                        MessageBox.Show("Kitap başarıyla silindi.");
                    }
                }
            }
        }
        public void UpdateBookByIsbn(Book book)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
            UPDATE Books SET
                Title = @Title,
                Author = @Author,
                Year = @Year,
                Status = @Status
            WHERE ISBN = @ISBN";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@Year", book.PublishYear);
                    cmd.Parameters.AddWithValue("@Status", book.IsAvailable);
                    cmd.Parameters.AddWithValue("@ISBN", book.ISBN);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                        MessageBox.Show("Veritabanında belirtilen ISBN ile kitap bulunamadı.");
                    else
                        MessageBox.Show("Kitap bilgileri başarıyla güncellendi.");
                }
            }
        }
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Şifreyi byte dizisine çevirilecek (UTF8 formatında)
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);

                // SHA256 ile hash hesaplanacak
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Hash’i Base64 string formatına dönüştüryor ve geri döndürüyor
                return Convert.ToBase64String(hashBytes);
            }
        }



    }
}