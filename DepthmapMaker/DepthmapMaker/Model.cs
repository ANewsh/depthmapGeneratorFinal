using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthmapMaker
{
    internal class Model
    {
        List<float> vertices = new List<float>();
        List<uint> vertexIndices = new List<uint>();

        public Model(List<float> vectors, List<uint> indicies)
        {
            vertices = vectors;
            vertexIndices = indicies;
        }
        public List<float> Vertices { get { return vertices; } }
        public List<uint> VertexIndices { get { return vertexIndices; } }
    }
}
