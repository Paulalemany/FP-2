using System;

namespace Listas{
	// listas enlazadas de ENTEROS (fácilmente adaptable a cualquier otro tipo)
	class Lista{

		// CLASE NODO (clase privada para los nodos de la lista)
		private class Nodo{
			public int dato;   // información del nodo (podría ser de cualquier tipo)
			public Nodo sig;   // referencia al siguiente

			// la constructora por defecto sería:
			// public Nodo() {} // por defecto

			// constructora para nodos: rellena elto y enlaza siguiente
			public Nodo(int _dato=0, Nodo _sig=null) {  // valores por defecto dato=0; y sig=null
				dato = _dato;
				sig = _sig;
			}
		}
		// FIN CLASE NODO

		// CAMPOS Y MÉTODOS DE LA CLASE Lista
		// campo pri: referencia al primer nodo de la lista
		Nodo pri;  


		// constructora de la clase Lista
		public Lista(){  
			pri = null;   //  lista vacia
		}


		// insertar elto e al ppio de la lista
		public void InsertaPpio(int e){  
			// Nodo aux = new Nodo(e,pri);
			// pri = aux;
			pri = new Nodo(e,pri);
		}


        // buscar elto e
        // public bool BuscaDato(int e){

        // devuelve el num de eltos de la lista
        // public int NumElems(){

        // hace el sumatorio de los eltos de la lista
        //public int Suma(){

        // cuenta el núm de veces que aparece e en la lista
        // public int CuentaOc(int e){

        // devuelve el n-ésimo nodo de la lista, si existe;
        // laza excepton en otro caso
        // public int Nesimo(int n){ 

		//Comprobamos si la lista está ordenada de menor a mayor (izq a der)
        public bool Ordenada()
		{
			bool ordenada = true;

			//Hay que haya elementos en la lista
			//Si no hay elementos o hay un solo elemento la lista está ordenada
			if (pri != null) {

                Nodo nodo = pri;

                //Mientras el dato actual sea menor que el siguiente seguimos avanzando en la lista
                while (nodo.sig != null && nodo.dato <= nodo.sig.dato)
                {
                    nodo = nodo.sig;
                }

				//Si se ha salido sin llegar al final significa que la lista no está ordenada
                if (nodo.sig != null) ordenada = false;
            }

			
			return ordenada;
		}


        // Conversion a string
        public override string ToString() { 
			string salida = "";						
			Nodo aux = pri;
			while (aux!=null) {
				salida += aux.dato + " ";
				aux = aux.sig;
			}
			salida += "\n";
			return salida;
        }

	}

}

