using System;
using System.Collections.Generic;

namespace OptimizeMethods1
{
    class Program
    {
        const double NelderAlpha = 1; // Здесь меняется коэффициент отражения для метода Нелдера-Мида
        const double betta = 0.5; // Здесь меняется коэффициент сжатия для метода Нелдера-Мида
        const double gamma = 2; // Здесь меняется коэффициент растяжения для метода Нелдера-Мида
        const double NelderEps = 0.0000001; // Здесь меняется погрешность eps для метода Нелдера-Мида
        
        const double HookAlpha = 2; // Здесь меняется коэффициент уменьшения Дельта для метода Хука-Дживса
        const double HookEps = 0.00001; // Здесь меняется погрешность eps для метода Хука-Дживса

        const int n = 2; // Наша размерность функции
        static double[] x = new double[n + 1];
        static double[,] funi = new double[n + 1, n + 1];
        // Матрица имеет вид:
        // {
        // x11 x21 f(x11, x21)
        // x12 x22 f(x12, x22)
        // ...
        // }
        // (3*3)

        private static void Main()
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("1.) Метод Нелдер-Мида");
            Console.WriteLine("------------------------------------------------------------------------------" + "\n");
            Console.WriteLine("Нажмите любую кнопку для продолжения..." + "\n");
            Console.ReadLine();
            double[] NelderSolution = NelderMidMethod(); // { x1, x2, sol }
            Console.WriteLine("Минимальное найденное значение: " + NelderSolution[2]);
            Console.WriteLine("Оно находится в точке: (" + NelderSolution[0] + ", " + NelderSolution[1] + ")." + "\n");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("1.) Метод Хука-Дживса");
            Console.WriteLine("------------------------------------------------------------------------------" + "\n");
            Console.WriteLine("Нажмите любую кнопку для продолжения..." + "\n");
            Console.ReadLine();
            double[] HookSolution = HookDjeevsMethod(); // { x1, x2, sol }
            Console.WriteLine("Минимальное найденное значение: " + HookSolution[2]);
            Console.WriteLine("Оно находится в точке: (" + HookSolution[0] + ", " + HookSolution[1] + ")." + "\n");
        }

        private static double[] NelderMidMethod() {
            Random rnd = new Random();
            double[] sol = new double[n + 1]; // { x1, x2, ..., sol }
            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < n; j++)
                    funi[i, j] = rnd.Next(-10, 10);
                funi[i, n] = RosenbrockFun(funi[i, 0], funi[i, 1]);
            }

            do
            {
                Console.WriteLine("Посчитаем n+1 значений функции:");
                for (int i = 0; i < n + 1; i++)
                    Console.WriteLine("Точка: (" + funi[i, 0] + ", " + funi[i, 1] + "). Значение функции: " + funi[i, 2]);
                Console.WriteLine();

                double[] max = new double[n + 1];
                for (int i = 0; i < n + 1; i++)
                    max[i] = funi[0, i];
                for (int i = 0; i < n + 1; i++)
                    if (max[n] < funi[i, n])
                        for (int j = 0; j < n + 1; j++)
                            max[j] = funi[i,j];
                double[] xh = max; // { x1, x2, f(x1, x2) } - максимальное значение
                double[] min = new double[n + 1];
                for (int i = 0; i < n + 1; i++)
                    min[i] = funi[0, i];
                for (int i = 0; i < n + 1; i++)
                    if (min[n] > funi[i, n])
                        for (int j = 0; j < n + 1; j++)
                            min[j] = funi[i, j];
                double[] xl = min; // { x1, x2, f(x1, x2) } - минимальное значение
                double[] secondmax = new double[n + 1];
                for (int j = 0; j < n + 1; j++)
                    secondmax[j] = min[j];
                for (int i = 0; i < n + 1; i++)
                    if ((secondmax[2] < max[2] && secondmax[2] > min[2]) == false)
                    {
                        for (int j = 0; j < n + 1; j++)
                            secondmax[j] = funi[i, j];
                    }
                double[] xg = secondmax; // { x1, x2, f(x1, x2) } - следующее за максимальным
                double[] xc = new double[n]; // { x1, x2 }
                for (int i = 0; i < n; i++)
                    xc[i] = (xg[i] + xl[i]) / n; // Центр тяжести
                double[] xr = new double[n + 1];
                for (int i = 0; i < n; i++)
                    xr[i] = (1 + NelderAlpha) * xc[i] - NelderAlpha * xh[i]; // Отражение
                xr[n] = RosenbrockFun(xr[0], xr[1]);

                if (xr[n] < xl[n])
                {
                    double[] xe = new double[n + 1];
                    for (int i = 0; i < n; i++)
                        xe[i] = (1 - gamma) * xc[i] + gamma * xr[i]; // Растяжение
                    xe[n] = RosenbrockFun(xe[0], xe[1]);
                    double buf = xh[n];
                    if (xe[n] < xr[n])
                        for (int i = 0; i < n + 1; i++)
                            xh[i] = xe[i];
                    else
                        for (int i = 0; i < n + 1; i++)
                            xh[i] = xr[i];
                    for (int i = 0; i < n + 1; i++)
                        if (buf == funi[i, n])
                        {
                            for (int j = 0; j < n + 1; j++)
                                funi[i, j] = xh[j];
                        }
                }
                else if (xr[n] < xg[n] && xr[n] > xl[n])
                {
                    double buf = xh[n];
                    for (int i = 0; i < n + 1; i++)
                        xh[i] = xr[i];
                    for (int i = 0; i < n + 1; i++)
                        if (buf == funi[i, 2])
                        {
                            for (int j = 0; j < n + 1; j++)
                                funi[i, j] = xh[j];
                        }
                }
                else if (xr[n] > xg[n] && xr[n] < xl[n])
                {
                    double[] buf = new double[n + 1];
                    for (int i = 0; i < n + 1; i++)
                    {
                        buf[i] = xr[i];
                        xr[i] = xh[i];
                        xh[i] = buf[i];
                    }
                    squeeze(xh, xc, xl);
                }
                else
                    squeeze(xh, xc, xl);
                for (int i = 0; i < n + 1; i++)
                    sol[i] = xl[i];
            } while (Math.Sqrt(Math.Pow(funi[1, n] - funi[0, n], 2) + Math.Pow(funi[2, n] - funi[0, n], 2)) > NelderEps);
            return sol;
        }
        private static void squeeze(double[] _xh, double[] _xc, double[] _xl)
        {
            double[] xs = new double[n + 1];
            for (int i = 0; i < n; i++)
                xs[i] = betta * _xh[i] + (1 - betta) * _xc[i]; // "Сжатие"
            xs[n] = RosenbrockFun(xs[0], xs[1]);
            if (xs[n] < _xh[n])
            {
                double buf = _xh[2];
                for (int i = 0; i < n + 1; i++)
                    _xh[i] = xs[i];
                for (int i = 0; i < n + 1; i++)
                    if (buf == funi[i, 2])
                    {
                        for (int j = 0; j < n + 1; j++)
                            funi[i, j] = _xh[j];
                    }
            }
            else
                for (int i = 0; i < n + 1; i++) // Выполняем гомотетию
                {
                    for (int j = 0; j < n; j++)
                        funi[i, j] = _xl[j] + (funi[i, j] - _xl[j]) / 2;
                    funi[i, n] = RosenbrockFun(funi[i, 0], funi[i, 1]);
                }
        }

        private static double[] HookDjeevsMethod()
        {
            Random rnd = new Random();
            double[] sol = new double[n + 1]; // { x1, x2, f(x1,x2) }
            double delta = 1;
            for (int i = 0; i < n; i++)
                x[i] = rnd.Next(-10, 10);
            x[n] = RosenbrockFun(x[0], x[1]);
            Console.WriteLine("Возьмем случайную начальную точку: ");
            Console.WriteLine("Точка: (" + x[0] + ", " + x[1] + "). Значение функции: " + x[n] + "\n");
            double[] x1 = new double[n + 1];
            while (Math.Sqrt(2 * Math.Pow(delta, 2)) >= HookEps)
            {
                for (int i = 0; i < n + 1; i++)
                    x1[i] = x[i];
                bool IsGoodSearch = false;
                while (!IsGoodSearch)
                {
                    Console.WriteLine("Производим исследующий поиск: ");
                    Console.WriteLine("Производим приращение х1 с дельтой равной " + delta);
                    Console.WriteLine("Проверяем новую точку: (" + (x1[0] + delta) + ", " + x1[1] + "), Значение в точке: " + RosenbrockFun(x1[0] + delta, x1[1]) + "\n");
                    double curfun = x1[n];
                    if (RosenbrockFun(x1[0] + delta, x1[1]) < x1[n])
                    {
                        x1[0] += delta;
                        x1[n] = RosenbrockFun(x1[0], x1[1]);
                        IsGoodSearch = true;
                        Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
                        Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
                    }
                    else
                        Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
                    Console.WriteLine("Производим приращение х2 с дельтой равной " + delta);
                    Console.WriteLine("Проверяем новую точку: (" + x1[0] + ", " + (x1[1] + delta) + "), Значение в точке: " + RosenbrockFun(x1[0], x1[1]+delta) + "\n");
                    if (RosenbrockFun(x1[0], x1[1] + delta) < x1[n])
                    {
                        x1[1] += delta;
                        x1[n] = RosenbrockFun(x1[0], x1[1]);
                        IsGoodSearch = true;
                        Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
                        Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
                    }
                    else
                        Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
                    Console.WriteLine("Производим приращение х1 с дельтой равной " + (delta * (-1)));
                    Console.WriteLine("Проверяем новую точку: (" + (x1[0] + delta * (-1)) + ", " + x1[1] + "), Значение в точке: " + RosenbrockFun(x1[0] + (delta * (-1)), x1[1]) + "\n");
                    if (RosenbrockFun(x1[0] + delta * (-1), x1[1]) < x1[n])
                    {
                        x1[0] -= delta;
                        x1[n] = RosenbrockFun(x1[0], x1[1]);
                        IsGoodSearch = true;
                        Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
                        Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
                    }
                    else
                        Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
                    Console.WriteLine("Производим приращение х2 с дельтой равной " + (delta * (-1)));
                    Console.WriteLine("Проверяем новую точку: (" + x1[0] + ", " + (x1[1] + delta * (-1)) + "), Значение в точке: " + RosenbrockFun(x1[0], x1[1] + (delta * (-1))) + "\n");
                    if (RosenbrockFun(x1[0], x1[1] + delta * (-1)) < x1[n])
                    {
                        x1[1] -= delta;
                        x1[n] = RosenbrockFun(x1[0], x1[1]);
                        IsGoodSearch = true;
                        Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
                        Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
                    }
                    else
                        Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
                    if (Math.Sqrt(2 * Math.Pow(delta, 2)) <= HookEps)
                        break;
                    if (curfun != x1[n])
                    {
                        Console.WriteLine("Производим поиск по образцу: ");
                        double[] x2 = new double[n+1];
                        do
                        {
                            for (int i = 0; i < n; i++)
                                x2[i] = 2 * x1[i] - x[i];
                            x2[n] = RosenbrockFun(x2[0], x2[1]);
                            curfun = x2[n];
                            Console.WriteLine("Новая точка: (" + x2[0] + ", " + x2[1] + "). Значение функции: " + x2[n] + "\n");
                            if (x2[n] < x1[n])
                            {
                                for (int i = 0; i < n + 1; i++)
                                {
                                    x[i] = x1[i];
                                    x1[i] = x2[i];
                                }
                                Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
                                Console.WriteLine("Новая точка: (" + x1[0] + ", " + x1[1] + "), Значение в точке: " + x1[n]);
                            }
                            else
                            {
                                for (int i = 0; i < n + 1; i++)
                                    x[i] = x1[i];
                                Console.WriteLine("Поиск по образцу закончился неудачей. Возвращаемся к исследующему поиску..." + "\n");
                                Console.WriteLine("Текущая точка: (" + x[0] + ", " + x[1] + "), Значение в точке: " + x[n]);

                            }
                        } while (curfun < x1[n]);
                    }
                    else
                    {
                        delta = delta / HookAlpha;
                        Console.WriteLine("Исследуемый поиск прошел неудачно :(. Уменьшаем дельту...");
                        Console.WriteLine("Новая дельта: " + delta + "\n");
                    }
                }
            }
            Console.WriteLine("Дельта " + delta + " стала меньше Эпсилон " + HookEps + ". Заканчиваем алгоритм..." + "\n");
            for (int i = 0; i < n + 1; i++)
                sol[i] = x[i];
            return sol;
        }

        private static double RosenbrockFun(double _x1, double _x2)
        {
            return Math.Pow((1 - _x1), 2) + 100 * Math.Pow((_x2 - Math.Pow(_x1, 2)), 2);
        }
    }
}