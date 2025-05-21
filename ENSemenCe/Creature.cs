public class Creature
{
    public string Nom { get; set; }
    public int Niveau { get; set; }
    public int NombreActions { get; set; }
    public int NombreDpt { get; set; }
    public string[] Design { get; set; }

    public int X { get; set; }
    public int Y { get; set; }

    public Creature(string nom, int niveau, int nombreActions, int nombreDpt, string[] design)
    {
        Nom = nom;
        Niveau = niveau;
        NombreActions = nombreActions;
        NombreDpt = nombreDpt;
        Design = design;
    }

    public void PositionnerSurBord(int dimX, int dimY)
    {
        Random rnd = new();
        int bord = rnd.Next(4); // 0=haut, 1=bas, 2=gauche, 3=droite

        switch (bord)
        {
            case 0: // haut
                X = rnd.Next(dimX);
                Y = 0;
                break;
            case 1: // bas
                X = rnd.Next(dimX);
                Y = dimY - 1;
                break;
            case 2: // gauche
                X = 0;
                Y = rnd.Next(dimY);
                break;
            case 3: // droite
                X = dimX - 1;
                Y = rnd.Next(dimY);
                break;
        }
    }
}

// Niveau 1
public class CreatureNiveau1 : Creature
{
    public CreatureNiveau1(string nom)
        : base(nom, 1, 2, 5, Graphique.CreaturesDesign[1])
    {
    }
}

// Niveau 2
public class CreatureNiveau2 : Creature
{
    public CreatureNiveau2(string nom)
        : base(nom, 2, 3, 10, Graphique.CreaturesDesign[2])
    {
    }
}

// Niveau 3
public class CreatureNiveau3 : Creature
{
    public CreatureNiveau3(string nom)
        : base(nom, 3, 4, 15, Graphique.CreaturesDesign[3])
    {
    }
}

// Niveau 4
public class CreatureNiveau4 : Creature
{
    public CreatureNiveau4(string nom)
        : base(nom, 4, 5, 20, Graphique.CreaturesDesign[4])
    {
    }
}
