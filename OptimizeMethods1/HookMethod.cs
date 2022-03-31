using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizeMethods1
{
    class HookMethod
    {
        const double HookAlpha = 2; // Здесь меняется коэффициент уменьшения Дельта для метода Хука-Дживса
        const double HookEps = 0.00001; // Здесь меняется погрешность eps для метода Хука-Дживса

        public readonly double sol;
        public readonly Vector solv;

        public HookMethod(Vector v, FuncDatabase.Function fun)
        {

        }
    }
}