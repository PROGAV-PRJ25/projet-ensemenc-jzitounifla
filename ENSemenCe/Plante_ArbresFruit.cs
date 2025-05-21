public abstract class ArbreFruit : Plante
{
    public int NombreFruits { get; set; }
    public string typeFruits { get; set; }
    public int FrequenceProduction { get; set; } // Nouvel attribut

    public ArbreFruit(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        NombreFruits = 0;
        typeFruits = "";
        FrequenceProduction = 0;
    }
}

// Margu-ee-rite
public class Marguerite : ArbreFruit
{
    public Marguerite(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "Margu-ee-rite";
        typeFruits = "graines lumineuses";
        NombreFruits = 3;
        FrequenceProduction = 2; // par exemple tous les 2 cycles
    }
}

// Rob-ose
public class Robose : ArbreFruit
{
    public Robose(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "Rob-ose";
        typeFruits = "pétales d'acier";
        NombreFruits = 2;
        FrequenceProduction = 3;
    }
}

// LierR3
public class Lierre : ArbreFruit
{
    public Lierre(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "LierR3";
        typeFruits = "gousses grimpantes";
        NombreFruits = 4;
        FrequenceProduction = 2;
    }
}

// PoMWier
public class PoMWier : ArbreFruit
{
    public PoMWier(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "PoMWier";
        typeFruits = "pomwies";
        NombreFruits = 5;
        FrequenceProduction = 1;
    }
}

// Po1rier
public class Poirier : ArbreFruit
{
    public Poirier(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "Po1rier";
        typeFruits = "poires hybrides";
        NombreFruits = 4;
        FrequenceProduction = 2;
    }
}
