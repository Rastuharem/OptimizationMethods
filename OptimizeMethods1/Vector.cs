using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizeMethods1
{
    class Vector
    {
        public readonly int count;
        public double[] vec { get; set; }
        
        public Vector(int n, double[] x)
        {
            count = n;
            vec = x;
        }
        public Vector(int n)
        {
            count = n;
            vec = new double[n];
        }
        public Vector(Vector v)
        {
            count = v.count;
            vec = v.vec;
        }

        public double CalculateFun(FuncDatabase.Function fun)
        {
            var buf = new Vector(this);
            return fun.Invoke(buf);
        }
        public void RandomInitialize(int minval, int maxval)
        {
            var rnd = new Random();
            for (int i = 0;i<count;i++)
                vec[i] = rnd.Next(minval, maxval);
        }

        public static Vector operator+ (Vector v1, Vector v2)
        {
            if (v1.count < v2.count)
            {
                var buf = new Vector(v2);
                v2 = v1;
                v1 = buf;
            }
            Vector sol = new Vector(v1);
            for (int i = 0; i < v2.count; i++)
                sol.vec[i] += v2.vec[i];
            return sol;
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.count < v2.count)
            {
                var buf = new Vector(v2);
                v2 = v1;
                v1 = buf;
            }
            Vector sol = new Vector(v1);
            for (int i = 0; i < v2.count; i++)
                sol.vec[i] -= v2.vec[i];
            return sol;
        }
        public static bool operator== (Vector v1, Vector v2)
        {
            if (v1.count != v2.count)
                return false;
            for (int i = 0; i < v1.count; i++)
                    if (v1.vec[i] != v2.vec[i])
                        return false;
            return true;
        }
        public static bool operator!= (Vector v1, Vector v2)
        {
            if (v1 == v2)
                return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}