using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
public static class DonneesPlantes //TOUTES LES DONNÉES POUR GÉRER LES PLANTES
{
  public static readonly Dictionary<string, Dictionary<string, double>> PlantesRessources = new Dictionary<string, Dictionary<string, double>>
{
  {"Margu-ee-rite", new Dictionary<string, double> {
    {"huile", 150 },
    {"temp", 55},
    {"electricite", 24 },
    {"UV", 1.3 },
    {"Espace", 0}
  }},

  { "Rob-ose", new Dictionary<string, double> {
    {"huile", 300 },
    {"temp", 62},
    {"electricite", 30 },
    {"UV", 1.8 },
    {"Espace", 1}
      }},

  { "LierR3", new Dictionary<string, double> {
    {"huile", 210 },
    {"temp", 60},
    {"electricite", 18 },
    {"UV", 1.3 },
    {"Espace", 0}
      }},

  { "PoMWier", new Dictionary<string, double> {
    {"huile", 180 },
    {"temp", 55},
    {"electricite", 26 },
    {"UV", 1.3 },
    {"Espace", 2}
      }},

  { "Po1rier",new Dictionary<string, double> {
    {"huile", 240 },
    {"temp", 55},
    {"electricite", 26 },
    {"UV", 1.5 },
    {"Espace", 2}
      }}
};
}

public abstract class Plante
{

  //PROPRIÉTÉS COMMUNES À TOUTES LES PLANTES
  public string[] Design { get; set; } //Apparence de la plante dans le terminal
  public string TypePlante { get; set; } //-> une rose, une marguerite ou jsp
  public Parcelle Parcelle { get; set; } //parcelle où elle est plantée
  public int[] Coord { get; set; } //les coordonnées de la plante, avec [x, y].
  public int EspacementNecessaire { get; set; } //l'espacement nécessaire au bien-être de la plante 
  public int[] DureeVie { get; set; } //[déjà passé ; totale] 

  public double NiveauHuile { get; set; } //plantes ont besoin d'être assez huilées
  public double NiveauElectricite { get; set; } //besoin d'assez d'électricité dans l'air pour fonctionner
  public double NiveauUV { get; set; } //besoin d'assez d'UV aussi (ensoleillement)


  //CONSTRUCTEUR
  public Plante(Parcelle parcelle, int x, int y)
  {
    Design = ["", ""]; //euh pour l'instant y a rien 
    TypePlante = "PoMWier"; //marguerite par défaut, mais pas de raison que soit appelé. 
    Parcelle = parcelle;
    Coord = [x, y];
    EspacementNecessaire = Convert.ToInt16(DonneesPlantes.PlantesRessources[TypePlante]["Espace"]);
    DureeVie = [0, Constantes.PlantesDureeVie[TypePlante]]; //en 0 le temps de vie vécu (ici 0 mois), en 1 le total (plus facile à accéder par la suite)
  }

  //pour les différents types de plantes, faire diff matrices qui changent les propriétés de chaque 
  //string pour le nom de la plante, puis tableau pour nécessaire construction
  //tableau : [nb boulons, nb plaques fer, nb tiges fer, nb vis, noyau vapeur]

  public void Pousser(int mois, Parcelle parcelle) // ce que la plante fait chaque mois, dépend dudit mois (conditions météo). SE FAIT AU DÉBUT DU MOIS
  {
    DureeVie[0]++; //on augmente le temps vécu par la plante

    //REÇOIT DES TRUCS PAR LA MÉTÉO

    /*Huile : on a les précipitations en mm 
        -> on divise par 1000 pour les avoir en m
        -> on divise par 2 parce que 1 plante occupe 1/2m^2
        -> on multiplie par 1000 pour le mettre en litres (unité utiisée ici). ON PEUT SIMPLIFIER LE (x/1000)*1000
        -> on l'ajoute au niveau d'huile existant*/
    NiveauHuile += DonneesClimatiques.TousLesMois[mois].TauxPrecipitation / 2; //on ajoute à la plante la qté d'huile tombée sur sa zone
    /*Electricité : on a l'électrisation ambiante. et c'est tout*/
    NiveauElectricite += DonneesClimatiques.TousLesMois[mois].ElectrisationMoyenne;
    NiveauUV += DonneesClimatiques.TousLesMois[mois].Ensoleillement;

    //UTILISE DES TRUCS 
    NiveauHuile -= DonneesPlantes.PlantesRessources[TypePlante]["huile"] / 100 * (DonneesClimatiques.TousLesMois[mois].TemperatureMoyenne * 0.85); //utilise de l'huile
    NiveauElectricite -= DonneesPlantes.PlantesRessources[TypePlante]["electricite"] / 3; //le niveau d'électricité se fait diviser par 3
    NiveauUV -= DonneesPlantes.PlantesRessources[TypePlante]["UV"] / 3;
  }


  public int CalculerEtat(int mois, Parcelle parcelle) //Calculer l'état de la plante selon la météo du mois et la parcelle où elle est plantée
  /*
  Renvoie un entier : 
  3 => en très bonne santé
  2 => en bonne santé
  1 => en état dangereux 
  0 => Morte. 
  */
  {
    //Etat en fonction de la duree de vie : fragilité de la plante augmente plus elle est jeune ou plus elle est vieille. 
    double fragilite = 0; //de base, vaut 0. 
    if (DureeVie[1] / 3 > DureeVie[0]) //si est dans le premier tiers de sa vie
    {
      fragilite = DureeVie[0] / (DureeVie[1] / 3);
    }
    if (DureeVie[1] - (DureeVie[1] / 3) < DureeVie[0]) //si est dans le dernier tiers de sa vie
    {
      fragilite = 1 - (DureeVie[0] / (DureeVie[1] / 3));
    }

    //ETAT EN FONCTION DE L'HUILE : doit être à ±10% pour très bien, ±20% pour bien, sinon mauvais
    int etatHuile = 1;
    //si on est à ±20% du niveau d'huile requis
    if (NiveauHuile > DonneesPlantes.PlantesRessources[TypePlante]["huile"] - (0.2 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]) && NiveauHuile < DonneesPlantes.PlantesRessources[TypePlante]["huile"] + (0.2 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]))
    {
      //si on est dans les 10% 
      if (NiveauHuile > DonneesPlantes.PlantesRessources[TypePlante]["huile"] - (0.1 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]) && NiveauHuile < DonneesPlantes.PlantesRessources[TypePlante]["huile"] + (0.1 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]))
        etatHuile = 3;
      else etatHuile = 2;
    }

    //Etat en fonction de l'ELECTRISATION
    int etatElectrisation = 1;
    //si on est à ±20% du niveau d'électricite requis
    if (NiveauElectricite > DonneesPlantes.PlantesRessources[TypePlante]["electricite"] - (0.2 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]) && NiveauElectricite < DonneesPlantes.PlantesRessources[TypePlante]["huile"] + (0.2 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]))
    {
      //si on est dans les 10% 
      if (NiveauElectricite > DonneesPlantes.PlantesRessources[TypePlante]["electricite"] - (0.1 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]) && NiveauElectricite < DonneesPlantes.PlantesRessources[TypePlante]["huile"] + (0.1 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]))
        etatElectrisation = 3;
      else etatElectrisation = 2;
    }

    //Etat en fonction du niveau UV
    int etatUV = 1;
    //si on est à ±50% du niveau d'UV requis
    if (NiveauUV > DonneesPlantes.PlantesRessources[TypePlante]["electricite"] - (0.5 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]) && NiveauUV < DonneesPlantes.PlantesRessources[TypePlante]["huile"] + (0.5 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]))
    {
      //si on est dans les 20% 
      if (NiveauUV > DonneesPlantes.PlantesRessources[TypePlante]["electricite"] - (0.2 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]) && NiveauUV < DonneesPlantes.PlantesRessources[TypePlante]["huile"] + (0.2 * DonneesPlantes.PlantesRessources[TypePlante]["huile"]))
        etatUV = 3;
      else etatUV = 2;
    }

    int etatGeneral = Convert.ToInt16((3 + ((0.5 * fragilite + 0.5) * ((etatHuile + etatElectrisation + etatUV) / 3))) / (1 + (0.5 * fragilite + 0.5)));

    return etatGeneral;
  }




}


/*
  
~○⦿○~
○⦿○○⦿
  |
  |
 ———


*/