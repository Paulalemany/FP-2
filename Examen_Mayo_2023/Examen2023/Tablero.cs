using System;
using System.IO;
using Listas;

// https://www.kongregate.com/games/ejbarreto/puzlogic
// Eduardo Barreto

namespace puzlogic {
    class Tablero {

        // -1 Casilla muerta
        // 0 hueco vacío
        // [1...9] valor
        int[,] tab;     // matriz de números

        bool [,] fijas; // matriz de posiciones fijas
        Lista pend;  // lista de dígitos pendientes
        int fil,col; // posición del cursor (fila y columna)


        // métodos...
        public Tablero(int[,] tb, int[] pd)
        {
            // Inicializa las matrices con el tamaño tb
            tab = tb;
            fijas = new bool[tb.GetLength(0),tb.GetLength(1)];
            pend = new Lista();

            //fijas si es vacio = true, otro false
            for (int i = 0; i < tb.GetLength(0); i++)
            {
                for (int j = 0; j < tb.GetLength(1); j++)
                {
                    tab[i,j] = tb[i,j];
                    //La casilla es fija
                    if (tb[i, j] == -1 || tb[i, j] > 0)
                    {
                        fijas[i, j] = true;
                    }
                    else { fijas[i, j] = false; }
                }
            }

            //Llenamos con pd pend
            for (int i = 0; i < pd.Length; i++)
            {
                pend.InsertaPpio(pd[i]);
            }

            //Cursor en 0,0
            fil = col = 0;
        }

        public void Render()
        {
            Console.Clear();

            

            for (int i = 0;i < tab.GetLength(0);i++)
            {
                for(int j = 0;j < tab.GetLength(1);j++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    if (tab[i,j] == 0)
                    {
                        Console.Write("·" + " ");
                    }
                    else if (tab[i,j] == -1)
                    {
                        Console.Write(" " + " ");
                    }
                    else
                    {
                        if (!fijas[i,j]) Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(tab[i, j] + " ");
                    }
                    
                }

                Console.Write('\n');
            }
            Console.Write('\n');

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("pends: " + pend.ToString());

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('\n');

            Console.WriteLine(col.ToString() + fil.ToString());

            Console.SetCursorPosition(col * 2, fil);

            

        }

        public void MueveCursor(char c)
        {
            if ( col > 0 && c == 'l') { col--; }                       //'l' izquierda
            else if (col < tab.GetLength(0) - 1 && c == 'r') { col++; }    //'r' derecha
            else if (fil > 0 && c == 'u') { fil--; }                   //'u' arriba
            else if (fil < tab.GetLength(1) - 1 && c == 'd') { fil++; }    //'d' abajo
        }

        private bool NumViable(int num)
        {
            bool posible = false;

            if (!fijas[fil, col] && !Contenida(num)) { posible = true; }

            return posible;
        }

        //Método auxiliar para comprobar que el número se puede poner
        private bool Contenida(int num)
        {
            bool contenida = false;

            //recorremos la fila y columna del sitio
            int i = 0;
            int j = 0;

            while (i < tab.GetLength(0) && tab[i, col] != num) i++;
            while (j < tab.GetLength(1) && tab[fil, j] != num) j++;

            if (i != tab.GetLength(0) || j != tab.GetLength(1)) contenida = true;

            return contenida;
        }

        public bool PonNumero(int num)
        {
            bool pon = false;

            if (NumViable(num) && pend.BuscaDato(num)) 
            {
                pon = true; 
                pend.EliminaElto(num);
                tab[fil, col] = num;
            }

            return pon;
        }

        public bool QuitaNumero()
        {
            bool quitar = false;
            if (tab[fil,col] > 0 && !fijas[fil, col])
            {
                pend.InsertaPpio(tab[fil, col]);
                tab[fil, col] = 0;
                quitar = true;
            }

            return quitar;
        }

        private bool FinJuego()
        {
            bool fin = true;

            //Queremos comprobar si todas las casillas libres tienen número
            //Recorremos toda la matriz
            for (int i = 0; i < fijas.GetLength(0); i++)
            {
                for (int j = 0; j < fijas.GetLength(1); j++)
                {
                    //Si la casilla no es fija y no tiene número el juego no termina
                    if (!fijas[i,j] && tab[i,j] == 0) fin = false;
                }
            }

            return fin;
        }
    }
}        
        
