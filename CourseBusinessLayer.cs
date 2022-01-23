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
        List<Course> course = new List<Course>();

        public static void addCourse()
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
                        SqlCommand cmd = new SqlCommand("insert into Degree values(@name,@duration,@fees,@seats,@placement,@level)", con);
                        cmd.Parameters.AddWithValue("@name",course.CourseName);
                        cmd.Parameters.AddWithValue("@duration", course.CourseDuration);
                        cmd.Parameters.AddWithValue("@fees", course.Fees);
                        cmd.Parameters.AddWithValue("seats",course.seatsavaialble);
                        cmd.Parameters.AddWithValue("@placement",course.isplacementavailable);
                        cmd.Parameters.AddWithValue("@level",course.Level1);
                        int rows=cmd.ExecuteNonQuery();
                        Console.WriteLine("Rows affected" + rows);
                    }
        }

        public static void introduceDiploma(DiplomaCourse course)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Diploma values(@name,@duration,@fees,@seats,@type)", con);
                cmd.Parameters.AddWithValue("@name", course.CourseName);
                cmd.Parameters.AddWithValue("@duration", course.CourseDuration);
                cmd.Parameters.AddWithValue("@fees", course.Fees);
                cmd.Parameters.AddWithValue("seats", course.seatsavaialble);
                cmd.Parameters.AddWithValue("@type", course.Type1);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine("Rows affected" + rows);
            }
        }

        public static void printDegreeCourse()
        {
            SqlDataAdapter da1 = new SqlDataAdapter();
            SqlDataAdapter da2 = new SqlDataAdapter();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            using (SqlConnection con=new SqlConnection(connstring))
            {
                //con.Open();
                SqlCommand command = new SqlCommand("select * from degree",con);
                SqlCommand command1 = new SqlCommand("select * from diploma", con);
                da1.SelectCommand = command;
                da2.SelectCommand = command1;
                da1.Fill(ds1, "Degree");
                da2.Fill(ds2, "Diploma");
                foreach (DataRow row in ds1.Tables["Degree"].Rows)
                {
                    Console.WriteLine(row[1] + "\t" + row[2]+"\t"+row[3]);
                }

                foreach (DataRow row in ds2.Tables["Diploma"].Rows)
                {
                    Console.WriteLine(row[1] + "\t" + row[2]);
                }
            }
        }

        public static void Main(string[] a)
        {
            //addCourse();
            printDegreeCourse();
            Console.ReadKey();
        }
    }
}
