using System.Drawing;

public class Monde
{
    public string[] Mois { get; set; }
    public Monde()
    {

    }
    public void TracerNPattern(string pattern, int n, ConsoleColor couleur = ConsoleColor.White)
    {
        Console.ForegroundColor = couleur;
        for (int i = 0; i < n; i++)
        {
            Console.Write(pattern);
        }
    }
    public void TracerCadre(string bordGauche, string bordDroit, int MargeGauche)
    {
        ConsoleColor Cadre = ConsoleColor.White;
        Console.Write(bordGauche);
        TracerNPattern("-", MargeGauche, Cadre);
        Console.Write(bordDroit);
    }
    public void ChangerCouleurEtat()
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    public void Simuler()
    {
        //parcelle 
        List<Plante[,]> Parcelle = new List<Plante[,]>
{
    new Plante[,] { { null, null },{ null, null } },
    new Plante[,] { { null, null }, { null, null } }
};

        int ParcelleSlectionnee = 0;
        //console
        int YConsole = 20;
        int YGrilleStart = 2;
        int MargeGauche = 10;

        //taille case
        int YTailleCase = 1;
        int XTailleCase = 2 * YTailleCase;

        //intercase
        int YInterCase = 1;
        int XInterCase = 1;

        //taille grille
        int YTailleGrille = 2;
        int XTailleGrille = 2;
        //coordonnees absolue
        int YTailleGrilleCaractere = (YInterCase + YTailleCase) * YTailleGrille + 1;
        int XTailleGrilleCaractere = XInterCase + XTailleCase * XTailleGrille - 1;

        //parcours
        int yCase = 0;
        int yGrille = 0;
        Console.Clear();
        for (int yConsole = 0; yConsole < YConsole; yConsole++)
        {
            //espace sur le cote
            TracerNPattern(" ", MargeGauche);
            //haut du cadre
            if (yConsole == YGrilleStart)
                TracerCadre("┌", "┐", MargeGauche);
            //base du cadre
            else if (yConsole == YGrilleStart - 1 + YTailleGrilleCaractere)
                TracerCadre("└", "┘", MargeGauche);
            //grille
            else if (YGrilleStart < yConsole && yConsole < YGrilleStart - 1 + YTailleGrilleCaractere)
            {
                Console.Write("|");
                //interCase
                if ((yConsole - YGrilleStart) % (YTailleCase + YInterCase) == 0)
                {
                    TracerNPattern(" ", MargeGauche);
                    yGrille++;
                }
                //case
                else
                {
                    yCase = (yCase + 1) % YTailleCase;
                    ChangerCouleurEtat();
                    for (int xGrille = 0; xGrille < XTailleGrille; xGrille++)
                    {
                        if (Parcelle[ParcelleSlectionnee][xGrille, yGrille] == null)
                            TracerNPattern(" ", XTailleCase);
                        else
                            Console.Write(Parcelle[ParcelleSlectionnee][xGrille, yGrille].Design[yCase]);
                        Console.Write(" ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("|");
            }
            Console.WriteLine("");
            //menu1 sur le cote
            //menu2 sur le cote
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


