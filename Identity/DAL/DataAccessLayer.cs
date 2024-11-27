using Microsoft.Data.SqlClient;
using System.Data;
 
using Identity.Models;
using System.Data.SqlClient;
using System.Configuration;
namespace Identity.DAL
{
    public class DataAccessLayer
    {
        public string InsertData(Produk objcust)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nama_produk", objcust.nama_produk);
                cmd.Parameters.AddWithValue("@jenis_produk", objcust.jenis_produk);
                cmd.Parameters.AddWithValue("@qty", objcust.qty);
                cmd.Parameters.AddWithValue("@Designation", objcust.Designation);
                cmd.Parameters.AddWithValue("@StaffNo", objcust.StaffNo);
                cmd.Parameters.AddWithValue("@Query", 1);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public string UpdateData(Produk objcust)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Produk", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", objcust.Id);
                cmd.Parameters.AddWithValue("@nama_produk", objcust.nama_produk);
                cmd.Parameters.AddWithValue("@jenis_produk", objcust.jenis_produk);
                cmd.Parameters.AddWithValue("@qty", objcust.qty);
                cmd.Parameters.AddWithValue("@Designation", objcust.Designation);
                cmd.Parameters.AddWithValue("@StaffNo", objcust.StaffNo);
                cmd.Parameters.AddWithValue("@Query", 2);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public int DeleteData(String ID)
        {
            SqlConnection con = null;
            int result;
            try
            {
                con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Produk", con);
                cmd.CommandType = CommandType.StoredProcedure;
            
                cmd.Parameters.AddWithValue("@Id", ID);
                cmd.Parameters.AddWithValue("@nama_produk", null);
                cmd.Parameters.AddWithValue("@jenis_produk", null);
                cmd.Parameters.AddWithValue("@qty", null);
                cmd.Parameters.AddWithValue("@Designation", null);
                cmd.Parameters.AddWithValue("@StaffNo", null);
                cmd.Parameters.AddWithValue("@Query", 3);
                con.Open();
                result = cmd.ExecuteNonQuery();
                return result;
            }
            catch
            {
                return result = 0;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Produk> Selectalldata()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<Produk> custlist = null;
            try
            {
                con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Produk", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", null);
                cmd.Parameters.AddWithValue("@nama_produk", null);
                cmd.Parameters.AddWithValue("@jenis_produk", null);
                cmd.Parameters.AddWithValue("@qty", null);
                cmd.Parameters.AddWithValue("@Designation", null);
                cmd.Parameters.AddWithValue("@StaffNo", null);
                cmd.Parameters.AddWithValue("@Query", 4);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                custlist = new List<Produk>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Produk cobj = new Produk();
                    cobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    cobj.nama_produk = ds.Tables[0].Rows[i]["nama_produk"].ToString();
                    cobj.jenis_produk = ds.Tables[0].Rows[i]["jenis_produk"].ToString();
                    cobj.qty = ds.Tables[0].Rows[i]["qty"].ToString();
                    cobj.StaffNo = ds.Tables[0].Rows[i]["StaffNo"].ToString();
                   

                    custlist.Add(cobj);
                }
                return custlist;
            }
            catch
            {
                return custlist;
            }
            finally
            {
                con.Close();
            }
        }

        public Produk SelectDatabyID(string ProdukID)
        {
            SqlConnection con = null;
            DataSet ds = null;
            Produk cobj = null;
            try
            {
                con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Produk", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ProdukID);
                cmd.Parameters.AddWithValue("@nama_produk", null);
                cmd.Parameters.AddWithValue("@jenis_produk", null);
                cmd.Parameters.AddWithValue("@qty", null);
                cmd.Parameters.AddWithValue("@Designation", null);
                cmd.Parameters.AddWithValue("@StaffNo", null);
                cmd.Parameters.AddWithValue("@Query", 5);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cobj = new Produk();
                    cobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProdukID"].ToString());
                    cobj.nama_produk = ds.Tables[0].Rows[i]["nama_produk"].ToString();
                    cobj.jenis_produk = ds.Tables[0].Rows[i]["jenis_produk"].ToString();
                    cobj.qty = ds.Tables[0].Rows[i]["qty"].ToString();
                    cobj.Designation = ds.Tables[0].Rows[i]["Designation"].ToString();
                    cobj.StaffNo = ds.Tables[0].Rows[i]["StaffNo"].ToString();
                    

                }
                return cobj;
            }
            catch
            {
                return cobj;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
