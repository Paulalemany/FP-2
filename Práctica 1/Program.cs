//Paula <alemany Rodríguez

namespace Práctica_1
{
    internal class Program
    {

        const bool DEBUG = true;

        struct Coor {   // coordenadas en el tablero  
            public int x, y; }

        struct Pilar {  // pilar en el tablero 
            public Coor coor;   // posición en el tablero
            public int val; }   // valor 

        struct Rect {  // rectangulo determinado por dos esquinas
            public Coor lt, rb; }  // left-top, right-bottom

        struct Tablero { // tamaño, pilares, rectángulos marcados
            public int fils, cols;  // num fils y cols del tablero   
            public Pilar[] pils;    // array de pilares
            public Rect[] rects;    // array de rectángulos
            public int numRects; }  // num de rectángulos definidos = prim pos libre en rect

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        static char leeInput()
        {
            char d = ' ';
            while (d == ' ')
            {
                if (Console.KeyAvailable)
                {
                    string tecla = Console.ReadKey().Key.ToString();
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
                        case "Spacebar":
                            d = 'c';
                            break;
                        case "Escape":
                        case "Q":
                            d = 'q';
                            break;
                    }
                }
            }
            return d;
        }

        void LeeNivel(string file, Tablero tab)
        {
            //Abrimos el archivo que se va a leer
            StreamReader sr = new StreamReader(file);

            //Creamos el tablero
            tab.fils = int.Parse(sr.ReadLine());        //Filas del tablero
            tab.cols = int.Parse(sr.ReadLine());        //Columnas del tablero
        }
    }
}
