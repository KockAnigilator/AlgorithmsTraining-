using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11_PipelineOptimization
{
    public class Well
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Well(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}

