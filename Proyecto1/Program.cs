using System;

/* Grupo Numero 4
 * Jonathan  Morales Barrientos
 * Jonatahan David Muñoz Lopez
 * Angel Porras Carvajal
 * */

class Program
{
    // Variables de estado estáticas para llevar el seguimiento de las transacciones
    static int numeroFacturaConsecutivo = 1;
    static int[] numeroPago = new int[10];
    static DateTime[] fecha = new DateTime[10];
    static string[] hora = new string[10];
    static string[] cedula = new string[10];
    static string[] nombre = new string[10];
    static string[] apellido1 = new string[10];
    static string[] apellido2 = new string[10];
    static int[] numeroCaja = new int[10];
    static int[] tipoServicio = new int[10];
    static string[] numeroFactura = new string[10];
    static double[] montoPagar = new double[10];
    static double[] montoComision = new double[10];
    static double[] montoDeducido = new double[10];
    static double[] montoPagaCliente = new double[10];
    static double[] vuelto = new double[10];
    // Método principal del programa

    static void Main(string[] args)
    {
        ConfigurarConsola(); // Configuración de la apariencia de la consola


        int opcion;
        // Bucle principal del programa
        do
        {
            // Mostrar encabezado
            MostrarEncabezado("Sistema de Pago de Servicios Públicos");
            // Mostrar menú principal
            Console.WriteLine("Menú Principal");
            Console.WriteLine("1. Inicializar Vectores");
            Console.WriteLine("2. Realizar Pagos");
            Console.WriteLine("3. Consultar Pagos");
            Console.WriteLine("4. Modificar Pagos");
            Console.WriteLine("5. Eliminar Pagos");
            Console.WriteLine("6. Submenú Reportes");
            Console.WriteLine("7. Salir");

            Console.Write("Seleccione una opción: ");
            opcion = LeerEntero(); // Leer la opción del usuario
                                   // Realizar una acción según la opción seleccionada
            switch (opcion)
            {
                case 1:
                    InicializarVectores();
                    break;
                case 2:
                    RealizarPagos();
                    break;
                case 3:
                    ConsultarPagos();
                    break;
                case 4:
                    ModificarPagos();
                    break;
                case 5:
                    EliminarPagos();
                    break;
                case 6:
                    SubmenuReportes();
                    break;
                case 7:
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                    break;
            }

        } while (opcion != 7);
        // Continuar el bucle hasta que el usuario seleccione la opción de salir (7)
    } // Fin del método Main

    static void InicializarVectores()// Método para inicializar los vectores de datos
    {
        numeroFacturaConsecutivo = 1;

        for (int i = 0; i < 10; i++)// Asignar valores iniciales a cada elemento de los vectores

        {
            numeroPago[i] = i + 1;
            fecha[i] = DateTime.Now;
            hora[i] = DateTime.Now.ToString("HH:mm");
            cedula[i] = "";
            nombre[i] = "";
            apellido1[i] = "";
            apellido2[i] = "";
            numeroCaja[i] = new Random().Next(1, 4);
            tipoServicio[i] = 0;
            numeroFactura[i] = "";
            montoPagar[i] = 0;
            montoComision[i] = 0;
            montoDeducido[i] = 0;
            montoPagaCliente[i] = 0;
            vuelto[i] = 0;
        }
        Console.WriteLine("Vectores inicializados correctamente\n");
    }

    static void RealizarPagos() // Método para realizar pagos
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Ingrese datos para el pago #{i + 1}");
            // conteo de ingresar pagos 

            Console.Write("Cedula (solo números de 9 dígitos): ");
            while (true)
            {
                string inputCedula = Console.ReadLine();
                if (inputCedula.Length == 9 && int.TryParse(inputCedula, out _))
                {
                    cedula[i] = inputCedula;
                    break;
                }
                else
                {
                    Console.Write("Por favor, ingrese su número de cedula nuevamente (9 dígitos): ");
                }
            }

            Console.Write("Nombre: ");
            nombre[i] = Console.ReadLine();

            Console.Write("Primer Apellido: ");
            apellido1[i] = Console.ReadLine();

            Console.Write("Segundo Apellido: ");
            apellido2[i] = Console.ReadLine();

            int tipoServicioActual;
            do
            {
                Console.Write("Tipo de Servicio (1= Recibo de Luz, 2= Recibo Teléfono, 3= Recibo de Agua): ");
                if (!int.TryParse(Console.ReadLine(), out tipoServicioActual) || (tipoServicioActual < 1 || tipoServicioActual > 3))
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                else
                    tipoServicio[i] = tipoServicioActual;
            } while (tipoServicio[i] == 0);

            numeroFactura[i] = GenerarNumeroFacturaConsecutivo();
            numeroFacturaConsecutivo++;

            do
            {
                Console.Write("Monto a Pagar: ");
            } while (!double.TryParse(Console.ReadLine(), out montoPagar[i]));

            switch (tipoServicio[i])
            {
                case 1:
                    montoComision[i] = montoPagar[i] * 0.04;
                    break;
                case 2:
                    montoComision[i] = montoPagar[i] * 0.055;
                    break;
                case 3:
                    montoComision[i] = montoPagar[i] * 0.065;
                    break;
                default:
                    break;
            }

            montoDeducido[i] = montoPagar[i] + montoComision[i];


            do
            {
                Console.WriteLine($"Monto con deducible: {montoDeducido[i]}");
                Console.WriteLine("Monto a Pagar del Cliente: ");
            } while (!double.TryParse(Console.ReadLine(), out montoPagaCliente[i]) || montoPagaCliente[i] < montoDeducido[i]);


            vuelto[i] = montoPagaCliente[i] - montoDeducido[i];

            MostrarResultadoPago(i);
            Console.WriteLine($"Número de Factura asignado: {numeroFactura[i]}\n");

            if (i < 9)
            {
                Console.Write("¿Desea ingresar otro pago? (S/N): ");
                string continuar;
                do
                {
                    continuar = Console.ReadLine().ToUpper();
                    if (continuar != "S" && continuar != "N")
                        Console.WriteLine("Opción inválida. Intente nuevamente.");
                } while (continuar != "S" && continuar != "N");

                if (continuar == "N")
                    break;
            }
            else
            {
                Console.WriteLine("Vectores llenos. No se permiten más pagos.\n");
                break;
            }
        }
    }

    static void EliminarPagos()
    {
        Console.Write("Ingrese el número de pago que desea eliminar: ");
        int numeroPagoEliminar;
        if (!int.TryParse(Console.ReadLine(), out numeroPagoEliminar))
        {
            Console.WriteLine("Número de pago inválido.");
            return;
        }

        int indice = Array.IndexOf(numeroPago, numeroPagoEliminar); // arrayindex es para buscar en el arreglo

        if (indice != -1 && numeroPago[indice] != 0 && !string.IsNullOrWhiteSpace(cedula[indice])) // si encontro la factura a eliminar 
        {
            Console.Clear();
            MostrarResultadoPago(indice);
            Console.Write("\n¿Está seguro de eliminar este pago? (S/N): ");
            string confirmacion = Console.ReadLine().ToUpper();

            if (confirmacion == "S")
            {
                // Eliminar el pago
                numeroPago[indice] = 0;
                cedula[indice] = "";
                nombre[indice] = "";
                apellido1[indice] = "";
                apellido2[indice] = "";
                tipoServicio[indice] = 0;
                numeroFactura[indice] = "";
                montoPagar[indice] = 0;
                montoComision[indice] = 0;
                montoDeducido[indice] = 0;
                montoPagaCliente[indice] = 0;
                vuelto[indice] = 0;

                Console.WriteLine("Pago eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Operación de eliminación cancelada.");
            }
        }
        else
        {
            Console.WriteLine("Pago no registrado.");
        }
    }

    static void MostrarEncabezado(string titulo)
    {
        Console.SetCursorPosition((Console.WindowWidth - titulo.Length) / 2, Console.CursorTop);
        Console.WriteLine(titulo + "\n");
    }

    static void MostrarResultadoPago(int indice)
    {
        // Factura 
        Console.Clear();

        MostrarEncabezado("Sistema de Pago de Servicios Públicos");

        Console.Write("Número de Pago:".PadRight(25));
        Console.WriteLine(numeroPago[indice]);

        Console.Write("Fecha:".PadRight(25));
        Console.Write($"{fecha[indice].ToString("dd/MM/yyyy"),-20}");
        Console.Write("Hora:".PadRight(15));
        Console.WriteLine($"{hora[indice],-20}");
        Console.WriteLine();

        Console.Write("Cedula:".PadRight(25));
        Console.Write($"{cedula[indice],-20}");
        Console.Write("Nombre:".PadRight(15));
        Console.WriteLine($"{nombre[indice],-20}");

        Console.Write("Primer Apellido:".PadRight(25));
        Console.Write($"{apellido1[indice],-20}");
        Console.Write("Segundo Apellido:".PadRight(15));
        Console.WriteLine($"{apellido2[indice],-20}");
        Console.WriteLine();

        Console.Write("Tipo de Servicio:".PadRight(25));
        Console.WriteLine($"{ObtenerNombreTipoServicio(tipoServicio[indice])} [{tipoServicio[indice]}]");

        Console.Write("Número de Factura:".PadRight(25));
        Console.Write($"{numeroFactura[indice],-20}");
        Console.Write("Monto a Pagar:".PadRight(15));
        Console.WriteLine($"{montoPagar[indice],-20}");

        Console.Write("Comisión autorizada:".PadRight(25));
        Console.Write($"{montoComision[indice],-20}");
        Console.Write("Paga con:".PadRight(15));
        Console.WriteLine($"{montoPagaCliente[indice],-20}");

        Console.Write("Monto Deducido:".PadRight(25));
        Console.Write($"{montoDeducido[indice],-20}");
        Console.Write("Vuelto:".PadRight(15));
        Console.WriteLine($"{vuelto[indice],-20}");
    }

    static void SubmenuReportes()
    {
        int opcionReporte;

        do
        {
            Console.WriteLine("Submenú Reportes");
            Console.WriteLine("1. Ver todos los Pagos");
            Console.WriteLine("2. Ver Pagos por tipo de Servicio");
            Console.WriteLine("3. Ver Pagos por código de caja");
            Console.WriteLine("4. Ver Dinero Comisionado por servicios");
            Console.WriteLine("5. Regresar Menú Principal");

            Console.Write("Seleccione una opción de reporte: ");
            opcionReporte = LeerEntero();

            switch (opcionReporte)
            {
                case 1:
                    VerTodosLosPagos();
                    break;
                case 2:
                    VerPagosPorTipoServicio();
                    break;
                case 3:
                    VerPagosPorCodigoCaja();
                    break;
                case 4:
                    VerComisionesPorServicio();
                    break;
                case 5:
                    Console.WriteLine("Regresando al Menú Principal...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                    break;
            }

        } while (opcionReporte != 5);
    }

    static void VerTodosLosPagos()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!string.IsNullOrWhiteSpace(cedula[i])) // recorre el arreglo buscando el pago 
            {
                MostrarResultadoPago(i);
                Console.WriteLine();
            }
        }
    }

    static void ConsultarPagos()
    {
        bool hayPagos = false;
        for (int i = 0; i < 10; i++)
        {
            if (numeroPago[i] != 0)
            {
                hayPagos = true;
                break;
            }
        }

        if (!hayPagos) // mensaje cuando no hay pagos por el false
        {
            Console.WriteLine("No hay pagos realizados. Presione Enter para continuar.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Informe: Todos los Pagos".PadLeft(Console.WindowWidth / 2));

        for (int i = 0; i < 10; i++)
        {
            if (numeroPago[i] != 0)
            {
                Console.WriteLine($"================ Pago #{numeroPago[i]} ================");
                MostrarResultadoPago(i);

                Console.WriteLine("\nPresione Enter para ver el siguiente pago, o 'S' para seleccionar un pago específico: ");
                string opcion = Console.ReadLine();

                if (opcion.ToUpper() == "S")
                {
                    Console.Write("Ingrese el número de pago que desea ver en detalle: ");
                    int numeroPagoSeleccionado;
                    if (int.TryParse(Console.ReadLine(), out numeroPagoSeleccionado) && numeroPagoSeleccionado >= 1 && numeroPagoSeleccionado <= 10) // solicita el numero de cedula para verficiar el pago
                    {
                        int indiceSeleccionado = Array.IndexOf(numeroPago, numeroPagoSeleccionado);
                        if (indiceSeleccionado != -1 && numeroPago[indiceSeleccionado] != 0 && !string.IsNullOrWhiteSpace(cedula[indiceSeleccionado]))
                        {
                            Console.Clear();
                            MostrarResultadoPago(indiceSeleccionado);
                            Console.WriteLine("\nPresione Enter para continuar.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Pago no registrado o número de pago inválido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Número de pago inválido.");
                    }
                }
            }
        }
        Console.WriteLine("No hay más pagos para mostrar. Presione Enter para regresar al Menú Principal.");
        Console.ReadLine();
    }

    static void ModificarPagos()
    {
        Console.Write("Ingrese el número de pago que desea modificar: ");
        int numeroPagoModificar;
        if (!int.TryParse(Console.ReadLine(), out numeroPagoModificar)) // out fuera del arreglo 
        {
            Console.WriteLine("Número de pago inválido.");
            return;
        }

        int indice = Array.IndexOf(numeroPago, numeroPagoModificar);

        if (indice != -1 && numeroPago[indice] != 0 && !string.IsNullOrWhiteSpace(cedula[indice]))
        {
            Console.Clear();
            MostrarResultadoPago(indice);
            Console.WriteLine("\nIngrese los nuevos datos para este pago.");

            Console.Write("Cedula (solo números de 9 dígitos): ");
            while (true)
            {
                string inputCedula = Console.ReadLine();
                if (inputCedula.Length == 9 && int.TryParse(inputCedula, out _))
                {
                    cedula[indice] = inputCedula;
                    break;
                }
                else
                {
                    Console.Write("Por favor, ingrese su número de cedula nuevamente (9 dígitos): ");
                }
            }

            Console.Write("Nombre: ");
            nombre[indice] = Console.ReadLine();

            Console.Write("Primer Apellido: ");
            apellido1[indice] = Console.ReadLine();

            Console.Write("Segundo Apellido: ");
            apellido2[indice] = Console.ReadLine();

            int tipoServicioActual;
            do
            {
                Console.Write("Tipo de Servicio (1= Recibo de Luz, 2= Recibo Teléfono, 3= Recibo de Agua): ");
                if (!int.TryParse(Console.ReadLine(), out tipoServicioActual) || (tipoServicioActual < 1 || tipoServicioActual > 3))
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                else
                    tipoServicio[indice] = tipoServicioActual;
            } while (tipoServicio[indice] == 0);

            do
            {
                Console.Write("Monto a Pagar: ");
            } while (!double.TryParse(Console.ReadLine(), out montoPagar[indice]));

            switch (tipoServicio[indice])
            {
                case 1:
                    montoComision[indice] = montoPagar[indice] * 0.04;
                    break;
                case 2:
                    montoComision[indice] = montoPagar[indice] * 0.055;
                    break;
                case 3:
                    montoComision[indice] = montoPagar[indice] * 0.065;
                    break;
                default:
                    break;
            }

            montoDeducido[indice] = montoPagar[indice] - montoComision[indice];

            do
            {
                Console.Write("Monto a Pagar del Cliente: ");
            } while (!double.TryParse(Console.ReadLine(), out montoPagaCliente[indice]) || montoPagaCliente[indice] < montoDeducido[indice]);

            vuelto[indice] = montoPagaCliente[indice] - montoDeducido[indice];

            Console.WriteLine("Pago modificado correctamente.");
        }
        else
        {
            Console.WriteLine("Pago no registrado.");
        }
    }

    static void VerPagosPorTipoServicio()
    {
        Console.Write("Ingrese el tipo de servicio (1= Recibo de Luz, 2= Recibo Teléfono, 3= Recibo de Agua): ");
        int tipoServicioFiltrar;
        if (!int.TryParse(Console.ReadLine(), out tipoServicioFiltrar) || (tipoServicioFiltrar < 1 || tipoServicioFiltrar > 3))
        {
            Console.WriteLine("Tipo de servicio inválido.");
            return;
        }

        Console.Clear();
        MostrarEncabezado($"Pagos para el Tipo de Servicio {ObtenerNombreTipoServicio(tipoServicioFiltrar)}");

        bool hayPagos = false;
        for (int i = 0; i < 10; i++) // recorre los arreglos para buscar los servicios en clientes 
        {
            if (tipoServicio[i] == tipoServicioFiltrar)
            {
                hayPagos = true;
                MostrarResultadoPago(i);
                Console.WriteLine();
            }
        }

        if (!hayPagos)
            Console.WriteLine("No hay pagos para el tipo de servicio seleccionado.");

        Console.WriteLine("\nPresione Enter para regresar al Submenú de Reportes.");
        Console.ReadLine();
    }

    static void VerPagosPorCodigoCaja()
    {
        Console.Write("Ingrese el código de caja (1, 2, 3): ");
        int codigoCajaFiltrar;
        if (!int.TryParse(Console.ReadLine(), out codigoCajaFiltrar) || (codigoCajaFiltrar < 1 || codigoCajaFiltrar > 3)) // si el numero es menor a 1 o mayor a 3 ingrese caja invalida
        {
            Console.WriteLine("Código de caja inválido.");
            return;
        }

        Console.Clear();
        MostrarEncabezado($"Pagos para el Código de Caja {codigoCajaFiltrar}");

        bool hayPagos = false; // se inicia el hayPagos en falso 
        for (int i = 0; i < 10; i++) // si se encuentra entra a mostrar los resultados del pago
        {
            if (numeroCaja[i] == codigoCajaFiltrar)
            {
                hayPagos = true;
                MostrarResultadoPago(i);
                Console.WriteLine();
            }
        }

        if (!hayPagos) // si no se encuentra queda en false
            Console.WriteLine($"No hay pagos registrados para el código de caja {codigoCajaFiltrar}.");

        Console.WriteLine("\nPresione Enter para regresar al Submenú de Reportes.");
        Console.ReadLine();
    }

    static void VerComisionesPorServicio()
    {
        Console.Clear();
        MostrarEncabezado("Comisiones por Tipo de Servicio");

        double totalComisionesLuz = 0;
        double totalComisionesTelefono = 0;
        double totalComisionesAgua = 0;

        for (int i = 0; i < 10; i++)
        {
            switch (tipoServicio[i])
            {
                case 1:
                    totalComisionesLuz += montoComision[i];
                    break;
                case 2:
                    totalComisionesTelefono += montoComision[i];
                    break;
                case 3:
                    totalComisionesAgua += montoComision[i];
                    break;
                default:
                    break;
            }
        }

        Console.WriteLine($"Comisiones para Recibo de Luz: ${totalComisionesLuz}");
        Console.WriteLine($"Comisiones para Recibo de Teléfono: ${totalComisionesTelefono}");
        Console.WriteLine($"Comisiones para Recibo de Agua: ${totalComisionesAgua}");

        Console.WriteLine("\nPresione Enter para regresar al Submenú de Reportes.");
        Console.ReadLine();
    }

    static string ObtenerNombreTipoServicio(int tipo)
    {
        switch (tipo)
        {
            case 1:
                return "Recibo de Luz";
            case 2:
                return "Recibo Teléfono";
            case 3:
                return "Recibo de Agua";
            default:
                return "Desconocido";
        }
    }

    static int LeerEntero()
    {
        int numero;
        while (!int.TryParse(Console.ReadLine(), out numero))
        {
            Console.Write("Entrada no válida. Por favor, ingrese un número entero: ");
        }
        return numero;
    }

    static string GenerarNumeroFacturaConsecutivo()
    {
        return $"FACT-{DateTime.Now.ToString("yyyyMMdd")}-{numeroFacturaConsecutivo:D3}";
    }

    static void ConfigurarConsola()
    {
        Console.Title = "Sistema de Pago de Servicios \n  los Pros de Informatica  ";


        Console.WindowWidth = 120;
        Console.WindowHeight = 30;
        Console.BufferWidth = 120;
        Console.BufferHeight = 300;
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
    }
}