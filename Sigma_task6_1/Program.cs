using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma_task6_1
{
    class Polynomial
    {
        private double[] poly;
        public Polynomial(params double[] coeff)
        {
            poly = new double[coeff.Length];
            for (int i = 0; i < coeff.Length; i++)
                poly[i] = coeff[i];
        }
        public double this[int index]
        {
            get
            {
                return poly[index];
            }
            set
            {
                if (value != 0)
                {
                    if (poly.Length - 1 >= index)
                        poly[index] = value;
                    if (poly.Length - 1 < index)
                    {
                        double[] newPoly = new double[index + 1];
                        for (int i = 0; i < poly.Length; ++i)
                            newPoly[i] = poly[i];
                        newPoly[index] = value;
                        poly = newPoly;
                    }
                }
                else
                {
                    if (poly.Length - 1 >= index)
                    {
                        poly[index] = value;
                    }
                }
            }
        }

        public override string ToString()
        {
            string print = "";
            for (int i = 0; i < poly.Length; i++)
            {
                if (poly[i] != 0)
                {
                    print += poly[i];
                    if (i != 0)
                        print += "x^" + i;
                    if (i != poly.Length - 1)
                        print += " + ";
                }
            }
            return print;
        }
        public static int max(int m, int n)
        {
            return (m > n) ? m : n;
        }
        public void Parse(string makePolynomial)
        {
            try
            {
                string withoutSpaces = String.Concat(makePolynomial.Where(c => !Char.IsWhiteSpace(c)));
                string[] listCoeffs = withoutSpaces.Split('+');
                double[] newPoly = new double[listCoeffs.Length];

                for (int i = 1; i < listCoeffs.Length; ++i)
                    if (!double.TryParse(listCoeffs[i].Split('x')[0], out newPoly[i]))
                        throw new ArgumentException("Incorrect input!");
                if(!double.TryParse(listCoeffs[0], out newPoly[0]))
                    throw new ArgumentException("Incorrect input!");
                poly = newPoly;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        public static Polynomial operator+(Polynomial pol1, Polynomial pol2)
        {
            int size = max(pol1.poly.Length, pol2.poly.Length);
            double[] sum = new double[size];
            for (int i = 0; i < pol1.poly.Length; ++i)
                sum[i] = pol1.poly[i];
            for (int i = 0; i < pol2.poly.Length; ++i)
                sum[i] += pol2.poly[i];

            return new Polynomial(sum);
        }

        public static Polynomial operator*(Polynomial pol1, Polynomial pol2)
        {
            double[] prod = new double[pol1.poly.Length + pol2.poly.Length - 1];

            for (int i = 0; i < pol1.poly.Length; ++i)
                for (int j = 0; j < pol2.poly.Length; ++j)
                    prod[i + j] += pol1.poly[i] * pol2.poly[j];
            return new Polynomial(prod);
        }

        public static Polynomial operator-(Polynomial pol1, Polynomial pol2)
        {
            int size = max(pol1.poly.Length, pol2.poly.Length);
            double[] sub = new double[size];
            for (int i = 0; i < pol1.poly.Length; ++i)
                sub[i] = pol1.poly[i];
            for (int i = 0; i < pol2.poly.Length; ++i)
                sub[i] -= pol2.poly[i];

            return new Polynomial(sub);
        }

        public static implicit operator Polynomial(double val)
        {
            return new Polynomial(val);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Polynomial test1 = new Polynomial(2, 3, 0, 5, 3);
            Console.WriteLine(test1);

            Polynomial test2 = new Polynomial(1, 2, 5);
            Console.WriteLine(test2);
            Polynomial res = test1 * test2;
            Console.WriteLine(res);
            res = test1 + test2;
            Console.WriteLine(res);
            res = test1 - test2;
            Console.WriteLine(res);

            res = 10.5;
            Console.WriteLine(res);

            test1.Parse("5 + 2x^1 + 3x^2 + 5x^3 + 12x^5");
            Console.WriteLine(test1);
        }
    }
}
