using Coordinates;
namespace SetArray;

class SetCoor{

  // atributos de la clase   
  Coor [] coors; // array con coordenadas
  int oc;        // núm de componentes ocupadas del array = primera pos libre

  public SetCoor(int tam=10) // constructora
  {
        coors = new Coor[tam];      //Creamos el array con el tamaño correspondiente 
        oc = 0;
  }   

  // Privado. Busca elto en el array y devuelve su posición (-1 si no está)
  private int SearchElem(Coor c)
  {
        int i = 0;
        while (i < oc && coors[i] != c) { i++; }

        if (i == oc) { i = -1; }        //Si la coordenada no está en el array se devuelve -1
        return i; 
  }

  public bool Add(Coor c) // añadir elto al conjunto
  {
        bool add = true;
        if (SearchElem(c) != -1) { add = false; }   //Si la coordenada está no se hace nada
        else
        {
            //Comprobamos si hay espacio en el array
            if (oc == coors.Length) { /*Excepción*/ }
            else //Si cabe lo añadimos al array
            {
                coors[oc] = c;
                oc++;
            }
        }

        return add; 
  } 
  
  public bool Remove(Coor c) // eliminar elto del cto
  { 
        bool remove = true;
        int id = SearchElem(c); //Buscamos el elemento
        if (id == -1) {  remove = false; }  //Si el elemento no está devolvemos false
        else
        {
            for (int i = id; i < oc; i++)       //Eliminamos la coordenada
            {
                coors[i] = coors[i + 1];
            }
            oc--;
        }
        return remove; 
  }  

  public Coor PopElem() // extracción de un elto (cualquiera) del cto
  { 
        //El de menor coste es el último al tener que desplazar menos elementos?
        Coor c = new Coor(); return c ; 
  }   

  public int Size(){ return 0; }  // numero de eltos del conjunto

  public bool IsElementOf(Coor c){ return true; } // pertenencia 
 
  public override string ToString(){ return " "; } // conversión a string
   
}