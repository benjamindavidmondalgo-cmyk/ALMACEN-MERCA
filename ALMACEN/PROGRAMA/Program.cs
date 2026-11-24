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
<<<<<<< HEAD
                Console.WriteLine("3. Ver Guías");
                Console.WriteLine("4. Ver Facturas");
                Console.WriteLine("5. Registrar Guía con Fecha y Hora");
                Console.WriteLine("6. Registrar Factura con Fecha y Hora");
                Console.WriteLine("7. Ver contenido de una Guía guardada");
                Console.WriteLine("8. Salir");
                Console.WriteLine("========================================");
                Console.Write("Selecciona una opción (1-8): ");

=======
                Console.WriteLine("3. Ver documentos (Facturas / Guías)");
                Console.WriteLine("4. Registrar Guía con Fecha y Hora");
                Console.WriteLine("5. Ver contenido de una Guía guardada"); 
                Console.WriteLine("6. Registrar Factura");
                Console.WriteLine("7. Ver Facturas Guardadas");
                Console.WriteLine("8. Ver Resumen General del Almacén");
                Console.WriteLine("9. Buscar en Inventario");
                Console.WriteLine("10. Salir");
                Console.WriteLine("========================================");
                Console.Write("Selecciona una opción (1-10): ");
>>>>>>> e36a0b97e3744518047989f9b05c4157d69ac403
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("\n--- REGISTRAR / AGREGAR STOCK ---");
<<<<<<< HEAD
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
=======
                        Console.Write("Ingresa la cantidad: ");
                        string cantidad = Console.ReadLine()?.Trim();
                        Console.Write("Ingresa el nombre del producto: ");
                        string nombreProducto = Console.ReadLine()?.Trim();

                        if (!string.IsNullOrWhiteSpace(cantidad) && !string.IsNullOrWhiteSpace(nombreProducto))
                        {
                            string detalle = $"{nombreProducto} - Cantidad: {cantidad}";
                            bool yaExiste = inventario.Exists(item => item.ToLower().Contains(nombreProducto.ToLower()));
                            if (yaExiste)
>>>>>>> e36a0b97e3744518047989f9b05c4157d69ac403
                            {
                                Console.WriteLine("\n[X] Error: Este producto o registro ya existe en el sistema.");
                            }
                            else
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
                        Console.Write("Ingresa el número de guía: ");
                        string numeroGuia = Console.ReadLine()?.Trim();
                        Console.Write("Ingresa la descripción o detalle de la guía: ");
                        string detalleGuia = Console.ReadLine()?.Trim();

                        if (!string.IsNullOrWhiteSpace(numeroGuia) && !string.IsNullOrWhiteSpace(detalleGuia))
                        {
                            try
                            {
                                Directory.CreateDirectory(rutaGuias);

                                bool guiaDuplicada = false;
                                string[] guiasExistentes = Directory.GetFiles(rutaGuias, "*.txt");
            
                                foreach (string archivo in guiasExistentes)
                                {
                                    string contenido = File.ReadAllText(archivo);
                                    if (contenido.Contains($"Número de Guía: {numeroGuia}"))
                                    {
                                        guiaDuplicada = true;
                                        break;
                                    }
                                }

                                if (guiaDuplicada)
                                {
                                    Console.WriteLine($"\n[X] Error: Ya existe una guía registrada con el número '{numeroGuia}'.");
                                }
                                else
                                {
                                    string marcaTiempoArchivo = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                                    string nombreArchivoGuia = Path.Combine(rutaGuias, $"Guia_{marcaTiempoArchivo}.txt");
                
                                    string contenidoGuia = $"REGISTRO DE GUÍA\nFecha y Hora: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\nNúmero de Guía: {numeroGuia}\nDetalle: {detalleGuia}\n";

<<<<<<< HEAD
                                File.WriteAllText(nombreArchivoGuia, contenidoGuia);

                                Console.WriteLine("\n[✓] ¡Guía guardada con éxito en la carpeta Guias!");
=======
                                    File.WriteAllText(nombreArchivoGuia, contenidoGuia);
                                    Console.WriteLine($"\n[✓] ¡Guía guardada con éxito en la carpeta Guias!");
                                }
>>>>>>> e36a0b97e3744518047989f9b05c4157d69ac403
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"\n[!] Error al crear la guía: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n[X] Error: Todos los campos de la guía son obligatorios.");
                        }
                        break;

                    case "6":
                        Console.WriteLine("\n--- REGISTRAR NUEVA FACTURA ---");
                        Console.Write("Ingresa el número de factura o descripción: ");
                        string detalleFactura = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(detalleFactura))
                        {
                            try
                            {
                                Directory.CreateDirectory(rutaFacturas);

                                string marcaTiempoFactura = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                                string nombreArchivoFactura = Path.Combine(rutaFacturas, $"Factura_{marcaTiempoFactura}.txt");

                                string contenidoFactura = $"REGISTRO DE FACTURA\nFecha y Hora: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\nDetalle: {detalleFactura}\n";

                                File.WriteAllText(nombreArchivoFactura, contenidoFactura);

                                Console.WriteLine("\n[✓] ¡Factura guardada con éxito en la carpeta Facturas!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"\n[!] Error al crear la factura: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n[X] Error: El detalle de la factura no puede estar vacío.");
                        }
                        break;

                    case "7":
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
                
                                        string contenidoArchivo = File.ReadAllText(guias[i]);
                                        string primeraLineaGuia = Path.GetFileName(guias[i]); 

                                        string[] lineas = File.ReadAllLines(guias[i]);
                                        string infoMostrar = Path.GetFileName(guias[i]);

                                        foreach (var linea in lineas)
                                        {
                                            if (linea.StartsWith("Número de Guía:") || linea.StartsWith("Detalle:"))
                                            {
                                                infoMostrar += " | " + linea;
                                            }
                                        }

                                        Console.WriteLine($"{i + 1}. {infoMostrar}");
                                    }

                                    Console.Write("\nElige el número de la guía que deseas leer completo: ");
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
                                               
                    case "6":
                        Console.WriteLine("\n--- REGISTRAR NUEVA FACTURA ---");
                        Console.Write("Ingresa el número de factura: ");
                        string numeroFactura = Console.ReadLine()?.Trim();
                        Console.Write("Ingresa el nombre del proveedor: ");
                        string proveedor = Console.ReadLine()?.Trim();
                        Console.Write("Ingresa el detalle de la compra: ");
                        string detalleFactura = Console.ReadLine()?.Trim();
                        Console.Write("Ingresa el monto total (S/): ");
                        string monto = Console.ReadLine()?.Trim();

<<<<<<< HEAD
                    case "8":
=======
                        if (!string.IsNullOrWhiteSpace(numeroFactura) &&
                            !string.IsNullOrWhiteSpace(proveedor) &&
                            !string.IsNullOrWhiteSpace(detalleFactura) &&
                            !string.IsNullOrWhiteSpace(monto))
                            {
                                try
                                {
                                    string rutaFacturas = Path.Combine(rutaDocumentos, "facturas");
                                    Directory.CreateDirectory(rutaFacturas);

                                    bool facturaDuplicada = false;
                                    string[] facturasExistentes = Directory.GetFiles(rutaFacturas, "*.txt");
            
                                    foreach (string archivo in facturasExistentes)
                                    {
                                        string contenido = File.ReadAllText(archivo);
                                        if (contenido.Contains($"Número de Factura: {numeroFactura}"))
                                        {
                                            facturaDuplicada = true;
                                            break;
                                        }
                                    }

                                    if (facturaDuplicada)
                                    {
                                        Console.WriteLine($"\n[X] Error: Ya existe una factura registrada con el número '{numeroFactura}'.");
                                    }
                                    else
                                    {
                                        string marcaTiempoArchivo = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                                        string nombreArchivoFactura = Path.Combine(rutaFacturas, $"Factura_{marcaTiempoArchivo}.txt");
                                        string contenidoFactura = $"REGISTRO DE FACTURA\nFecha y Hora: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\nNúmero de Factura: {numeroFactura}\nProveedor: {proveedor}\nDetalle: {detalleFactura}\nMonto Total: S/ {monto}\n";

                                        File.WriteAllText(nombreArchivoFactura, contenidoFactura);
                                        Console.WriteLine("\n[✓] ¡Factura guardada con éxito en la carpeta facturas!");
                                    }
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
                        MostrarResumenGeneral(inventario, rutaGuias, Path.Combine(rutaDocumentos, "facturas"));
                        break;
                    case "9":
                        BuscarEnInventario(inventario);
                        break;    
                    case "10": 
>>>>>>> e36a0b97e3744518047989f9b05c4157d69ac403
                        salir = true;
                        Console.WriteLine("\n¡Saliendo del sistema. Hasta luego!");
                        break;

                    default:
<<<<<<< HEAD
                        Console.WriteLine("\n[X] Opción no válida. Ingresa un número del 1 al 8.");
=======
                        Console.WriteLine("\n[X] Opción no válida. Ingresa un número del 1 al 10.");
>>>>>>> e36a0b97e3744518047989f9b05c4157d69ac403
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

        // Método estático de la clase Program 
        static void MostrarResumenGeneral(List<string> inventario, string rutaGuias, string rutaFacturas)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("        RESUMEN GENERAL DEL ALMACÉN     ");
            Console.WriteLine("========================================");
            
            Console.WriteLine($"• Registros de stock en historial: {inventario.Count}");

            int cantidadGuias = Directory.Exists(rutaGuias) ? Directory.GetFiles(rutaGuias, "*.txt").Length : 0;
            Console.WriteLine($"• Total de guías registradas:     {cantidadGuias}");

            int cantidadFacturas = Directory.Exists(rutaFacturas) ? Directory.GetFiles(rutaFacturas, "*.txt").Length : 0;
            Console.WriteLine($"• Total de facturas registradas:  {cantidadFacturas}");
            
            Console.WriteLine("========================================\n");
        }

        // Método para filtrar y buscar movimientos específicos en el inventario
        static void BuscarEnInventario(List<string> inventario)
        {
            Console.Write("\nIngresa el término o fecha a buscar (ej. INGRESO, Factura, 2026-10-19): ");
            string busqueda = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                Console.WriteLine("[!] El término de búsqueda no puede estar vacío.");
                return;
            }

            var resultados = inventario.FindAll(linea => linea.Contains(busqueda, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"\n--- RESULTADOS DE BÚSQUEDA (Coincidencias: {resultados.Count}) ---");
            if (resultados.Count > 0)
            {
                foreach (string registro in resultados)
                {
                    Console.WriteLine($"  ➜ {registro}");
                }
            }
            else
            {
                Console.WriteLine(" No se encontraron registros que coincidan con la búsqueda.");
            }
            Console.WriteLine("-----------------------------------------------------------\n");
        }
    } 
} 