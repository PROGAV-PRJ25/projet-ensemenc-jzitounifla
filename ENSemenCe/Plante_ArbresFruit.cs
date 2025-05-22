using System;
using System.Collections.Generic;

// Classe abstraite de base
public abstract class ArbreFruit : Plante
{
    public int NombreFruits { get; set; }
    public string TypeFruits { get; set; }
    public int FrequenceProduction { get; set; }
    public int ProductionTour { get; set; }
    public ArbreFruit(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        NombreFruits = 0;
        FrequenceProduction = 0;
        ProductionTour = 0;
    }
}

// Sous-classes concrètes


public class Lierre : ArbreFruit
{
    public Lierre(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "LierR3";
        TypeFruits = "gousses grimpantes";
        NombreFruits = 4;
        FrequenceProduction = 2;
        EspacementNecessaire = Convert.ToInt16(DonneesPlantes.PlantesRessources[TypePlante]["Espace"]);
        DureeVie = [0, Constantes.PlantesDureeVie[TypePlante]]; //en 0 le temps de vie vécu (ici 0 mois), en 1 le total (plus facile à accéder par la suite)
        InitialiserRessources();
    }
}

public class PoMWier : ArbreFruit
{
    public PoMWier(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "PoMWier";
        TypeFruits = "pomwies";
        NombreFruits = 5;
        FrequenceProduction = 1;
        EspacementNecessaire = Convert.ToInt16(DonneesPlantes.PlantesRessources[TypePlante]["Espace"]);
        DureeVie = [0, Constantes.PlantesDureeVie[TypePlante]]; //en 0 le temps de vie vécu (ici 0 mois), en 1 le total (plus facile à accéder par la suite)
        InitialiserRessources();
    }
}

public class Poirier : ArbreFruit
{
    public Poirier(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "Po1rier";
        TypeFruits = "poires hybrides";
        NombreFruits = 4;
        FrequenceProduction = 2;
        EspacementNecessaire = Convert.ToInt16(DonneesPlantes.PlantesRessources[TypePlante]["Espace"]);
        DureeVie = [0, Constantes.PlantesDureeVie[TypePlante]]; //en 0 le temps de vie vécu (ici 0 mois), en 1 le total (plus facile à accéder par la suite)
        InitialiserRessources();
    }
}

// Structure de données pour infos statiques
public class ArbreFruitInfo
{
    public string Nom { get; }
    public string TypeFruits { get; }
    public int NombreFruits { get; }
    public int FrequenceProduction { get; }
    public int EspacementNecessaire { get; }

    public ArbreFruitInfo(string nom, string typeFruits, int nombreFruits, int frequence)
    {
        Nom = nom;
        TypeFruits = typeFruits;
        NombreFruits = nombreFruits;
        FrequenceProduction = frequence;
        EspacementNecessaire = Convert.ToInt16(DonneesPlantes.PlantesRessources[nom]["Espace"]);
    }
}

// Dictionnaire global des types d'arbres fruitiers
public static class ArbreFruitRegistry
{
    public static readonly Dictionary<string, ArbreFruitInfo> Infos = new()
    {
        { "LierR3",     new ArbreFruitInfo("LierR3", "gousses grimpantes", 4, 2) },
        { "PoMWier",    new ArbreFruitInfo("PoMWier", "pomwies", 5, 1) },
        { "Po1rier",    new ArbreFruitInfo("Po1rier", "poires hybrides", 4, 2) },
    };
}
