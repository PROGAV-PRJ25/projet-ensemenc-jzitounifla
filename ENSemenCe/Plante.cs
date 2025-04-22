using System.ComponentModel;

public abstract class Plante
{
  public new string[] Design { get; set; }
  public new string[] Etat { get; set; } //à voir, ce sera sans doute une méthode 
  public new string Type { get; set; } //-> une rose, une marguerite ou jsp

  public new Parcelle Parcelle { get; set; } //parcelle où elle est plantée
  public new int[] NecessaireConstruction { get; set; } //à voir, parce que déjà utilisé à ce stade. 
  public new int[] ResultatDemontage { get; set; }
  public new int NombreFruits { get; set; } //slmt si arbre fruitier
  public Plante()
  {

  }

  //pour les différents types de plantes, faire diff matrices qui changent les propriétés de chaque 
  //string pour le nom de la plante, puis tableau pour nécessaire construction
  //tableau : [nb boulons, nb plaques fer, nb tiges fer, nb vis, noyau vapeur]
  public void Initialisation(string plante)
  {
    Dictionary<string, int[]> dicoPlantesConstruction = new Dictionary<string, int[]>
    {
      ["Margu-ee-rite"] = [0, 0, 0, 0, 1],
      ["Rob-ose"] = [10, 0, 1, 1, 1],
      ["PoMWier"] = [20, 2, 1, 3, 1],
      ["Poir1er"] = [25, 2, 1, 4, 1],
      ["LierR3"] = [10, 0, 3, 1, 1],
    };
  }

  /*
    public int Etat()
    {

    }
    */




}


/*
  
~○⦿○~
○⦿○○⦿
  |
  |
 ———


*/