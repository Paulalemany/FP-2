﻿//Paula Alemany Rodríguez

namespace Práctica_1
{
    internal class Program
    {

        const bool DEBUG = true;

        #region Structs
        struct Coor {   // coordenadas en el tablero  
            public int x, y;
        }

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
        #endregion


        static void Main(string[] args)
        {
            Tablero tab;
            string file;                    //Ruta de acceso al nivel

            Coor act;                       //Pos actual del cursor -> Se puede inicializar en (0,0) <-
            act.x = act.y = 0;
            Coor ori;                       //Esquina origen del posible nuevo rectángulo
            ori.x = -1; ori.y = 0;

            Inicializa(out file);           //Creamos la ruta de acceso del nivel
            LeeNivel(file, out tab);            //Leemos el nivel

            Render(tab, act, ori);          //Render inicial

        }

        #region Métodos

        #region Input
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
        #endregion

        #region inicialización

        static void Inicializa(out string file)
        {
            //Incicializamos el nivel
            Console.WriteLine("Escriba el número del nivel que desea jugar ");      //Tal vez sea mejor sacar esta línea y dejar el método mudo
            string nivel = Console.ReadLine();
            char[] cadena = nivel.ToCharArray();

            if (cadena.Length < 3)
            {
                for (int i = cadena.Length; i <= 2; i++) { nivel = "0" + nivel; }
            }

            file = "puzzles/puzzles/" + nivel + ".txt";
        }

        static void LeeNivel(string file, out Tablero tab)
        {
            //Abrimos el archivo que se va a leer
            StreamReader sr = new StreamReader(file);

            //Creamos el tablero
            tab = new Tablero();                        //Creamos el tablero
            tab.fils = int.Parse(sr.ReadLine());        //Filas del tablero
            tab.cols = int.Parse(sr.ReadLine());        //Columnas del tablero

            tab.pils = new Pilar[tab.cols * tab.fils];  //inicialización del array -> EN EL ENUNCIADO PONE QUE DEBE TENER UNA COMPONENTE POR PILAR <-
            tab.rects = new Rect[tab.cols * tab.fils];  //Inicialización array de rectángulos

            int j = 0;      //Contador de filas
            int k = 0;      //Contador de pilares
            while (!sr.EndOfStream)
            {
                //Leemos la fila del tablero
                string fila = sr.ReadLine();
                //Dividimos la fila por espacios
                string[] pilares = fila.Split(' ');
                //Guardamos los números ignorando los guiones
                for (int i = 0; i < pilares.Length; i++)
                {
                    if (pilares[i] == "-") { }  //Si es un guion lo ignoramos
                    else
                    {
                        //Creamos un pilar
                        Pilar pil = new Pilar();
                        pil.val = int.Parse(pilares[i]);
                        pil.coor.x = j;
                        pil.coor.y = i;

                        //Guardamos el pilar en el array
                        tab.pils[k] = pil;
                        k++;
                    }
                }
                j++;
            }

        }
        #endregion

        Rect NormalizaRect(Coor c1, Coor c2)
        {
            Rect r = new Rect();

            //left - right
            if (c1.x < c2.x)
            {
                r.lt.x = c1.x;
                r.rb.x = c2.x;
            }
            else
            {
                r.lt.x = c2.x;
                r.rb.x = c1.x;
            }

            //top - bottom
            if (c1.y < c2.y)
            {
                r.lt.y = c1.y;
                r.rb.y = c2.y;
            }
            else
            {
                r.lt.y = c2.y;
                r.rb.y = c1.y;
            }

            return r;
        }

        #region Renders
        static void Render(Tablero tab, Coor act, Coor ori)
        {
            Console.Clear();
            InterseccionesRender(tab);
            PilarRender(tab);
            RectanglesRender(tab);
            ActualRectangle(tab, act, ori);
            Cursor(act);

            if (DEBUG) Debug(act, ori);
            
        }

        //Vamos a hacer un render por cada fase probablemente
        static void InterseccionesRender(Tablero tab) 
        {
            for(int i = 0; i < tab.fils; i++)       //Recorre las filas del tablero
            {
                for (int j = 0; j <= tab.cols; j++)  //Recorre las columnas del tablero
                {
                     Console.Write("+   ");
                }
                //Hacemos el salto de línea
                Console.Write('\n');

          
                
                

                    Console.Write('\n');
            }
        }

        static void PilarRender(Tablero tab) { }

        static void RectanglesRender(Tablero tab) { }

        static void ActualRectangle(Tablero tab, Coor act, Coor ori) { }

        static void Debug(Coor act, Coor ori) 
        {
            Console.WriteLine();        //Dejamos un poco de aire
            //Rectángulos que llevamos
            Console.WriteLine("Ori: ({0},{1})    Act: ({2},{3}) ", ori.x, ori.y, act.x, act.y);
        }

        static void Cursor(Coor act) { }
        #endregion


        #endregion

    }
}
