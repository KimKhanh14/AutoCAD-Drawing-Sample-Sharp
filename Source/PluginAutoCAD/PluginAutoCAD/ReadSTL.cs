using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ReadFileSTL
{
    public class ReadSTL
    {
        private bool processError;
        public ReadSTL()
        {
            processError = false;
        }
        public TriangleMesh[] ReadASCII_STL(string file)
        {
            List<TriangleMesh> meshes = new List<TriangleMesh>();
            StreamReader stl_reader = new StreamReader(file);
            string line_str;
            while (!stl_reader.EndOfStream)
            {
                line_str = stl_reader.ReadLine().Trim();
                string[] line_data = line_str.Split(' ');
                if (line_data[0] == "solid")
                {
                    while (line_data[0] != "endsolid")
                    {
                        line_str = stl_reader.ReadLine().Trim();
                        line_data = line_str.Split(' ');

                        if (line_data[0] == "endsolid")
                            break;

                        TriangleMesh mesh = new TriangleMesh();

                        try
                        {
                            mesh.norm1.x = double.Parse(line_data[2]);
                            mesh.norm1.y = double.Parse(line_data[3]);
                            mesh.norm1.z = double.Parse(line_data[4]);

                            mesh.norm2 = mesh.norm3 = mesh.norm1;

                            line_str = stl_reader.ReadLine();

                            foreach (Vector vert in mesh.vertices)
                            {
                                line_str = stl_reader.ReadLine().Trim();
                                while (line_str.IndexOf("  ") != -1)
                                    line_str = line_str.Replace("  ", " ");
                                line_data = line_str.Split(' ');
                                vert.x = double.Parse(line_data[1]);
                                vert.y = double.Parse(line_data[2]);
                                vert.z = double.Parse(line_data[3]);
                            }

                        }
                        catch
                        {
                            processError = true;
                            break;
                        }

                        line_str = stl_reader.ReadLine();
                        line_str = stl_reader.ReadLine();

                        meshes.Add(mesh);
                    }
                }
            }
            return meshes.ToArray();
        }
    }
}
