//Paula Alemany Rodríguez

namespace Práctica_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tablero tab;
            int lap = 200;  //Retardo para bucle principal
            int lapFantasmas = 3000;    //Retardo de los fantasmas
            int level = 0;

            LevelMenu(out tab, ref level);
            tab.Render();

            bool gameOver = false;
            bool win = false;
            bool exit = false;

            char c = ' ';

            while (!exit)
            {
                //Juego
                while (!gameOver && !win)    //Poner condición del juego
                {
                    //Input del usuario
                    LeeInput(ref c);

                    //Procesamiento del input
                    if (c != ' ' && tab.CambiaDir(c)) c = ' ';
                    tab.MuevePacman();

                    //IA de los fantasmas
                    tab.MueveFantasmas(lapFantasmas);
                    gameOver = tab.Captura();              //comprobar colisiones

                    //Renderizado
                    tab.Render();

                    //retardo
                    System.Threading.Thread.Sleep(lap);

                    //Celda de los fantasmas
                    lapFantasmas -= lap;

                    //Comprobamos si gana el pacman
                    win = tab.FinNivel();
                }
                //resultado de la partida
                FinPartida(win);

                //Continuación de la partida
                LevelMenu(out tab, ref level);
                win = false;
                gameOver = false;
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

        static void FinPartida(bool win)
        {
            Console.Clear();
            if (win)
            {
                Console.WriteLine("FELICIDADES POR TU VICTORIA :D");
            }
            else { Console.WriteLine("F Ya lo conseguirás a la próxima :("); }
        }

        static void LevelMenu(out Tablero tab, ref int l)
        {
            Console.WriteLine("Cargando nivel {0}", l);
            string level = l.ToString();

            //Añadimos el 0 si es necesario
            if (level.Length < 2) level = "0" + level;

            string file = "levels/level" + level + ".dat";

            tab = new Tablero(file);
            l++;

            //retardo
            System.Threading.Thread.Sleep(2000);
        }

    }
}
