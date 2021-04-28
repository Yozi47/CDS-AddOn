using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Rubjerg.Graphviz;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using CDS_AddOn.Controllers;
using CDS_AddOn.Models;

namespace CDS_AddOn
{
    public partial class PlanTreeDiagram : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string coursesWithPreReqsQuery = "select C.COURSE_NUM, c.COURSE_NAME, c.CREDIT_HRS, cp.CATEGORY\n" +
                                                "from COURSE_PROGRAM cp join COURSE c on cp.COURSE_NUM = c.COURSE_NUM\n" +
                                                    "join PROGRAM p on p.PROG_NUM = cp.PROG_NUM \n" +
                                                    $"where p.PROG_NUM = '{ SelectionSingleton.DropdownSelection}'\n" +
                                                    "and c.COURSE_NUM in (Select course_num from prerequisite)";
            string preReqQuery = "select C.COURSE_NUM, c.CREDIT_HRS, cp.CATEGORY, pr.pre_course, pr.option_num\n" +
                                   "from COURSE_PROGRAM cp join COURSE c on cp.COURSE_NUM = c.COURSE_NUM\n" +
                                        "join PROGRAM p on p.PROG_NUM = cp.PROG_NUM\n" +  
                                        "join prerequisite pr on cp.COURSE_NUM = pr.course_num\n" +
                                   $"where p.PROG_NUM = '{SelectionSingleton.DropdownSelection}'";

            string coursesWithoutPreReqQuery = "select C.COURSE_NUM, c.COURSE_NAME, c.CREDIT_HRS, cp.CATEGORY\n"+
                                                "from COURSE_PROGRAM cp join COURSE c on cp.COURSE_NUM = c.COURSE_NUM\n"+
	                                                "join PROGRAM p on p.PROG_NUM = cp.PROG_NUM \n"+
                                                    $"where p.PROG_NUM = '{ SelectionSingleton.DropdownSelection}'\n" +
	                                                "and c.COURSE_NUM not in (Select course_num from prerequisite)";

            DBReader prgCourseReader = new DBReader(coursesWithPreReqsQuery);
            DBReader preCourseReader = new DBReader(preReqQuery);
            DBReader withoutPreCourseReader = new DBReader(coursesWithoutPreReqQuery);


            SelectionSingleton.AllListedPrereqs = preCourseReader.GetTableData();
            SelectionSingleton.CoursesWithPreReqs = prgCourseReader.GetTableData();
            SelectionSingleton.PreReqLackingCourses = withoutPreCourseReader.GetTableData();

            TableToNodeConverter.ConvertToNodes(SelectionSingleton.CoursesWithPreReqs, "COURSE_NUM", SelectionSingleton.PreExistingCourseCluster, "Cluster of Pre - Existing Courses");
            TableToNodeConverter.ConvertToNodes(SelectionSingleton.PreReqLackingCourses, "COURSE_NUM", SelectionSingleton.PreReqsLackingCourseCluster, "Cluster of Pre-reqs Lacking Courses");
            TableToNodeConverter.CreateEdgesWithPreReqs( "COURSE_NUM", "pre_course", SelectionSingleton.NonExistingCourseCluster, "Cluster of non - Existing Courses "); // TODO: Check for non existing pre-reqs with query not GraphViz Nodes
            //TODO: Above line needs edit, should be non-existing prereqs..


            TableToNodeConverter.CreateEdgesWithPreReqs("course_num", "COURSE_NUM", "pre_course");
            TableToNodeConverter.FillSvg();
        }

        protected void SeeFourYearPlanButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlanBreakdown.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");

        }
    }
}