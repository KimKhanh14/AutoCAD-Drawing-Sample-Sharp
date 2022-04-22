using System;
using System.Collections.Generic;
using System.Text;

namespace ReadFileSTL
{
    public class TriangleMesh
    {
        public Vector norm1, norm2, norm3;
        public Vector[] vertices;
        public TriangleMesh()
        {
            norm1 = new Vector();
            norm2 = new Vector();
            norm3 = new Vector();

            List<Vector> verts = new List<Vector>();
            for (int i = 0; i < 3; i++)
                verts.Add(new Vector());
            vertices = verts.ToArray();
        }
    }
}
