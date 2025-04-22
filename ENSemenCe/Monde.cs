using System.Drawing;

public class Monde
{
    public string[] Mois { get; set; }

    //composants 
    public Dictionary<string, int> Composants { get; set; }

    //selection
    public int ParcelleSlectionnee { get; set; }
    public string MenuSelectionnee { get; set; }
    public string SectionSelectionnee { get; set; }

    //grille
    public int XTailleGrille { get; set; }
    public int YTailleGrille { get; set; }
    public List<Parcelle> ListParcelle { get; set; }

    public Monde(int dimX, int dimY)
    {
        MenuSelectionnee = "MenuGeneral";
        SectionSelectionnee = "Retour";
        ParcelleSlectionnee = 0;
        XTailleGrille = dimX;
        YTailleGrille = dimY;
        ListParcelle = [new Parcelle(XTailleGrille, YTailleGrille, "Potager")];

        Composants = new Dictionary<string, int>
        {
            { "Boulons", 0 },
            { "Plaques de fer", 0 },
            { "Tiges de fer", 0 },
            { "Vis", 0 },
            { "Noyaux à moteur", 0 }
        };
    }
    //affichage
    public void ChangerCouleurEtat(int Etat)
    {
        if (Etat == 1)
            Console.ForegroundColor = ConsoleColor.Red;
        else if (Etat == 2)
            Console.ForegroundColor = ConsoleColor.Yellow;
        else
            Console.ForegroundColor = ConsoleColor.Green;
    }
    public void Affichage()
    {
        ListParcelle[ParcelleSlectionnee].NiveauCloture = 1;
        ListParcelle[ParcelleSlectionnee].NiveauArroseur = 1;
        ListParcelle[ParcelleSlectionnee].NiveauPalissade = 1;//bug au niv 1
        ListParcelle[ParcelleSlectionnee].NiveauPanneau = 2;

        //coordonnees absolue
        int YTailleGrilleCaractere = (Graphique.YInterCase + Graphique.YTailleCase) * YTailleGrille + 1;
        int XTailleGrilleCaractere = (Graphique.XInterCase + Graphique.XTailleCase) * XTailleGrille + 1;

        //parcours
        int yCase = 0;
        int yGrille = 0;
        int ligneArroseur = 0;
        bool estGrille = true;

        Console.Clear();
        Graphique.TracerTitreEncadre(Graphique.Titre, XTailleGrilleCaractere);
        Graphique.SauterNLigne(3);
        Graphique.AfficherDictionnaire(Composants, Graphique.Palette["Composants"]);
        for (int yConsole = 0; yConsole < Graphique.YConsole; yConsole++)
        {
            estGrille = true;
            //espace sur le cote
            Graphique.TracerPatternLongueurN(" ", Graphique.MargeGauche);
            //haut du cadre
            if (yConsole == Graphique.YGrilleStart)
                Graphique.TracerClotureHautBas(ListParcelle[ParcelleSlectionnee].NiveauCloture, XTailleGrilleCaractere, "Haut");
            //base du cadre
            else if (yConsole == Graphique.YGrilleStart - 1 + YTailleGrilleCaractere)
                Graphique.TracerClotureHautBas(ListParcelle[ParcelleSlectionnee].NiveauCloture, XTailleGrilleCaractere, "Bas");
            //grille
            else if (Graphique.YGrilleStart < yConsole && yConsole < Graphique.YGrilleStart - 1 + YTailleGrilleCaractere)
            {
                Graphique.TracerClotureCote(ListParcelle[ParcelleSlectionnee].NiveauCloture, yConsole);
                //interCase
                if ((yConsole - Graphique.YGrilleStart) % (Graphique.YTailleCase + Graphique.YInterCase) == 0)
                {
                    ligneArroseur++;
                    if (ligneArroseur % Graphique.ArroseurFrequenceY[ListParcelle[ParcelleSlectionnee].NiveauArroseur] == 0)
                    {
                        Graphique.TracerInterligneAroseur(arroseur: true, ListParcelle[ParcelleSlectionnee].NiveauArroseur, ListParcelle[ParcelleSlectionnee].NiveauPalissade, longueurTotal: XTailleGrilleCaractere - 2);
                    }
                    else
                        Graphique.TracerInterligneAroseur(arroseur: false, ListParcelle[ParcelleSlectionnee].NiveauArroseur, ListParcelle[ParcelleSlectionnee].NiveauPalissade, longueurTotal: XTailleGrilleCaractere - 2);
                    yGrille++;
                }
                //case
                else
                {
                    ChangerCouleurEtat(1);
                    for (int xGrille = 0; xGrille < XTailleGrille; xGrille++)
                    {
                        if (ListParcelle[ParcelleSlectionnee].MatricePlantes[xGrille, yGrille] == null)
                            Graphique.TracerPatternLongueurN(" ", Graphique.XTailleCase);
                        else
                            Console.Write(ListParcelle[ParcelleSlectionnee].MatricePlantes[xGrille, yGrille].Design[yCase]);
                        //ne faire un espace après la derniere case
                        if (xGrille != XTailleGrille - 1)
                            Graphique.TracerPalissadeVertical(ListParcelle[ParcelleSlectionnee].NiveauPalissade, yCase);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Graphique.TracerClotureCote(ListParcelle[ParcelleSlectionnee].NiveauCloture, yConsole);
                yCase = (yCase + 1) % (Graphique.YTailleCase + Graphique.YInterCase);
            }
            else
                estGrille = false;
            if (Constantes.Menus[MenuSelectionnee].ContainsKey(yConsole))
            {
                if (!estGrille)
                    Graphique.TracerPatternLongueurN(" ", XTailleGrilleCaractere);
                Graphique.TracerPatternLongueurN(" ", Graphique.MargeAvantMenu);
                if (Constantes.Menus[MenuSelectionnee][yConsole] == SectionSelectionnee)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("> " + Constantes.Menus[MenuSelectionnee][yConsole]);
                    Console.ResetColor();
                }
                else
                    Console.Write("> " + Constantes.Menus[MenuSelectionnee][yConsole]);
            }
            Console.WriteLine("");
        }
    }

    //action 
    public void GererEntreeClavier()
    {
        Affichage();
        Console.ForegroundColor = Graphique.Palette["Message"];
        Console.WriteLine("Fleche du haut et du bas pour naviguer espace pour valider");
        ConsoleKeyInfo touche = Console.ReadKey(true);
        switch (touche.Key)
        {
            //Up et Down se déplacer
            case ConsoleKey.UpArrow:
                PrendreSectionSuivantePrecedente(false);
                break;

            case ConsoleKey.DownArrow:
                PrendreSectionSuivantePrecedente(true);
                break;

            //valider
            case ConsoleKey.Spacebar:
                break;
            default:
                {
                    GererEntreeClavier();
                } // Appel récursif
                return; // Important pour ne pas continuer après l'appel récursif
        }
        // Tu peux faire d'autres trucs ici avec la variable `a`
    }

    public void PrendreSectionSuivantePrecedente(bool suivante)
    {
        var valeurs = Constantes.Menus[MenuSelectionnee].Values.ToList();
        int index = valeurs.IndexOf(SectionSelectionnee);

        if (index == -1)
        {
            throw new ArgumentException($"Clé '{MenuSelectionnee}' non trouvée dans le dictionnaire.");
        }
        int indexRenvoye = 0;
        if (suivante)
            indexRenvoye = (index + 1) % valeurs.Count;
        else
            indexRenvoye = (index - 1 + valeurs.Count) % valeurs.Count;
        SectionSelectionnee = valeurs[indexRenvoye];
        GererEntreeClavier();
    }

    public void Valider()
    {

    }
    //action sur plante
    public void ArroserTout()
    {
        foreach (Parcelle parcelle in ListParcelle)
        {
            parcelle.ArroserParcelle();
        }
    }
    public void ArroserParcelleSelectionnee()
    {
        //selectionnerParcelle
        int index = 0;
        ListParcelle[index].ArroserParcelle();
    }
    public void ArroserPlante()
    {
        int x = 0;
        int y = 0;
        //ListParcelle[ParcelleSlectionnee].MatricePlantes[x, y].Arroser();
    }
    public void Planter()
    {
    }
    public void DemonterPlante()
    {
        //selectionnerPlantes
        //retirer
        //ajouter aux dico composants
    }
    public void EclairerPlante()
    {

    }

    public void Recolter()
    {

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


