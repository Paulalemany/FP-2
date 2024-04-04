using Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            break;
                        case 3:
                            cas[j, i] = Casilla.Vitamina;
                            break;
                        case 4:
                            cas[j, i] = Casilla.MuroCelda;
                            break;
                        //personajes
                        case 5:
                            cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                            pers[c].pos = new Coor(j, i);
                            pers[c].dir = new Coor(1, 0);
                            break;
                        case 6:
                            cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                            pers[c].pos = new Coor(j, i);
                            pers[c].dir = new Coor(1, 0);
                            break;
                        case 7:
                            cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                            pers[c].pos = new Coor(j, i);
                            pers[c].dir = new Coor(1, 0);
                            break;
                        case 8:
                            cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                            pers[c].pos = new Coor(j, i);
                            pers[c].dir = new Coor(1, 0);
                            break;
                        case 9:
                            cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                            pers[c].pos = new Coor(j, i);
                            pers[c].dir = new Coor(0, 1);
                            break;
                    }
                    
                }
            }
            s.Close();
            lapFantasmas = lapCarcelFantasmas;
            if (DEBUG) { rnd = new Random(100); }
            else rnd = new Random();
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
            if (DEBUG) { Console.WriteLine("Columnas: {0}, Filas: {1}", c, f); }

            stream.Close();
        }

    }
}
