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
        List<Student> students = new List<Student>();

        public static void InsertStudent(string name, DateTime dateofbirth)
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
                Console.WriteLine("Id generated:", id);
                Console.WriteLine("Rows affected" + rows);
            }
        }

        public static void getAllStudents()
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
                    Console.WriteLine(row[0] + "\t" + row[1] + "\t" + Convert.ToDateTime(row[2]).ToString("yyyy-mm-dd") );
                }
                 
            }
        }

        public static void getStudentbyId(int id)
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

        public static void UpdateStudent(int id,string name)
        {
            using(SqlConnection con=new SqlConnection(connstring))
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
            using(SqlConnection con=new SqlConnection(connstring))
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


        public static void Main(string[] a)
        {
            /*Console.WriteLine("Enter name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter date of birth");
            string dob = Console.ReadLine();
            DateTime date = Convert.ToDateTime(dob);
            //InsertStudent(name, date);*/
            //getAllStudents();
            //getStudentbyId(102);
            //UpdateStudent(107, "RehanKarol");
            DeleteStudent(105);

        }

    } 
}
