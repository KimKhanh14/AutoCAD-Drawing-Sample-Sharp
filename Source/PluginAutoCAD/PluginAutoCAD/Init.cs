using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace PluginAutoCAD
{
    public class Init : IExtensionApplication
    {
        [CommandMethod("MyFirstCommand")]
        public void cmdFirstCommand()
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;
            //var db = doc.Database;
            var ed = doc.Editor;

            ed.WriteMessage("\nHello Khánh");
        }

        public void Initialize()
        {

        }

        public void Terminate()
        {

        }
    }
}
