using CDS_AddOn.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CDS_AddOn.Models
{
    public class GetMostUsedDataSets
    {
        public static DataSet GetCoursesOpenedByPreReqsOf(string courseNum)
        {
            string theQuery = $"select C.COURSE_NUM, c.CREDIT_HRS, cp.CATEGORY, pr.pre_course, pr.option_num, pr.CREDIT_HRS as \"PreCourse_CrHrs\" \n" +
                                "from COURSE_PROGRAM cp join COURSE c on cp.COURSE_NUM = c.COURSE_NUM\n" +
                                    "join PROGRAM p on p.PROG_NUM = cp.PROG_NUM,\n" +
                                    "(Select p.pre_course, p.option_num, c.CREDIT_HRS, p.course_num\n" +
                                    "from prerequisite p join COURSE c on p.pre_course = c.COURSE_NUM) pr\n" +
                                $"where p.PROG_NUM = '{SelectionSingleton.DropdownSelection}'\n" +
                                    "and cp.COURSE_NUM = pr.course_num \n"+
                                    "and pr.pre_course in (Select p.pre_course\n"+
                                                          "from prerequisite p\n"+
                                                          $"where p.course_num = '{courseNum}')";
            DBReader dBReader = new DBReader(theQuery);
            return dBReader.GetTableData();
        }
        /// <summary>
        /// Creates a dictionary with list of pre-requisites as a key and with a value of 1
        /// </summary>
        /// <param name="thisCourse"></param>
        /// <returns></returns>
        public static Dictionary<List<string>, double> GetListOfPreReqsFor(string thisCourse)
        {
            Dictionary<List<string>, double> returnDict = new Dictionary<List<string>, double>();
            List<string> optionSet = new List<string>();
            string preReqCourseOfThisCourseQuery = "select * from prerequisite p \n" +
                                                    $"where p.course_num = '{thisCourse}' ";
            DBReader dBReader = new DBReader(preReqCourseOfThisCourseQuery);
            DataSet preReqCourseOfThisCourseCollection =  dBReader.GetTableData();
            int optionNum = 1;
            foreach(DataRow preReqRow in preReqCourseOfThisCourseCollection.Tables[0].Rows)
            {
                if(optionNum == int.Parse(preReqRow[1].ToString()))
                {
                    optionSet.Add(preReqRow[2].ToString());
                }
                else
                {
                    optionNum = int.Parse(preReqRow[1].ToString());
                    returnDict.Add(new List<string>(optionSet), 1);
                    optionSet.Clear();
                    optionSet.Add(preReqRow[2].ToString());
                }

            }
            returnDict.Add(new List<string>(optionSet), 1);
            optionSet.Clear();
            return returnDict;
        }

        public static List<string> GetListOfCoursesWithPreReqOf(string thisCourse)
        {
            List<string> returnList = new List<string>();
            string listOfCoursesWithPreReqOf = $"select C.COURSE_NUM, c.CREDIT_HRS, cp.CATEGORY, pr.pre_course, pr.option_num, pr.CREDIT_HRS as \"PreCourse_CrHrs\" \n" +
                                "from COURSE_PROGRAM cp join COURSE c on cp.COURSE_NUM = c.COURSE_NUM\n" +
                                    "join PROGRAM p on p.PROG_NUM = cp.PROG_NUM,\n" +
                                    "(Select p.pre_course, p.option_num, c.CREDIT_HRS, p.course_num\n" +
                                    "from prerequisite p join COURSE c on p.pre_course = c.COURSE_NUM) pr\n" +
                                $"where p.PROG_NUM = '{SelectionSingleton.DropdownSelection}'\n" +
                                    "and cp.COURSE_NUM = pr.course_num \n" +
                                    $"and pr.pre_course  = '{thisCourse}'";
            DBReader dBReader = new DBReader(listOfCoursesWithPreReqOf);
            DataSet preReqCourseOfThisCourseCollection = dBReader.GetTableData();
            foreach (DataRow preReqRow in preReqCourseOfThisCourseCollection.Tables[0].Rows)
            {
                returnList.Add(preReqRow[0].ToString());

            }
            return returnList;
        }
    }
}