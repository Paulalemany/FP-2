//Paula Alemany Rodríguez

using System;
using System.IO;


namespace puzlogic
{
    class Program
    {
        

        static void Main()
        {
            #region Ejemplo
            // ejemplo del enunciado
            // tablero    
            //int[,] tabEj = new int[5, 5]
            //      {{ 0,-1, 0,-1, 5},
            //       {-1, 3,-1, 0,-1},
            //       { 6,-1,-1,-1, 0},
            //       {-1, 0,-1, 6,-1},
            //       { 5,-1, 4,-1, 0}};
            //// pendientes
            //int[] pendEj = new int[6] { 4, 5, 6, 4, 5, 6 };

            #endregion

            int[,] tabEj;
            int[] pendEj;

            LeeNivel("ex.txt", out tabEj, out pendEj);

            Tablero tab = new Tablero(tabEj, pendEj);
            tab.Render();

            while (true)
            {
                char c = ' ';
                while (c == ' ') { c = LeeInput(); }

                ProcesaInput(tab, c);
                tab.Render();
            }

        }


        static char LeeInput()
        {
            char d = ' ';
            if (Console.KeyAvailable)
            {
                string tecla = Console.ReadKey(true).Key.ToString();
                switch (tecla)
                {
                    case "LeftArrow": d = 'l'; break;
                    case "UpArrow": d = 'u'; break;
                    case "RightArrow": d = 'r'; break;
                    case "DownArrow": d = 'd'; break;
                    case "Spacebar":                    // borrar num
                    case "S": d = 's'; break;
                    case "Escape":                      // salir
                    case "Q": d = 'q'; break;
                    case "P": d = 'p'; break;
                    // lectura de dígito
                    default:
                        if (tecla.Length == 2 && tecla[0] == 'D' && tecla[1] >= '0' && tecla[1] <= '9') d = tecla[1];
                        else d = ' ';
                        break;
                }
                while (Console.KeyAvailable)
                    Console.ReadKey().Key.ToString();
            }
            return d;
        }

        static void ProcesaInput(Tablero tab, char c)
        {
            //Movimiento del cursor
            if (c == 'l' || c == 'r' || c == 'u' || c == 'd') { tab.MueveCursor(c); }
            else if (c == 's') { tab.QuitaNumero(); }
            else if (c == '1' || c == '2' || c == '3' || c == '4'
                || c == '5' || c == '6' || c == '7' || c == '8' || c == '9') 
            {
                int val = (int)(c - '0');
                tab.PonNumero(val); 
            }
        }

        static void LeeNivel(string file, out int[,] tb, out int[] pd)
        {
            StreamReader game = new StreamReader(file);

            if (game == null) throw new Exception("Archivo no encontrado");

            string[] digs = game.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int columnas = int.Parse(digs[0]);
            int filas = int.Parse(digs[1]);

            tb = new int[columnas,filas];

            //Rellenamos tb
            for (int i = 0; i < filas; i++)
            {
                //Leemos la siguiente linea
                digs = game.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                //Recorremos el array de la linea leida
                for (int j = 0; j < digs.Length; j++)
                {
                    tb[i,j] = int.Parse(digs[j]);
                }
            }

            //Leemos pends
            digs = game.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            pd = new int[digs.Length];

            for (int i = 0;i < digs.Length; i++)
            {
                pd[i] = int.Parse(digs[i]);
            }


            game.Close();
        }

    }
}
