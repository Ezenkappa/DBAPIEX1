using DBSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DBSchool.DataSource
{
    public class DSTSlist
    {
        public List<TStudentList> GetTSlist(int tid)
        {
            string SqlString = "";
            List<TStudentList> tlist = new List<TStudentList>();
            using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["The_Sid"].ConnectionString))
            {
                SqlString = "SELECT stu.s_id , stu.s_name , stu.grade , cou.cour_name FROM dbo.Course cou " +
                    "INNER JOIN dbo.curriculum cur ON cur.cour_id = cou.Cour_id " +
                    "INNER JOIN dbo.Student stu ON stu.S_id = cur.S_Id " +
                    "WHERE cou.T_id = @tid; ";

                using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
                {
                    Sql_Conn.Open();
                    Sql_Command.Parameters.AddWithValue("@tid", tid);
                    using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
                    {
                        while (Sql_Reader.Read())
                        {
                            TStudentList stData = new TStudentList();
                            stData.s_id = Convert.ToInt32(Sql_Reader["s_id"].ToString());
                            stData.s_name = Sql_Reader["s_name"].ToString().Trim();
                            stData.grade = Convert.ToInt32(Sql_Reader["grade"].ToString());
                            stData.cour_name = Sql_Reader["cour_name"].ToString().Trim();
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