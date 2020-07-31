using DBSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DBSchool.DataSource
{
    public class DSSlist
    {
        public List<StudentList> GetSlist(int cid)
        {
            string SqlString = "";
            List<StudentList> slist = new List<StudentList>();
            using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["The_Sid"].ConnectionString))
            {
                SqlString = "SELECT stu.s_id , stu.s_name , stu.grade FROM dbo.Course cou " +
                    "INNER JOIN dbo.curriculum cur ON cur.cour_id = cou.Cour_id " +
                    "INNER JOIN dbo.Student stu ON stu.S_id = cur.S_Id " +
                    "WHERE cou.Cour_id = @cid; ";

                using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
                {
                    Sql_Conn.Open();
                    Sql_Command.Parameters.AddWithValue("@cid", cid);
                    using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
                    {
                        while (Sql_Reader.Read())
                        {
                            StudentList stData = new StudentList();
                            stData.s_id = Convert.ToInt32(Sql_Reader["s_id"].ToString());
                            stData.s_name = Sql_Reader["s_name"].ToString().Trim();
                            stData.grade = Convert.ToInt32(Sql_Reader["grade"].ToString());


                            slist.Add(stData);
                        }

                        Sql_Reader.Close();
                    }
                    Sql_Conn.Close();
                }
            }
            return slist;
          }
        }
}