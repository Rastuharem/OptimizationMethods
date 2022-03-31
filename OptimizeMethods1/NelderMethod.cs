using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizeMethods1
{
    class NelderMethod
    {
        public const double NelderAlpha = 1; // Здесь меняется коэффициент отражения для метода Нелдера-Мида
        public const double betta = 0.5; // Здесь меняется коэффициент сжатия для метода Нелдера-Мида
        public const double gamma = 2; // Здесь меняется коэффициент растяжения для метода Нелдера-Мида
        public const double NelderEps = 0.0000001; // Здесь меняется погрешность eps для метода Нелдера-Мида

        public readonly double sol;
        public readonly Vector solv;

        public NelderMethod(Vector v, FuncDatabase.Function fun)
        {
            
        }
    }
}