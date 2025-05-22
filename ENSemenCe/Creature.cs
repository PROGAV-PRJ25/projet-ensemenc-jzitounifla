public class Creature
{
    public string Nom { get; set; }
    public int Niveau { get; set; }
    public int NombreActions { get; set; }//Nombre d actions restantes
    public int NombreDpt { get; set; }//Nombre de deplacemnt
    public string[] Design { get; set; }

    public Monde MondeCreature { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Creature(Monde monde, string nom, int niveau, int nombreActions, int nombreDpt, string[] design)
    {
        Nom = nom;
        Niveau = niveau;
        NombreActions = nombreActions;
        NombreDpt = nombreDpt;
        Design = design;
        MondeCreature = monde;
    }

    public void PositionnerSurBord()//PLACE LA CREATURE SUR UN BORD DE MANIERE ALEATOIRE
    {
        Random rnd = new();
        int bord = rnd.Next(4); // 0=haut, 1=bas, 2=gauche, 3=droite
        int dimX = MondeCreature.XTailleGrille;
        int dimY = MondeCreature.YTailleGrille;
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
    public void Manger()//SUPPRIME LA PLANTE SUR LA CASE DE LA CREATURE
    {
        MondeCreature.ListParcelle[MondeCreature.ParcelleSlectionnee].MatricePlantes[X, Y] = null;
    }
    public void DeplacerAleatoirement()//DEPLACE LA CREATURE DE MANIERE ALEATOIRE
    {
        Random rnd = new();

        // Directions possibles : haut, bas, gauche, droite
        List<(int dx, int dy)> directions = new List<(int, int)>
    {
        (0, -1), // haut
        (0, 1),  // bas
        (-1, 0), // gauche
        (1, 0)   // droite
    };

        // Mélanger les directions pour choisir une direction valide au hasard
        directions = directions.OrderBy(_ => rnd.Next()).ToList();

        int dimX = MondeCreature.XTailleGrille;
        int dimY = MondeCreature.YTailleGrille;

        foreach (var (dx, dy) in directions)
        {
            int newX = X + dx;
            int newY = Y + dy;

            // Vérifie si la nouvelle position est dans les limites du plateau
            if (newX >= 0 && newX < dimX && newY >= 0 && newY < dimY)
            {
                X = newX;
                Y = newY;
                break;
            }
        }
    }
    public void TourCreature()//DEPLACE LA CREATURE ET SUPPRIME LA PLANTE EN FONCTION DU NOMBRE DE DEPLACEMENT
    {
        for (int i = 0; i < NombreDpt; i++)
        {
            DeplacerAleatoirement();
            Manger();
            MondeCreature.Affichage();
            Thread.Sleep(Constantes.TempsDpt);
        }
        NombreActions -= 1;
    }
}

// Niveau 1
public class CreatureNiveau1 : Creature
{
    public CreatureNiveau1(Monde monde, string nom)
        : base(monde, nom, 1, 2, 5, Graphique.CreaturesDesign[1])
    {
    }
}

// Niveau 2
public class CreatureNiveau2 : Creature
{
    public CreatureNiveau2(Monde monde, string nom)
        : base(monde, nom, 2, 3, 10, Graphique.CreaturesDesign[2])
    {
    }
}

// Niveau 3
public class CreatureNiveau3 : Creature
{
    public CreatureNiveau3(Monde monde, string nom)
        : base(monde, nom, 3, 4, 15, Graphique.CreaturesDesign[3])
    {
    }
}

// Niveau 4
public class CreatureNiveau4 : Creature
{
    public CreatureNiveau4(Monde monde, string nom)
        : base(monde, nom, 4, 5, 20, Graphique.CreaturesDesign[4])
    {
    }
}

//fee
public class Fee : Creature
{
    public int Bonus { get; set; }
    public Fee(Monde monde, string nom, int bonus)
        : base(monde, nom, 1, 2, 5, Graphique.FeeDesign)
    {
    }
}
