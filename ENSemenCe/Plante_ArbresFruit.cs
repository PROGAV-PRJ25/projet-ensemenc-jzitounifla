public abstract class ArbreFruit : Plante
{
    public new int NombreFruits { get; set; } //slmt si arbre fruitier

    public ArbreFruit(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {
        NombreFruits = 0;
    }

}
public class PoMWier : ArbreFruit
{
    public PoMWier(Parcelle parcelle, int x, int y) : base(parcelle, x, y)
    {; }

}