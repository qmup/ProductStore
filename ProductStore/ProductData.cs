using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore
{
    class ProductData
    {
        string strConnection;
        public ProductData()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["CSharpProjectFall2017"].ConnectionString;
            return strConnection;
        }
        public DataTable getProducts()
        {
            string sql = "select * from Products";
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtProduct = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(dtProduct);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally {
                con.Close();
            }
            return dtProduct;
        }

        public bool addProduct(Product p)
        {
            bool result;
            SqlConnection con = new SqlConnection(strConnection);
            string sql = "Insert Products values (@ID,@Name,@Price,@Quantity)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", p.productID);
            cmd.Parameters.AddWithValue("@Name", p.productName);
            cmd.Parameters.AddWithValue("@Price", p.unitPrice);
            cmd.Parameters.AddWithValue("@Quantity", p.quantity);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
        public bool updateProduct(Product p)
        {
            bool result;
            SqlConnection con = new SqlConnection(strConnection);
            string sql = "Update Products set ProductName=@Name,UnitPrice=@Price,Quantity=@Quantity where ProductID=@ID)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", p.productID);
            cmd.Parameters.AddWithValue("@Name", p.productName);
            cmd.Parameters.AddWithValue("@Price", p.unitPrice);
            cmd.Parameters.AddWithValue("@Quantity", p.quantity);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
        public bool DeleteProduct(int productID)
        {
            bool result;
            SqlConnection con = new SqlConnection(strConnection);
            string sql = "Delete Products where ProductID=@ID)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", productID);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
    }
}
