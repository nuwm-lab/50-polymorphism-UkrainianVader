using System;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 
    abstract class Solid
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        protected Solid(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        // Return volume of the solid
        public abstract double Volume();

        // Print coefficients / parameters of the solid
        public virtual void PrintCoefficients()
        {
            Console.WriteLine($"Center: ({X}, {Y}, {Z})");
        }
    }
    class Ellipsoid : Solid
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public Ellipsoid(double x, double y, double z, double a, double b, double c) : base(x, y, z)
        {
            if (a <= 0 || b <= 0 || c <= 0) throw new ArgumentOutOfRangeException("axes must be positive");
            A = a; B = b; C = c;
        }

        public override double Volume()
        {
            // Volume of ellipsoid: 4/3 * pi * a * b * c
            return (4.0 / 3.0) * Math.PI * A * B * C;
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine("Ellipsoid:");
            base.PrintCoefficients();
            Console.WriteLine($"Axes: a={A}, b={B}, c={C}");
        }
    }
 class Sphere : Solid
    {
        public double Radius { get; }

        public Sphere(double x, double y, double z, double radius) : base(x, y, z)
        {
            if (radius <= 0) throw new ArgumentOutOfRangeException(nameof(radius));
            Radius = radius;
        }

        public override double Volume()
        {
            return (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine("Sphere:");
            base.PrintCoefficients();
            Console.WriteLine($"Radius: {Radius}");
        }
    }
    class Program

    {
        static void Main(string[] args)
        {
            // Demonstration of polymorphism: create different solids (sphere, ellipsoid)
            // and handle them through a common base type `Solid`.

            Solid[] solids = new Solid[] {
                new Sphere(0, 0, 0, 2.5),
                new Ellipsoid(1, 1, 1, 2.0, 3.0, 4.0)
            };

            foreach (var s in solids)
            {
                s.PrintCoefficients();           // virtual/override — resolved at runtime
                Console.WriteLine($"Volume = {s.Volume():F4}\n"); // abstract implemented by derived
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
