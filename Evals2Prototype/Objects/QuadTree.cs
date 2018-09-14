using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Evals2Prototype.Objects
{
    class QuadTree
    {
        public BoundingBox Bounds { get; set; }
        public List<QuadTree> Nodes { get; set; }
        public QuadTree NodeBL { get; set; }
        public QuadTree NodeBR { get; set; }
        public QuadTree NodeTL { get; set; }
        public QuadTree NodeTR { get; set; }
        public List<AnimatedSprite> Objects { get; set; }

        public QuadTree()
        {
            BoundingBox g = new BoundingBox()
        }
    }
}
