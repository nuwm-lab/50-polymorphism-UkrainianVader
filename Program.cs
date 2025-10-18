using System;
using System.Collections.Generic;
namespace Lab4
{
    // Базовий клас одновимірного вектора розмірності 4
    public class Vector4D
    {
        protected double[] elements; // Масив елементів вектора
        // Конструктор
        public Vector4D()
        {
            elements = new double[4];
        }
        // Віртуальний метод для задання елементів вектора
        public virtual void SetElements()
        {
            Console.WriteLine("Введіть 4 елементи вектора:");
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Елемент [{i}]: ");
                while (!double.TryParse(Console.ReadLine(), out elements[i]))
                {
                    Console.Write("Некоректне значення. Введіть число: ");
                }
            }
        }
        // Метод для задання елементів з масиву (для тестування)
        public virtual void SetElements(double[] values)
        {
            if (values.Length == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    elements[i] = values[i];
                }
            }
        }
        // Віртуальний метод для виведення вектора на екран
        public virtual void Display()
        {
            Console.Write("Вектор: [");
            for (int i = 0; i < 4; i++)
            {
                Console.Write(elements[i]);
                if (i < 3) Console.Write(", ");
            }
            Console.WriteLine("]");
        }
        // Віртуальний метод для знаходження максимального елемента
        public virtual double FindMax()
        {
            double max = elements[0];
            for (int i = 1; i < 4; i++)
            {
                if (elements[i] > max)
                    max = elements[i];
            }
            return max;
        }
    }
    // Похідний клас матриці 4x4
    public class Matrix : Vector4D
    {
        private double[,] matrix; // Двовимірний масив для матриці
        // Конструктор
        public Matrix()
        {
            matrix = new double[4, 4];
        }
        // Перевантажений метод для задання елементів матриці
        public override void SetElements()
        {
            Console.WriteLine("Введіть елементи матриці 4x4:");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write($"Елемент [{i},{j}]: ");
                    while (!double.TryParse(Console.ReadLine(), out matrix[i, j]))
                    {
                        Console.Write("Некоректне значення. Введіть число: ");
                    }
                }
            }
        }
        // Метод для задання елементів з двовимірного масиву (для тестування)
        public void SetElements(double[,] values)
        {
            if (values.GetLength(0) == 4 && values.GetLength(1) == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        matrix[i, j] = values[i, j];
                    }
                }
            }
        }
        // Перевантажений метод для виведення матриці на екран
        public override void Display()
        {
            Console.WriteLine("Матриця 4x4:");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < 4; j++)
                {
                    Console.Write($"{matrix[i, j]:F2} ");
                }
                Console.WriteLine("|");
            }
        }
        // Перевантажений метод для знаходження максимального елемента матриці
        public override double FindMax()
        {
            double max = matrix[0, 0];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[i, j] > max)
                        max = matrix[i, j];
                }
            }
            return max;
        }
    }
    // Головний клас програми
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  Лабораторна робота 5: Динамічне створення об'єктів          ║");
            Console.WriteLine("║  Демонстрація віртуальних методів та поліморфізму            ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");

            try
            {
                // Демонстрація динамічного створення об'єктів
                DemonstrateDynamicPolymorphism();
                
                Console.WriteLine("\n" + new string('═', 65) + "\n");
                
                // Інтерактивний режим з динамічним вибором типу
                RunDynamicMode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Ігноруємо помилку при перенаправленні вводу
            }
        }
        // Метод для демонстрації динамічного поліморфізму
        static void DemonstrateDynamicPolymorphism()
        {
            Console.WriteLine("📌 ДЕМОНСТРАЦІЯ ДИНАМІЧНОГО ПОЛІМОРФІЗМУ\n");
            Console.WriteLine("Створюємо масив покажчиків базового типу Vector4D,");
            Console.WriteLine("але фактичний тип об'єкта визначається динамічно!\n");
            // Масив покажчиків на базовий клас (тип невідомий на етапі компіляції)
            Vector4D[] objects = new Vector4D[4];
            
            // Динамічне створення різних типів об'єктів
            Console.WriteLine("Створюємо об'єкти динамічно (тип визначається під час виконання):\n");
            
            // Об'єкт 1: Vector4D
            objects[0] = new Vector4D();
            objects[0].SetElements(new double[] { 1.5, 8.3, 3.7, 5.2 });
            Console.WriteLine("✓ Створено об'єкт типу Vector4D");
            // Об'єкт 2: Matrix
            objects[1] = new Matrix();
            ((Matrix)objects[1]).SetElements(new double[,] {
                { 2.1, 4.5, 1.8, 3.3 },
                { 7.2, 9.6, 2.4, 5.7 },
                { 1.1, 3.8, 12.5, 4.2 },
                { 6.3, 2.9, 8.1, 1.7 }
            });
            Console.WriteLine("✓ Створено об'єкт типу Matrix");
            // Об'єкт 3: Vector4D
            objects[2] = new Vector4D();
            objects[2].SetElements(new double[] { 10.5, 2.1, 15.8, 7.3 });
            Console.WriteLine("✓ Створено об'єкт типу Vector4D");
            // Об'єкт 4: Matrix
            objects[3] = new Matrix();
            ((Matrix)objects[3]).SetElements(new double[,] {
                { 5.5, 3.2, 8.8, 1.1 },
                { 9.9, 6.6, 4.4, 2.2 },
                { 11.1, 7.7, 13.3, 10.0 },
                { 3.3, 8.8, 5.5, 14.4 }
            });
            Console.WriteLine("✓ Створено об'єкт типу Matrix");
            Console.WriteLine("\n" + new string('-', 65));
            Console.WriteLine("УВАГА! Тип об'єкта невідомий на етапі компіляції!");
            Console.WriteLine("Віртуальні методи викликаються динамічно під час виконання.");
            Console.WriteLine(new string('-', 65) + "\n");
            // Обробка об'єктів через базовий клас (поліморфізм!)
            for (int i = 0; i < objects.Length; i++)
            {
                Console.WriteLine($"\n▶ Об'єкт #{i + 1} (фактичний тип: {objects[i].GetType().Name}):");
                Console.WriteLine(new string('-', 65));
                
                // Виклик віртуального методу Display() - тип визначається динамічно!
                objects[i].Display();
                
                // Виклик віртуального методу FindMax() - тип визначається динамічно!
                double max = objects[i].FindMax();
                Console.WriteLine($"Максимальний елемент: {max}");
                
                Console.WriteLine($"→ Викликано метод з класу: {objects[i].GetType().Name}");
            }
            Console.WriteLine("\n" + new string('═', 65));
            Console.WriteLine(" ВИСНОВОК: Віртуальні методи дозволяють викликати правильну");
            Console.WriteLine("   версію методу в залежності від фактичного типу об'єкта,");
            Console.WriteLine("   навіть якщо ми працюємо через покажчик базового класу!");
            Console.WriteLine(new string('═', 65));
        }
        // Інтерактивний режим з динамічним вибором типу об'єкта
        static void RunDynamicMode()
        {
            Console.WriteLine(" ІНТЕРАКТИВНИЙ РЕЖИМ З ДИНАМІЧНИМ ВИБОРОМ ТИПУ\n");
            List<Vector4D> dynamicObjects = new List<Vector4D>();
            bool continueAdding = true;
            while (continueAdding)
            {
                Console.WriteLine("\nВиберіть тип об'єкта для створення:");
                Console.WriteLine("1 - Вектор 4D");
                Console.WriteLine("2 - Матриця 4x4");
                Console.WriteLine("0 - Завершити додавання");
                Console.Write("Ваш вибір: ");
                
                string? choice = Console.ReadLine();
                // Динамічне створення об'єкта на основі вибору користувача
                Vector4D? newObject = null;
                
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n→ Динамічно створюємо об'єкт типу Vector4D...");
                        newObject = CreateVector();
                        Console.WriteLine("✓ Об'єкт Vector4D створено!");
                        break;
                        
                    case "2":
                        Console.WriteLine("\n→ Динамічно створюємо об'єкт типу Matrix...");
                        newObject = CreateMatrix();
                        Console.WriteLine("✓ Об'єкт Matrix створено!");
                        break;
                        
                    case "0":
                        continueAdding = false;
                        break;
                        
                    default:
                        Console.WriteLine("❌ Некоректний вибір!");
                        continue;
                }
                if (newObject != null)
                {
                    dynamicObjects.Add(newObject);
                    Console.WriteLine($"Об'єкт додано до колекції (всього об'єктів: {dynamicObjects.Count})");
                }
            }
            if (dynamicObjects.Count > 0)
            {
                Console.WriteLine("\n" + new string('═', 65));
                Console.WriteLine($"ОБРОБКА {dynamicObjects.Count} ДИНАМІЧНО СТВОРЕНИХ ОБ'ЄКТІВ:");
                Console.WriteLine(new string('═', 65));
                double globalMax = double.MinValue;
                string maxObjectType = "";
                int maxObjectIndex = -1;
                // Обробка через базовий клас (поліморфізм!)
                for (int i = 0; i < dynamicObjects.Count; i++)
                {
                    Console.WriteLine($"\n▶ Об'єкт #{i + 1} (тип: {dynamicObjects[i].GetType().Name}):");
                    Console.WriteLine(new string('-', 65));
                    // Виклик віртуальних методів
                    dynamicObjects[i].Display();
                    double max = dynamicObjects[i].FindMax();
                    Console.WriteLine($"Максимальний елемент: {max}");
                    if (max > globalMax)
                    {
                        globalMax = max;
                        maxObjectType = dynamicObjects[i].GetType().Name;
                        maxObjectIndex = i + 1;
                    }
                }
                Console.WriteLine("\n" + new string('═', 65));
                Console.WriteLine("ГЛОБАЛЬНИЙ ПІДСУМОК:");
                Console.WriteLine($"Максимальний елемент серед усіх об'єктів: {globalMax}");
                Console.WriteLine($"Знайдено в об'єкті #{maxObjectIndex} (тип: {maxObjectType})");
                Console.WriteLine(new string('═', 65));
            }
            else
            {
                Console.WriteLine("\n⚠ Жодного об'єкта не було створено.");
            }
        }
        // Метод для динамічного створення вектора
        static Vector4D CreateVector()
        {
            Vector4D vector = new Vector4D();
            
            Console.WriteLine("Виберіть спосіб введення:");
            Console.WriteLine("1 - Ввести вручну");
            Console.WriteLine("2 - Використати випадкові значення");
            Console.Write("Ваш вибір: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                vector.SetElements();
            }
            else
            {
                Random rand = new Random();
                double[] values = new double[4];
                for (int i = 0; i < 4; i++)
                {
                    values[i] = Math.Round(rand.NextDouble() * 20, 2);
                }
                vector.SetElements(values);
                Console.WriteLine("Згенеровано випадкові значення.");
            }

            return vector;
        }
        // Метод для динамічного створення матриці
        static Matrix CreateMatrix()
        {
            Matrix matrix = new Matrix();
            
            Console.WriteLine("Виберіть спосіб введення:");
            Console.WriteLine("1 - Ввести вручну");
            Console.WriteLine("2 - Використати випадкові значення");
            Console.Write("Ваш вибір: ");
            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                matrix.SetElements();
            }
            else
            {
                Random rand = new Random();
                double[,] values = new double[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        values[i, j] = Math.Round(rand.NextDouble() * 20, 2);
                    }
                }
                matrix.SetElements(values);
                Console.WriteLine("Згенеровано випадкові значення.");
            }
            return matrix;
        }
    }
}
