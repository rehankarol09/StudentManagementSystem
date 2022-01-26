using System;


namespace StudentManagementProject
{
    class UserInterface
    {
        StudentBusinessLayer studentBusiness = new StudentBusinessLayer();
        CourseBusinessLayer courseBusiness = new CourseBusinessLayer();
        EnrollBusinessLayer enrollBusinessLayer = new EnrollBusinessLayer();
        public void ShowFirstScreen()
        {
            Console.WriteLine("Tell us who you aree\t");
            Console.WriteLine("Choose: 1 for student\t 2 for Admin");
            int choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    StudentScreen();
                    break;
                case 2:
                    AdminScreen();
                    break;
                default:
                    Console.WriteLine("Enter correct choice");
                    break;
            }
        }

        public void StudentScreen()
        {
            Console.WriteLine("Choose your option");
            Console.WriteLine("\nEnter your choice:\n1.Register for a Course\n2.Show all Student Enrollments\n3.Show all Student Details\n4:Enroll for a course\n 5:Updte Student Details\n");
            int choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter your name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter date of birth");
                    string date = Console.ReadLine();
                    DateTime dateTime10 = Convert.ToDateTime(date);
                    studentBusiness.InsertStudent(name, dateTime10);
                    break;
                case 2:
                    enrollBusinessLayer.getAllEnrollments();
                    break;
                case 3:
                    studentBusiness.getAllStudents();
                    break;
                case 4:
                    enrollBusinessLayer.AddEnrollMent();
                    break;
                case 5:
                    studentBusiness.getIntialData();
                    Console.WriteLine("Enter your id");
                    int id = Int32.Parse(Console.ReadLine());
                    
                    if(studentBusiness.students.Contains(id))
                    {
                        Console.WriteLine("Enter name");
                        string namec = Console.ReadLine();
                        studentBusiness.UpdateStudent(id, namec);
                    }
                    else
                    {
                        Console.WriteLine("Records not available");
                    }
                    break;
                default:
                    Console.WriteLine("Enter correct choice");
                    break;
            }
        }

        public void AdminScreen()
        {
            Console.WriteLine("You are in admin screen");
            Console.WriteLine("---Welcome to Admin Dashboard---");
            Console.WriteLine("\nEnter your choice:\n1.Show all Student Details\n2.Show all current Student Enrollments\n" +
                "3.Introduce new course\n4.Show all courses\n5.Display Student Details by ID\n6:Update Course Details\n 7.Enroll Student");
            int ch = Convert.ToInt32(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    studentBusiness.getAllStudents();
                 
                    break;
                case 2:
                    enrollBusinessLayer.getAllEnrollments();
                    break;
                case 3:
                    courseBusiness.addCourse();
                    break;
                case 4:
                    courseBusiness.printDegreeCourse();
                    break;
                case 5:
                    studentBusiness.getIntialData();
                    Console.WriteLine("Enter student id");
                    int id = Int32.Parse(Console.ReadLine());
                    if (studentBusiness.students.Contains(id))
                    {
                        studentBusiness.getStudentbyId(id);
                    }
                    else
                    {
                        Console.WriteLine("Record not available");
                    }
                    break;
                case 6:
                    courseBusiness.getIntialData();
                    Console.WriteLine("Enter your id");
                    int courseid = Int32.Parse(Console.ReadLine());

                    if (courseBusiness.courseids.Contains(courseid))
                    {
                        Console.WriteLine("Enter fees");
                        double fees = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter seats");
                        int seats = Convert.ToInt32(Console.ReadLine());
                        courseBusiness.updateCourse(courseid, fees, seats);
                    }
                    else
                    {
                        Console.WriteLine("Records not available");
                    }
                    break;
                case 7:
                    enrollBusinessLayer.AddEnrollMent();
                    break;
                default:
                    Console.WriteLine("Please enter correct choice");
                    break;
            }
        }
    }
}
