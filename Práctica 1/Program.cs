//Paula Alemany Rodríguez

namespace Práctica_1
{
    internal class Program
    {

        const bool DEBUG = true;
        const int HUECO_Y = 4;
        const int HUECO_X = 2;

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
            string file;                    //Ruta de acceso al nivel

            Inicializa(out file);           //Creamos la ruta de acceso del nivel
            while(JuegaNivel(file))         //Mientras queramos seguir jugando
            {
                Inicializa(out file);       //Cargamos el nuevo nivel
            }

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

        static void ProcesaInput(char ch, ref Tablero tab, ref Coor act, ref Coor ori)
        {
            if (ch == 'l' && act.y > 0) { act.y--; }
            else if (ch == 'r' && act.y < tab.cols - 1) { act.y++; }
            else if (ch == 'u' && act.x > 0) { act.x--; }
            else if (ch == 'd' && act.x < tab.fils - 1) { act.x++; }
            else if (ch == 'c')
            {
                //Si Ori no está marcado marcamos ori
                if (EliminaRect(ref tab, act)) { }
                else if (ori.x == -1)
                {
                    //Comenzamos con el inserta
                    ori = act;
                }
                //Si ya está marcado ori insertamos el rectángulo
                else
                {
                    InsertaRect(ref tab, act, ori);
                    ori.x = -1;
                }
            }

        }
        #endregion

        #region inicialización

        static void Inicializa(out string file)
        {
            //Incicializamos el nivel
            Console.Clear();
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

            Pilar[] aux = new Pilar[tab.cols * tab.fils];   //Array auxiliar para los pilares
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
                        aux[k] = pil;
                        k++;
                    }
                }
                j++;
            }

            //Creamos el verdadero array de pilares con la longitud de K
            tab.pils = new Pilar[k];  //inicialización del array
            //Rellenamos con el auxiliar
            for (int i = 0; i < tab.pils.Length; i++) { tab.pils[i] = aux[i]; }

        }
        #endregion

        static Rect NormalizaRect(Coor c1, Coor c2)
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
            
            for (int i = 0; i < tab.numRects; i++)
            {
                RenderRect(tab.rects[i]);
            }
            PilarRender(tab);
            ActualRectangle(act, ori);
            Cursor(act);

            Console.SetCursorPosition(0, tab.cols + tab.fils);
            if (DEBUG) Debug(tab, act, ori);
            
        }

        static void InterseccionesRender(Tablero tab) 
        {
            for(int i = 0; i <= tab.fils; i++)       //Recorre las filas del tablero
            {
                for (int j = 0; j <= tab.cols; j++)  //Recorre las columnas del tablero
                {
                    //Se multiplica para que el tablero se quede de un buen tamaño
                    Console.SetCursorPosition(j * HUECO_Y, i * HUECO_X);
                    Console.Write("+");
                }
                //Hacemos el salto de línea
                Console.Write('\n');
                Console.Write('\n');
            }
        }

        static void PilarRender(Tablero tab) 
        {
            for (int j = 0; j < tab.pils.Length; j++)   //Recorre los pilares del tablero
            {
                if (tab.pils[j].val != 0)
                {
                    //Se le suma para ponerlo en el centro
                    Console.SetCursorPosition((tab.pils[j].coor.y * HUECO_Y) + 2, (tab.pils[j].coor.x * HUECO_X) + 1);
                    Console.Write(tab.pils[j].val);
                }
            }
           
        }

        static void RenderRect(Rect r) 
        {
            //Dibujamos la horizontal
            for (int i = r.lt.y; i <= r.rb.y; i++)
            {
                
                //Nos colocamos en su lt
                Console.SetCursorPosition(i * HUECO_Y + 1, r.lt.x * HUECO_X);
                Console.Write("---");
                //Intento de colorear
                //Console.BackgroundColor = ConsoleColor.Green;
                //Console.SetCursorPosition(i * HUECO_Y + 1, r.lt.x * HUECO_X + 1);
                //Console.Write("    ");
                //Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(i * HUECO_Y + 1, r.rb.x * HUECO_X + 2);
                Console.Write("---");
                
            }

            //Dibujamos la vertical
            for (int i = r.lt.x; i <= r.rb.x; i++)
            {
                //Nos colocamos en su lt
                Console.SetCursorPosition(r.lt.y * HUECO_Y , i * HUECO_X + 1 );
                Console.Write("|");
                //Console.BackgroundColor = ConsoleColor.Green;
                //Console.SetCursorPosition(r.lt.y * HUECO_Y + 1, i * HUECO_X + 1);
                //Console.Write("    ");
                //Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(r.rb.y * HUECO_Y + HUECO_Y , i * HUECO_X + 1);
                Console.Write("|");
            }

            
        }

        static void ActualRectangle(Coor act, Coor ori) 
        {
            //Estamos creando un rectángulo
            if (ori.x != -1)
            {
                //Creamos un rect normalizado entre act y ori
                Rect r = NormalizaRect(ori, act);
                //Llamamos al render rect (Pintando en verde)
                Console.ForegroundColor = ConsoleColor.Green;
                RenderRect(r);

                //Devolvemos a su color las cosas
                Console.ForegroundColor= ConsoleColor.White;
            }
        }

        static void Debug(Tablero tab, Coor act, Coor ori) 
        {
            Console.WriteLine();        //Dejamos un poco de aire

            Console.Write("Rects: ");
            for (int i = 0; i < tab.numRects; i++)
            {
                Console.Write(" ({0},{1})-({2},{3}) ", tab.rects[i].lt.x, tab.rects[i].lt.y, tab.rects[i].rb.x, tab.rects[i].rb.y);
            }
            Console.WriteLine();
            //Rectángulos que llevamos
            Console.WriteLine("Ori: ({0},{1})    Act: ({2},{3}) ", ori.x, ori.y, act.x, act.y);
        }

        static void Cursor(Coor act) 
        {
            Console.SetCursorPosition(act.y * HUECO_Y + 2, act.x * HUECO_X + 1);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        #endregion

        #region Rectangulos
        static bool Dentro(Coor c, Rect r)
        {
            return r.lt.y <= c.y && c.y <= r.rb.y && r.lt.x <= c.x && c.x <= r.rb.x;
        }

        static bool InterSect(Rect r1, Rect r2)
        {
            int rb;
            int ig;
            Coor c;

            //Vemos en que dirección va el rect
            if (r1.lt.x == r1.rb.x)     //Va en horizontal
            {
                c.x = r1.lt.x;  //La coordenada x no cambia es la fija
                c.y = r1.lt.y;  //La coordenada y es la que va aumentando 
                rb = r1.rb.y;   //Máx de la coordenada y

                //Vamos comprobando si está dentro de r2 cada coordenada de r1
                while (c.y <= rb && !Dentro(c, r2))
                {
                    c.y++;
                }
                ig = c.y;   //Guardamos el resultado de la búsqueda
            }
            else    //Va en vertical
            {
                c.y = r1.lt.y;  //La coordenada y no cambia es la fija
                c.x = r1.lt.x ; //La coordenada x es la que va aumentando
                rb = r1.rb.x;   //Máx de la coordenada x

                //Vamos comprobando si está dentro de r2 cada coordenada de r1 
                while (c.x <= rb && !Dentro(c, r2))
                {
                    c.x++;
                }

                ig = c.x; //Guardamos el resultado de la búsqueda
            }
            
            return ig <= rb;    //Si se ha encontrado alguna menor o igual al máximo del rect es que hay intersección
        }

        static void InsertaRect(ref Tablero tab, Coor c1, Coor c2)  //No se si habrá que pasar el tablero por algo al estar modificando num
        {
            Rect r = NormalizaRect(c1, c2);

            //Comprobamos que el rectángulo no solape a ninguno de los ya existentes
            int i = 0;
            while(i < tab.numRects && !InterSect(r, tab.rects[i])) { i++; }

            //Si llega al final es que no solapa a ninguno
            if(i == tab.numRects)
            {
                tab.rects[tab.numRects] = r;
                tab.numRects++;
            }
           
        }

        static bool EliminaRect(ref Tablero tab, Coor c)
        {
            bool e = false;

            //Buscamos en el tablero si algún rectángulo tiene la coordenada
            int i = 0;
            while (i < tab.numRects && !Dentro(c, tab.rects[i])) { i++; }

            //Si no llega al final es que hay que eliminar
            if (i < tab.numRects)
            {
                //Eliminamos el rect
                for (int j = i; j < tab.numRects - 1; j++)
                {
                    tab.rects[j] = tab.rects[j+1];
                }

                e = true;
                tab.numRects--;
            }

            return e;
        }
        #endregion

        static int AreaRect(Rect r)
        {
            int s;
            int a = 1;
            int h = 1;

            for (int i = r.lt.y; i < r.rb.y; i++) { a++; }      //Sumamos el ancho
            for (int i = r.lt.x; i < r.rb.x; i++) { h++; }      //Sumamos el alto
            s = a * h;      //Calculamos el área
            return s;
        }

        static bool CheckRect(Rect r, Pilar[] p)
        {
            bool b = false;
            int i = 0;

            while (i < p.Length && !Dentro(p[i].coor, r)) i++;

            //Si no ha llegado al final es que ha encontrado uno
            if (i != p.Length && AreaRect(r) == p[i].val) b = true;

            return b;
        }

        static bool FinJuego(Tablero tab)
        {
            bool fin = false;

            //Recorre los rectángulos que hay en el tablero y hace el check, si llega al final es que el tablero está completos
            int i = 0;
            //No estoy segura de si esto sería el lenght pero ahora testearé
            if (tab.numRects == tab.pils.Length)
            {
                while (i < tab.pils.Length && CheckRect(tab.rects[i], tab.pils)) i++;
                //Si llega al final significa que el juego se ha finalizado correctamente
                if (i == tab.pils.Length) { fin = true; }
            }
           
            return fin;
        }

        static bool JuegaNivel(String file)
        {
            Tablero tab;
            bool nivel = false;
            bool exit = false;

            Coor act;                       //Pos actual del cursor -> Se puede inicializar en (0,0) <-
            act.x = act.y = 0;
            Coor ori;                       //Esquina origen del posible nuevo rectángulo
            ori.x = -1; ori.y = 0;

            LeeNivel(file, out tab);        //Leemos el nivel

            Render(tab, act, ori);          //Render inicial

            //Bucle principal del juego
            while (!nivel && !exit)
            {
                char c = ' ';

                while (c == ' ') { c = leeInput(); }    //Simplemente no actuamos hasta que el jugador haga algo
                ProcesaInput(c, ref tab, ref act, ref ori);
                Render(tab, act, ori);
                nivel = FinJuego(tab);
            }
            if (exit) { return false; }
            else { return true; }
        }

        #endregion

    }
}
