using System.ComponentModel.Design;
using Coordinates;
using SetArray;

namespace Plantilla;

class Program
{
    static string [] ops = {"Quit", "Add", "Remove", "PopElem", "IsElementOf", "Empty", "Size"};
    static void Main(string[] args){
        SetCoor s = new SetCoor(6);
        s.Add(new Coor(3,4)); s.Add(new Coor(1,2)); s.Add(new Coor(2,5));
        bool cont=true;
        while (cont) {
            int op = Menu(ops);
            if (op==0) cont=false;
            else Test(op,s);
        }
        
    }

    static void Test(int op, SetCoor s){
        if (ops[op]=="Add") {
            Console.Write("Elem "); 
            Coor c = Coor.Parse(Console.ReadLine());
            try { s.Add(c); }
            catch (Exception e) {Console.WriteLine(e.Message);}
        } else if (ops[op]=="Remove") {
            Console.Write("Elem "); 
            Coor c = Coor.Parse(Console.ReadLine());
            if (s.Remove(c)) Console.Write("Deleted");
            else Console.Write("Not found"); 
        } else if (ops[op]=="PopElem") { 
            try { 
                Coor c = s.PopElem(); 
                Console.WriteLine($"Extracted {c}");
            } catch (Exception e) {Console.WriteLine(e.Message);}
        } else if (ops[op]=="IsElementOf") {            
            Console.Write("Elem "); 
            Coor c = Coor.Parse(Console.ReadLine());
            Console.WriteLine(s.IsElementOf(c));
        } else if (ops[op]=="Empty") {    
            Console.WriteLine(s.Empty());
        } else if (ops[op]=="Size") {
            Console.WriteLine(s.Size());
        }    
        Console.WriteLine($"Set: {s}");
    }                

    static int Menu(string [] ops){                
        Console.WriteLine("\nOption: ");
        int op;
        do {
            for (int i=0; i<ops.Length; i++) 
                Console.WriteLine($"{i}. {ops[i]}");
            op = int.Parse(Console.ReadLine());
        } while (op<0 || op>ops.Length);
        return op;
    }

}