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
            string rutaFacturas = Path.Combine(rutaDocumentos, "Facturas");

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
                Console.WriteLine("3. Ver Guías");
                Console.WriteLine("4. Ver Facturas");
                Console.WriteLine("5. Registrar Guía con Fecha y Hora");
                Console.WriteLine("6. Ver contenido de una Guía guardada");
                Console.WriteLine("7. Salir");
                Console.WriteLine("========================================");
                Console.Write("Selecciona una opción (1-7): ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("\n--- REGISTRAR / AGREGAR STOCK ---");
                        Console.Write("Ingresa el producto: ");
                        string producto = Console.ReadLine();

                        Console.Write("Ingresa la cantidad: ");
                        string cantidadTexto = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(producto) &&
                            int.TryParse(cantidadTexto, out int cantidad) &&
                            cantidad > 0)
                        {
                            string registro = $"{DateTime.Now:yyyy-MM-dd HH:mm} | Producto: {producto.Trim()} | Cantidad: {cantidad}";

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
                            Console.WriteLine("\n[X] Error: Ingresa un producto válido y una cantidad numérica mayor a 0.");
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
                        Console.WriteLine("\n--- GUÍAS ---");
                        try
                        {
                            if (Directory.Exists(rutaGuias))
                            {
                                string[] archivosGuias = Directory.GetFiles(rutaGuias, "*.*", SearchOption.AllDirectories);

                                if (archivosGuias.Length == 0)
                                {
                                    Console.WriteLine("La carpeta Guias está vacía.");
                                }
                                else
                                {
                                    Console.WriteLine("Guías encontradas:");
                                    for (int i = 0; i < archivosGuias.Length; i++)
                                    {
                                        string rutaRelativa = archivosGuias[i].Replace(rutaGuias, "").TrimStart(Path.DirectorySeparatorChar);
                                        Console.WriteLine($"{i + 1}. {rutaRelativa}");
                                    }

                                    Console.Write("\nIngresa el número de guía a eliminar (o 0 para volver): ");
                                    if (int.TryParse(Console.ReadLine(), out int idxGuia) && idxGuia >= 1 && idxGuia <= archivosGuias.Length)
                                    {
                                        string objetivo = archivosGuias[idxGuia - 1];
                                        Console.Write($"¿Eliminar '{Path.GetFileName(objetivo)}'? (S/N): ");
                                        if (Console.ReadLine()?.Trim().ToUpper() == "S")
                                        {
                                            File.Delete(objetivo);
                                            Console.WriteLine("\n[✓] Guía eliminada.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nEliminación cancelada.");
                                        }
                                    }
                                    else if (idxGuia != 0)
                                    {
                                        Console.WriteLine("\n[X] Número no válido.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("[!] La carpeta 'Guias' todavía no existe.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[!] Error al procesar las guías: {ex.Message}");
                        }
                        break;

                    case "4":
                        Console.WriteLine("\n--- FACTURAS ---");
                        try
                        {
                            if (Directory.Exists(rutaFacturas))
                            {
                                string[] archivosFacturas = Directory.GetFiles(rutaFacturas, "*.*", SearchOption.AllDirectories);

                                if (archivosFacturas.Length == 0)
                                {
                                    Console.WriteLine("La carpeta Facturas está vacía.");
                                }
                                else
                                {
                                    Console.WriteLine("Facturas encontradas:");
                                    for (int i = 0; i < archivosFacturas.Length; i++)
                                    {
                                        string rutaRelativa = archivosFacturas[i].Replace(rutaFacturas, "").TrimStart(Path.DirectorySeparatorChar);
                                        Console.WriteLine($"{i + 1}. {rutaRelativa}");
                                    }

                                    Console.Write("\nIngresa el número de factura a eliminar (o 0 para volver): ");
                                    if (int.TryParse(Console.ReadLine(), out int idxFactura) && idxFactura >= 1 && idxFactura <= archivosFacturas.Length)
                                    {
                                        string objetivo = archivosFacturas[idxFactura - 1];
                                        Console.Write($"¿Eliminar '{Path.GetFileName(objetivo)}'? (S/N): ");
                                        if (Console.ReadLine()?.Trim().ToUpper() == "S")
                                        {
                                            File.Delete(objetivo);
                                            Console.WriteLine("\n[✓] Factura eliminada.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nEliminación cancelada.");
                                        }
                                    }
                                    else if (idxFactura != 0)
                                    {
                                        Console.WriteLine("\n[X] Número no válido.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("[!] La carpeta 'Facturas' todavía no existe.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[!] Error al procesar las facturas: {ex.Message}");
                        }
                        break;

                    case "5":
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

                                Console.WriteLine("\n[✓] ¡Guía guardada con éxito en la carpeta Guias!");
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

                    case "6":
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
                                        string guiaSeleccionada = guias[indice - 1];
                                        string contenidoLeido = File.ReadAllText(guiaSeleccionada);
                                        Console.WriteLine("\n----------------------------------------");
                                        Console.WriteLine(contenidoLeido);
                                        Console.WriteLine("----------------------------------------");

                                        Console.Write("\n¿Deseas eliminar esta guía? (S/N): ");
                                        if (Console.ReadLine()?.Trim().ToUpper() == "S")
                                        {
                                            File.Delete(guiaSeleccionada);
                                            Console.WriteLine("\n[✓] Guía eliminada.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nLa guía se mantuvo guardada.");
                                        }
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

                    case "7":
                        salir = true;
                        Console.WriteLine("\n¡Saliendo del sistema. Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("\n[X] Opción no válida. Ingresa un número del 1 al 7.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

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