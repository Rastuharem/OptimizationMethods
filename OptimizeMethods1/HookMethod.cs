using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizeMethods1
{
    class HookMethod
    {
        const double HookAlpha = 2; // Здесь меняется коэффициент уменьшения Дельта для метода Хука-Дживса
        const double HookEps = 0.000000001; // Здесь меняется погрешность eps для метода Хука-Дживса

        public readonly double sol;
        public readonly Vector solv;

        public HookMethod(Vector v, FuncDatabase.Function fun)
        {
            double delta = 1;
            v.RandomInitialize(-10, 10);

            Console.WriteLine("Возьмем случайную начальную точку: ");
            Console.Write("Точка: (");
            for (int i = 0; i < v.count - 1; i++)
                Console.Write(v.vec[i] + ", ");
            Console.WriteLine(v.vec[v.count-1] + "). Значение функции: " + v.CalculateFun(fun) + "\n");
            var x = new Vector(v.count);
            while (Math.Sqrt(2 * Math.Pow(delta, 2)) >= HookEps)
            { 
                x = v;
                double startfun = x.CalculateFun(fun);
                bool IsGoodSearch = false;
                int k = 0;
                while (!IsGoodSearch && k < v.count)
                {
                    if (MakeExploreSearch(x, k, delta, fun))
                        IsGoodSearch = true;
                    else if (MakeExploreSearch(x, k, delta * (-1), fun))
                        IsGoodSearch = true;
                    k++;
                }
               
                if (Math.Sqrt(2 * Math.Pow(delta, 2)) <= HookEps)
                    break;
                if (startfun != x.CalculateFun(fun))
                {
                    Console.WriteLine("Производим поиск по образцу: ");
                    var new_x = new Vector(v.count);
                    do
                    {
                        for (int i = 0; i < v.count; i++)
                            new_x.vec[i] = 2 * x.vec[i] - v.vec[i];
                        startfun = new_x.CalculateFun(fun);
                        Console.Write("Новая точка: (");
                        for (int i = 0; i < v.count - 1; i++)
                            Console.Write(new_x.vec[i] + ", ");
                        Console.WriteLine(new_x.vec[v.count - 1] + "). Значение функции: " + new_x.CalculateFun(fun) + "\n");
                        if (new_x.CalculateFun(fun) < x.CalculateFun(fun))
                        {
                            v = x;
                            x = new_x;
                            Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
                            Console.Write("Новая точка: (");
                            for (int i = 0; i < v.count - 1; i++)
                                Console.Write(x.vec[i] + ", ");
                            Console.WriteLine(x.vec[v.count - 1] + "). Значение функции: " + x.CalculateFun(fun) + "\n");
                        }
                        else
                        {
                            v = x;
                            Console.WriteLine("Поиск по образцу закончился неудачей. Возвращаемся к исследующему поиску..." + "\n");
                            Console.Write("Текущая точка: (");
                            for (int i = 0; i < v.count - 1; i++)
                                Console.Write(new_x.vec[i] + ", ");
                            Console.WriteLine(new_x.vec[v.count - 1] + "). Значение функции: " + new_x.CalculateFun(fun) + "\n");
                        }
                    } while (startfun < x.CalculateFun(fun));
                }
                else
                {
                    delta = delta / HookAlpha;
                    Console.WriteLine("Исследуемый поиск прошел неудачно :(. Уменьшаем дельту...");
                    Console.WriteLine("Новая дельта: " + delta + "\n");
                }
            }
            Console.WriteLine("Дельта " + delta + " стала меньше Эпсилон " + HookEps + ". Заканчиваем алгоритм..." + "\n");
            solv = v;
            sol = solv.CalculateFun(fun);
        }

        public bool MakeExploreSearch(Vector v, int k, double delta, FuncDatabase.Function fun)
        {
            var delta_v = v;
            Console.WriteLine("Производим исследующий поиск: ");
            Console.WriteLine("Производим приращение х" + (k + 1) + " с дельтой равной " + delta);
            delta_v.vec[k] += delta;
            Console.Write("Проверяем новую точку: (");
            for (int i = 0; i < v.count-1; i++)
                Console.Write(delta_v.vec[i] + ", ");
            Console.WriteLine(delta_v.vec[v.count - 1] + "), Значение в точке: " + delta_v.CalculateFun(fun) + "\n");
            if (delta_v.CalculateFun(fun) < v.CalculateFun(fun))
            {
                v = delta_v;
                Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
                Console.Write("Новая точка: (");
                for (int i = 0; i < v.count - 1; i++)
                    Console.Write(v.vec[i] + ", ");
                Console.WriteLine(v.vec[v.count - 1] + "), Значение в точке: " + v.CalculateFun(fun) + "\n");
                return true;
            }
            else
            {
                Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
                delta_v.vec[k] -= delta;
            }
            return false;
        }
    }
}