 
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Identity.Models;
using System.Data.SqlClient;
using System.Configuration;
 
namespace Identity.DAL
{
    public class DataAccessLayer:DALConnection
    {
        public DataTable Getall()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(strConnect);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Units_GetfromDB";
                if (con.State == ConnectionState.Closed) { con.Open(); }
                var rdr = cmd.ExecuteReader();

                dt.Load(rdr);


                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Produk> GetAll_()
        {
            try
            {
                List<Produk> obj_List = new List<Produk>();
                DataAccessLayer obj = new DataAccessLayer();
                DataTable dt = obj.Getall();
                foreach (DataRow dtr in dt.Rows)
                {
                    Produk objUnit = new Produk
                    {
                       // obj.UnitID = int.Parse(dt.Rows[0]["UnitID"].ToString());
                         Id = int.Parse(dt.Rows[0]["Id"].ToString()),
                        nama_produk = dtr["nama_produk"].ToString(),
                        jenis_produk = dtr["jenis_produk"].ToString(),
                        qty = dtr["qty"].ToString(),
                        Designation = dtr["Designation"].ToString(),
                        StaffNo = dtr["StaffNo"].ToString()
                        
                    };
                    obj_List.Add(objUnit);
                }


                return obj_List;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
