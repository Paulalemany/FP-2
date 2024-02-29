using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoja3_1
{
    public class Complejo
    {
        //Atributos
        private int real;
        private int img;

        //Constructora
        public Complejo() { real = 0; img = 0; }
        public Complejo(int r, int i)
        {
            real = r; img = i;
        }

        //Operadores + - *, leer, escribir, ==, !=, getters y setters
        public static Complejo operator +(Complejo num1, Complejo num2)
        {
            Complejo num = new Complejo();

            num.real = num1.real + num2.real;
            num.img = num1.img + num2.img;
            return num;
        }

        public static Complejo operator -(Complejo num1, Complejo num2)
        {
            Complejo num = new Complejo();

            num.real = num1.real - num2.real;
            num.img = num1.img - num2.img;
            return num;
        }

        public static Complejo operator *(Complejo num1, Complejo num2)
        {
            Complejo num = new Complejo();

            //Multiplicamos como si fuese por un paréntesis
            
            num.real = (num1.real * num2.real) + -1 *(num1.img * num2.img);
            num.img = (num1.real * num2.img) + (num1.img * num2.real);
            return num;
        }


        public static bool operator ==(Complejo num1, Complejo num2)
        {
            return (num1.real == num2.real) && (num1.img == num2.img);
        }

        public static bool operator !=(Complejo num1, Complejo num2)
        {
            return (num1.real != num2.real) || (num1.img != num2.img);
        }

        public void WriteComplejo()
        {
            //Diferenciamos por si el num img es negativo o positivo
            if(this.img >= 0)
            {
                Console.Write("{0} + {1}i",this.real, this.img);
            }
            else Console.Write("{0} {1}i",this.real, this.img);

        }

        public void ReadComplejo()
        {

            Console.WriteLine("Escriba la parte real del número ");
            this.SetterRe(int.Parse(Console.ReadLine()));

            Console.WriteLine("Escriba la parte imaginaria del número ");
            this.SetterImg(int.Parse(Console.ReadLine()));

        }

        public int GetterRe() { return this.real; }
        public void SetterRe(int r) { this.real = r; }  //Tal taz habría que pasarle el complejo no estoy segura

        public int GetterI() { return this.img; }
        public void SetterImg(int i) { this.img = i; } //Tal taz habría que pasarle el complejo no estoy segura
    }
}
