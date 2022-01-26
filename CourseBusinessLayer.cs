using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StudentManagementProject
{
    class CourseBusinessLayer
    {
        static string connstring = "Data Source=DESKTOP-VEA4621\\SQLEXPRESS;Initial Catalog=cgstudy;Integrated Security=True";
         public  List<int> courseids;
        public CourseBusinessLayer()
        {
            courseids = new List<int>();
        }

        public  void addCourse()
        {
            Console.WriteLine("Course Name:");
            string CourseName = Console.ReadLine();
            Console.WriteLine("Course Duration:");
            string CourseDuration = Console.ReadLine();
            Console.WriteLine("Course Fees:");
            float CourseFees = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter Seats available:");
            int seats = Int32.Parse(Console.ReadLine());
            //Level level;

            Console.WriteLine("Enter Degree/Diploma");
            string choice = Console.ReadLine();
            if (choice == "Degree")
            {
                Console.WriteLine("enter your degree type :Bachelors/Masters");
                string dtype = Console.ReadLine();
                Console.WriteLine("Is placement available true:false");
                string avail = Console.ReadLine();
                if (dtype == "Bachelors")
                { 
                    introduceDegree(new DegreeCourse(CourseName, CourseDuration, CourseFees, seats, Level.bachelors, avail));
                }
                else if (dtype == "Masters")
                {
                    introduceDegree(new DegreeCourse(CourseName, CourseDuration, CourseFees, seats, Level.masters, avail));
                }
            }else if(choice=="Diploma")
            {
                Console.WriteLine("Enter your Diploma Type: Academics/Professional");
                string ch = Console.ReadLine();
                if (ch.Equals("Academics"))
                {
                    introduceDiploma(new DiplomaCourse(CourseName, CourseDuration, seats, CourseFees, Type.academics));
                }
                else if(ch.Equals("Professional"))
                {
                    introduceDiploma(new DiplomaCourse(CourseName, CourseDuration, seats, CourseFees, Type.professional));
                }
            }

        }

        public static void introduceDegree(DegreeCourse course)
        {
           using(SqlConnection con=new SqlConnection(connstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into Course(coursename,CourseDuration,Coursefees,seatsavailable,Course,Type,Placement)  values(@name,@duration,@fees,@seats,@Course,@Type,@Placement)", con);
                        cmd.Parameters.AddWithValue("@name",course.CourseName);
                        cmd.Parameters.AddWithValue("@duration", course.CourseDuration);
                        cmd.Parameters.AddWithValue("@fees", course.Fees);
                        cmd.Parameters.AddWithValue("seats",course.seatsavaialble);
                        
                        //cmd.Parameters.AddWithValue("@placement",course.isplacementavailable);
                        cmd.Parameters.AddWithValue("@Course","Degree");
                        cmd.Parameters.AddWithValue("@Type",course.Level1);
                        if(course.isplacementavailable.Equals("true"))
                        {
                            cmd.Parameters.AddWithValue("@Placement", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Placement", 0);

                }
                int rows=cmd.ExecuteNonQuery();
                        Console.WriteLine("Rows affected" + rows);
                    }
        }

        public static void introduceDiploma(DiplomaCourse course)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Course(coursename,CourseDuration,Coursefees,seatsavailable,Course,Type) values(@name,@duration,@fees,@seats,@course,@type)", con);
                cmd.Parameters.AddWithValue("@name", course.CourseName);
                cmd.Parameters.AddWithValue("@duration", course.CourseDuration);
                cmd.Parameters.AddWithValue("@fees", course.Fees);
                cmd.Parameters.AddWithValue("seats", course.seatsavaialble);
                cmd.Parameters.AddWithValue("@course", "Diploma");
                cmd.Parameters.AddWithValue("@type", course.Type1);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine("Course Added");
            }
        }

        public  void printDegreeCourse()
        {
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataSet ds1 = new DataSet();
            using (SqlConnection con=new SqlConnection(connstring))
            {
                //con.Open();
                SqlCommand command = new SqlCommand("select * from course",con);
                
                da1.SelectCommand = command;
              
                da1.Fill(ds1, "Course");
               
                foreach (DataRow row in ds1.Tables["Course"].Rows)
                {
                    //courseids.Add(Convert.ToInt32(row[0]));
                    Console.WriteLine(row[1] + "\t" + row[2]+"\t"+row[3]);
                }
            }
        }

        public void getIntialData()
        {
            courseids.Clear();
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataSet ds1 = new DataSet();
            using (SqlConnection con = new SqlConnection(connstring))
            {
                //con.Open();
                SqlCommand command = new SqlCommand("select * from course", con);

                da1.SelectCommand = command;

                da1.Fill(ds1, "Course");

                foreach (DataRow row in ds1.Tables["Course"].Rows)
                {
                    courseids.Add(Convert.ToInt32(row[0]));
                   
                }
            }
        }

        public  void updateCourse(int id,double fees,int seats)
        {
            SqlCommand cmd;
            
            using(SqlConnection con=new SqlConnection(connstring))
            {
                con.Open();
                cmd = new SqlCommand("procupdateCourse", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters.AddWithValue("@fees", fees);
                cmd.Parameters.AddWithValue("@seatsavailable", seats);

                SqlParameter output = new SqlParameter();
                output.ParameterName="@returnvalue";
                output.SqlDbType = SqlDbType.Int;
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                cmd.ExecuteNonQuery();
                string outputvalue =output.Value.ToString(); 
                if(outputvalue.Equals("1"))
                    {
                       Console.WriteLine("Record Updated");
                    }
                else
                {
                    Console.WriteLine("Record not found");
                }

            }
        }

     /*   public static void Main(string[] a)
        {
            //addCourse();
            //printDegreeCourse();
            updateCourse(102, 9000, 67);
            Console.ReadKey();
        }*/
    }
}
