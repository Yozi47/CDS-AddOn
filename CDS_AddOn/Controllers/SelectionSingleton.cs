using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;

namespace CDS_AddOn.Controllers
{
    public sealed class SelectionSingleton
    {
        private readonly static SelectionSingleton instance;
        private static string dropdownSelection;
        private static DataSet allCoursesWithoutPrereqs;
        private static List<string> allListedPreReqs;
        public static string treePngPath = Path.GetFullPath(@"C:\Users\ybrad\OneDrive\Desktop\IT680\Phase 3\CDS-AddOn\CDS_AddOn\tree.png");
        public static string treeSvgPath = Path.GetFullPath(@"C:\Users\ybrad\OneDrive\Desktop\IT680\Phase 3\CDS-AddOn\CDS_AddOn\tree.svg");
        public static string treeDotPath = Path.GetFullPath(@"C:\Users\ybrad\OneDrive\Desktop\IT680\Phase 3\CDS-AddOn\CDS_AddOn\tree.dot");



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
    }
}