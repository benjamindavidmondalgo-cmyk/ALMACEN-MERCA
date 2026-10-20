using System;
using System.Collections.Generic;
using System.IO;

namespace ALMACEN
{
    class Program
    {
        static void Main(string[] args)
        {
            string rutaTxt = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\movimientos_vicmar.txt");
            string rutaDocumentos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DOCUMENTOS");
            
            string rutaGuias = Path.Combine(rutaDocumentos, "Guias");

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
                Console.WriteLine("3. Ver documentos (Facturas / Guías)");
                Console.WriteLine("4. Registrar Guía con Fecha y Hora");
                Console.WriteLine("5. Ver contenido de una Guía guardada"); 
                Console.WriteLine("6. Registrar Factura");
                Console.WriteLine("7. Ver Facturas Guardadas");
                Console.WriteLine("8. Salir");
                Console.WriteLine("========================================");
                Console.Write("Selecciona una opción (1-8): ");
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
                                File.AppendAllText(rutaTxt, registro + Environment.NewLine);
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
                        break;

                    case "3":
                        Console.WriteLine("\n--- DOCUMENTOS (FACTURAS / GUÍAS) ---");
                        try
                        {
                            if (Directory.Exists(rutaDocumentos))
                            {
                                string[] archivos = Directory.GetFiles(rutaDocumentos, "*.*", SearchOption.AllDirectories);

                                if (archivos.Length == 0)
                                {
                                    Console.WriteLine("La carpeta DOCUMENTOS y sus subcarpetas están vacías.");
                                }
                                else
                                {
                                    Console.WriteLine("Archivos encontrados:");
                                    foreach (var archivo in archivos)
                                    {
                                        string rutaRelativa = archivo.Replace(rutaDocumentos, "").TrimStart(Path.DirectorySeparatorChar);
                                        Console.WriteLine($" - {rutaRelativa}");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("[!] La carpeta 'DOCUMENTOS' todavía no existe.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[!] Error al leer los documentos: {ex.Message}");
                        }
                        break;

                    case "4":
                        Console.WriteLine("\n--- REGISTRAR NUEVA GUÍA ---");
                        Console.Write("Ingresa el número de guía o descripción: ");
                        string detalleGuia = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(detalleGuia))
                        {
                            try
                            {
                                Directory.CreateDirectory(rutaGuias);

                                string marcaTiempoArchivo = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                                string nombreArchivoGuia = Path.Combine(rutaGuias, $"Guia_{marcaTiempoArchivo}.txt");

                                string contenidoGuia = $"REGISTRO DE GUÍA\nFecha y Hora: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\nDetalle: {detalleGuia}\n";

                                File.WriteAllText(nombreArchivoGuia, contenidoGuia);

                                Console.WriteLine($"\n[✓] ¡Guía guardada con éxito en la carpeta Guias!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"\n[!] Error al crear la guía: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n[X] Error: El detalle de la guía no puede estar vacío.");
                        }
                        break;

                    case "5": 
                        Console.WriteLine("\n--- VER CONTENIDO DE GUÍAS GUARDADAS ---");
                        try
                        {
                            if (Directory.Exists(rutaGuias))
                            {
                                string[] guias = Directory.GetFiles(rutaGuias, "*.txt");

                                if (guias.Length == 0)
                                {
                                    Console.WriteLine("No hay guías registradas todavía.");
                                }
                                else
                                {
                                    Console.WriteLine("Guías disponibles:");
                                    for (int i = 0; i < guias.Length; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {Path.GetFileName(guias[i])}");
                                    }

                                    Console.Write("\nElige el número de la guía que deseas leer: ");
                                    if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= guias.Length)
                                    {
                                        string contenidoLeido = File.ReadAllText(guias[indice - 1]);
                                        Console.WriteLine("\n----------------------------------------");
                                        Console.WriteLine(contenidoLeido);
                                        Console.WriteLine("----------------------------------------");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\n[X] Número de guía no válido.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("[!] Aún no existe la carpeta de Guías.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[!] Error al leer las guías: {ex.Message}");
                        }
                        break;
                                               
                    case "6":
                        Console.WriteLine("\n--- REGISTRAR NUEVA FACTURA ---");
                        Console.Write("Ingresa el número de factura: ");
                        string numeroFactura = Console.ReadLine();
                        Console.Write("Ingresa el nombre del proveedor: ");
                        string proveedor = Console.ReadLine();
                        Console.Write("Ingresa el detalle de la compra: ");
                        string detalleFactura = Console.ReadLine();
                        Console.Write("Ingresa el monto total (S/): ");
                        string monto = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(numeroFactura) &&
                            !string.IsNullOrWhiteSpace(proveedor) &&
                            !string.IsNullOrWhiteSpace(detalleFactura) &&
                            !string.IsNullOrWhiteSpace(monto))
                        {
                            try
                            {
                                string rutaFacturas = Path.Combine(rutaDocumentos, "facturas");
                                Directory.CreateDirectory(rutaFacturas);
                                string marcaTiempoArchivo = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                                string nombreArchivoFactura = Path.Combine(rutaFacturas, $"Factura_{marcaTiempoArchivo}.txt");
                                string contenidoFactura = $"REGISTRO DE FACTURA\nFecha y Hora: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\nNúmero de Factura: {numeroFactura}\nProveedor: {proveedor}\nDetalle: {detalleFactura}\nMonto Total: S/ {monto}\n";

                                File.WriteAllText(nombreArchivoFactura, contenidoFactura);
                                Console.WriteLine("\n[✓] ¡Factura guardada con éxito en la carpeta facturas!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"\n[!] Error al crear la factura: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n[X] Error: Todos los campos de la factura son obligatorios.");
                        }
                        break;     

                    case "7":
                        Console.WriteLine("\n--- VER FACTURAS GUARDADAS ---");
                        string rutaFacturasGuardadas = Path.Combine(rutaDocumentos, "facturas");
                        try
                        {
                            if (Directory.Exists(rutaFacturasGuardadas))
                            {
                                string[] facturas = Directory.GetFiles(rutaFacturasGuardadas, "*.txt");
                                if (facturas.Length == 0)
                                {
                                    Console.WriteLine("No hay facturas registradas todavía.");
                                }
                                else
                                {
                                    Console.WriteLine("Facturas disponibles:");
                                    for (int i = 0; i < facturas.Length; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {Path.GetFileName(facturas[i])}");
                                    }
                                    Console.Write("\nElige el número de la factura que deseas leer: ");

                                    if (int.TryParse(Console.ReadLine(), out int indice) &&
                                        indice >= 1 && indice <= facturas.Length)
                                    {
                                        string contenidoLeido = File.ReadAllText(facturas[indice - 1]);
                                        Console.WriteLine("\n----------------------------------------");
                                        Console.WriteLine(contenidoLeido);
                                        Console.WriteLine("----------------------------------------");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\n[X] Número de factura no válido.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("[!] Aún no existe la carpeta de facturas.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[!] Error al leer las facturas: {ex.Message}");
                        }

                        break;



                    case "8": 
                        salir = true;
                        Console.WriteLine("\n¡Saliendo del sistema. Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("\n[X] Opción no válida. Ingresa un número del 1 al 6.");
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