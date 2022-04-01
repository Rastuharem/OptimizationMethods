using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizeMethods1
{
    class HookMethod : Method
    {
        const double HookAlpha = 2; // Здесь меняется коэффициент уменьшения Дельта для метода Хука-Дживса
        const double HookEps = 0.0000001; // Здесь меняется погрешность eps для метода Хука-Дживса

        public HookMethod(Vector v, FuncDatabase.Function fun) : base(v, fun) { }

        protected override Vector DoAlgorithm(Vector v, FuncDatabase.Function fun)
        {
            v.RandomInitialize(-10, 10);
            Console.WriteLine("Возьмем случайную начальную точку: ");
            v.ConsoleOut(fun);

            var x = new Vector(v.count);
            double delta = 1;
            while (Math.Sqrt(2 * Math.Pow(delta, 2)) >= HookEps)
            {
                x = new Vector(v);
                double startfun = x.CalculateFun(fun);
                bool IsGoodSearch = false;
                int k = 0;
                while (!IsGoodSearch && k < v.count)
                {
                    x = MakeExploreSearch(x, k, delta, fun);
                    if (x.CalculateFun(fun) < v.CalculateFun(fun))
                    {
                        IsGoodSearch = true;
                        v = new Vector(x);
                    }
                    else
                    {
                        x = MakeExploreSearch(x, k, delta * (-1), fun);
                        if (x.CalculateFun(fun) < v.CalculateFun(fun))
                        {
                            IsGoodSearch = true;
                            v = new Vector(x);
                        }
                    }
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
                        new_x.ConsoleOut(fun);
                        if (new_x.CalculateFun(fun) < x.CalculateFun(fun))
                        {
                            v = x;
                            x = new_x;
                            Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
                            x.ConsoleOut(fun);
                        }
                        else
                        {
                            v = x;
                            Console.WriteLine("Поиск по образцу закончился неудачей. Возвращаемся к исследующему поиску..." + "\n");
                            new_x.ConsoleOut(fun);
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
            return v;
        }
        private Vector MakeExploreSearch(Vector v, int k, double delta, FuncDatabase.Function fun)
        {
            var delta_v = new Vector(v);
            Console.WriteLine("Производим исследующий поиск: ");
            Console.WriteLine("Производим приращение х" + (k + 1) + " с дельтой равной " + delta);
            delta_v.vec[k] += delta;
            Console.Write("Проверяем новую точку: (");
            delta_v.ConsoleOut(fun);
            if (delta_v.CalculateFun(fun) < v.CalculateFun(fun))
            {
                v = new Vector(delta_v);
                Console.WriteLine("Значение функции в новой точке меньше предыдущей! Меняем." + "\n");
                v.ConsoleOut(fun);
            }
            else
                Console.WriteLine("Значение функции в новой точке не меньше предыдущей :(. Проверяем следующую..." + "\n");
            return v;
        }
    }
}