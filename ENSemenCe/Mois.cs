using System;
using System.Collections.Generic;

public class MoisClimatique
{
    public string Nom { get; set; }
    public double TauxPrecipitation { get; set; } // en mm
    public double TemperatureMoyenne { get; set; } // en °C
    public double ElectrisationMoyenne { get; set; } // en V
    public double Ensoleillement { get; set; } // coefficient

    public MoisClimatique(string nom, double tauxPrecipitation, double temperatureMoyenne, double electrisation, double ensoleillement)
    {
        Nom = nom;
        TauxPrecipitation = tauxPrecipitation;
        TemperatureMoyenne = temperatureMoyenne;
        ElectrisationMoyenne = electrisation;
        Ensoleillement = ensoleillement;
    }

    public override string ToString()
    {
        return $"{Nom} || Précipitations : {TauxPrecipitation} mm d'huile — Température : {TemperatureMoyenne} °C — Electrisation de l'air : {ElectrisationMoyenne} — Ensoleillement : {Ensoleillement}";
    }
}

public static class DonneesClimatiques
{
    public static readonly List<MoisClimatique> TousLesMois = new List<MoisClimatique> //données de base, pour climat tempéré. 
    {
        //FORMAT : nom, précipitations, température, electrisation, ensoleillement. Oui, il fait très chaud.
        new MoisClimatique("Janvier", 78.2, 43.5, 15, 0.7),
        new MoisClimatique("Février", 52.4, 44.1, 17, 0.6),
        new MoisClimatique("Mars", 60.3, 47.8, 17, 0.6),
        new MoisClimatique("Avril", 55.0, 51.2, 17, 0.8),
        new MoisClimatique("Mai", 68.9, 55.4, 16, 0.9),
        new MoisClimatique("Juin", 75.3, 59.6, 14, 1),
        new MoisClimatique("Juillet", 48.7, 62.1, 12, 1.2),
        new MoisClimatique("Août", 51.9, 61.7, 12, 1.2),
        new MoisClimatique("Septembre", 60.0, 57.8, 13, 1.1),
        new MoisClimatique("Octobre", 85.2, 53.0, 13, 0.9),
        new MoisClimatique("Novembre", 95.6, 47.6, 14, 0.8),
        new MoisClimatique("Décembre", 102.3, 44.2, 15, 0.8),
    };
}
