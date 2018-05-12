using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProektModelirane
{
    public class Simulation
    {
        private Double time;
        private Double step;
        private Dictionary<String, Double> variables;
        private Dictionary<String, String> derivatives;

        private Dictionary<String, List<Double>> variable_history;

        public Simulation(Double time, Double step, Dictionary<String, Double> variables, Dictionary<String, String> derivatives)
        {
            this.variables = variables;
            this.derivatives = derivatives;
            this.time = time;
            this.step = step;
            variable_history = new Dictionary<String, List<Double>>();
        }

        public Dictionary<String, List<Double>> RunSimulation()
        {
            /* * * 
                     * Метод на Ойлер
                     * derivative.Key - името на прозводната, y'
                     * derivative.Value - изразът на производната, f(y)
                     * Hares[0] = 100           - y[0] - начална стойност
                     * Hares' = Hares*10/100    - y' = f(y) = derivative.Value
                     * h = 1                    - стъпката Step
                     * t = 5                    - време Time
                     * Hares[0] = 100
                     * Hares[1] = Hares[0] + h*Hares' = 100 + 1*(100*10/100) = 100 + 10 = 110
                     * Hares[2] = Hares[1] + h*Hares' = 110 + 1*(110*10/100) = 110 + 11 = 121
                     * Hares[3] = Hares[2] + h*Hares' = 121 + 1*(121*10/100) = 121 + 12,1 = 133,1
                     * 
                     * с унифицирана формула:
                     * Hares[N+1] = Hares[N] + h*Hares' //където Hares' се изчислява за времевия интервал N
                     * y[N+1]     = y[N] + Step * f(y[N])
                     * 
                     * стойностите y[N] се съхраняват в списък List<Double>, който е стойност на variable_history
                     * f(y[N]) се изчислява на всяка итерация и се съхранява като стойност на variables
                     * * */
            foreach (String variableName in variables.Keys)
            {
                variable_history.Add(variableName, new List<Double>());
                //добавяме началната стойност в списъка от резултати - y[0]
                variable_history[variableName].Add(variables[variableName]);
            }

            for (Double t = step; t <= time; t += step)
            {
                foreach (KeyValuePair<String, String> derivative in derivatives)
                {
                    String expression = derivative.Value;

                    foreach (KeyValuePair<String, Double> variable in variables)
                        expression = expression.Replace(variable.Key, variable.Value.ToString());

                    //
                    Double dExpr = PostfixCalculator.Startup.SolveRPN(expression);

                    Double nextValue = variables[derivative.Key] + (step * dExpr);

                    variable_history[derivative.Key].Add(nextValue);
                }

                foreach (KeyValuePair<String, List<Double>> vhPair in variable_history)
                {
                    variables[vhPair.Key] = vhPair.Value[vhPair.Value.Count - 1];
                }
            }
            return variable_history;
        }

    }
}
