using System;
using System.Collections.Generic;

public class Polynom
{
    public int degree;
    public int[] coefficients;



    public Polynom(params int[] coefficients)
    {

        this.coefficients = coefficients;
        SetDegree(this);
    }

    public static void SetDegree(Polynom p)
    {

        int count = 0;

        for (int i = 0; i < p.coefficients.Length; i++)
        {
            if (p.coefficients[i] == 1)
            {
                p.degree = i + 1;
                count++;
            }
        }
        if (count == 0)
            p.degree = 0;
    }

    public static void PrintPolynom(Polynom p)
    {
        for (int i = 0; i < p.coefficients.Length; i++)
        {
            Console.WriteLine($"x{i}  " + p.coefficients[i]);
        }
        Console.WriteLine("\n");
    }

    public static void PrintDel(KeyValuePair<Polynom, Polynom> pair)
    {
        Console.WriteLine("Частное = ");
        for (int i = 0; i < pair.Key.coefficients.Length; i++)
        {
            Console.WriteLine($"x{i}  " + pair.Key.coefficients[i]);
        }
        Console.WriteLine("\n");
        Console.WriteLine("Остаток = ");
        for (int i = 0; i < pair.Value.coefficients.Length; i++)
        {
            Console.WriteLine($"x{i}  " + pair.Value.coefficients[i]);
        }
        Console.WriteLine("\n");
    }

    public static Polynom operator *(Polynom polynom1, Polynom polynom2)
    {
        int[] coeffs = new int[polynom1.coefficients.Length + polynom2.coefficients.Length - 1];
        for (int i = 0; i < polynom1.coefficients.Length; ++i)
            for (int j = 0; j < polynom2.coefficients.Length; ++j)
                coeffs[i + j] += polynom1.coefficients[i] * polynom2.coefficients[j];
        for (int k = 0; k < coeffs.Length; k++)
            if (coeffs[k] % 2 == 0)
                coeffs[k] = 0;
            else
                coeffs[k] = 1;
        return new Polynom(coeffs);
    }

    public static Polynom operator +(Polynom p1, Polynom p2)
    {
        Polynom res;
        if (p1.degree >= p2.degree)
            res = new Polynom(p1.coefficients);
        else
            res = new Polynom(p2.coefficients);

        for (int i = 0; i < res.degree; i++)
        {
            if (i >= p1.degree)
                res.coefficients[i] = p2.coefficients[i];
            else if (i >= p2.degree)
                res.coefficients[i] = p1.coefficients[i];
            else if ((p1.coefficients[i] + p2.coefficients[i]) % 2 == 0)
                res.coefficients[i] = 0;
            else
                res.coefficients[i] = 1;
        }
        SetDegree(res);
        return res;
    }

    public static KeyValuePair<Polynom, Polynom> operator /(Polynom p11, Polynom p22)
    {
        int[] minus;
        int[] res;
        int[] temp1 = new int[p11.coefficients.Length];
        int[] temp2 = new int[p22.coefficients.Length];
        p11.coefficients.CopyTo(temp1, 0);
        p22.coefficients.CopyTo(temp2, 0);
        Polynom p1 = new Polynom(temp1);
        Polynom p2 = new Polynom(temp2);
        if (p1.degree >= p2.degree)
        {
            res = new int[p1.degree - p2.degree + 1];
            minus = new int[p1.coefficients.Length];
            SetDegree(p1);
            SetDegree(p2);

            while (p1.degree >= p2.degree && p1.degree != 0)
            {

                res[p1.degree - p2.degree] = 1;

                for (int j = p2.degree; j > 0; j--)
                {
                    if (res[p1.degree - p2.degree] * p2.coefficients[j - 1] != 0)
                    {
                        minus[p1.degree - p2.degree + j - 1] = 1;
                    }
                    else
                        minus[p1.degree - p2.degree + j - 1] = 0;
                }
                for (int k = p1.degree - 1; k >= 0; k--)
                    if (p1.coefficients[k] != minus[k])
                        p1.coefficients[k] = 1;
                    else if (p1.coefficients[k] == minus[k])
                        p1.coefficients[k] = 0;

                SetDegree(p1);

            }
            Array.Resize(ref p1.coefficients, p1.degree + 1);
            return new KeyValuePair<Polynom, Polynom>(new Polynom(res), p1);
        }
        else
        {
            //res = new int[p2.degree - p1.degree + 1];
            //minus = new int[p2.coefficients.Length];
            //SetDegree(p2);
            //SetDegree(p2);

            //while (p2.degree >= p1.degree && p2.degree != 0)
            //{

            //    res[p2.degree - p1.degree] = 1;

            //    for (int j = p1.degree; j > 0; j--)
            //    {
            //        if (res[p2.degree - p1.degree] * p1.coefficients[j - 1] != 0)
            //        {
            //            minus[p2.degree - p1.degree + j - 1] = 1;
            //        }
            //        else
            //            minus[p2.degree - p1.degree + j - 1] = 0;
            //    }
            //    for (int k = p2.degree - 1; k >= 0; k--)
            //        if (p2.coefficients[k] != minus[k])
            //            p2.coefficients[k] = 1;
            //        else if (p2.coefficients[k] == minus[k])
            //            p2.coefficients[k] = 0;

            //    SetDegree(p2);

            //}
            //Array.Resize(ref p2.coefficients, p2.degree + 1);
            //return new KeyValuePair<Polynom, Polynom>(new Polynom(res), p2);
            SetDegree(p1);
            Array.Resize(ref p1.coefficients, p1.degree + 1);
            return new KeyValuePair<Polynom, Polynom>(new Polynom(0), p1);
        }


    }


    public static List<Polynom[,]> NOD(Polynom p11, Polynom p22)
    {
        int[] temp1 = new int[p11.coefficients.Length];
        int[] temp2 = new int[p22.coefficients.Length];
        p11.coefficients.CopyTo(temp1, 0);
        p22.coefficients.CopyTo(temp2, 0);
        Polynom p1 = new Polynom(temp1);
        Polynom p2 = new Polynom(temp2);
        KeyValuePair<Polynom, Polynom> p;
        Polynom[,] matrix;
        List<Polynom[,]> polynoms = new List<Polynom[,]>();
        if (p1.degree >= p2.degree)
        {
            p = p1 / p2;
            while (p.Value.degree > 0)
            {

                matrix = new Polynom[2, 2] { { new Polynom(p.Key.coefficients), new Polynom(1) }, { new Polynom(1), new Polynom(0) } };
                polynoms.Add(matrix);
                p1 = p2;
                p2 = p.Value;
                p = p1 / p2;
            }
            matrix = new Polynom[2, 2] { { new Polynom(p.Key.coefficients), new Polynom(1) }, { new Polynom(1), new Polynom(0) } };
            polynoms.Add(matrix);
        }
        else
        {
            p = p2 / p1;
            while (p.Value.degree > 0)
            {
                matrix = new Polynom[2, 2] { { new Polynom(p.Key.coefficients), new Polynom(1) }, { new Polynom(1), new Polynom(0) } };
                polynoms.Add(matrix);
                p2 = p1;
                p1 = p.Value;
                p = p2 / p1;

            }
        }
        polynoms.Reverse();
        return polynoms;
    }

    public static Polynom[,] Mult(List<Polynom[,]> polynoms)
    {
        while (polynoms.Count > 1)
        {
            polynoms[0] = Mult(polynoms[0], polynoms[1]);
            polynoms.RemoveAt(1);
        }
        return polynoms[0];
    }

    public static Polynom[,] Mult(Polynom[,] p1, Polynom[,] p2)
    {
        Polynom[,] r = new Polynom[2, 2] { { new Polynom(0), new Polynom(0) }, { new Polynom(0), new Polynom(0) } };
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    r[i, j] += p1[i, k] * p2[k, j];
                }
            }
        }
        return r;
    }


    static void Main()
    {
        var a1 = new Polynom(1,0,0,0,1);
        var a2 = new Polynom(0,0,1,1);
        var a3 = new Polynom(1);

        var u1 = new Polynom(1,0,0,1,0,1);
        var u2 = new Polynom(1,1,1,1,0,1);
        var u3 = new Polynom(1,0,1,1,1,1);

        var u = u1 * u2 * u3;

        var temp1 = u / u1;

        var temp2 = u / u2;

        var temp3 = u / u3;



        var ux1 = NOD(temp1.Key, u1);

        var ux2 = NOD(temp2.Key, u2);

        var ux3 = NOD(temp3.Key, u3);

        
        var s1 = Mult(ux1);
        var s2 = Mult(ux2);
        var s3 = Mult(ux3);

        var e1 = s1[1, 1] * temp1.Key;
        var e2 = s2[1, 1] * temp2.Key;
        var e3 = s3[1, 1] * temp3.Key;

        var sum = a1 * e1 + a2 * e2 + a3 * e3;
        var f = sum/u;


        //PrintPolynom(sum);


        PrintDel(f);
        Console.ReadKey();
    }
}