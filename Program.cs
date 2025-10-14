using System;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 123123123123
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 
    // Base class implements IDisposable and a finalizer (destructor)
    abstract class Solid : IDisposable
    {
    public double X { get; protected set; }
    public double Y { get; protected set; }
    public double Z { get; protected set; }

        protected Solid(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
            Console.WriteLine($"Solid created at ({X}, {Y}, {Z})");
        }

        // Set-method to allow updating center coordinates after construction
        public void SetCenter(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
            Console.WriteLine($"Center updated to ({X}, {Y}, {Z})");
        }

        // Return volume of the solid
        public abstract double Volume();

        // Print coefficients / parameters of the solid
        public void PrintCoefficients()
        {
            Console.WriteLine($"Center: ({X}, {Y}, {Z})");
        }

        // --- IDisposable pattern ---
        private bool _disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation to allow overrides in derived classes.
    protected void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                // free managed resources here (none in this demo)
                Console.WriteLine($"Disposing managed resources of Solid at ({X}, {Y}, {Z})");
            }

            // free unmanaged resources here (none in this demo)
            Console.WriteLine($"Finalizing/cleaning native part of Solid at ({X}, {Y}, {Z})");

            _disposed = true;
        }

        // Finalizer (destructor) — called by the GC if Dispose wasn't called.
        ~Solid()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
    }
    class Ellipsoid : Solid
    {
        public double A { get; protected set; }
        public double B { get; protected set; }
        public double C { get; protected set; }

        public Ellipsoid(double x, double y, double z, double a, double b, double c) : base(x, y, z)
        {
            if (a <= 0 || b <= 0 || c <= 0) throw new ArgumentOutOfRangeException("axes must be positive");
            A = a; B = b; C = c;
        }

        // set-method to update axes
        public void SetAxes(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0) throw new ArgumentOutOfRangeException("axes must be positive");
            A = a; B = b; C = c;
            Console.WriteLine($"Ellipsoid axes updated to a={A}, b={B}, c={C}");
        }

        public override double Volume()
        {
            // Volume of ellipsoid: 4/3 * pi * a * b * c
            return (4.0 / 3.0) * Math.PI * A * B * C;
        }

        public void PrintEllipsoidCoefficients()
        {
            Console.WriteLine("Ellipsoid:");
            base.PrintCoefficients();
            Console.WriteLine($"Axes: a={A}, b={B}, c={C}");
        }

        // Override disposal to demonstrate derived cleanup
        protected void DisposeEllipsoid(bool disposing)
        {
            if (disposing)
            {
                Console.WriteLine($"Disposing managed resources of Ellipsoid at ({X}, {Y}, {Z})");
            }
            Console.WriteLine($"Cleaning native part of Ellipsoid at ({X}, {Y}, {Z})");
            base.Dispose(disposing);
        }
    }
 class Sphere : Solid
    {
        public double Radius { get; protected set; }

        public Sphere(double x, double y, double z, double radius) : base(x, y, z)
        {
            if (radius <= 0) throw new ArgumentOutOfRangeException(nameof(radius));
            Radius = radius;
        }

        // set-method to update radius
        public void SetRadius(double r)
        {
            if (r <= 0) throw new ArgumentOutOfRangeException(nameof(r));
            Radius = r;
            Console.WriteLine($"Sphere radius updated to {Radius}");
        }

        public override double Volume()
        {
            return (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
        }

        public void PrintSphereCoefficients()
        {
            Console.WriteLine("Sphere:");
            base.PrintCoefficients();
            Console.WriteLine($"Radius: {Radius}");
        }

        protected void DisposeSphere(bool disposing)
        {
            if (disposing)
            {
                Console.WriteLine($"Disposing managed resources of Sphere at ({X}, {Y}, {Z})");
            }
            Console.WriteLine($"Cleaning native part of Sphere at ({X}, {Y}, {Z})");
            base.Dispose(disposing);
        }
    }
    class Program

    {
        static void Main(string[] args)
        {
            // Read coefficients from user for Sphere
            Console.WriteLine("Enter sphere center (x y z) and radius (separated by spaces), e.g. '0 0 0 2.5':");
            var line = Console.ReadLine();
            double sx = 0, sy = 0, sz = 0, sr = 1;
            if (!string.IsNullOrWhiteSpace(line))
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 4
                    && double.TryParse(parts[0], out sx)
                    && double.TryParse(parts[1], out sy)
                    && double.TryParse(parts[2], out sz)
                    && double.TryParse(parts[3], out sr))
                {
                    // parsed successfully
                }
                else
                {
                    Console.WriteLine("Invalid input, using defaults.");
                }
            }

            // Validate radius before constructing Sphere
            if (sr <= 0)
            {
                Console.WriteLine($"Invalid radius ({sr}) provided — using default radius = 1");
                sr = 1;
            }

            try
            {
                using (var s = new Sphere(0, 0, 0, sr))
                {
                    s.SetCenter(sx, sy, sz);
                    s.PrintSphereCoefficients();
                    Console.WriteLine($"Volume = {s.Volume():F4}\n");
                } // Dispose called here for Sphere
            }
            catch (ArgumentOutOfRangeException e1)
            {
                Console.WriteLine($"Failed to create Sphere: {e1.Message}. Using fallback radius=1.");
                using (var s = new Sphere(0, 0, 0, 1))
                {
                    s.SetCenter(sx, sy, sz);
                    s.PrintCoefficients();
                    Console.WriteLine($"Volume = {s.Volume():F4}\n");
                }
            }

            // Read coefficients for Ellipsoid
            Console.WriteLine("Enter ellipsoid center (x y z) and axes a b c, e.g. '1 1 1 2 3 4':");
            line = Console.ReadLine();
            double ex = 1, ey = 1, ez = 1, ea = 2, eb = 2, ec = 2;
            if (!string.IsNullOrWhiteSpace(line))
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 6
                    && double.TryParse(parts[0], out ex)
                    && double.TryParse(parts[1], out ey)
                    && double.TryParse(parts[2], out ez)
                    && double.TryParse(parts[3], out ea)
                    && double.TryParse(parts[4], out eb)
                    && double.TryParse(parts[5], out ec))
                {
                    // parsed successfully
                }
                else
                {
                    Console.WriteLine("Invalid input, using defaults.");
                }
            }

            // Validate axes before constructing Ellipsoid
            if (ea <= 0 || eb <= 0 || ec <= 0)
            {
                Console.WriteLine($"Invalid ellipsoid axes provided (a={ea}, b={eb}, c={ec}) — using defaults a=b=c=2");
                ea = eb = ec = 2;
            }

            Ellipsoid ell = null;
            try
            {
                ell = new Ellipsoid(0, 0, 0, ea, eb, ec);
            }
            catch (ArgumentOutOfRangeException e2)
            {
                Console.WriteLine($"Failed to create Ellipsoid: {e2.Message}. Using fallback axes a=b=c=2.");
                ell = new Ellipsoid(0, 0, 0, 2, 2, 2);
            }

            ell.SetCenter(ex, ey, ez);
            ell.PrintEllipsoidCoefficients();
            Console.WriteLine($"Volume = {ell.Volume():F4}\n");

            // Force a GC to demonstrate finalizer behavior (for demo only)
            Console.WriteLine("Forcing GC to demonstrate finalizer (not recommended in production)");
            ell = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
