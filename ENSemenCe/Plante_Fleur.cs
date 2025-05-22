public static class DonneesFleurs
{
    public static readonly Dictionary<string, FleurInfo> Infos = new()
    {
        { "Margu-ee-rite", new FleurInfo("Margu-ee-rite", "graines lumineuses", 2) },
        { "Rob-ose",     new FleurInfo("Rob-ose", "pétales d'acier", 3) }
    };
}

public abstract class Fleur : Plante
{
    public int NombreFleur { get; set; }
    public string TypeFleur { get; set; }

    public Fleur(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        NombreFleur = 0;
    }
}
public class Marguerite : Fleur
{
    public Marguerite(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "Margu-ee-rite";
        TypeFleur = "graines lumineuses";
        EspacementNecessaire = Convert.ToInt16(DonneesPlantes.PlantesRessources[TypePlante]["Espace"]);
        DureeVie = [0, Constantes.PlantesDureeVie[TypePlante]]; //en 0 le temps de vie vécu (ici 0 mois), en 1 le total (plus facile à accéder par la suite)
        InitialiserRessources();
    }
}

public class Robose : Fleur
{
    public Robose(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "Rob-ose";
        TypeFleur = "pétales d'acier";
        EspacementNecessaire = Convert.ToInt16(DonneesPlantes.PlantesRessources[TypePlante]["Espace"]);
        DureeVie = [0, Constantes.PlantesDureeVie[TypePlante]]; //en 0 le temps de vie vécu (ici 0 mois), en 1 le total (plus facile à accéder par la suite)
        InitialiserRessources();
    }
}

public class FleurInfo
{
    public string Nom { get; }
    public string TypeFleur { get; }
    public int NombreFleur { get; }
    public int EspacementNecessaire { get; }

    public FleurInfo(string nom, string typeFleur, int nombreFleur)
    {
        Nom = nom;
        TypeFleur = typeFleur;
        NombreFleur = nombreFleur;
        EspacementNecessaire = Convert.ToInt16(DonneesPlantes.PlantesRessources[nom]["Espace"]);
    }
}