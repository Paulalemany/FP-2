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

            LevelMenu(out tab, level);
            tab.Render();

            bool gameOver = false;
            bool win = false;
            bool exit = false;

            char c = ' ';

            while (!exit)   //Lógica para cargar el siguiente nivel
            {
                //Juego
                while (!gameOver && !win && !exit)    //Poner condición del juego
                {
                    //Input del usuario
                    LeeInput(ref c);

                    //Procesamiento del input
                    if (c != ' ')
                    {
                        if (c == 'p')
                        {
                            //Menú de pausa
                            PauseMenu(tab, out exit);

                        }
                        else
                        {
                            tab.CambiaDir(c);
                        }

                        c = ' ';
                    }
                    
                    tab.MuevePacman();
                    gameOver = tab.Captura();              //comprobar colisiones

                    //IA de los fantasmas
                    tab.MueveFantasmas(lapFantasmas);
                    if (!gameOver) gameOver = tab.Captura();              //comprobar colisiones

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
                FinPartida(win, gameOver, ref level);

                //Poner un menú para salir, guardar etc...

                //Continuación de la partida
                win = false;
                gameOver = false;
                exit = false;
                LevelMenu(out tab, level);
               
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
                    case "P": dir = 'p'; break;
                }
            }
            while (Console.KeyAvailable) Console.ReadKey().Key.ToString();
        }

        static void FinPartida(bool win, bool gameOver, ref int l)
        {
            Console.Clear();
            if (win)
            {
                Console.WriteLine("FELICIDADES POR TU VICTORIA :D");
                l++;    //para que pase al siguiente nivel solo si gana
            }
            else if (gameOver){ Console.WriteLine("F Ya lo conseguirás a la próxima :("); }
            else Console.WriteLine("Gracias por jugar");
        }

        static void LevelMenu(out Tablero tab, int l)
        {
            Console.WriteLine("Cargar partida [0] Cargar nivel {0} [1]", l);
            int op = int.Parse(Console.ReadLine());
            while (op != 0 && op != 1)
            {
                Console.WriteLine("Opción no válida, introduzca una opción: ");
                op = int.Parse(Console.ReadLine());
            }

            if (op == 1)
            {
                Console.WriteLine("Cargando nivel {0}", l);
                string level = l.ToString();

                //Añadimos el 0 si es necesario
                if (level.Length < 2) level = "0" + level;

                string file = "levels/level" + level + ".dat";

                tab = new Tablero(file);
            }
            else
            {
                Console.WriteLine("Cargando partida");

                tab = new Tablero("partida.txt");
            }
            
            //retardo
            System.Threading.Thread.Sleep(2000);
        }

        static void PauseMenu(Tablero tab, out bool exit) //En todas estas cosas habría que poner excepciones por si el usuario pone algoo no valido
        {
            Console.Clear();

            Console.WriteLine("Continuar [0], Guardar y salir [1]");

            int e = int.Parse(Console.ReadLine());

            if (e == 0) { exit = false; } //Simplemente continuamos la partida
            else
            {
                //Sistema para guardar la partida
                tab.GuardarPartida();
                exit = true;
                
            }


        }

    }
}
