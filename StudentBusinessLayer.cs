using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Text;

namespace StudentManagementProject
{
    class StudentBusinessLayer
    { 
    static string connstring = "Data Source=DESKTOP-VEA4621\\SQLEXPRESS;Initial Catalog=cgstudy;Integrated Security=True";
        Student s1 = new Student();
        public List<int> students;
        
        public StudentBusinessLayer()
        {
           students = new List<int>();
        }

        public  void InsertStudent(string name, DateTime dateofbirth)
        {
            
            InsertintoStudent(new Student(name, dateofbirth));
        }
        public  static void InsertintoStudent(Student s)
        {
            using(SqlConnection con=new SqlConnection(connstring))
            {
                con.Open();
                //string query = "insert into Student(name) values()";
                SqlCommand comand;
                comand= new SqlCommand("procrinsertStudent", con);

               comand.CommandType = CommandType.StoredProcedure;

                comand.Parameters.AddWithValue("@name", s.Name);
                comand.Parameters.AddWithValue("@dateofbirth", s.Dob);

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@id";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Output;
                comand.Parameters.Add(parameter);

                int rows=comand.ExecuteNonQuery();
                string id = parameter.Value.ToString();
               // Console.WriteLine("Row affted:" + rows);
                
                Console.WriteLine("Record Inserted succesfully");
            }
        }

        public  void getAllStudents()
        {
            
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            using (SqlConnection con=new SqlConnection(connstring))
            {
                //con.Open();
                SqlCommand cmd = new SqlCommand("procgetAllStudents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                foreach(DataRow row in dt.Rows)
                {
                    //students.Add(Convert.ToInt32(row[0]));
                    Console.WriteLine(row[0] + "\t" + row[1] + "\t" + Convert.ToDateTime(row[2]).ToString("yyyy-MMM-dd") );
                }
                 
            }
        }

        public void getIntialData()
        {
            students.Clear();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connstring))
            {
                //con.Open();
                SqlCommand cmd = new SqlCommand("procgetAllStudents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    students.Add(Convert.ToInt32(row[0]));
                }

            }
        }

        public  void getStudentbyId(int id)
        {

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand("procgetAllStudentByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                da.SelectCommand = cmd;
                da.Fill(ds, "student");
                foreach (DataRow row in ds.Tables["student"].Rows)
                {
                   
                    Console.WriteLine(row[0] + "\t" + row[1] + "\t" + Convert.ToDateTime(row[2]).ToString("yyyy-MMM-dd"));      
                }
            }
        }

        public  void UpdateStudent(int id,string name)
        { 
            using (SqlConnection con=new SqlConnection(connstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("updateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                int rows=cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    Console.WriteLine(rows + " Affected");
                }
            }
        }

        public static void DeleteStudent(int id)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("deletestudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine(rows + " Affected");
                }
            }
        }
    } 
}
