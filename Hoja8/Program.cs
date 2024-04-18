using System;
using System.Collections.Generic;
using Listas;

namespace Main
{
	class MainClass
	{
		static string [] ops = {"Quit", "InsertaPpio", "Ordenada"};
		public static void Main (string[] args)
		{			
			Lista l = new Lista();
			bool cont=true;
        	while (cont) {
        	    int op = Menu(ops);
        	    if (op==0) cont=false;
        	    else if (op == 1 ) Test(op,l);
				else Orden(op,l);
        	}
		}
        

	    static void Test(int op, Lista l){
	        if (ops[op]=="InsertaPpio") {
	            Console.Write("Elem? "); 
	            int e = int.Parse(Console.ReadLine());
				l.InsertaPpio(e);
			}
	        Console.WriteLine($"Lista: {l}");
	    }    
		
		static void Orden(int op, Lista l)
		{
            if (ops[op] == "Ordenada")
            {
                Console.WriteLine(l.Ordenada());
            }
            Console.WriteLine($"Lista: {l}");
        }

	    static int Menu(string [] ops){                
	        Console.WriteLine("\nOption: ");
	        int op;
	        do {
	            for (int i=0; i<ops.Length; i++) 
	                Console.WriteLine($"{i}. {ops[i]}");
				Console.WriteLine();
	            op = int.Parse(Console.ReadLine());
	        } while (op<0 || op>ops.Length);
	        return op;
	    }
	}

}
