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
                Console.WriteLine("6. Registrar Factura con Fecha y Hora");
                Console.WriteLine("7. Ver contenido de una Guía guardada");
                Console.WriteLine("8. Retirar / Descontar Stock");
                Console.WriteLine("9. Ver resumen de stock y alertas");
                Console.WriteLine("10. Salir");
                Console.WriteLine("========================================");
                Console.Write("Selecciona una opción (1-10): ");

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

                        Console.Write("Número de guía: ");
                        string numeroGuia = Console.ReadLine() ?? "";

                        Console.Write("Proveedor: ");
                        string proveedor = Console.ReadLine() ?? "";

                        Console.Write("Detalle: ");
                        string detalleGuia = Console.ReadLine() ?? "";

                        Console.Write("Cantidad de prendas: ");
                        if (!int.TryParse(Console.ReadLine(), out int cantidadPrendas) || cantidadPrendas < 0)
                        {
                            Console.WriteLine("\n[X] Cantidad no válida. Operación cancelada.");
                            break;
                        }

                        Console.Write("Monto total en soles: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal monto) || monto < 0)
                        {
                            Console.WriteLine("\n[X] Monto no válido. Operación cancelada.");
                            break;
                        }

                        try
                        {
                            Directory.CreateDirectory(rutaGuias);
                            DateTime fecha = DateTime.Now;
                            string archivo = Path.Combine(rutaGuias, $"Guia_{fecha:yyyy-MM-dd_HH-mm-ss}.txt");

                            string contenido =
                                $"REGISTRO DE GUÍA - VICMAR\n" +
                                $"Fecha: {fecha}\n" +
                                $"Número de guía: {numeroGuia}\n" +
                                $"Proveedor: {proveedor}\n" +
                                $"Detalle: {detalleGuia}\n" +
                                $"Cantidad: {cantidadPrendas} prendas\n" +
                                $"Monto total: S/ {monto:F2}\n" +
                                $"Tipo: ENTRADA";

                            File.WriteAllText(archivo, contenido);

                            string rutaMovimientos = Path.GetFullPath(
                                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\movimientos_vicmar.txt"));

                            string registroEntrada = $"{fecha:yyyy-MM-dd HH:mm} | Producto: {detalleGuia.Trim()} | Cantidad: {cantidadPrendas}";
                            File.AppendAllText(rutaMovimientos, registroEntrada + Environment.NewLine);
                            inventario.Add(registroEntrada);

                            Console.WriteLine("\n[✓] Guía registrada correctamente.");
                            Console.WriteLine("[✓] Mercadería agregada al inventario.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\n[!] Error: " + ex.Message);
                        }
                        break;

                    case "6":
                        Console.WriteLine("\n--- REGISTRAR NUEVA FACTURA ---");

                        Console.Write("Número de factura: ");
                        string numeroFactura = Console.ReadLine() ?? "";

                        Console.Write("Cliente: ");
                        string cliente = Console.ReadLine() ?? "";

                        Console.Write("Detalle de mercadería: ");
                        string detalleFactura = Console.ReadLine() ?? "";

                        Console.Write("Cantidad de prendas: ");
                        if (!int.TryParse(Console.ReadLine(), out int cantidadFactura) || cantidadFactura < 0)
                        {
                            Console.WriteLine("\n[X] Cantidad no válida. Operación cancelada.");
                            break;
                        }

                        Console.Write("Monto total en soles: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal montoFactura) || montoFactura < 0)
                        {
                            Console.WriteLine("\n[X] Monto no válido. Operación cancelada.");
                            break;
                        }

                        try
                        {
                            Directory.CreateDirectory(rutaFacturas);
                            DateTime fechaFac = DateTime.Now;
                            string archivoFac = Path.Combine(rutaFacturas, $"Factura_{fechaFac:yyyy-MM-dd_HH-mm-ss}.txt");

                            string contenidoFac =
                                $"REGISTRO DE FACTURA - VICMAR\n" +
                                $"Fecha: {fechaFac}\n" +
                                $"Número de factura: {numeroFactura}\n" +
                                $"Cliente: {cliente}\n" +
                                $"Detalle: {detalleFactura}\n" +
                                $"Cantidad: {cantidadFactura} prendas\n" +
                                $"Monto total: S/ {montoFactura:F2}\n" +
                                $"Tipo: SALIDA";

                            File.WriteAllText(archivoFac, contenidoFac);

                            string rutaMovimientosFac = Path.GetFullPath(
                                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\movimientos_vicmar.txt"));

                            string registroSalidaFac = $"{fechaFac:yyyy-MM-dd HH:mm} | SALIDA | Producto: {detalleFactura.Trim()} | Cantidad: {cantidadFactura}";
                            File.AppendAllText(rutaMovimientosFac, registroSalidaFac + Environment.NewLine);
                            inventario.Add(registroSalidaFac);

                            Console.WriteLine("\n[✓] Factura registrada correctamente.");
                            Console.WriteLine("[✓] Mercadería descontada del inventario.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\n[!] Error: " + ex.Message);
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
                        
                    case "8":
                        Console.WriteLine("\n--- RETIRAR / DESCONTAR STOCK ---");
                        Console.Write("Ingresa el producto: ");
                        string productoRetiro = Console.ReadLine()?.Trim() ?? "";

                        Console.Write("Ingresa la cantidad a retirar: ");
                        if (!int.TryParse(Console.ReadLine(), out int cantidadRetiro) || cantidadRetiro <= 0)
                        {
                            Console.WriteLine("\n[X] Cantidad no válida. Operación cancelada.");
                            break;
                        }

                        if (!string.IsNullOrWhiteSpace(productoRetiro))
                        {
                            int disponible = CalcularStockDisponible(inventario, productoRetiro);

                            if (cantidadRetiro > disponible)
                            {
                                Console.WriteLine($"\n[X] Stock insuficiente para '{productoRetiro}'. Disponible actual: {disponible}.");
                            }
                            else
                            {
                                string registroSalida = $"{DateTime.Now:yyyy-MM-dd HH:mm} | SALIDA | Producto: {productoRetiro} | Cantidad: {cantidadRetiro}";

                                try
                                {
                                    File.AppendAllText(rutaTxt, registroSalida + Environment.NewLine);
                                    inventario.Add(registroSalida);
                                    Console.WriteLine($"\n[✓] Salida registrada. Stock restante de '{productoRetiro}': {disponible - cantidadRetiro}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"\n[!] Error al guardar la salida: {ex.Message}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n[X] Error: Ingresa un producto válido.");
                        }
                        break;
                    case "9":
                        MostrarResumenStock(inventario);
                        Console.WriteLine("\nPresiona una tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case "10":
                        salir = true;
                        Console.WriteLine("\n¡Saliendo del sistema. Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("\n[X] Opción no válida. Ingresa un número del 1 al 8.");
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

        static int CalcularStockDisponible(List<string> inventario, string productoBuscado)
        {
            int total = 0;

            foreach (string linea in inventario)
            {
                string[] partes = linea.Split('|');
                if (partes.Length < 3) continue;

                bool esSalida = linea.IndexOf("| SALIDA |", StringComparison.OrdinalIgnoreCase) >= 0;

                string productoLinea = "";
                int cantidadLinea = 0;

                foreach (string parte in partes)
                {
                    string limpia = parte.Trim();
                    if (limpia.StartsWith("Producto:", StringComparison.OrdinalIgnoreCase))
                    {
                        productoLinea = limpia.Substring("Producto:".Length).Trim();
                    }
                    else if (limpia.StartsWith("Cantidad:", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(limpia.Substring("Cantidad:".Length).Trim(), out cantidadLinea);
                    }
                }

                if (string.Equals(productoLinea, productoBuscado, StringComparison.OrdinalIgnoreCase) && cantidadLinea > 0)
                {
                    total += esSalida ? -cantidadLinea : cantidadLinea;
                }
            }

            return total;
        }
        static void MostrarResumenStock(List<string> inventario)
        {
            Console.WriteLine("\n--- RESUMEN DE STOCK ACTUAL ---");
            Dictionary<string, int> stockPorProducto = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (string linea in inventario)
            {
                string productoLinea = "";
                int cantidadLinea = 0;
                bool esSalida = linea.Contains("SALIDA", StringComparison.OrdinalIgnoreCase);

                string[] partes = linea.Split('|');
                foreach (string parte in partes)
                {
                    string limpia = parte.Trim();
                    if (limpia.StartsWith("Producto:", StringComparison.OrdinalIgnoreCase))
                    {
                        productoLinea = limpia.Substring("Producto:".Length).Trim();
                    }
                    else if (limpia.StartsWith("Cantidad:", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(limpia.Substring("Cantidad:".Length).Trim(), out cantidadLinea);
                    }
                }

                if (!string.IsNullOrEmpty(productoLinea))
                {
                    if (!stockPorProducto.ContainsKey(productoLinea))
                    {
                        stockPorProducto[productoLinea] = 0;
                    }
                    stockPorProducto[productoLinea] += esSalida ? -cantidadLinea : cantidadLinea;
                }
            }

            if (stockPorProducto.Count == 0)
            {
                Console.WriteLine("No hay productos registrados.");
                return;
            }

            foreach (var kvp in stockPorProducto)
            {
                if (kvp.Value <= 5)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"- {kvp.Key}: {kvp.Value} unidades disponibles [¡ALERTA: STOCK BAJO!]");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"- {kvp.Key}: {kvp.Value} unidades disponibles");
                }
            }
        }
    }
}
