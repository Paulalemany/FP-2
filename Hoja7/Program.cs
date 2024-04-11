using System;
using System.Collections.Generic;
using Listas;

namespace Main
{
	class MainClass
	{
		static string [] ops = {"Quit", "InsertaPpio", "BuscaElto", "nEsimo", "CuentaOc"};
		public static void Main (string[] args)
		{			
			Lista l = new Lista();
			l.InsertaPpio(4);
			l.InsertaPpio(7);
			l.InsertaPpio(2);
			bool cont=true;
        	while (cont) {
        	    int op = Menu(ops);
        	    if (op==0) cont=false;
        	    else Test(op,l);
        	}
		}
        

	    static void Test(int op, Lista l){
	        if (ops[op]=="InsertaPpio") {
	            Console.Write("Elem? "); 
	            int e = int.Parse(Console.ReadLine());
				l.InsertaPpio(e);
			} else if (ops[op]=="BuscaElto") {
				Console.Write("Elem? "); 
				int e = int.Parse(Console.ReadLine());
				if (l.BuscaDato(e)) Console.WriteLine("Esta");
				else Console.WriteLine("No esta");
			} else if (ops[op]=="nEsimo"){
				Console.Write("Pos? "); 
				int pos = int.Parse(Console.ReadLine());
				try {
					int e = l.Nesimo(pos);
					Console.Write($"elem en {pos}: {e}"); 
				} catch (Exception e) {
					Console.WriteLine(e.Message); 
				}
			} else if (ops[op] == "CuentaOc"){
                Console.Write("Elem?: ");
                int e = int.Parse(Console.ReadLine());
				Console.WriteLine("Ocurrencias " + l.CuentaOc(e));
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
