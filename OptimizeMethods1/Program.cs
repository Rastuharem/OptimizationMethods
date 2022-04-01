using System;

namespace OptimizeMethods1
{
    class Program
    {
        const int n = 2; // Наша размерность функции

        private static void Main()
        {
            Vector x = new Vector(n);
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("1.) Метод Нелдер-Мида");
            Console.WriteLine("------------------------------------------------------------------------------" + "\n");
            Console.WriteLine("Нажмите любую кнопку для продолжения..." + "\n");
            Console.ReadLine();
            NelderMethod Nelder = new NelderMethod(x, FuncDatabase.Rosenbrock);
            Console.WriteLine("ОТВЕТ:");
            Nelder.solv.ConsoleOut(FuncDatabase.Rosenbrock);
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("1.) Метод Хука-Дживса");
            Console.WriteLine("------------------------------------------------------------------------------" + "\n");
            Console.WriteLine("Нажмите любую кнопку для продолжения..." + "\n");
            Console.ReadLine();
            HookMethod Hook = new HookMethod(x, FuncDatabase.Rosenbrock);
            Console.WriteLine("ОТВЕТ:");
            Hook.solv.ConsoleOut(FuncDatabase.Rosenbrock);
        }
    }
}