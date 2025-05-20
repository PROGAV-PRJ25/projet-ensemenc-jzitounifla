public abstract class ArbreFruit : Plante
{
    public new int NombreFruits { get; set; } //slmt si arbre fruitier

    public ArbreFruit(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        NombreFruits = 0;
    }

}
// Margu-ee-rite
public class Marguerite : Plante
{
    public Marguerite(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "Margu-ee-rite";
    }
}

// Rob-ose
public class Robose : Plante
{
    public Robose(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "Rob-ose";
    }
}

// LierR3
public class Lierre : Plante
{
    public Lierre(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "LierR3";
    }
}

// PoMWier (déjà défini)
public class PoMWier : ArbreFruit
{
    public PoMWier(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "PoMWier";
    }
}

// Po1rier
public class Poirier : ArbreFruit
{
    public Poirier(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        TypePlante = "Po1rier";
    }
}
