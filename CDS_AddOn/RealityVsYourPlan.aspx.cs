using CDS_AddOn.Controllers;
using CDS_AddOn.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace CDS_AddOn
{
    public partial class RealityVsYourPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            double[] yValues = CountCourseTypes();
            string[] xValues = { "Existing Courses", "Hidden Pre-reqs" };
            PieChartCredits.Titles.Add("Existing Courses and Hidden Pre-reqs");
            PieChartCredits.Series.Add("Default");
            PieChartCredits.ChartAreas.Add("ChartArea1");
            PieChartCredits.Legends.Add("Description");
            PieChartCredits.Series["Default"].Points.DataBindXY(xValues, yValues);

            PieChartCredits.Series["Default"].Points[0].Color = Color.Green;
            PieChartCredits.Series["Default"].Points[1].Color = Color.Red;

            PieChartCredits.Series["Default"].ChartType = SeriesChartType.Pie;
            PieChartCredits.Series["Default"]["PieLabelStyle"] = "Inside";
            PieChartCredits.Series["Default"].Points[0].Label = CountCourseTypes()[0].ToString() + $"\n({CountCourseTypesCreditHour()[0]} Credit Hours)";
            PieChartCredits.Series["Default"].Points[1].Label = CountCourseTypes()[1].ToString() + $"\n({CountCourseTypesCreditHour()[1]} Credit Hours)";
            PieChartCredits.Series["Default"].Points[0].LegendText = xValues[0];
            PieChartCredits.Series["Default"].Points[1].LegendText = xValues[1];

            PieChartCredits.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            PieChartCredits.Legends[0].Enabled = true;

        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlanTreeDiagram.aspx");
        }

        private double[] CountCourseTypes()
        {
            double[] redAndGreenColoredCourses = new double[2];
            double redCourse = 0;
            double greenCourse = 0;
            foreach(Course course in TableToNodeConverter.listOfAllCourses)
            {
                if(course.CourseNode.GetColor() == Color.Green)
                {
                    greenCourse += 1;
                }
                else
                {
                    redCourse += 1;
                }
            }
            redAndGreenColoredCourses[0] = greenCourse;
            redAndGreenColoredCourses[1] = redCourse;
            return redAndGreenColoredCourses;

        }

        private double[] CountCourseTypesCreditHour()
        {
            double[] redAndGreenColoredCourses = new double[2];
            double redCourse = 0;
            double greenCourse = 0;
            foreach (Course course in TableToNodeConverter.listOfAllCourses)
            {
                if (course.CourseNode.GetColor() == Color.Green)
                {
                    greenCourse += course.CreditHours;
                }
                else
                {
                    redCourse += course.CreditHours;
                }
            }
            redAndGreenColoredCourses[0] = greenCourse;
            redAndGreenColoredCourses[1] = redCourse;
            return redAndGreenColoredCourses;

        }
    }
}