// Freney Studios (C) Copyright 2024
// ======================================
// Program.cs / Marco Panseri / 07/12/2024
// ======================================
using System;
using Values;
using System.Collections.Generic;

class Program
{   

    static string version             = "0.1";
    static string milestone           = "IceFlames";
    static List<Service_t> ServicesList = [];

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {   
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[INFO] No arguments passed, enter interactive mode?");
            Console.Write(">(y/N):");
            var choice = Console.ReadLine()?.Trim().ToLower();

            if (choice == "y" || choice == "yes")
            {
                InteractiveMode();
            }
            else
            {   
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting from Nova");
                Console.ResetColor();
            }
        }
        else
        {
            NonInteractiveMode(args);
        }
    }

    static void InteractiveMode()
    {   
        PrintLargeCaptions();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Welcome to the interactive mode");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Insert a command (or type \"exit\" to exit)");
        Console.ResetColor();

        while (true)
        {
            Console.ResetColor();
            Console.Write(">");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
                continue;

            if (input.ToLower() == "exit")
            {   
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting interactive mode...");
                Console.ResetColor();
                break;
            }

            ProcessCommand(input.Split(' '));
        }
    }

    static void NonInteractiveMode(string[] args)
    {   
        Console.ResetColor();
        PrintLargeCaptions();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Execution in arg-parse mode");
        ProcessCommand(args);
    }

    static void ProcessCommand(string[] args)
    {   
        Service_t TempServ;
        TempServ = new Service_t();
        string command = args[0].ToLower();

        for (int i = 0; i < args.GetLength(0); i++)
        {   
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[DEBUG] Argument {i + 1}: {args[i]}");
        }
        switch (command)
        {
            case "--version":
            case "-v":
            case "version":
                ShowVersion();
                return;

            case "--help":
            case "-h":
            case "help":
                ShowHelp();
                return;
            
            case "--servlist":
            case "servlist":
            case "selist":
            case "servicelist":
            case "service-list":
                PrintServList();
                return;

            case "--addservice":
            case "addservice":
            case "add-service":

                try
                {
                    TempServ.Name   = args[1];
                    TempServ.Desc   = args[2];
                    TempServ.Author = args[3];
                    if (args[4] == "on" || args[4] == "On" || args[4] == "ON" || args[4] == "enabled" )
                    {
                        TempServ.State = true;
                    }
                    else
                    {
                        TempServ.State = false;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not enough arguments provided. Use --addservice [name] [description] [author] [enabled/disabled]");
                    Console.ResetColor();
                    return;
                }
                catch (Exception ex)
                {   
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error while creating new service: {ex.Message}");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("New service info: ");
                PrintServInfo(TempServ);


                try
                {
                    ServicesList.Add(TempServ);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Service added successfully");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error while adding service: {ex.Message}");
                    Console.ResetColor();
                }

                Console.ResetColor();
                return;

            case "--editservice":
            case "editservice":
            case "edit-service":
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("[INFO] Cmd struct: --editservice <name> <author> <desc>");
                Console.ResetColor();
                try
                {
                    TempServ = GetServiceFromName(ServicesList,args[1]);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error while searching for service: {ex.Message}");
                    Console.ResetColor();
                    return;
                }  
                return;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Unknow command: {command}");
                break;
        }
    }

    static void ShowVersion()
    {
        Console.WriteLine($"Nova {version} {milestone}");
    }

    static Service_t GetServiceFromName
    (
        List<Service_t> slist,
        string          sname
    )
    {   
        Service_t TempServ;
        TempServ = new Service_t();

        foreach (Service_t serv in slist)
        {
            if (serv.Name.ToLower() == sname.ToLower())
            {
                TempServ = serv;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Service found: {TempServ.Name}");
                Console.ResetColor();

                break;
            }
        }

        return TempServ;
    }

    static void PrintServInfo (Service_t ser)
    {
        Console.WriteLine($"- {ser.Name}");
        Console.WriteLine($"  Desc  : {ser.Desc}");
        Console.WriteLine($"  Author: {ser.Author}");
        Console.WriteLine($"  State : {ser.State}");
        Console.WriteLine("");
    }

    static void PrintServList()
    {   
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("============[ Nova Services List ]==============");

        foreach (Service_t ser in ServicesList)
        {
            PrintServInfo(ser);
        }

        Console.WriteLine("============[ Nova Services List ]==============");
        Console.ResetColor();
    }

    static void ShowHelp()
    {
        Console.WriteLine("Comandi disponibili:");
        Console.WriteLine("--name, -n [valore]: Specifica il nome da salutare.");
        Console.WriteLine("--repeat, -r [numero]: Specifica quante volte ripetere il saluto.");
        Console.WriteLine("--version, -v: Mostra la versione del programma.");
        Console.WriteLine("--help, -h: Mostra questo messaggio di aiuto.");
        Console.WriteLine("exit: Esce dalla modalità interattiva.");
    }

    static void PrintLargeCaptions()
    {   
        Console.ResetColor();
        Console.WriteLine("");
        Console.WriteLine( "  ######                ######                                                      ");
        Console.WriteLine( "  ################      #!###!###!!!####                                            ");
        Console.WriteLine( "  ##################    !###!##!!#########                                          ");
        Console.WriteLine( "  ####    ####    ###   #*#  ###  *######*#                                          ");
        Console.WriteLine( "  ####  ####   ######   *#*   #*  ###**#*#                                         ");
        Console.WriteLine( "  ####    ##   ######   ###  # #  ###*##^#*           Freney Studios                ");
        Console.WriteLine( "  ####  ######   ####   #^^  ##   #)###^#^^         ##################              ");
        Console.WriteLine($"  ####  ###      ####   ^##  ###  ###(#####      Nova {version} - {milestone}          ");
        Console.WriteLine( "   ##################    ########^######^##                                         ");
        Console.WriteLine( "     ################      ###?#########?##                                         ");
        Console.WriteLine( "               ######                ######                                         ");
        Console.WriteLine("");
    }

}

