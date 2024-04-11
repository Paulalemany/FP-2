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
		public bool BuscaDato(int e){
			/*
			Nodo aux=pri;
			bool enc=false;
			while (aux!=null && !enc){
				if (aux.dato==e) enc=true;
				else aux=aux.sig;
			}
			return enc;
			*/

			Nodo aux=pri;
			while (aux!=null && aux.dato!=e) aux=aux.sig;
			return aux!=null;
		}

		// devuelve el num de eltos de la lista
		// public int NumElems(){

		// hace el sumatorio de los eltos de la lista
		//public int Suma(){

		// cuenta el núm de veces que aparece e en la lista
		public int CuentaOc(int e) 
		{
			int c = 0;
            //Recorremos la lista contando los datos de e
            Nodo aux = pri;
			while (aux != null)
			{
				if (aux.dato == e) c++;
				aux = aux.sig;
			}
            return c;
		}

		// devuelve el n-ésimo nodo de la lista, si existe;
		// laza excepton en otro caso
		public int Nesimo(int n){ 
			if (n<0) throw new Exception("Error Listas: n-esimo con negativo");
			Nodo aux = pri;
			while (aux!=null && n>0) {
				aux = aux.sig;
				n--;
			}
			if (aux==null) throw new Exception("Error Listas: n-esimo no existe");
			else return aux.dato;
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

