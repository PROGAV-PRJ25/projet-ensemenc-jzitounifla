using System.Drawing;

public class Monde
{
    public string[] Mois { get; set; }
    public Monde()
    {

    }
    public static void TracerPatternLongueurN(string pattern, int n, ConsoleColor couleur = ConsoleColor.White)
    {
        Console.ForegroundColor = couleur;
        for (int i = 0; i < n / pattern.Length; i++)
        {
            Console.Write(pattern);
        }
        Console.Write(pattern.Substring(0, n % pattern.Length));
    }
    public void TracerCadre(string bordGauche, string bordDroit, int longueur)
    {
        ConsoleColor Cadre = ConsoleColor.White;
        Console.Write(bordGauche);
        TracerCloture("horizontal", Cadre, longueur);
        Console.Write(bordDroit);
    }
    public void TracerCloture(string direction, ConsoleColor couleur, int longueur = 0, int yGrilleStart = 0, int yConsole = 0)
    {
        Console.ForegroundColor = couleur;
        string clotureHorizontal = "-┼";
        string clotureVertical = "|┼";
        if (direction == "horizontal")
        {
            TracerPatternLongueurN(clotureHorizontal, longueur, couleur);
        }
        else
        {
            Console.Write(clotureVertical[(yConsole - yGrilleStart) % clotureVertical.Length]);
        }
    }
    public void TracerInterligneAroseur(string arroseur, int longueurInterval, int frequenceArroseur, int longueurTotal)
    {
        ConsoleColor Cadre = ConsoleColor.White;
        int nombreArroseur = longueurTotal / (longueurInterval * frequenceArroseur);
        if (nombreArroseur != 0)
        {
            for (int i = 0; i < nombreArroseur; i++)
            {
                for (int j = 0; j < frequenceArroseur - 1; j++)
                {
                    TracerPatternLongueurN(" ", longueurInterval);
                }
                TracerPatternLongueurN(" ", longueurInterval - 1);
                Console.Write(arroseur);
            }
        }
        TracerPatternLongueurN(" ", longueurTotal % (longueurInterval * frequenceArroseur));
    }
    public void ChangerCouleurEtat()
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    public static void SauterNLigne(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Console.Write("\n");
        }
    }
    public static void TracerTitreEncadre(string texte, ConsoleColor couleur)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        SauterNLigne(2);
        TracerPatternLongueurN("-", 100, couleur);
        SauterNLigne(2);
        //Titre
        TracerPatternLongueurN(" ", 30, couleur);
        for (int i = 0; i < texte.Length; i++)
        {
            TracerPatternLongueurN(" ", 3, couleur);
            Console.Write(texte[i]);
        }
        //2 eme ligne
        SauterNLigne(2);
        TracerPatternLongueurN("-", 100, couleur);
        SauterNLigne(2);
    }
    public void Affichage()
    {
        //parcelle 
        List<Plante[,]> Parcelle = new List<Plante[,]>
{
    new Plante[,] { { null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null },{ null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null } },
new Plante[,] { { null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null },{ null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null } },
new Plante[,] { { null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null },{ null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null } },
new Plante[,] { { null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null },{ null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null } },
new Plante[,] { { null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null },{ null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null } },
new Plante[,] { { null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null },{ null, null, null, null, null, null }, { null, null, null, null, null, null }, { null, null, null, null, null, null } },
};

        int ParcelleSlectionnee = 0;
        //console
        int YConsole = 30;
        int YGrilleStart = 3;
        int MargeGauche = 10;

        //taille case
        int YTailleCase = 2;
        int XTailleCase = 2 * YTailleCase;

        //intercase
        int YInterCase = 1;
        int XInterCase = 1;

        //taille grille
        int YTailleGrille = 6;
        int XTailleGrille = 6;
        //coordonnees absolue
        int YTailleGrilleCaractere = (YInterCase + YTailleCase) * YTailleGrille + 1;
        int XTailleGrilleCaractere = (XInterCase + XTailleCase) * XTailleGrille + 1;

        //parcours
        int yCase = 0;
        int yGrille = 0;
        int ligneArroseur = 0;

        //autre
        int frequenceArroseurX = 2;
        int frequenceArroseurY = 2;
        Console.Clear();
        TracerTitreEncadre("ENSemenCe", ConsoleColor.Green);
        for (int yConsole = 0; yConsole < YConsole; yConsole++)
        {
            //espace sur le cote
            TracerPatternLongueurN(" ", MargeGauche);
            //haut du cadre
            if (yConsole == YGrilleStart)
                TracerCadre("┌", "┐", XTailleGrilleCaractere - 2);
            //base du cadre
            else if (yConsole == YGrilleStart - 1 + YTailleGrilleCaractere)
                TracerCadre("└", "┘", XTailleGrilleCaractere - 2);
            //grille
            else if (YGrilleStart < yConsole && yConsole < YGrilleStart - 1 + YTailleGrilleCaractere)
            {
                TracerCloture("vertical", ConsoleColor.White, yGrilleStart: YGrilleStart, yConsole: yConsole);
                //interCase
                if ((yConsole - YGrilleStart) % (YTailleCase + YInterCase) == 0)
                {
                    ligneArroseur++;
                    if (ligneArroseur % frequenceArroseurY == 0)
                    {
                        TracerInterligneAroseur(arroseur: ".", longueurInterval: XTailleCase + 1, frequenceArroseur: frequenceArroseurX, longueurTotal: XTailleGrilleCaractere - 2);
                    }
                    else
                        TracerPatternLongueurN(" ", XTailleGrilleCaractere - 2);
                    yGrille++;
                }
                //case
                else
                {
                    ChangerCouleurEtat();
                    for (int xGrille = 0; xGrille < XTailleGrille; xGrille++)
                    {
                        if (Parcelle[ParcelleSlectionnee][xGrille, yGrille] == null)
                            TracerPatternLongueurN(" ", XTailleCase);
                        else
                            Console.Write(Parcelle[ParcelleSlectionnee][xGrille, yGrille].Design[yCase]);
                        //ne faire un espace après la derniere case
                        if (xGrille != XTailleGrille - 1)
                            Console.Write(" ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                TracerCloture("vertical", ConsoleColor.White, yGrilleStart: YGrilleStart, yConsole: yConsole);
                yCase = (yCase + 1) % (YTailleCase + YInterCase);
            }
            Console.WriteLine("");
            //menu1 sur le cote
        }
    }
}








/*

BIENVENUE AU POTAGER PRINCIPAL

_____________________    > PLANTES                  > PLANTE 1
_____________________    > ALLER AU MARCHÉ          > PLANTE 2
_____________________                               > ETC
_____________________
_____________________
_____________________
_____________________
_____________________
_____________________


BIENVENUE AU MARCHÉ

VENDRE                      ACHETER
> 




ACHETER DES PARCELLES -> ensoleillement, humidité, proba feu, proba innondation, proba invasion sauterelles ou jsp... 
> POTAGER -> ACQUIS
> RIVIÈRE -> bcp plus humide, moins de feux
> DESERT -> bcp de feu, moins d'innondation... 

*/


