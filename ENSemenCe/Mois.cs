using System;
using System.Collections.Generic;

public class MoisClimatique
{
    public string Nom { get; set; }
    public double TauxPrecipitation { get; set; } // en mm
    public double TemperatureMoyenne { get; set; } // en °C

    public MoisClimatique(string nom, double tauxPrecipitation, double temperatureMoyenne)
    {
        Nom = nom;
        TauxPrecipitation = tauxPrecipitation;
        TemperatureMoyenne = temperatureMoyenne;
    }

    public override string ToString()
    {
        return $"{Nom} - Précipitations : {TauxPrecipitation} mm, Température : {TemperatureMoyenne} °C";
    }
}

public static class DonneesClimatiques
{
    public static readonly List<MoisClimatique> TousLesMois = new List<MoisClimatique>
    {
        new MoisClimatique("Janvier", 78.2, 3.5),
        new MoisClimatique("Février", 52.4, 4.1),
        new MoisClimatique("Mars", 60.3, 7.8),
        new MoisClimatique("Avril", 55.0, 11.2),
        new MoisClimatique("Mai", 68.9, 15.4),
        new MoisClimatique("Juin", 75.3, 19.6),
        new MoisClimatique("Juillet", 48.7, 22.1),
        new MoisClimatique("Août", 51.9, 21.7),
        new MoisClimatique("Septembre", 60.0, 17.8),
        new MoisClimatique("Octobre", 85.2, 13.0),
        new MoisClimatique("Novembre", 95.6, 7.6),
        new MoisClimatique("Décembre", 102.3, 4.2),
    };
}
