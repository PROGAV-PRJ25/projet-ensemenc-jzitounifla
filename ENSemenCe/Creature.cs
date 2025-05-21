public class Creature
{
    // Attributs (propriétés)
    public string Nom { get; set; }
    public int Niveau { get; set; }
    public int NombreActions { get; set; }
    public int NombreDpt { get; set; }

    public string[] Design { get; set; }

    // Constructeur par défaut
    public Creature(string nom, int niveau, int nombreActions, int nombreDpt, string[] design)
    {
        Nom = nom;
        Niveau = niveau;
        NombreActions = nombreActions;
        NombreDpt = nombreDpt;
        Design = design;
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
