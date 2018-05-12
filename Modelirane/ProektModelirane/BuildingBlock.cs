using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProektModelirane
{
    public class BuildingBlock
    {
        String name;
        String expression;

        public BuildingBlock()
        {
            this.name = "";
            this.expression = "";
        }       
                
        public BuildingBlock(String name, String expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public String GetName()
        {
            return name;
        }

        public String GetExpression()
        {
            return expression;
        }
 
    }
}
