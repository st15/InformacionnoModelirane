using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProektModelirane
{
    public abstract class BuildingBlock
    {
        protected String name;
        protected String expression;
        protected Rectangle rectangle;
        protected List<ActionConnector> connectors = new List<ActionConnector>();


        public BuildingBlock()
        {
            this.name = NameGenerator.Generate(this);
            this.expression = "";
            this.rectangle = new Rectangle();
        }

        public BuildingBlock(String name, String value)
        {
            this.name = name;
            this.expression = value;
        }

        public String GetName()
        {
            return name;
        }

        public String GetExpression()
        {
            return expression;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public void SetExpression(String value)
        {
            this.expression = value;
        }

        public void Connect(ActionConnector r)
        {
            this.connectors.Add(r);
        }

        public List<ActionConnector> GetConnectors()
        {
            return this.connectors;
        }

        public void RemoveConnector(ActionConnector r)
        {
            if (connectors.Contains(r))
                connectors.Remove(r);
        }

        public void Move(Point p)
        {
            this.rectangle.Location = p;
        }

        public int Contains(Point p)
        {
            return (rectangle.Contains(p) ? 0 : -1);
        }

        public Rectangle GetBounds()
        {
            return rectangle;
        }
    }
}
