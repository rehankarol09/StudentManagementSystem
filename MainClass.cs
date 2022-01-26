

namespace StudentManagementProject
{
    class MainClass
    {
         public static void Main(string[] a)
        {
            UserInterface u = new UserInterface();
            StudentBusinessLayer studentBusinessLayer = new StudentBusinessLayer();
            CourseBusinessLayer courseBusinessLayer = new CourseBusinessLayer();

           
            do
            {
                
                u.ShowFirstScreen();
            } while (true);
        }
    }
}
