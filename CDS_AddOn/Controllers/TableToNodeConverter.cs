using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Rubjerg.Graphviz;

namespace CDS_AddOn.Controllers
{
    public class TableToNodeConverter
    {
        private static RootGraph root = RootGraph.CreateNew("Courses Tree Diagrams", GraphType.Directed);

        //private static SubGraph courseWithNoPrereqsCluster = root.GetOrAddSubgraph("Cluster of Courses without Prereqs");
        //private static SubGraph courseWithPrereqsCluster = root.GetOrAddSubgraph("Cluster of Courses with Prereqs");
        //private static SubGraph course100LevelCluster = courseWithPrereqsCluster.GetOrAddSubgraph("Cluster of 100 Level");
        //private static SubGraph course200LevelCluster = courseWithPrereqsCluster.GetOrAddSubgraph("Cluster of 200 Level");
        //private static SubGraph course300LevelCluster = courseWithPrereqsCluster.GetOrAddSubgraph("Cluster of 300 Level");
        //private static SubGraph course400LevelCluster = courseWithPrereqsCluster.GetOrAddSubgraph("Cluster of 400 Level");


        // Dictionary<Course Node,[Credit Hours, Head Count,Tail Count]> == Example => {{IT210:[4,2,3]}
        private static Dictionary<Node, int[]> nodesWithHeadsAndTailsCount = new Dictionary<Node, int[]>();



        private static bool rootHasValue = false;
        public static void ConvertToNodes(DataSet aSet, string nodeColumnName, string nodeCrHrs)
        {
            //clusterName = root.GetOrAddSubgraph(clusterStringRepr);
            foreach (DataRow courseRow in aSet.Tables[0].Rows)
            {
                Node existingCourseNode = root.GetOrAddNode(courseRow[nodeColumnName].ToString());
                existingCourseNode.SafeSetAttribute("color", "green", "none");
                AddToDataStructure(existingCourseNode, int.Parse(courseRow[nodeCrHrs].ToString()));
                //clusterName.AddExisting(existingCourseNode);
                //courseWithNoPrereqsCluster.AddExisting(existingCourseNode);
                //existingCourseNode.Edges().Count();
            }
            rootHasValue = true;
                        
        }
        //public static void CreateEdgesWithPreReqs(string preReqCourseNumColumnName, string allCoursesCourseNumColumnName, string preReqCoursePreCourseColumnName)
        //{

        //    DataTable preReqTable = SelectionSingleton.AllListedPrereqs.Tables[0];
        //    foreach (DataRow courseRow in SelectionSingleton.CoursesWithPreReqs.Tables[0].Rows)
        //    {
        //        foreach (DataRow preReqRow in preReqTable.Rows)
        //        {
        //            if (courseRow[allCoursesCourseNumColumnName].ToString().Equals(preReqRow[preReqCourseNumColumnName].ToString()))
        //            {
        //                //AlignNodes(root.GetNode(courseRow[allCoursesCourseNumColumnName].ToString()));
        //                if (!(root.GetNode(preReqRow[preReqCoursePreCourseColumnName].ToString()) is null))
        //                {
        //                    //AlignNodes(root.GetNode(preReqRow[preReqCoursePreCourseColumnName].ToString()));
        //                    root.GetOrAddEdge(root.GetNode(preReqRow[preReqCoursePreCourseColumnName].ToString()),
        //                                        root.GetNode(courseRow[allCoursesCourseNumColumnName].ToString()), "Pre-Req to");
        //                }
        //                else
        //                {
        //                    Node newPreReqCourseNode = root.GetOrAddNode(preReqRow[preReqCoursePreCourseColumnName].ToString());
        //                    newPreReqCourseNode.SafeSetAttribute("color", "red", "none");
        //                    //AlignNodes(newPreReqCourseNode);

        //                    root.GetOrAddEdge(newPreReqCourseNode,
        //                                      root.GetNode(courseRow[allCoursesCourseNumColumnName].ToString()), "Pre-Req to");
        //                }
        //            }
        //        }
        //    }
        //    rootHasValue = true;

        //}

        public static void CreateEdgesWithPreReqsV2(string headColumnName, string tailColumnName, string headCrHrs, string tailCrHrs)
        {
            DataTable preReqTable = SelectionSingleton.AllListedPrereqs.Tables[0];
            //setWithPreReqs = root.GetOrAddSubgraph(subGraphName);

            foreach (DataRow preReqRow in preReqTable.Rows)
            {

                //AlignNodes(root.GetNode(courseRow[allCoursesCourseNumColumnName].ToString()));
                if (!(root.GetNode(preReqRow[tailColumnName].ToString()) is null))
                {
                    //AlignNodes(root.GetNode(preReqRow[preReqCoursePreCourseColumnName].ToString()));
                    root.GetOrAddEdge(root.GetNode(preReqRow[tailColumnName].ToString()),
                                        root.GetNode(preReqRow[headColumnName].ToString()), "Pre-Req to");
                    ItsTail(root.GetNode(preReqRow[tailColumnName].ToString()), int.Parse(preReqRow[tailCrHrs].ToString()));
                    ItsHead(root.GetNode(preReqRow[headColumnName].ToString()), int.Parse(preReqRow[headCrHrs].ToString()));

                }
                else
                {
                    Node newPreReqCourseNode = root.GetOrAddNode(preReqRow[tailColumnName].ToString());
                    newPreReqCourseNode.SafeSetAttribute("color", "red", "none");
                    //AlignNodes(newPreReqCourseNode);
                    //setWithPreReqs.AddExisting(newPreReqCourseNode);
                    root.GetOrAddEdge(newPreReqCourseNode,
                                        root.GetNode(preReqRow[headColumnName].ToString()), "Pre-Req to");
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
                root.ToPngFile(SelectionSingleton.treePngPath);
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
            SubGraph noHeadsCluster = root.GetOrAddSubgraph("Cluster of Courses without Prereqs");
            noHeadsCluster.SafeSetAttribute("label", "Cluster of Courses without Prereqs", "");
            SubGraph someHeadsSomeTailsCluster = root.GetOrAddSubgraph("Cluster of Courses some connections both ways");
            SubGraph noTailsCluster = root.GetOrAddSubgraph("Cluster of Courses that is not Pre-req to any other courses");
            SubGraph noHeadsNoTailsCluster = root.GetOrAddSubgraph("Cluster of Courses without any connections");
            noHeadsNoTailsCluster.SafeSetAttribute("rank", "max", "same");
            noHeadsCluster.SafeSetAttribute("rank", "min", "same"); // TODO: Try seperating min heads and tails with max heads and tails. It may clean it up a little.
                                                                    // TODO: Find a way to bring subgraphs down. 


            foreach (Node course in nodesWithHeadsAndTailsCount.Keys)
            {
               
                if(nodesWithHeadsAndTailsCount[course][1] == 0 && nodesWithHeadsAndTailsCount[course][2] > 0)
                {
                    noHeadsCluster.AddExisting(course);
                }
                else if (nodesWithHeadsAndTailsCount[course][1] > 0 && nodesWithHeadsAndTailsCount[course][2] == 0)
                {
                    noTailsCluster.AddExisting(course);

                }
                else if (nodesWithHeadsAndTailsCount[course][1] == 0 && nodesWithHeadsAndTailsCount[course][2] == 0)
                {
                    noHeadsNoTailsCluster.AddExisting(course);
                }

            }


        }
        //public static void AlignNodes(Node aCourseNode)
        //{
        //    string courseCode = aCourseNode.GetName();
        //    int courseLevel;
        //    bool conversion = int.TryParse(courseCode.Substring(courseCode.Length - 3, 3), out courseLevel);
        //    if ( conversion && courseLevel >= 400)
        //    {
        //        course400LevelCluster.AddExisting(aCourseNode);
        //    }
        //    else if (conversion && courseLevel >= 300)
        //    {
        //        course300LevelCluster.AddExisting(aCourseNode);
        //    }
        //    else if (conversion && courseLevel >= 200)
        //    {
        //        course200LevelCluster.AddExisting(aCourseNode);
        //    }
        //    else if (conversion && courseLevel > 0)
        //    {
        //        course100LevelCluster.AddExisting(aCourseNode);
        //    }

        //}

        //// You can programmatically construct graphs as follows
        //RootGraph root = RootGraph.CreateNew("Some Unique Identifier", GraphType.Directed);

        //// The node names are unique identifiers within a graph in Graphviz
        //Node nodeA = root.GetOrAddNode("A");
        //Node nodeB = root.GetOrAddNode("B");
        //Node nodeC = root.GetOrAddNode("C");
        //Node nodeD = root.GetOrAddNode("D");

        //// The edge name is only unique between two nodes
        //Edge edgeAB = root.GetOrAddEdge(nodeA, nodeB, "Some edge name");
        //Edge edgeBC = root.GetOrAddEdge(nodeB, nodeC, "Some edge name");
        //Edge anotherEdgeBC = root.GetOrAddEdge(nodeB, nodeC, "Another edge name");

        //// We can attach attributes to nodes, edges and graphs to store information and instruct
        //// graphviz by specifying layout parameters. At the moment we only support string
        //// attributes. Cgraph assumes that all objects of a given kind (graphs/subgraphs, nodes,
        //// or edges) have the same attributes. The attributes first have to be introduced for a
        //// certain kind, before we can use it.
        //Node.IntroduceAttribute(root, "my attribute", "defaultvalue");
        //nodeA.SetAttribute("my attribute", "othervalue");

        //// To introduce and set an attribute at the same time, there are convenience wrappers
        //edgeAB.SafeSetAttribute("color", "red", "black");
        //edgeBC.SafeSetAttribute("arrowsize", "2.0", "1.0");

        //// We can simply export this graph to a text file in dot format
        //root.ToDotFile(Controllers.SelectionSingleton.treeDotPath);
        //root.ComputeLayout();
        //root.ToSvgFile(Controllers.SelectionSingleton.treeSvgPath);

    }
}