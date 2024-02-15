//Paula <alemany Rodríguez

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
            Tablero tab = new Tablero();    //Creamos el tablero
            string file;

            Inicializa(out file);           //Creamos la ruta de acceso del tablero

            LeeNivel(file, tab);

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

        static void LeeNivel(string file, Tablero tab)
        {
            //Abrimos el archivo que se va a leer
            StreamReader sr = new StreamReader(file);

            //Creamos el tablero
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
                        pil.coor.x = i;
                        pil.coor.y = j;

                        //Guardamos el pilar en el array
                        tab.pils[k] = pil;
                        k++;
                    }
                }
                j++;
            }

        }
        #endregion

        #endregion

    }
}
