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
        



        private static bool rootHasValue = false;
        public static void ConvertToNodes(DataSet aSet,string allCoursesCourseNumColumnName, SubGraph clusterName, string clusterStringRepr)
        {
            clusterName = root.GetOrAddSubgraph(clusterStringRepr);
            foreach (DataRow courseRow in aSet.Tables[0].Rows)
            {
                Node existingCourseNode = root.GetOrAddNode(courseRow[$"{allCoursesCourseNumColumnName}"].ToString());
                existingCourseNode.SafeSetAttribute("color", "green", "none");
                clusterName.AddExisting(existingCourseNode);
                //courseWithNoPrereqsCluster.AddExisting(existingCourseNode);
                //existingCourseNode.Edges().Count();
            }
            rootHasValue = true;
                        
        }
        public static void CreateEdgesWithPreReqs(string preReqCourseNumColumnName, string allCoursesCourseNumColumnName, string preReqCoursePreCourseColumnName)
        {

            DataTable preReqTable = SelectionSingleton.AllListedPrereqs.Tables[0];
            foreach (DataRow courseRow in SelectionSingleton.CoursesWithPreReqs.Tables[0].Rows)
            {
                foreach (DataRow preReqRow in preReqTable.Rows)
                {
                    if (courseRow[allCoursesCourseNumColumnName].ToString().Equals(preReqRow[preReqCourseNumColumnName].ToString()))
                    {
                        //AlignNodes(root.GetNode(courseRow[allCoursesCourseNumColumnName].ToString()));
                        if (!(root.GetNode(preReqRow[preReqCoursePreCourseColumnName].ToString()) is null))
                        {
                            //AlignNodes(root.GetNode(preReqRow[preReqCoursePreCourseColumnName].ToString()));
                            root.GetOrAddEdge(root.GetNode(preReqRow[preReqCoursePreCourseColumnName].ToString()),
                                                root.GetNode(courseRow[allCoursesCourseNumColumnName].ToString()), "Pre-Req to");
                        }
                        else
                        {
                            Node newPreReqCourseNode = root.GetOrAddNode(preReqRow[preReqCoursePreCourseColumnName].ToString());
                            newPreReqCourseNode.SafeSetAttribute("color", "red", "none");
                            //AlignNodes(newPreReqCourseNode);

                            root.GetOrAddEdge(newPreReqCourseNode,
                                              root.GetNode(courseRow[allCoursesCourseNumColumnName].ToString()), "Pre-Req to");
                        }
                    }
                }
            }
            rootHasValue = true;

        }

        public static void CreateEdgesWithPreReqs(string courseNameColumn, string preCourseColumn, SubGraph setWithPreReqs, string subGraphName)
        {
            DataTable preReqTable = SelectionSingleton.AllListedPrereqs.Tables[0];
            setWithPreReqs = root.GetOrAddSubgraph(subGraphName);

            foreach (DataRow preReqRow in preReqTable.Rows)
            {

                //AlignNodes(root.GetNode(courseRow[allCoursesCourseNumColumnName].ToString()));
                if (!(root.GetNode(preReqRow[preCourseColumn].ToString()) is null))
                {
                    //AlignNodes(root.GetNode(preReqRow[preReqCoursePreCourseColumnName].ToString()));
                    root.GetOrAddEdge(root.GetNode(preReqRow[preCourseColumn].ToString()),
                                        root.GetNode(preReqRow[courseNameColumn].ToString()), "Pre-Req to");
                }
                else
                {
                    Node newPreReqCourseNode = root.GetOrAddNode(preReqRow[preCourseColumn].ToString());
                    newPreReqCourseNode.SafeSetAttribute("color", "red", "none");
                    //AlignNodes(newPreReqCourseNode);
                    setWithPreReqs.AddExisting(newPreReqCourseNode);
                    root.GetOrAddEdge(newPreReqCourseNode,
                                        root.GetNode(preReqRow[courseNameColumn].ToString()), "Pre-Req to");
                }
            }
            rootHasValue = true;

        }

        public static void FillSvg()
        {
            if (rootHasValue)
            {
                root.ComputeLayout();
                root.ToSvgFile(SelectionSingleton.treeSvgPath);
            }
            else
            {
                throw new NullReferenceException("Content is empty in Graph");
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