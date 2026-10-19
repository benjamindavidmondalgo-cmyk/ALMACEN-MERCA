using System;
using System.Collections.Generic;
using System.IO;

namespace ALMACEN
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lista en memoria para guardar temporalmente el inventario actual
            List<string> inventario = new List<string>();
            bool salir = false;

            // Ruta dinámica para apuntar al archivo de texto en la raíz del proyecto
            string rutaTxt = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\movimientos_vicmar.txt");

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("       SISTEMA DE ALMACÉN - VICMAR      ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Agregar stock / producto");
                Console.WriteLine("2. Salir");
                Console.WriteLine("========================================");
                Console.Write("Selecciona una opción (1-2): ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("\n--- AGREGAR STOCK ---");
                        Console.Write("Ingresa el nombre del producto y la cantidad: ");
                        string detalleProducto = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(detalleProducto))
                        {
                            // 1. Lo guardamos en la lista temporal del programa
                            inventario.Add(detalleProducto);

                            // 2. Registramos el movimiento automáticamente en el archivo .txt
                            try
                            {
                                string registro = $"{DateTime.Now} -> INGRESO: {detalleProducto}\n";
                                File.AppendAllText(rutaTxt, registro);
                                Console.WriteLine("\n[✓] ¡Stock agregado y registrado en el archivo de texto con éxito!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"\n[!] El producto se guardó en memoria, pero hubo un error al escribir el .txt: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n[X] Error: El campo no puede estar vacío.");
                        }
                        break;

                    case "2":
                        salir = true;
                        Console.WriteLine("\n¡Saliendo del sistema VICMAR. Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("\n[X] Opción no válida. Ingresa 1 o 2.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}