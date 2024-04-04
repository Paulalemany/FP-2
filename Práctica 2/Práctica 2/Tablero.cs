﻿using Coordinates;
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
                            pers[1].pos = new Coor(j, i);
                            pers[1].dir = new Coor(1, 0);
                            break;
                        case 6:
                            cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                            pers[2].pos = new Coor(j, i);
                            pers[2].dir = new Coor(1, 0);
                            break;
                        case 7:
                            cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                            pers[3].pos = new Coor(j, i);
                            pers[3].dir = new Coor(1, 0);
                            break;
                        case 8:
                            cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                            pers[4].pos = new Coor(j, i);
                            pers[4].dir = new Coor(1, 0);
                            break;
                        case 9: //pacman
                            cas[j, i] = Casilla.Libre;  ///Se podría cambiar también por comida
                            pers[0].pos = new Coor(j, i);
                            pers[0].dir = new Coor(0, 1);
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

            stream.Close();
        }
        #endregion

        #region Render
        public void Render()
        {
            TableroRender();
            PersonajesRender();

            Console.SetCursorPosition(0, cas.GetLength(1) * 2);
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
                            Console.BackgroundColor = ConsoleColor.White;
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
            if (pers[0].dir == new Coor(1, 0)) Console.Write(">>");
            else if (pers[0].dir == new Coor(0, 1)) Console.Write("VV");
            else if (pers[0].dir == new Coor(-1, 0)) Console.Write("<<");
            else if (pers[0].dir == new Coor(0, -1)) Console.Write("∧∧");


            for (int i = 1; i < pers.Length; i++)
            {
                Console.SetCursorPosition(pers[i].pos.X * 2, pers[i].pos.Y);

                Console.BackgroundColor = colors[i];
                Console.Write("ºº");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        #endregion


    }
}
