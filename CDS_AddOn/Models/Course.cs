using Rubjerg.Graphviz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CDS_AddOn.Models
{
    public class Course
    {
        private Node aCourse;
        private int creditHours;
        public Course(Node theCourse, int itsHours)
        {
            this.aCourse = theCourse;
            this.creditHours = itsHours;
        }

        public Node CourseNode
        {
            get
            {
                return this.aCourse;
            }
            set
            {
                this.aCourse = value;
            }
        }

        public int CreditHours
        {
            get
            {
                return this.creditHours;
            }
            set
            {
                this.creditHours = value;
            }
        }

        public override string ToString()
        {
            return $"{this.aCourse} with {this.creditHours}";
        }
    }
}