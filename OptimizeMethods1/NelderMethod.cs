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
            List<Vector> Simplex = new List<Vector>();
            for (int i = 0; i < v.count + 1; i++)
            {
                var SimpComp = new Vector(v.count);
                SimpComp.RandomInitialize(-10, 10);
                Simplex.Add(SimpComp);
            }

            do
            {
                Console.WriteLine("Посчитаем n+1 значений функции:");
                for (int i = 0; i < Simplex.Count; i++)
                {
                    Console.Write("Точка: (");
                    for (int j = 0; j < v.count - 1; j++)
                        Console.Write(Simplex[i].vec[j] + ", ");
                    Console.WriteLine(Simplex[i].vec[v.count-1] + "). Значение функции: " + Simplex[i].CalculateFun(fun));
                }
                Console.WriteLine();

                var xh = Simplex[0]; // { x1, x2, ..., xn } - максимальное значение
                for (int i = 0; i < Simplex.Count; i++)
                    if (xh.CalculateFun(fun) < Simplex[i].CalculateFun(fun))
                        xh = Simplex[i];
                var xl = Simplex[0];  // { x1, x2, ..., xn } - минимальное значение
                for (int i = 0; i < Simplex.Count; i++)
                    if (xl.CalculateFun(fun) > Simplex[i].CalculateFun(fun))
                        xl = Simplex[i];
                var xg = xl;  // { x1, x2, ..., xn } - следующее за максимальным
                for (int i = 0; i < Simplex.Count; i++)
                    if (xg.CalculateFun(fun) < Simplex[i].CalculateFun(fun) && Simplex[i].CalculateFun(fun) != xh.CalculateFun(fun))
                        xg = Simplex[i];
                var xc = new Vector(v.count); // { x1, x2, ..., xn } - Центр тяжести
                for (int i = 0; i < v.count; i++)
                    xc.vec[i] = (xg.vec[i] + xl.vec[i]) / v.count;
                var xr = new Vector(v.count); // { x1, x2, ..., xn } - Отражение
                for (int i = 0; i < v.count; i++)
                    xr.vec[i] = (1 + NelderAlpha) * xc.vec[i] - NelderAlpha * xh.vec[i];

                if (xr.CalculateFun(fun) < xl.CalculateFun(fun))
                {
                    var xe = new Vector(v.count); // { x1, x2, ..., xn } - Растяжение
                    for (int i = 0; i < v.count; i++)
                        xe.vec[i] = (1 - gamma) * xc.vec[i] + gamma * xr.vec[i];
                    var buf = xh.CalculateFun(fun);
                    if (xe.CalculateFun(fun) < xr.CalculateFun(fun))
                        xh = xe;
                    else
                        xh = xr;
                    for (int i = 0; i < Simplex.Count; i++)
                        if (buf == Simplex[i].CalculateFun(fun))
                            Simplex[i] = xh;
                }
                else if (xr.CalculateFun(fun) < xg.CalculateFun(fun) && xr.CalculateFun(fun) > xl.CalculateFun(fun))
                {
                    var buf = xh.CalculateFun(fun);
                    xh = xr;
                    for (int i = 0; i < Simplex.Count; i++)
                        if (buf == Simplex[i].CalculateFun(fun))
                            Simplex[i] = xh;
                }
                else if (xr.CalculateFun(fun) > xg.CalculateFun(fun) && xr.CalculateFun(fun) < xl.CalculateFun(fun))
                {
                    var buf = new Vector(v.count);
                    buf = xr;
                    xr = xh;
                    xh = buf;
                    Squeeze(xh, xc, xl, Simplex, fun);
                }
                else
                    Squeeze(xh, xc, xl, Simplex, fun);
                solv = xl;
                sol = solv.CalculateFun(fun);
            } while (CycleChecker(Simplex, fun) > NelderEps);
        }

        private static void Squeeze(Vector xh, Vector xc, Vector xl, List<Vector> simp, FuncDatabase.Function fun)
        {
            var xs = new Vector(xl.count); // { x1, x2, ..., xn } - Сжатие
            for (int i = 0; i < xs.count; i++)
                xs.vec[i] = betta * xh.vec[i] + (1 - betta) * xc.vec[i];
            if (xs.CalculateFun(fun) < xh.CalculateFun(fun))
            {
                var buf = xh.CalculateFun(fun);
                xh = xs;
                for (int i = 0; i < simp.Count; i++)
                    if (buf == simp[i].CalculateFun(fun))
                        simp[i] = xh;
            }
            else
                for (int i = 0; i < simp.Count; i++) // Выполняем гомотетию
                    for (int j = 0; j < xl.count; j++)
                        simp[i].vec[j] = xl.vec[j] + (simp[i].vec[j] - xl.vec[j]) / 2;
        }
        private static double CycleChecker(List<Vector> simp, FuncDatabase.Function fun)
        {
            double answ = 0;
            for (int i = 1; i<simp.Count;i++)
                answ += Math.Pow(simp[i].CalculateFun(fun)-simp[0].CalculateFun(fun), 2);
            return Math.Sqrt(answ);
        }
    }
}