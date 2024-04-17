//Paula Alemany Rodríguez

using Coordinates;

namespace Práctica_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tablero tab = new Tablero("levels/level00.dat");
            tab.Render();

            int lap = 200;  //Retardo para bucle principal
            int lapFantasmas = 3000;

            char c = ' ';

            while (true)    //Poner condición del juego
            {
                //Input del usuario
                LeeInput(ref c);

                //Procesamiento del input
                if (c != ' ' && tab.CambiaDir(c)) c = ' ';
                tab.MuevePacman();

                //IA de los fantasmas
                tab.MueveFantasmas(lapFantasmas);

                //Renderizado
                tab.Render();

                //retardo
                System.Threading.Thread.Sleep(lap);

                //Celda de los fantasmas
                lapFantasmas -= lap;
            }
        }

        static void LeeInput(ref char dir)
        {
            if (Console.KeyAvailable)
            {
                string tecla = Console.ReadKey(true).Key.ToString();
                switch (tecla)
                {
                    case "LeftArrow": dir = 'l'; break;
                    case "UpArrow": dir = 'u'; break;
                    case "RightArrow": dir = 'r'; break;
                    case "DownArrow": dir = 'd'; break;
                }
            }
            while (Console.KeyAvailable) Console.ReadKey().Key.ToString();
        }

    }
}
