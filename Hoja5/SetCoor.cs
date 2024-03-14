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

  public bool Add(Coor c){ return true; } // añadir elto al conjunto
  
  public bool Remove(Coor c){ return true; }  // eliminar elto del cto

  public Coor PopElem(){ Coor c = new Coor(); return c ; }   // extracción de un elto (cualquiera) del cto

  public int Size(){ return 0; }  // numero de eltos del conjunto

  public bool IsElementOf(Coor c){ return true; } // pertenencia 
 
  public override string ToString(){ return " "; } // conversión a string
   
}