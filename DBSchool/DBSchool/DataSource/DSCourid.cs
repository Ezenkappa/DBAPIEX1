using DBSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DBSchool.DataSource
{
    public class DSCourid
    {
        public List<ModelCourid> GetCour(int cid)
        {
            string SqlString = "";
            List<ModelCourid> clist = new List<ModelCourid>();
            using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["The_Sid"].ConnectionString))
            {
                SqlString = "SELECT tea.t_name , dept.dept_name , cou.Credit FROM dbo.Course cou " +
                    "INNER JOIN dbo.Department dept ON cou.Dept_id = dept.Dept_id " +
                    "INNER JOIN dbo.Teacher tea ON cou.T_id = tea.T_id " +
                    "WHERE cou.Cour_id = @cid ";
                
                using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
                {
                    Sql_Conn.Open();
                    Sql_Command.Parameters.AddWithValue("@cid", cid);
                    using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
                    {
                        while (Sql_Reader.Read())
                        {
                            ModelCourid stData = new ModelCourid();
                            stData.t_name = Sql_Reader["t_name"].ToString().Trim();
                            stData.dept_name = Sql_Reader["dept_name"].ToString().Trim();
                            stData.Credit = Convert.ToInt32(Sql_Reader["Credit"].ToString());

                            clist.Add(stData);
                        }

                        Sql_Reader.Close();
                    }
                    Sql_Conn.Close();
                }
            }
            return clist;
        }
        }
}