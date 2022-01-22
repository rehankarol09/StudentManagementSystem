using System;


namespace StudentManagementProject
{
    //Modal
    class Student
    {
        protected int id;
        protected string name;
        protected DateTime dob;

        public Student()
        {

        }
        public Student(string name,DateTime dob)
        {
            this.name = name;
            this.dob = dob;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }
        public string Name
        {
            get => name;
            set => name = value;
        }
        public DateTime Dob
        {
            get => dob;
            set => dob = value;
        }

        public override string ToString()
        {
            return String.Format(Id+"\t"+Name+"\t"+Dob+"\t");
        }

        //Insert 
        //Display
        //Display By ID
        //Update
        //Delete

        

    }
}
