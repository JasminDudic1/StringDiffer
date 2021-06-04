using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamb
{
    class StaticData
    {

        public static int windowHeight;
        public static int windowWidth;

        public static void Initialize(int height,int width)
        {
            windowHeight = height;
            windowWidth = width;

        }

    }
}
