using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProektModelirane
{
    public class NameGenerator
    {
        static int flowCounter = 1;
        static int stockCounter = 1;
        static int constantCounter = 1;

        public static String Generate(BuildingBlock b)
        {
            String name = "";
            if (b is Stock)
                name = "Stock" + stockCounter++;
            else if (b is Flow)
                name = "Flow" + flowCounter++;
            else if (b is Constant)
                name = "Constant" + constantCounter++;
            return name;
        }
    }
}
