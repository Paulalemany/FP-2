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

        Tablero(int[,] tb, int[] pd)
        {
            // Inicializa las matrices con el tamaño tb
            tab = new int[,];
            

            //Inicializa tab con tb
            //fijas si es vacio = true, otro false

            //Llenamos con pd pend

            //Cursor en 0,0
        }

    }
}        
        
