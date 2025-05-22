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
  public static readonly List<string> TousTypesPlantes_Fleurs = new() { "Margu-ee-rite", "Rob-ose" };
  public static readonly List<string> TousTypesPlantes_ArbreFruit = new() { "LierR3", "PoMWier", "Po1rier" };
  public static readonly List<List<string>> TousTypesPlantes = new List<List<string>> { TousTypesPlantes_Fleurs, TousTypesPlantes_ArbreFruit };
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

  public int AffichageTour { get; set; } //combien de case afficher
  public int Etat { get; set; }//sante de la plante
  public bool Malade { get; set; }
  Random random = new Random();



  //CONSTRUCTEUR
  public Plante(Parcelle parcelle, int x, int y) //les trucs non gérés ici sont gérés individuellement dans la sous-classe (on a besoin d'autres infos)
  {
    Malade = false;
    Parcelle = parcelle;
    Coord = [x, y];
    AffichageTour = 1;
    Etat = 3;
  }

  public void InitialiserRessources()
  {
    NiveauHuile = DonneesPlantes.PlantesRessources[TypePlante]["huile"];
    NiveauElectricite = DonneesPlantes.PlantesRessources[TypePlante]["electricite"];
    NiveauUV = DonneesPlantes.PlantesRessources[TypePlante]["UV"];
  }

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
    AffichageTour += 1;

    //Decider si la plante est malade ce mois-ci
    if (Constantes.ProbaPlanteMalade < random.NextDouble())
    { //si la plante est tiree au sort...
      Malade = true;
      Console.WriteLine("ATTENTION !! Votre plante " + TypePlante + " est malade !! Guerissez-la vite car cela diminue de moitié son état...");
    }
    Etat = CalculerEtatGeneral(mois, parcelle);
  }

  public int[] VerifierEtatSpecifique(string ressource)
  {
    int niveau = 1;
    double detail;

    double valeurActuelle;
    double valeurIdeale = DonneesPlantes.PlantesRessources[TypePlante][ressource];

    switch (ressource)
    {
      case "huile":
        valeurActuelle = NiveauHuile;
        break;
      case "electricite":
        valeurActuelle = NiveauElectricite;
        break;
      case "UV":
        valeurActuelle = NiveauUV;
        break;
      default:
        Console.WriteLine("Ressource invalide.");
        return [0, 0];
    }

    if (valeurActuelle > valeurIdeale * 0.8 && valeurActuelle < valeurIdeale * 1.2)
    {
      niveau = (valeurActuelle > valeurIdeale * 0.9 && valeurActuelle < valeurIdeale * 1.1) ? 3 : 2;
    }

    detail = valeurActuelle / valeurIdeale * 100;
    return [niveau, Convert.ToInt32(detail)];
  }


  public int CalculerEtatGeneral(int mois, Parcelle parcelle) //Calculer l'état de la plante selon la météo du mois et la parcelle où elle est plantée
  /*
  Renvoie un entier : 
  3 => en très bonne santé
  2 => en bonne santé
  1 => en état dangereux 
  0 => Morte. 
  */
  {
    // État en fonction de la durée de vie : fragilité de la plante augmente plus elle est jeune ou plus elle est vieille. 
    double fragilite = 0;
    if (DureeVie[1] / 3.0 > DureeVie[0]) // premier tiers
    {
      fragilite = DureeVie[0] / (DureeVie[1] / 3.0);
    }
    if (DureeVie[1] - (DureeVie[1] / 3.0) < DureeVie[0]) // dernier tiers
    {
      fragilite = 1 - (DureeVie[0] / (DureeVie[1] / 3.0));
    }

    // État des différents critères
    int etatHuile = VerifierEtatSpecifique("huile")[0];
    int etatElectrisation = VerifierEtatSpecifique("electricite")[0];
    int etatUV = VerifierEtatSpecifique("UV")[0];

    double moyenne = (etatHuile + etatElectrisation + etatUV) / 3.0;
    double poidsFragilite = 0.5 * fragilite + 0.5;

    double resultat = (3 + poidsFragilite * moyenne) / (1 + poidsFragilite);


    if (Malade) { resultat /= 2; } //Si la plante est malade elle est en moitié bon état


    int etatGeneral = (int)Math.Round(resultat);
    // Forcer le résultat à être entre 0 et 3
    return Math.Max(0, Math.Min(3, etatGeneral));
  }

  public int PreparerArrosagePlante() //CHOISIR NOMBRE DE LITRES À METTRE À PLANTE -> que pour arrosage spécifique d'une plante. 
  {
    int[] etatHuile = VerifierEtatSpecifique("huile");
    int litresAchetes;
    Console.WriteLine($"Etat de la plante avant arrosage : niveau d'huile à {etatHuile[1]}% de sa valeur idéale, ce qui correspond à une note de {etatHuile[0]} sur 3.");
    if (etatHuile[1] < 100)
    {
      Console.WriteLine($"Il manque {DonneesPlantes.PlantesRessources[TypePlante]["huile"] - NiveauHuile} pour que la plante retrouve son état optimal.");
    }
    else
    {
      Console.WriteLine("Cette plante robotique est parfaitement huilée !! Voulez-vous tout de même l'arroser ?");
      Console.Write("Entrez 'oui' ou 'non' : ");
      string reponse = Console.ReadLine()!;
      while ((reponse != "oui") && (reponse != "non"))
      {
        Console.Write("Entrez 'oui' ou 'non' : ");
        reponse = Console.ReadLine()!;
      }
      if (reponse == "non") return 0; //VOIR POUR FAIRE UN RETURN 0 ??? 
    }
    Console.WriteLine($"Prix de l'huile : {Constantes.CoutBoulons["litreHuile"]} boulons par litre. Combien de litres voulez vous acheter ? (Précisez la quantité en L, ou 0 pour annuler l'achat, puis pressez 'Entrée')");
    Console.Write("> Quantité souhaitée : ");
    litresAchetes = Convert.ToInt16(Console.ReadLine()!);
    return litresAchetes;
  }
  public void ArroserPlante(double litres, int mois, Parcelle parcelle)
  {
    NiveauHuile += litres;
    CalculerEtatGeneral(mois, parcelle);
  }

}



/*
  
~○⦿○~
○⦿○○⦿
  |
  |
 ———


*/