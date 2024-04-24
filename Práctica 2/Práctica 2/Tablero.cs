using FP2P2;
using SetArray;
using System.Xml.Linq;

namespace Práctica_2
{
    class Tablero
    {
        #region Casillas

        // contenido de las casillas
        enum Casilla { Libre, Muro, Comida, Vitamina, MuroCelda };

        // matriz de casillas (tablero)
        Casilla[,] cas;
        #endregion

        #region Personajes

        // representacion de los personajes (pacman y fantasmas)
        struct Personaje
        {
            public Coor pos, dir, // posicion y direccion actual
            ini; // posicion inicial (para fantasmas)
        }

        // vector de personajes, 0 es pacman y el resto fantasmas
        Personaje[] pers;
        #endregion

        #region Colores
        // colores para los personajes
        ConsoleColor[] colors = {ConsoleColor.DarkYellow, ConsoleColor.Red,
            ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkBlue };
        #endregion

        #region Parámetros

        const int lapCarcelFantasmas = 3000; // retardo para quitar el muro a los fantasmas

        int lapFantasmas; // tiempo restante para quitar el muro

        int numComida; // numero de casillas restantes con comida o vitamina

        Random rnd; // generador de aleatorios
        #endregion

        private bool DEBUG = true;  // flag para mensajes de depuracion en consola

        #region tablero
        public Tablero(string file)    //Constructora
        {
            pers = new Personaje[5];    //Array de personajes
            int columnas, filas;
            filasColumnas(file, out filas, out columnas);

            cas = new Casilla[columnas, filas];     //Inicializamos las casillas del tablero con el tamaño correspondiente

            //Volvemos a leer el tablero guardando la información en cada casilla
            StreamReader s = new StreamReader(file);
            for (int i = 0; i < filas; i++)
            {
                string linea = s.ReadLine();
                string[] largo = linea.Split(' ');
                for (int j = 0; j < columnas; j++)
                {
                    if (largo[j] == "") { }
                    else
                    {
                        int c = int.Parse(largo[j]);

                        switch (c)
                        {
                            //mapa
                            case 0:
                                cas[j, i] = Casilla.Libre;
                                break;
                            case 1:
                                cas[j, i] = Casilla.Muro;
                                break;
                            case 2:
                                cas[j, i] = Casilla.Comida;
                                numComida++;
                                break;
                            case 3:
                                cas[j, i] = Casilla.Vitamina;
                                numComida++;
                                break;
                            case 4:
                                cas[j, i] = Casilla.MuroCelda;
                                break;

                            //personajes
                            case 5:
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[1].pos = new Coor(j, i);
                                pers[1].ini = pers[1].pos;
                                pers[1].dir = new Coor(1, 0);
                                break;
                            case 6:
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[2].pos = new Coor(j, i);
                                pers[2].dir = new Coor(1, 0);
                                pers[2].ini = pers[2].pos;
                                break;
                            case 7:
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[3].pos = new Coor(j, i);
                                pers[3].ini = pers[3].pos;
                                pers[3].dir = new Coor(1, 0);
                                break;
                            case 8:
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[4].pos = new Coor(j, i);
                                pers[4].ini = pers[4].pos;
                                pers[4].dir = new Coor(1, 0);
                                break;
                            case 9: //pacman
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[0].pos = new Coor(j, i);
                                pers[0].ini = pers[0].pos;
                                pers[0].dir = new Coor(0, 1);
                                break;
                        }
                    }
                    

                    if (file == "partida.txt")
                    {
                        //La carga de los personajes es diferente
                        string personajes = s.ReadLine();
                        string[] data = personajes.Split(' ');

                        //El primer dato es el personaje que es
                        switch (data[0])
                        {
                            case "5":
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[1].pos = new Coor(int.Parse(data[1]), int.Parse(data[2]));
                                pers[1].ini = new Coor(int.Parse(data[3]), int.Parse(data[4]));
                                pers[1].dir = new Coor(int.Parse(data[5]), int.Parse(data[6]));
                                break;
                            case "6":
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[2].pos = new Coor(int.Parse(data[1]), int.Parse(data[2]));
                                pers[2].dir = new Coor(int.Parse(data[3]), int.Parse(data[4]));
                                pers[2].ini = new Coor(int.Parse(data[5]), int.Parse(data[6]));
                                break;
                            case "7":
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[3].pos = new Coor(int.Parse(data[1]), int.Parse(data[2]));
                                pers[3].ini = new Coor(int.Parse(data[3]), int.Parse(data[4]));
                                pers[3].dir = new Coor(int.Parse(data[5]), int.Parse(data[6]));
                                break;
                            case "8":
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[4].pos = new Coor(int.Parse(data[1]), int.Parse(data[2]));
                                pers[4].ini = new Coor(int.Parse(data[3]), int.Parse(data[4]));
                                pers[4].dir = new Coor(int.Parse(data[3]), int.Parse(data[4]));
                                break;
                            case "9": //pacman
                                cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                                pers[0].pos = new Coor(int.Parse(data[1]), int.Parse(data[2]));
                                pers[0].dir = new Coor(int.Parse(data[3]), int.Parse(data[4]));
                                break;
                        }

                    }

                }
            }
            s.Close();
            lapFantasmas = lapCarcelFantasmas;
            if (DEBUG) { rnd = new Random(100); }
            else rnd = new Random();
        }

        //Me falta una fila entera por algún motivo que desconozco
        public void GuardarPartida()
        {
            StreamWriter s = new StreamWriter("partida.txt");

            int k;
            for (int i = 0; i < cas.GetLength(1); i++)  //Columnas
            {
                for (int j = 0; j < cas.GetLength(0); j++)  //filas
                {
                    //Guardamos cada dato
                    switch(cas[i,j])
                    {
                        string num = " ";

                        switch (k)
                        {
                            case 0:
                                num = "9 ";
                                break;
                            case (1):
                                num = "5 ";
                                break;
                            case (2):
                                num = "6 ";
                                break;
                            case (3):
                                num = "7 ";
                                break;
                            case (4):
                                num = "8 ";
                                break;

                        }
                        s.Write(num);
                    }
                    else
                    {
                       //Guardamos la casilla
                       switch (cas[j, i])
                       {
                        //Estados de la casilla
                        case Casilla.Libre:
                            s.Write("0 ");
                            break;
                        case Casilla.Muro:
                            s.Write("1 ");
                            break;
                        case Casilla.Comida:
                            s.Write("2 ");
                            break;
                        case Casilla.Vitamina:
                            s.Write("3 ");
                            break;
                        case Casilla.MuroCelda:
                            s.Write("4 ");
                            break;

                       }
                    }

                }
                s.Write('\n');
            }

            //Guardamos los personajes en líneas aparte para guardar todos los datos
            for (int i = 0; i < pers.Length; i++)
            {
                switch (i)
                {
                    //Pacman
                    case 0:
                        s.Write("9 ");
                        break;

                    case 1:
                        s.Write("5 ");
                        break;

                    case 2:
                        s.Write("6 ");
                        break;

                    case 3:
                        s.Write("7 ");
                        break;
                    case 4:
                        s.Write("8 ");
                        break;

                }

                s.Write(pers[i].pos.ToString());
                s.Write(pers[i].ini.ToString());
                s.Write(pers[i].dir.ToString());

                s.Write('\n');
            }

            s.Close();
        }

        //Método para ver si en la posición hay un personaje
        bool PersonajeCasilla(int i, int j, out int k)
        {
            k = 0;
            while (k < pers.Length && (j != pers[k].pos.X || i != pers[k].pos.Y)) k++;

            if (k == pers.Length)
            {
                //No ha encontrado ningún personaje en la psoición
                return false;
            }
            else return true;
        }

        //Método auxiliar para contar número de filas y de columnas
        void filasColumnas(string file, out int f, out int c)
        {
            StreamReader stream = new StreamReader(file);   ///Lanzar excepción si no se encuentra el archivo

            //Tenemos que determinar el número de filas y de columnas para poder inicializar cas

            //(Para saber el número de columnas podemos hacer un split y para el número de filas que lea saltando de línea hasta que sea nulo)
            string line = stream.ReadLine();  //Lee la primera línea del archivo
            string[] largo = line.Split(' ');

            c = largo.Length;
            f = 0;

            //Ahora contamos el número de líneas
            while (!stream.EndOfStream)
            {
                if (line.Length != stream.ReadLine().Length) Console.WriteLine("Error en la línea {0}", f); ///Habría que saltar una excepción
                f++;
            }

            stream.Close();
        }
        #endregion

        #region Render
        public void Render()
        {
            TableroRender();
            PersonajesRender();

        }

        void TableroRender()
        {
            //Pintamos el mapa
            for (int i = 0; i < cas.GetLength(0); i++)
            {
                for (int j = 0; j < cas.GetLength(1); j++)
                {
                    Console.SetCursorPosition(i * 2, j);
                    switch (cas[i, j])
                    {
                        case Casilla.Libre:
                            Console.Write("  ");
                            break;
                        case Casilla.Muro:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write("  ");
                            break;
                        case Casilla.Comida:
                            Console.Write("··");
                            break;
                        case Casilla.Vitamina:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("**");
                            break;
                        case Casilla.MuroCelda:
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write("  ");
                            break;
                    }

                   Console.BackgroundColor= ConsoleColor.Black;
                   Console.ForegroundColor= ConsoleColor.White;
                }
            }
        }

        void PersonajesRender()
        {
            //El pacman va a parte
            Console.SetCursorPosition(pers[0].pos.X * 2, pers[0].pos.Y);
            Console.BackgroundColor = colors[0];

            //Dependiendo de hacia donde mira cambia el dibujito
            if (pers[0].dir.X == 1 && pers[0].dir.Y == 0) Console.Write(">>");
            else if (pers[0].dir.X == 0 && pers[0].dir.Y == 1) Console.Write("VV");
            else if (pers[0].dir.X == -1 && pers[0].dir.Y == 0) Console.Write("<<");
            else if (pers[0].dir.X == 0 && pers[0].dir.Y == -1) Console.Write("AA");
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 1; i < pers.Length; i++)
            {
                Console.SetCursorPosition(pers[i].pos.X * 2, pers[i].pos.Y);

                Console.BackgroundColor = colors[i];
                Console.Write("ºº");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        #endregion

        #region Movimiento Pacman
        bool Siguiente(Coor pos, Coor dir, out Coor newPos)
        {
            //Calcula la nueva posición
            newPos = pos + dir;

            //Se puede salir por un borde y aparecer por el opuesto (Si hay pasillo)
            //Comprobamos si está en un borde
            if (newPos.X >= cas.GetLength(0))            //Se conectan los extremos der/izq
            { newPos.X = 0; }
            else if (newPos.Y >= cas.GetLength(1))       //Se conectan los extremos down/up
            { newPos.Y = 0; }
            else if (newPos.X < 0)                      //Se conectan de izq/der
            { newPos.X = cas.GetLength(0) - 1; }
            else if (newPos.Y < 0)                      //Se conectan up down
            { newPos.Y = cas.GetLength(1) - 1; }

            //Devuelve true si newPos != muro
            return cas[newPos.X, newPos.Y] != Casilla.Muro; 
        }

        public void MuevePacman()
        {
            //Llamamos al método anterior para ver a donde se tiene que desplazar
            Coor newPos;

            if (Siguiente(pers[0].pos, pers[0].dir, out newPos)) pers[0].pos = newPos;

            if (cas[pers[0].pos.X, pers[0].pos.Y] == Casilla.Vitamina)
            {
                //Movemos todos los fantasmas a su inicio
                for (int i = 1; i < pers.Length; i++)
                {
                    pers[i].pos = pers[i].ini;
                }

                // Si es así devolvemos los fantasmas a su origen y cambiamos la casilla a libre
                cas[pers[0].pos.X, pers[0].pos.Y] = Casilla.Libre;
                numComida--;
            }
            //Comprobamos si está encima de una vitamina o comida
            else if (cas[pers[0].pos.X, pers[0].pos.Y] == Casilla.Comida)
            {
                //Si es así devolvemos los fantasmas a su origen y cambiamos la casilla a libre
                cas[pers[0].pos.X, pers[0].pos.Y] = Casilla.Libre;
                numComida--;
                
            }

        }

        //Sería el procesa input
        public bool CambiaDir(char c)
        {
            Coor newPos;
            Coor newDir = new Coor();
            bool cambia = false;

            if (c == 'l')
            {
                newDir.X = -1;        //izquierda
                newDir.Y = 0;
            }
            else if (c == 'r')
            {
                newDir.X = 1;         //derecha
                newDir.Y = 0;
            }
            else if (c == 'u')
            {
                newDir.X = 0;
                newDir.Y = -1;        //arriba 
            }
            else if (c == 'd')
            {
                newDir.X = 0;
                newDir.Y = 1;         //abajo
            }

            if (Siguiente(pers[0].pos, newDir, out newPos))
            {
                pers[0].dir = newDir;
                cambia = true;
            }

            return cambia;
        }


        #endregion

        #region Fantasmas

        bool HayFantasma(Coor c)
        {
            //Hacemos un bucle buscando si alguno de los fantasmas está en la pos c
            int i = 1;
            while (i < pers.Length && pers[i].pos != c) { i++; }

            //Si llega al final es que no hay ninguno
            if (i == pers.Length) return false;
            else return true;
        }

        int PosiblesDirs(int fant, out SetCoor cs)
        {
            //Creamos el conjunto de coordenadas vacío cs
            cs = new SetCoor();
            Coor c1 = new Coor(1,0);
            Coor c2 = new Coor(0,1);
            Coor c3 = new Coor(-1,0);
            Coor c4 = new Coor(0,-1);

            Coor newCoor = new Coor();
            //Realmente c1 y c2 son vectores directores no su posición
            //Lo que hay que añadir a cs son las direcciones no las posiciones
            if (Siguiente(pers[fant].pos, c1, out newCoor) && !HayFantasma(newCoor)) { cs.Add(c1); }
            if (Siguiente(pers[fant].pos, c2, out newCoor) && !HayFantasma(newCoor)) { cs.Add(c2); }
            if (Siguiente(pers[fant].pos, c3, out newCoor) && !HayFantasma(newCoor)) { cs.Add(c3); }
            if (Siguiente(pers[fant].pos, c4, out newCoor) && !HayFantasma(newCoor)) { cs.Add(c4); }

            //Evitamos que se de la vuelta
            if (cs.Size() > 1)
            {
                Coor opuesta = pers[fant].dir;  //Para no modificar el original
                //Buscamos la dirección opuesta de donde nos estamos moviendo
                opuesta.X *= -1;
                opuesta.Y *= -1;

                if (cs.IsElementOf(opuesta)) cs.Remove(opuesta);
            }
            else cs.Add(new Coor(0, 0));    //Por si se quedan sin ninguna direccion a la que moverse que no sea nulo

            return cs.Size();

        }

        void SeleccionaDir(int fant)
        {
            SetCoor cs;
            PosiblesDirs(fant, out cs);

            int index = rnd.Next(cs.Size());
            pers[fant].dir = cs.TakeElement(index);
        }

        void EliminaMuroFantasmas()
        {
            //Buscamos las casillas de muro celda
            for (int i = 0; i < cas.GetLength(0); i++)
            {
                for (int j = 0; j < cas.GetLength(1); j++)
                {
                    //Cuando encontramos el muro lo cambiamos
                    if (cas[i, j] == Casilla.MuroCelda) cas[i, j] = Casilla.Libre;  

                    ///Puede que esto sea poco eficiente ya que tiene que buscar en todo el array y no termina hasta el final
                }
            }
        }

        public void MueveFantasmas(int lap)
        {
            if (lap <= 0)
            {
                EliminaMuroFantasmas();
                //Empieza en 1 porque nos saltamos al pacman
                for (int i = 1; i < pers.Length; i++)
                {
                    Coor newCoor = new Coor();
                    SeleccionaDir(i);
                    //Si se puede desplazar en esa dirección lo movemos
                    if (Siguiente(pers[i].pos, pers[i].dir, out newCoor)) pers[i].pos = newCoor;
                }
            }
            
        }

        #endregion

        #region Final Nivel

        public bool Captura()
        {
            bool captura  = true;

            int i = 1;
            while (i < pers.Length && pers[i].pos != pers[0].pos) { i++; }

            //Si llega al final significa que no coincide con ninguno
            if (i == pers.Length) captura = false;

            return captura;
        }

        public bool FinNivel()
        {
            //Si la comida es menor o igual a 0 significa que ha ganado el jugador
            return numComida < 0;
        }
        #endregion


    }
}
