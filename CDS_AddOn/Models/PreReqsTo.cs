using Rubjerg.Graphviz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDS_AddOn.Models
{
    public class PreReqsTo
    {
        private Course toWhichCourse;
        private List<Course> preReqsList;

        public PreReqsTo(Course aCourseName, List<Course> listOfPreReqs)
        {
            this.toWhichCourse = aCourseName;
            this.preReqsList = listOfPreReqs;
        }

        public Course WhichCourse
        {
            get
            {
                return this.toWhichCourse;
            }
            set
            {
                this.toWhichCourse = value;
            }
        }

        public List<Course> PreReqsList
        {
            get
            {
                return this.preReqsList;
            }
            set
            {
                this.preReqsList = value;
            }
        }

        public override string ToString()
        {
            return $"{toWhichCourse} has {preReqsList.Count} pre-reqs";
        }

    }
}