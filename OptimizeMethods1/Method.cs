using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizeMethods1
{
    abstract class Method
    {
        public readonly double sol;
        public readonly Vector solv;

        public Method(Vector v, FuncDatabase.Function fun)
        {
            solv = DoAlgorithm(v, fun);
            sol = solv.CalculateFun(fun);
        }

        protected abstract Vector DoAlgorithm(Vector v, FuncDatabase.Function fun);
    }
}