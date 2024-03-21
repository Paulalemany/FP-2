//Paula Alemany Rodríguez

using System;
using Coordinates;
using SetArray;

namespace Hoja6
{
    internal class Program
    {
        const bool DEBUG = true; // para depuración: deja ver posicion de las minas
        static Random rnd = new Random(); // generador de aleatorios para colocar minas
        struct Tablero
        {
            // '1'..'8' indica num de minas alrededor   
            // '·' indica que no hay minas alrededor
            // 'x' marcada como mina, 'o' no destapada, '*' mina destapada
            public char[,] casilla;
            public SetCoor minas;  // Conjunto de (coordenadas de) minas
            public Coor cursor;    // Coordenadas del cursor            
        }

        static void Main(string[] args)
        {
            // tablero sencillo para depuracion
            Tablero t = Tab1();

            Render(t);

            // bucle ppal
            while (true)
            {
                // leer input
                // procesar input
            }
        }

        // tablero de prueba
        static Tablero Tab1()
        {
            Tablero t;
            t.cursor = new Coor(0, 0);
            int fils = 4, cols = 5;
            t.casilla = new char[fils, cols];
            for (int i = 0; i < fils; i++)
                for (int j = 0; j < cols; j++)
                {
                    t.casilla[i, j] = 'o';
                }
            t.minas = new SetCoor(fils * cols / 2);
            t.minas.Add(new Coor(0, 0));
            t.minas.Add(new Coor(0, 2));
            t.minas.Add(new Coor(1, 0));
            t.minas.Add(new Coor(1, 2));
            t.minas.Add(new Coor(2, 0));
            t.minas.Add(new Coor(2, 1));
            return t;
        }

        static void Render(Tablero tab)
        {
            Console.Clear();
            for (int i = 0; i < tab.casilla.GetLength(0); i++)
            {
                for (int j = 0; j < tab.casilla.GetLength(1); j++)
                {
                    if (tab.casilla[i, j] == '·')
                    { // no cambiamos color
                    }
                    else if (tab.casilla[i, j] == '1')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (tab.casilla[i, j] == '2')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (tab.casilla[i, j] == '3')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (tab.casilla[i, j] == '4')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (tab.casilla[i, j] == '5')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    else if (tab.casilla[i, j] == '6')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (tab.casilla[i, j] == '7')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else if (tab.casilla[i, j] == '8')
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else if (tab.casilla[i, j] == 'x')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (tab.casilla[i, j] == 'o')
                    { // no cambiamos color
                    }
                    else if (tab.casilla[i, j] == '*')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    // Para depuración, marcamos las minas en morado
                    if (DEBUG && tab.minas.IsElementOf(new Coor(i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    Console.Write($" {tab.casilla[i, j]}");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(tab.cursor.Y * 2 + 1, tab.cursor.X);
        }

        static bool ProcesaInput(ref Tablero t, char c)
        {
            switch (c)
            {
                case 'l':
                    if (t.cursor.Y > 0) t.cursor.Y--;
                    break;
                case 'r':
                    if (t.cursor.Y < t.casilla.GetLength(1) - 1) t.cursor.Y++;
                    break;
                case 'u':
                    if (t.cursor.X > 0) t.cursor.X--;
                    break;
                case 'd':
                    if (t.cursor.X < t.casilla.GetLength(0) - 1) t.cursor.X++;
                    break;
                case 'x':
                    if (t.casilla[t.cursor.X, t.cursor.Y] == 'o')
                        t.casilla[t.cursor.X, t.cursor.Y] = 'x';
                    else if (t.casilla[t.cursor.X, t.cursor.Y] == 'x')
                        t.casilla[t.cursor.X, t.cursor.Y] = 'o';
                    break;
                case 'c':
                    if (HayMina(t, t.cursor.X, t.cursor.Y))
                    {
                        t.casilla[t.cursor.X, t.cursor.Y] = '*';
                        return true;
                    }
                    else if (t.casilla[t.cursor.X, t.cursor.Y] == 'o')
                    {
                        DescubreAdyacentes(t, t.cursor.X, t.cursor.Y);
                    }
                    break;
            }
            return false;
        }

        static char LeeInput()
        {
            char d = ' ';
            string tecla = Console.ReadKey(true).Key.ToString();
            switch (tecla)
            {
                case "LeftArrow":
                    d = 'l';
                    break;
                case "UpArrow":
                    d = 'u';
                    break;
                case "RightArrow":
                    d = 'r';
                    break;
                case "DownArrow":
                    d = 'd';
                    break;
                case "Escape":
                    d = 'q';
                    break;
                case "Spacebar":
                    d = 'c';
                    break;
                case "Enter":
                    d = 'x';
                    break;
            }
            while (Console.KeyAvailable)
                Console.ReadKey().Key.ToString();
            return d;
        }

        static bool HayMina(Tablero t, int i, int j)
        {
            return t.minas.IsElementOf(new Coor(i, j));
        }

        static int MinasAlrededor(Tablero t, int x, int y)
        {
            int fils = t.casilla.GetLength(0), cols = t.casilla.GetLength(1);
            int cont = 0;
            for (int i = Math.Max(0, x - 1); i <= Math.Min(fils - 1, x + 1); i++)
                for (int j = Math.Max(0, y - 1); j <= Math.Min(cols - 1, y + 1); j++)
                    if (HayMina(t, i, j)) cont++;
            return cont;
        }

        /*
        Al destapar una casilla que NO tiene mina, si no tiene minas alrededor 
        hay que descubrir las adyacentes y propagar el efecto sobre las mismas

        Problema al descubrir un casilla c sin minas alrededor se descubre la que tiene al lado c1;
        al descubrir c1, a su vez se decubriría nuevamente c.... -> ciclo infinito!!

        Esto ocurre en general al explorar mapas (grafos)
        
        Utilizamos dos ctos:
            - pendientes: casillas pendientes de visitar
            - visitadas
        
        Inicialmente, cuando se destapa una casilla c tenemos: pendientes={c}, visitadas={}

        Algoritmo:
            mientras queden pendientes:
                extraer casilla c de pendientes y decubrirla // obs: con seguridad no será una mina
                visitadas = visitadas U {c}
                si c NO tiene minas alrededor:
                    para cada casilla c1 adyacente a c, que no esté visitada:                        
                            pendientes = pendientes U {c1}
                

        */


        static void DescubreAdyacentes(Tablero t, int x, int y)
        {
            int fils = t.casilla.GetLength(0), cols = t.casilla.GetLength(1);
            SetCoor pendientes = new SetCoor(fils * cols),
                    visitados = new SetCoor(fils * cols);
            pendientes.Add(new Coor(x, y));
            while (!pendientes.Empty())
            {
                Coor c = pendientes.PopElem();
                visitados.Add(c);
                (x, y) = (c.X, c.Y);
                int cont = MinasAlrededor(t, x, y);
                if (cont > 0) t.casilla[x, y] = (char)(((int)'0') + cont);
                else
                { // cont==0
                    t.casilla[x, y] = '·';
                    for (int i = Math.Max(0, x - 1); i <= Math.Min(fils - 1, x + 1); i++)
                        for (int j = Math.Max(0, y - 1); j <= Math.Min(cols - 1, y + 1); j++)
                        {
                            Coor c1 = new Coor(i, j);
                            if (!visitados.IsElementOf(c1)) pendientes.Add(c1);
                        }
                }
            }
        }
    }

}
