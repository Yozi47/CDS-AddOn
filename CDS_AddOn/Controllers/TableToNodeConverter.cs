using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Rubjerg.Graphviz;
using CDS_AddOn.Models;

namespace CDS_AddOn.Controllers
{
    public class TableToNodeConverter
    {
        private static RootGraph root = RootGraph.CreateNew("Courses Tree Diagrams", GraphType.Directed);

        // Dictionary<Course Node,[Credit Hours, Head Count,Tail Count]> == Example => {{IT210:[4,2,3]}
        private static Dictionary<Node, int[]> nodesWithHeadsAndTailsCount = new Dictionary<Node, int[]>();
        public static List<Course> listOfAllCourses = new List<Course>();



        private static bool rootHasValue = false;
        public static void ConvertToNodes(DataSet aSet, string nodeColumnName, string nodeCrHrs)
        {
            //clusterName = root.GetOrAddSubgraph(clusterStringRepr);
            foreach (DataRow courseRow in aSet.Tables[0].Rows)
            {
                Node existingCourseNode = root.GetOrAddNode(courseRow[nodeColumnName].ToString());
                existingCourseNode.SafeSetAttribute("color", "green", "none");
                AddToDataStructure(existingCourseNode, int.Parse(courseRow[nodeCrHrs].ToString()));
                listOfAllCourses.Add(new Course(existingCourseNode, int.Parse(courseRow[nodeCrHrs].ToString())));
                
            }
            rootHasValue = true;
                        
        }

        public static void CreateEdgesWithPreReqsV2(string headColumnName, string tailColumnName, string headCrHrs, string tailCrHrs, string optionNum)
        {
            DataTable preReqTable = SelectionSingleton.AllListedPrereqs.Tables[0];
            //setWithPreReqs = root.GetOrAddSubgraph(subGraphName);
            foreach (DataRow preReqRow in preReqTable.Rows)
            {

                //AlignNodes(root.GetNode(courseRow[allCoursesCourseNumColumnName].ToString()));
                if (!(root.GetNode(preReqRow[tailColumnName].ToString()) is null))
                {
                    //AlignNodes(root.GetNode(preReqRow[preReqCoursePreCourseColumnName].ToString()));
                    Edge newEdge = root.GetOrAddEdge(root.GetNode(preReqRow[tailColumnName].ToString()),
                                        root.GetNode(preReqRow[headColumnName].ToString()), "Pre-Req to");
                    newEdge.SafeSetAttribute("label", $"{preReqRow[optionNum]}", "");
                    ItsTail(root.GetNode(preReqRow[tailColumnName].ToString()), int.Parse(preReqRow[tailCrHrs].ToString()));
                    ItsHead(root.GetNode(preReqRow[headColumnName].ToString()), int.Parse(preReqRow[headCrHrs].ToString()));

                }
                else
                {
                    Node newPreReqCourseNode = root.GetOrAddNode(preReqRow[tailColumnName].ToString());
                    newPreReqCourseNode.SafeSetAttribute("color", "red", "none");
                    listOfAllCourses.Add(new Course(newPreReqCourseNode, int.Parse(preReqRow[tailCrHrs].ToString())));

                    //AlignNodes(newPreReqCourseNode);
                    //setWithPreReqs.AddExisting(newPreReqCourseNode);
                    Edge newEdge = root.GetOrAddEdge(newPreReqCourseNode,
                                        root.GetNode(preReqRow[headColumnName].ToString()), "Pre-Req to");
                    newEdge.SafeSetAttribute("label", $"{preReqRow[optionNum]}", "");
                    ItsTail(newPreReqCourseNode, int.Parse(preReqRow[tailCrHrs].ToString()));
                    ItsHead(root.GetNode(preReqRow[headColumnName].ToString()), int.Parse(preReqRow[headCrHrs].ToString()));
                }
            }
            rootHasValue = true;

        }

        public static void FillSvg()
        {
            if (rootHasValue)
            {
                root.ComputeLayout();
                root.ToDotFile(SelectionSingleton.treeDotPath);
                //root.ToPngFile(SelectionSingleton.treePngPath);
                root.ToSvgFile(SelectionSingleton.treeSvgPath);
            }
            else
            {
                throw new NullReferenceException("Content is empty in Graph");
            }
            
        }
        private static void ItsHead(Node aCourseNode, int CreditHours)
        {
            if (nodesWithHeadsAndTailsCount.ContainsKey(aCourseNode))
            {
                nodesWithHeadsAndTailsCount[aCourseNode][1] += 1;
            }
            else
            {
                AddToDataStructure(aCourseNode, CreditHours);
                ItsHead(aCourseNode, CreditHours);
            }
        }

        private static void ItsTail(Node aCourseNode, int CreditHours)
        {
            if (nodesWithHeadsAndTailsCount.ContainsKey(aCourseNode))
            {
                nodesWithHeadsAndTailsCount[aCourseNode][2] += 1;
            }
            else
            {
                AddToDataStructure(aCourseNode,CreditHours);
                ItsTail(aCourseNode, CreditHours);
            }
        }
        private static void AddToDataStructure(Node aCourseNode, int creditHours)
        {
            if (!nodesWithHeadsAndTailsCount.ContainsKey(aCourseNode))
            {
                int[] nodeContents = { creditHours, 0, 0 };
                nodesWithHeadsAndTailsCount.Add(aCourseNode, nodeContents);
            }
        }

        public static void ArrangeNoHeadsToNoTails()
        {
            SubGraph noHeadsNoTailsCluster = root.GetOrAddSubgraph("Cluster of Courses without any connections");
            SubGraph withTailsOrHeads = root.GetOrAddSubgraph("Cluster of Courses with connections");
            SubGraph noHeadsCluster = withTailsOrHeads.GetOrAddSubgraph("Cluster of Courses without Prereqs");
            noHeadsCluster.SafeSetAttribute("label", "Cluster of Courses without Prereqs", "");
            //SubGraph someHeadsSomeTailsCluster = withTailsOrHeads.GetOrAddSubgraph("Cluster of Courses some connections both ways");
            //SubGraph noTailsCluster = withTailsOrHeads.GetOrAddSubgraph("Cluster of Courses that is not Pre-req to any other courses");


            //noHeadsNoTailsCluster.SafeSetAttribute("rank", "max", "same");
            //noHeadsCluster.SafeSetAttribute("rank", "min", "same"); // TODO: Try seperating min heads and tails with max heads and tails. It may clean it up a little.
            // TODO: Find a way to bring subgraphs down. 


            foreach (Node course in nodesWithHeadsAndTailsCount.Keys)
            {
               
                
                //else
                if (nodesWithHeadsAndTailsCount[course][1] == 0 && nodesWithHeadsAndTailsCount[course][2] == 0)
                {
                    noHeadsNoTailsCluster.AddExisting(course);
                }
                else
                {
                    withTailsOrHeads.AddExisting(course);
                    //if (nodesWithHeadsAndTailsCount[course][1] == 0 && nodesWithHeadsAndTailsCount[course][2] > 0)
                    //{
                    //    noHeadsCluster.AddExisting(course);
                    //}
                    //else if (nodesWithHeadsAndTailsCount[course][1] >= 2 && nodesWithHeadsAndTailsCount[course][2] >=2)
                    //{
                    //    someHeadsSomeTailsCluster.AddExisting(course);
                    //}
                    //else if (nodesWithHeadsAndTailsCount[course][1] > 0 && nodesWithHeadsAndTailsCount[course][2] == 0)
                    //{
                    //    noTailsCluster.AddExisting(course);
                    //}
                }

            }


        }


        public static void ConnectCourseWithBestPreReqSet(string headColumnName, string tailCrHrs, string headCrHrs) 
        {
            DataTable preReqTable = SelectionSingleton.AllListedPrereqs.Tables[0];
            foreach (DataRow preReqRow in preReqTable.Rows)
            {
                string thisCourse = preReqRow[headColumnName].ToString();
                List<string> bestSet = GetBestPreReqSetOf(thisCourse);
                foreach(string course in bestSet)
                {
                    if (!(root.GetNode(course) is null))
                    {
                        root.GetOrAddEdge(root.GetNode(course),
                                        root.GetNode(thisCourse), "Pre-Req to");
                        ItsTail(root.GetNode(course), int.Parse(preReqRow[tailCrHrs].ToString()));
                        ItsHead(root.GetNode(thisCourse), int.Parse(preReqRow[headCrHrs].ToString()));

                    }
                    else
                    {
                        Node newPreReqCourseNode = root.GetOrAddNode(course);
                        newPreReqCourseNode.SafeSetAttribute("color", "red", "none");
                        listOfAllCourses.Add(new Course(newPreReqCourseNode, int.Parse(preReqRow[tailCrHrs].ToString())));

                        root.GetOrAddEdge(newPreReqCourseNode,
                                            root.GetNode(thisCourse), "Pre-Req to");
                        ItsTail(newPreReqCourseNode, int.Parse(preReqRow[tailCrHrs].ToString()));
                        ItsHead(root.GetNode(thisCourse), int.Parse(preReqRow[headCrHrs].ToString()));
                    }

                }
            }

        }

        private static List<string> GetBestPreReqSetOf(string thisCourse) 
        {
            List<string> theBestSet = new List<string>();
            Dictionary<List<string>, double> setsOfPreReqs = GetMostUsedDataSets.GetListOfPreReqsFor(thisCourse);
            Dictionary<List<string>, double> setsOfPreReqsSecondaryCheck = new Dictionary<List<string>, double>();
            Dictionary<List<string>, double> setsOfPreReqsConstantCopy = new Dictionary<List<string>, double>(setsOfPreReqs);

            int longestSet = 0;
            foreach (List<string> setOfPreReqs in setsOfPreReqs.Keys)
            {
                if (setOfPreReqs.Count >= longestSet)
                {
                    longestSet = setOfPreReqs.Count;
                }
            }

            //DFS
            foreach (List<string> setOfPreReqs in setsOfPreReqsConstantCopy.Keys)
            {
                foreach(string course in setOfPreReqs)
                {
                    if(!(root.GetNode(course) is null))
                    {
                        setsOfPreReqs[setOfPreReqs] += longestSet/setOfPreReqs.Count;
                    }
                }
            }
            setsOfPreReqsConstantCopy = new Dictionary<List<string>, double>(setsOfPreReqs);

            double maxValue = setsOfPreReqs.Values.Max();
            foreach (List<string> setOfPreReqs in setsOfPreReqsConstantCopy.Keys)
            {
                if(setsOfPreReqs[setOfPreReqs] >= maxValue)
                {
                    setsOfPreReqsSecondaryCheck.Add(setOfPreReqs, 1);
                }
            }
            Dictionary<List<string>, double> setsOfPreReqsSecondaryCheckConstantCopy = new Dictionary<List<string>, double>(setsOfPreReqsSecondaryCheck);
            //DFS
            foreach (List<string> secondSets in setsOfPreReqsSecondaryCheckConstantCopy.Keys)
            {
                foreach (string course in secondSets)
                {
                    List<string> heads = GetMostUsedDataSets.GetListOfCoursesWithPreReqOf(course);
                    foreach(string headCourse in heads)
                    {
                        if (!(root.GetNode(headCourse) is null))
                        {
                            setsOfPreReqsSecondaryCheck[secondSets] += longestSet / secondSets.Count;
                        }
                    }
                }

            }
            setsOfPreReqsSecondaryCheckConstantCopy = new Dictionary<List<string>, double>(setsOfPreReqsSecondaryCheck);
            double secondaryMaxValue = setsOfPreReqsSecondaryCheckConstantCopy.Values.Max();
            foreach (List<string> secondSets in setsOfPreReqsSecondaryCheckConstantCopy.Keys)
            {
                if(setsOfPreReqsSecondaryCheckConstantCopy[secondSets] >= secondaryMaxValue)
                {
                    theBestSet = secondSets;
                }
            }
            return theBestSet;

        }

    }
}