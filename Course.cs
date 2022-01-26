

namespace StudentManagementProject
{
    public enum Level
    {
        bachelors,
        masters
    }

    public enum Type
    {
        professional,
        academics
    }

    public abstract class Course
        {
            string id;
            string name;
            string duration;
            float fees;
            public int seatsavaialble;

            public Course() { }
            public Course(string name, string duration, float fees, int seatsavaialble)
            {
                this.name = name;
                this.duration = duration;
                this.fees = fees;
                this.seatsavaialble = seatsavaialble;
            }

            public string CourseId
            {
                get { return id; }
                set { id = value; }
            }

            public string CourseName
            {
                get { return name; }
                set { name = value; }
            }

            public string CourseDuration
            {
                get { return duration; }
                set { duration = value; }
            }

            public float Fees
            {
                get { return fees; }
                set { fees = value; }
            }

            

        }

        class DegreeCourse : Course
        {
            // public string level;
            public string isplacementavailable;
            protected Level level;
            public DegreeCourse( string name, string duration, float fees, int seats, Level level, string isplacementavailable) : base( name, duration, fees, seats)
            {

                this.isplacementavailable = isplacementavailable;
                this.level = level;
            }

            public DegreeCourse() { }
            public string Level1
            {
            get => this.level.ToString();
            }

         
            public override string ToString()
            {
                return "\t" + CourseId + "\t" + CourseName + "\t" + CourseDuration + "\t" + " " + level + "\n";
            }

        }
        class DiplomaCourse : Course
        {
            // public string type;
            Type type;
            public DiplomaCourse( string name, string duration, int seats, float fees, Type type) : base( name, duration, fees, seats)
            {
                this.type = type;
            }

            public string Type1
            {
                get => this.type.ToString();
            }

           
            public override string ToString()
            {
                return "\t" + CourseId + "\t" + CourseName + "\t" + CourseDuration + "\t"  + "\t" + type + "\n";
            }

        }
    }

