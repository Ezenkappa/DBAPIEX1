using DBSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DBSchool.DataSource
{
    public class DSSid
    {
        public List<ModelSid> GetSid(int sid)
        {
            string SqlString = "";
            List<ModelSid> list = new List<ModelSid>();

            using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["The_Sid"].ConnectionString))
            {
                SqlString = "SELECT cou.cour_id , cou.cour_name , cou.Credit , tea.t_name FROM dbo.curriculum cur " +
                    "INNER JOIN dbo.Course cou ON cur.cour_id = cou.Cour_id " +
                    "INNER JOIN dbo.Teacher tea ON cou.T_id = tea.T_id " +
                    "WHERE cur.S_Id = @sid";
                
                using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
                {
                    Sql_Conn.Open();
                    Sql_Command.Parameters.AddWithValue("@sid", sid);
                    using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
                    {
                        while (Sql_Reader.Read())
                        {
                            ModelSid stData = new ModelSid();
                            stData.cour_id = Convert.ToInt32(Sql_Reader["cour_id"].ToString());
                            stData.cour_name = Sql_Reader["cour_name"].ToString().Trim();
                            stData.credit = Convert.ToInt32(Sql_Reader["Credit"].ToString());
                            stData.t_name = Sql_Reader["t_name"].ToString().Trim();
                            list.Add(stData);
                         }
                        
                        Sql_Reader.Close();
                    }
                    Sql_Conn.Close();
                }
            }

            return list;
        }
        public int GetSum(int sid , int type)
        {
            string SqlString = "";
            int nRows = 0;
            using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["The_Sid"].ConnectionString))
            {
                if (type == 1)
                {
                    SqlString = "SELECT COUNT(*) FROM dbo.curriculum cur " +
                    "INNER JOIN dbo.Course cou ON cur.cour_id = cou.Cour_id " +
                    "INNER JOIN dbo.Teacher tea ON cou.T_id = tea.T_id " +
                    "WHERE cur.S_Id = @sid";
                }
                else {
                    SqlString = "SELECT SUM(cou.credit) FROM dbo.curriculum cur " +
                    "INNER JOIN dbo.Course cou ON cur.cour_id = cou.Cour_id " +
                    "INNER JOIN dbo.Teacher tea ON cou.T_id = tea.T_id " +
                    "WHERE cur.S_Id = @sid";
                }
                using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
                {
                    Sql_Command.CommandText = SqlString;
                    Sql_Conn.Open();
                    Sql_Command.Parameters.AddWithValue("@sid", sid);
                    nRows = (int)Sql_Command.ExecuteScalar();

                    Sql_Conn.Close();
                }
            }
            return nRows;
        }
    }
}