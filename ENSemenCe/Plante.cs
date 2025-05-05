using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

public abstract class Plante
{
  public static Dictionary<string, int[]> dicoPlantesConstruction = new Dictionary<string, int[]>
  {
    ["Margu-ee-rite"] = [0, 0, 0, 0, 1],
    ["Rob-ose"] = [10, 0, 1, 1, 1],
    ["PoMWier"] = [20, 2, 1, 3, 1],
    ["Poir1er"] = [25, 2, 1, 4, 1],
    ["LierR3"] = [10, 0, 3, 1, 1],
  };
  public new string[] Design { get; set; } //Apparence de la plante dans le terminal
  public string Type { get; set; } //-> une rose, une marguerite ou jsp
  public Parcelle Parcelle { get; set; } //parcelle où elle est plantée
  public new int[] NecessaireConstruction { get; set; } //à voir, parce que déjà utilisé à ce stade. 
  public new int[] ResultatDemontage { get; set; } //Ce qu'on obtient en démontant la plante. 

  public int[] Coord { get; set; } //les coordonnées de la plante, avec [x, y].
  public int EspacementNecessaire { get; set; } //l'espacement nécessaire au bien-être de la plante 
  public Plante(Parcelle parcelle, int x, int y)
  {
    Design = ["", ""]; //euh pour l'instant y a rien 
    Type = "Margh-ee-rite"; //marguerite par défaut, mais pas de raison que soit appelé. 
    Parcelle = parcelle;
    NecessaireConstruction = dicoPlantesConstruction[Type];
    ResultatDemontage = NecessaireConstruction; //changer pour le multiplier par 3 par exemple -> for int... 
    Coord = [x, y];
    EspacementNecessaire = 0;
  }

  //pour les différents types de plantes, faire diff matrices qui changent les propriétés de chaque 
  //string pour le nom de la plante, puis tableau pour nécessaire construction
  //tableau : [nb boulons, nb plaques fer, nb tiges fer, nb vis, noyau vapeur]



  public int CalcEtat() //Calculer l'état de la plante 
  {
    int a = 1;
    return a;
  }




}


/*
  
~○⦿○~
○⦿○○⦿
  |
  |
 ———


*/