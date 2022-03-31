using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizeMethods1
{
    class FuncDatabase
    {
        public delegate double Function(Vector v);
        public Function Rosenbrock { get; set; }

        public FuncDatabase()
        {
            Rosenbrock = RosenbrockFun;
        }

        private double RosenbrockFun(Vector v)
        {
            double sol=0;
            for (int i = 0; i < v.count - 1; i++)
                sol += Math.Pow(1 - v.vec[i], 2) + 100 * Math.Pow(v.vec[i + 1] - Math.Pow(v.vec[i], 2), 2);
            return sol;
        }
    }
}
