using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

public abstract class Plante
{

  public string[] Design { get; set; } //Apparence de la plante dans le terminal
  public string Type { get; set; } //-> une rose, une marguerite ou jsp
  public Parcelle Parcelle { get; set; } //parcelle où elle est plantée
  public int[] Coord { get; set; } //les coordonnées de la plante, avec [x, y].
  public int EspacementNecessaire { get; set; } //l'espacement nécessaire au bien-être de la plante 
  public int[] DureeVie { get; set; } //D'un côté la durée de vie totale, de l'autre ce qui est déjà passé EN MOIS
  public Plante(Parcelle parcelle, int x, int y)
  {
    Design = ["", ""]; //euh pour l'instant y a rien 
    Type = "Margh-ee-rite"; //marguerite par défaut, mais pas de raison que soit appelé. 
    Parcelle = parcelle;
    Coord = [x, y];
    EspacementNecessaire = 0;
    DureeVie = [0, Constantes.PlantesDureeVie[Type]]; //en 0 le temps de vie vécu (ici 0 mois), en 1 le total (plus facile à accéder par la suite)
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