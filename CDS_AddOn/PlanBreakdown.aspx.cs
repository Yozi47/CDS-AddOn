﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDS_AddOn
{
    public partial class PlanBreakdown : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlanTreeDiagram.aspx");

        }

        protected void RealityVsYourPlanButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RealityVsYourPlan.aspx");

        }
    }
}