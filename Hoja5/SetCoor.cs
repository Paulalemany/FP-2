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
        bool Add = true;
        if (SearchElem(c) != -1) { Add = false; }   //Si la coordenada está no se hace nada
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

        return Add; 
  } 
  
  public bool Remove(Coor c){ return true; }  // eliminar elto del cto

  public Coor PopElem(){ Coor c = new Coor(); return c ; }   // extracción de un elto (cualquiera) del cto

  public int Size(){ return 0; }  // numero de eltos del conjunto

  public bool IsElementOf(Coor c){ return true; } // pertenencia 
 
  public override string ToString(){ return " "; } // conversión a string
   
}