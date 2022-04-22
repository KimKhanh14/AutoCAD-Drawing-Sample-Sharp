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
using ReadFileSTL;

namespace PluginAutoCAD
{
    public class Init : IExtensionApplication
    {
        [CommandMethod("MyReadSTL")]
        public void cmdReadSTL()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            Database db = HostApplicationServices.WorkingDatabase;

            Transaction trans = db.TransactionManager.StartTransaction();

            try
            {
                //Mã không gian vẽ
                ObjectId blockId = db.CurrentSpaceId;
                //Chọn không gian vẽ
                BlockTableRecord curSpace = trans.GetObject(blockId, OpenMode.ForWrite) as BlockTableRecord;

                ReadSTL stl_read = new ReadSTL();
                TriangleMesh[] mesh_arr = stl_read.ReadASCII_STL("Untitled1.stl");
                foreach (TriangleMesh mesh in mesh_arr)
                {
                    int step = 0; //Kiểm tra điểm đang xét là điểm thứ mấy
                    Point3d point1 = new Point3d();
                    Point3d point2 = new Point3d();
                    foreach (Vector vert in mesh.vertices)
                    {
                        if(step == 0) //Đọc điểm thứ nhất
                        {
                            point1 = new Point3d(vert.x, vert.y, vert.z);
                            step = 1;
                        }
                        else if(step == 1) //Đọc điểm thứ hai
                        {
                            point2 = new Point3d(vert.x, vert.y, vert.z);
                            step = 2;

                        }
                        else //Đọc điểm thứ 3
                        {
                            //Vẽ các đường thẳng từ 3 điểm đã cho tạo thành một tam giác
                            Point3d point3 = new Point3d(vert.x, vert.y, vert.z);
                            Line lineOb = new Line(point1, point3);
                            curSpace.AppendEntity(lineOb);
                            trans.AddNewlyCreatedDBObject(lineOb, true);

                            lineOb = new Line(point2, point3);
                            curSpace.AppendEntity(lineOb);
                            trans.AddNewlyCreatedDBObject(lineOb, true);

                            lineOb = new Line(point1, point2);
                            curSpace.AppendEntity(lineOb);
                            trans.AddNewlyCreatedDBObject(lineOb, true);
                        }
                    }
             
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex) //Nếu chương trình chạy không được sẽ báo lỗi
            {
                Application.ShowAlertDialog("Fail!");
            }
            trans.Commit();
        }

        public void Initialize()
        {

        }

        public void Terminate()
        {

        }
    }
}
