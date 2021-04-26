using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Rubjerg.Graphviz;
using System.Text;

namespace CDS_AddOn
{
    public partial class PlanTreeDiagram : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // You can programmatically construct graphs as follows
            RootGraph root = RootGraph.CreateNew("Some Unique Identifier", GraphType.Directed);

            // The node names are unique identifiers within a graph in Graphviz
            Node nodeA = root.GetOrAddNode("A");
            Node nodeB = root.GetOrAddNode("B");
            Node nodeC = root.GetOrAddNode("C");
            Node nodeD = root.GetOrAddNode("D");

            // The edge name is only unique between two nodes
            Edge edgeAB = root.GetOrAddEdge(nodeA, nodeB, "Some edge name");
            Edge edgeBC = root.GetOrAddEdge(nodeB, nodeC, "Some edge name");
            Edge anotherEdgeBC = root.GetOrAddEdge(nodeB, nodeC, "Another edge name");

            // We can attach attributes to nodes, edges and graphs to store information and instruct
            // graphviz by specifying layout parameters. At the moment we only support string
            // attributes. Cgraph assumes that all objects of a given kind (graphs/subgraphs, nodes,
            // or edges) have the same attributes. The attributes first have to be introduced for a
            // certain kind, before we can use it.
            Node.IntroduceAttribute(root, "my attribute", "defaultvalue");
            nodeA.SetAttribute("my attribute", "othervalue");

            // To introduce and set an attribute at the same time, there are convenience wrappers
            edgeAB.SafeSetAttribute("color", "red", "black");
            edgeBC.SafeSetAttribute("arrowsize", "2.0", "1.0");

            // We can simply export this graph to a text file in dot format
            root.ToDotFile(Controllers.SelectionSingleton.treeDotPath);
            root.ComputeLayout();
            root.ToSvgFile(Controllers.SelectionSingleton.treeSvgPath);
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