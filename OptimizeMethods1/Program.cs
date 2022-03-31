using System;

namespace OptimizeMethods1
{
    class Program
    {
        const int n = 2; // Наша размерность функции
        static Vector x = new Vector(n);

        private static void Main()
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("1.) Метод Нелдер-Мида");
            Console.WriteLine("------------------------------------------------------------------------------" + "\n");
            Console.WriteLine("Нажмите любую кнопку для продолжения..." + "\n");
            Console.ReadLine();
            NelderMethod Nelder = new NelderMethod(x, FuncDatabase.Rosenbrock);
            Console.WriteLine("Минимальное найденное значение: " + Nelder.sol);
            Console.Write("Оно находится в точке: (");
            for (int i = 0; i < Nelder.solv.count - 1; i++)
                Console.Write(Nelder.solv.vec[i] + ", ");
            Console.WriteLine(Nelder.solv.vec[x.count-1] + ")." + "\n");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("1.) Метод Хука-Дживса");
            Console.WriteLine("------------------------------------------------------------------------------" + "\n");
            Console.WriteLine("Нажмите любую кнопку для продолжения..." + "\n");
            Console.ReadLine();
            HookMethod Hook = new HookMethod(x, FuncDatabase.Rosenbrock);
            Console.WriteLine("Минимальное найденное значение: " + Hook.sol);
            Console.Write("Оно находится в точке: (");
            for (int i = 0; i < Hook.solv.count - 1; i++)
                Console.Write(Hook.solv.vec[i] + ", ");
            Console.WriteLine(Hook.solv.vec[x.count - 1] + ")." + "\n");
        }

        //private static double[] HookDjeevsMethod()
        //{
        //    Random rnd = new Random();
        //    double[] sol = new double[n + 1]; // { x1, x2, f(x1,x2) }
        //    double delta = 1;
        //    for (int i = 0; i < n; i++)
        //        x[i] = rnd.Next(-10, 10);
        //    x[n] = RosenbrockFun(x[0], x[1]);
        //    Console.WriteLine("Возьмем случайную начальную точку: ");
        //    Console.WriteLine("Точка: (" + x[0] + ", " + x[1] + "). Значение функции: " + x[n] + "\n");
        //    double[] x1 = new double[n + 1];
        //    while (Math.Sqrt(2 * Math.Pow(delta, 2)) >= HookEps)
        //    {
        //        for (int i = 0; i < n + 1; i++)
        //            x1[i] = x[i];
        //        bool IsGoodSearch = false;
        //        while (!IsGoodSearch)
        //        {
        //            Console.WriteLine("Производим исследующий поиск: ");
        //            Console.WriteLine("Производим приращение х1 с дельтой равной " + delta);
        //            Console.WriteLine("Проверяем новую точку: (" + (x1[0] + delta) + ", " + x1[1] + "), Значение в точке: " + RosenbrockFun(x1[0] + delta, x1[1]) + "\n");
        //            double curfun = x1[n];
        //            if (RosenbrockFun(x1[0] + delta, x1[1]) < x1[n])
        //            {
        //                x1[0] += delta;
        //                x1[n] = RosenbrockFun(x1[0], x1[1]);
        //                IsGoodSearch = true;
        //                Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
        //                Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
        //            }
        //            else
        //                Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
        //            Console.WriteLine("Производим приращение х2 с дельтой равной " + delta);
        //            Console.WriteLine("Проверяем новую точку: (" + x1[0] + ", " + (x1[1] + delta) + "), Значение в точке: " + RosenbrockFun(x1[0], x1[1]+delta) + "\n");
        //            if (RosenbrockFun(x1[0], x1[1] + delta) < x1[n])
        //            {
        //                x1[1] += delta;
        //                x1[n] = RosenbrockFun(x1[0], x1[1]);
        //                IsGoodSearch = true;
        //                Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
        //                Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
        //            }
        //            else
        //                Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
        //            Console.WriteLine("Производим приращение х1 с дельтой равной " + (delta * (-1)));
        //            Console.WriteLine("Проверяем новую точку: (" + (x1[0] + delta * (-1)) + ", " + x1[1] + "), Значение в точке: " + RosenbrockFun(x1[0] + (delta * (-1)), x1[1]) + "\n");
        //            if (RosenbrockFun(x1[0] + delta * (-1), x1[1]) < x1[n])
        //            {
        //                x1[0] -= delta;
        //                x1[n] = RosenbrockFun(x1[0], x1[1]);
        //                IsGoodSearch = true;
        //                Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
        //                Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
        //            }
        //            else
        //                Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
        //            Console.WriteLine("Производим приращение х2 с дельтой равной " + (delta * (-1)));
        //            Console.WriteLine("Проверяем новую точку: (" + x1[0] + ", " + (x1[1] + delta * (-1)) + "), Значение в точке: " + RosenbrockFun(x1[0], x1[1] + (delta * (-1))) + "\n");
        //            if (RosenbrockFun(x1[0], x1[1] + delta * (-1)) < x1[n])
        //            {
        //                x1[1] -= delta;
        //                x1[n] = RosenbrockFun(x1[0], x1[1]);
        //                IsGoodSearch = true;
        //                Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
        //                Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
        //            }
        //            else
        //                Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
        //            if (Math.Sqrt(2 * Math.Pow(delta, 2)) <= HookEps)
        //                break;
        //            if (curfun != x1[n])
        //            {
        //                Console.WriteLine("Производим поиск по образцу: ");
        //                double[] x2 = new double[n+1];
        //                do
        //                {
        //                    for (int i = 0; i < n; i++)
        //                        x2[i] = 2 * x1[i] - x[i];
        //                    x2[n] = RosenbrockFun(x2[0], x2[1]);
        //                    curfun = x2[n];
        //                    Console.WriteLine("Новая точка: (" + x2[0] + ", " + x2[1] + "). Значение функции: " + x2[n] + "\n");
        //                    if (x2[n] < x1[n])
        //                    {
        //                        for (int i = 0; i < n + 1; i++)
        //                        {
        //                            x[i] = x1[i];
        //                            x1[i] = x2[i];
        //                        }
        //                        Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
        //                        Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
        //                    }
        //                    else
        //                    {
        //                        for (int i = 0; i < n + 1; i++)
        //                            x[i] = x1[i];
        //                        Console.WriteLine("Поиск по образцу закончился неудачей. Возвращаемся к исследующему поиску..." + "\n");
        //                        Console.WriteLine("Текущая точка: (" + x[0] + ", " + x[1] + "), Значение в точке: " + x[n]);

        //                    }
        //                } while (curfun < x1[n]);
        //            }
        //            else
        //            {
        //                delta = delta / HookAlpha;
        //                Console.WriteLine("Исследуемый поиск прошел неудачно :(. Уменьшаем дельту...");
        //                Console.WriteLine("Новая дельта: " + delta + "\n");
        //            }
        //        }
        //    }
        //    Console.WriteLine("Дельта " + delta + " стала меньше Эпсилон " + HookEps + ". Заканчиваем алгоритм..." + "\n");
        //    for (int i = 0; i < n + 1; i++)
        //        sol[i] = x[i];
        //    return sol;
        //}
    }
}