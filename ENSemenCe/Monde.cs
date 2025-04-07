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
        Console.Write("┌");
        TracerNPattern("-", MargeGauche, Cadre);
        Console.Write("┐");
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
        int YConsole = 50;
        int YGrilleStart = 0;
        int MargeGauche = 10;

        //taille case
        int YTailleCase = 8;
        int XTailleCase = 4;

        //intercase
        int YInterCase = 1;
        int XInterCase = 1;

        //taille grille
        int YTailleGrille = 10;
        int XTailleGrille = 20;
        //coordonnees absolue
        int YTailleGrilleCaractere = YInterCase + YTailleCase * YTailleGrille - 1;
        int XTailleGrilleCaractere = XInterCase + XTailleCase * XTailleGrille - 1;

        //parcours
        int yCase = 0;
        for (int yConsole = 0; yConsole < YConsole; yConsole++)
        {
            //espace sur le cote
            TracerNPattern(" ", MargeGauche);
            //haut du cadre
            if (yConsole == YGrilleStart)
                TracerCadre("┌", "┐", MargeGauche);
            //base du cadre
            else if (yConsole == ((YInterCase + YTailleCase) * (YTailleGrilleCaractere + 1)))
                TracerCadre("└", "┘", MargeGauche);
            //grille
            else if (YGrilleStart >= yConsole && yConsole <= YTailleGrilleCaractere)
            {
                Console.Write("|");
                //interCase
                if (yConsole % (YTailleCase + 1) == 0)
                {
                    TracerNPattern(" ", MargeGauche);
                }
                //case
                else
                {
                    ChangerCouleurEtat();
                    for (int xGrille = 0; xGrille < XTailleGrille; xGrille++)
                    {
                        Console.Write(Parcelles[xGrille, yCase]);
                        Console.Write(" ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("|");
            }
        }
        //menu1 sur le cote
        //menu2 sur le cote
        Console.WriteLine("");
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


