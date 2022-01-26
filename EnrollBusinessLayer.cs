using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StudentManagementProject
{
    public class EnrollBusinessLayer
    {
         string connstring = "Data Source=DESKTOP-VEA4621\\SQLEXPRESS;Initial Catalog=cgstudy;Integrated Security=True";
        Enroll e = new Enroll();
        List<Enroll> enrolllist = new List<Enroll>();
        StudentBusinessLayer studentBusinessLayer = new StudentBusinessLayer();
        CourseBusinessLayer coursebusinesslayer = new CourseBusinessLayer();
        public void AddEnrollMent()
        {
            studentBusinessLayer.getIntialData();
            coursebusinesslayer.getIntialData();
            Console.WriteLine("Enter student id");
            int studentid = Int32.Parse(Console.ReadLine());
            if (studentBusinessLayer.students.Contains(studentid))
            {
                Console.WriteLine("Enter course id");
                
                int courseid = Int32.Parse(Console.ReadLine());
                int count = getSeatCount(courseid);
                if (count==0)
                {
                    Console.WriteLine("Seats not available");
                }
                    else{
                            if (coursebusinesslayer.courseids.Contains(courseid))
                            {
                                try
                                {
                                    SqlCommand cmd;

                                    using (SqlConnection con = new SqlConnection(connstring))
                                    {
                                        con.Open();
                                        cmd = new SqlCommand("insert into Enroll(studentid,courseid) values(@studentid,@courseid)", con);
                                        cmd.Parameters.AddWithValue("@studentid", studentid);
                                        cmd.Parameters.AddWithValue("courseid", courseid);

                                        int rows = cmd.ExecuteNonQuery();
                                        if (rows > 0)
                                        {
                                            cmd = new SqlCommand("update course set seatsavailable=seatsavailable-1 where courseid=@courseid", con);
                                            cmd.Parameters.AddWithValue("@courseid", courseid);
                                            cmd.ExecuteNonQuery();

                                            Console.WriteLine("Data updated");

                                        }
                                        else
                                        {
                                            Console.WriteLine("Something went wrong");
                                        }
                                    }
                                }
                                catch (SqlException)
                                {
                                    Console.WriteLine("Course is already selected");
                                }

                            }
                            else
                            {
                                Console.WriteLine("Course data not available: Create one");
                            }
                        }
            }
            else
            {
                Console.WriteLine("Student data not available: Create one");
            }
           
        }

        public int getSeatCount(int courseid)
        {
            string count;
            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("checkseatscount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", courseid);

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@count";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);
                cmd.ExecuteNonQuery();
                 count = parameter.Value.ToString();
                     
            }

            return Convert.ToInt32(count);
        }

        public void getAllEnrollments()
        {
            SqlCommand cmd;
            DataSet ds = new DataSet();
            SqlDataAdapter da=new SqlDataAdapter();
            using(SqlConnection con=new SqlConnection(connstring))
            {
                cmd = new SqlCommand("getAllEnrollments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(ds,"Enroll");
                foreach (DataRow row in ds.Tables["Enroll"].Rows)
                {
                    Console.WriteLine(row[0] + "\t" + row[1]);

                }
            }

        }
    }
}
