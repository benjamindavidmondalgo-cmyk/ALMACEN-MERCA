using System;
using System.Collections.Generic;
using System.IO;

namespace ALMACEN
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ruta dinámica hacia el archivo permanente en la raíz del proyecto
            string rutaTxt = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\movimientos_vicmar.txt");
            string rutaDocumentos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DOCUMENTOS");

            // Cargar el inventario previo desde el archivo si ya existe
            List<string> inventario = CargarInventarioDesdeArchivo(rutaTxt);
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("       SISTEMA DE ALMACÉN - VICMAR      ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Registrar / Agregar Stock");
                Console.WriteLine("2. Ver Inventario Actual");
                Console.WriteLine("3. Salir");
                Console.WriteLine("========================================");
                Console.Write("Selecciona una opción (1-3): ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("\n--- REGISTRAR / AGREGAR STOCK ---");
                        Console.Write("Ingresa el producto y la cantidad: ");
                        string detalle = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(detalle))
                        {
                            string registro = $"{DateTime.Now:yyyy-MM-dd HH:mm} | {detalle}";

                            try
                            {
                                // Se guarda de forma permanente en el archivo .txt
                                File.AppendAllText(rutaTxt, registro + Environment.NewLine);
                                
                                // Se actualiza la lista en memoria
                                inventario.Add(registro);

                                Console.WriteLine("\n[✓] ¡Registrado y guardado PERMANENTEMENTE en 'movimientos_vicmar.txt'!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"\n[!] Error al guardar en el archivo: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n[X] Error: El campo no puede estar vacío.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("\n--- INVENTARIO / HISTORIAL PERMANENTE ---");
                        if (inventario.Count == 0)
                        {
                            Console.WriteLine("No hay registros guardados en el archivo.");
                        }
                        else
                        {
                            for (int i = 0; i < inventario.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {inventario[i]}");
                            }
                        }
                        Console.WriteLine($"\n(Carpeta de documentos vinculada: {rutaDocumentos})");
                        break;

                    case "3":
                        salir = true;
                        Console.WriteLine("\n¡Saliendo del sistema. Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("\n[X] Opción no válida. Ingresa un número del 1 al 3.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // Método auxiliar para leer los datos del archivo .txt al arrancar
        static List<string> CargarInventarioDesdeArchivo(string ruta)
        {
            List<string> lineas = new List<string>();

            try
            {
                if (File.Exists(ruta))
                {
                    string[] contenido = File.ReadAllLines(ruta);
                    lineas.AddRange(contenido);
                }
                else
                {
                    // Si el archivo no existe, lo crea automáticamente vacío
                    File.Create(ruta).Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] No se pudo cargar el historial: {ex.Message}");
            }

            return lineas;
        }
    }
}