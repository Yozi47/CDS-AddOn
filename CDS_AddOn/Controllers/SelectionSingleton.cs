using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Configuration;
using Rubjerg.Graphviz;

namespace CDS_AddOn.Controllers
{
    public sealed class SelectionSingleton
    {
        private readonly static SelectionSingleton instance;
        private static string dropdownSelection;
        private static DataSet coursesWithPreReqs;
        private static DataSet allListedPreReqs;
        private static DataSet preReqLackingCourses;

        private static SubGraph preExistingCourseCluster;
        private static SubGraph nonExistingCourseCluster;
        private static SubGraph preReqsLackingCourseCluster;
        public static string treePngPath = Path.GetFullPath(@"C:\Users\ybrad\OneDrive\Desktop\IT680\Phase 3\CDS-AddOn\CDS_AddOn\tree.png");
        public static string treeSvgPath = Path.GetFullPath(@"C:\Users\ybrad\OneDrive\Desktop\IT680\Phase 3\CDS-AddOn\CDS_AddOn\tree.svg");
        public static string treeDotPath = Path.GetFullPath(@"C:\Users\ybrad\OneDrive\Desktop\IT680\Phase 3\CDS-AddOn\CDS_AddOn\tree.dot");
        public static string it4a_connStr = ConfigurationManager.ConnectionStrings["It4a_org"].ToString();


        private SelectionSingleton() { }
        static SelectionSingleton()
        {
            SelectionSingleton.instance = new SelectionSingleton();
        }
        public SelectionSingleton Instance
        {
            get
            {
                return SelectionSingleton.instance;
            }
        }

        public static string DropdownSelection
        {
            get
            {
                return dropdownSelection;
            }

            set
            {
                dropdownSelection = value;
            }
        }

        public static DataSet CoursesWithPreReqs
        {
            get
            {
                return coursesWithPreReqs;
            }

            set
            {
                coursesWithPreReqs = value;
            }
        }

        public static DataSet AllListedPrereqs
        {
            get
            {
                return allListedPreReqs;
            }

            set
            {
                allListedPreReqs = value;
            }
        }
        public static DataSet PreReqLackingCourses
        {
            get
            {
                return preReqLackingCourses;
            }

            set
            {
                preReqLackingCourses = value;
            }
        }

        public static SubGraph PreExistingCourseCluster
        {
            get
            {
                return preExistingCourseCluster;
            }

            set
            {
                preExistingCourseCluster = value;
            }
        }

        public static SubGraph NonExistingCourseCluster
        {
            get
            {
                return nonExistingCourseCluster;
            }

            set
            {
                nonExistingCourseCluster = value;
            }
        }
        public static SubGraph PreReqsLackingCourseCluster
        {
            get
            {
                return preReqsLackingCourseCluster;
            }

            set
            {
                preReqsLackingCourseCluster = value;
            }
        }
    }
}