using DBSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DBSchool.DataSource
{
    public class DSTid
    {
        public List<ModelTid> GetTea(int tid)
        {
            string SqlString = "";
            List<ModelTid> tlist = new List<ModelTid>();
            using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["The_Sid"].ConnectionString))
            {
                SqlString = "SELECT tea.t_name , dept.dept_name  , cou.cour_name, cou.Credit, " +
                    "case tea.Dept_id when cou.Dept_id then 'true' else 'False' END as isdept " +
                    "FROM dbo.Course cou " +
                    "INNER JOIN dbo.Department dept ON cou.Dept_id = dept.Dept_id " +
                    "INNER JOIN dbo.Teacher tea ON cou.T_id = tea.T_id " +
                    "WHERE tea.T_id = @tid ";

                using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
                {
                    Sql_Conn.Open();
                    Sql_Command.Parameters.AddWithValue("@tid", tid);
                    using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
                    {
                        while (Sql_Reader.Read())
                        {
                            ModelTid stData = new ModelTid();
                            stData.t_name = Sql_Reader["t_name"].ToString().Trim();
                            stData.dept_name = Sql_Reader["dept_name"].ToString().Trim();
                            stData.cour_name = Sql_Reader["cour_name"].ToString().Trim();
                            stData.Credit = Convert.ToInt32(Sql_Reader["Credit"].ToString());
                            stData.isDept = Convert.ToBoolean(Sql_Reader["isdept"]);
                            tlist.Add(stData);
                        }

                        Sql_Reader.Close();
                    }
                    Sql_Conn.Close();
                }
            }
            return tlist;
        }
    }
}