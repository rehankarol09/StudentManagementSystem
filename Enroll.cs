using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementProject
{
    class Enroll
    {
        int studentid;
        int courseid;
        public Enroll(int studentid,int courseid)
        {
            this.studentid = studentid;
            this.courseid = courseid;
        }
        public Enroll()
        {

        }
        public int StudentID
        {
            get => studentid;
            set => this.studentid = value;
        }
        public int CousreID
        {
            get => courseid;
            set => this.courseid = value;
        }
    }
}
