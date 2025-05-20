using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

public class Monde
{
    public int Mois { get; set; }

    //composants 
    public Dictionary<string, int> Composants { get; set; }

    //selection
    public int ParcelleSlectionnee { get; set; }
    public string MenuSelectionnee { get; set; }
    public string PrecedentMenuSelectionnee { get; set; }
    public string SectionSelectionnee { get; set; }
    public string TypePlanteSelectionne { get; set; }

    //grille
    public int XTailleGrille { get; set; }
    public int YTailleGrille { get; set; }
    public List<Parcelle> ListParcelle { get; set; }

    //selection
    public int[] CaseSelectionnee = [-1, -1];
    public bool CaseSelectionneePossible = false;

    //autre
    public string Message { get; set; }
    public string Information { get; set; }
    public bool Passer { get; set; }
    public Monde(int dimX, int dimY)
    {
        Mois = 0;
        Message = "";
        Information = "";
        MenuSelectionnee = "MenuGeneral";
        PrecedentMenuSelectionnee = "MenuGeneral";
        SectionSelectionnee = "Planter";
        TypePlanteSelectionne = Constantes.Menus["Planter"].Values.First();
        ParcelleSlectionnee = 0;
        XTailleGrille = dimX;
        YTailleGrille = dimY;
        ListParcelle = [new Parcelle(XTailleGrille, YTailleGrille, "Potager")];
        Graphique.YConsole = dimY * Graphique.YTailleCase + Graphique.YGrilleStart + 10;
        InitialisationPlante();
        Composants = new Dictionary<string, int>
        {
            {"boulons", 50},
            {"plaque", 50},
            {"tige", 50},
            {"vis", 50}
        };
    }
    public void InitialisationPlante()
    {
        CaseSelectionnee[1] = 0;
        for (int i = 0; i < XTailleGrille; i += 2)
        {
            CaseSelectionnee[0] = i;
            Planter("Margu-ee-rite", true);
        }
        CaseSelectionnee[0] = -1;
        CaseSelectionnee[0] = -1;
    }
    public void ChangerCouleurEtat(int Etat)
    {
        if (Etat == 1)
            Console.ForegroundColor = ConsoleColor.Red;
        else if (Etat == 2)
            Console.ForegroundColor = ConsoleColor.Yellow;
        else
            Console.ForegroundColor = ConsoleColor.Green;
    }
    public void Jouer()
    {
        Passer = false;
        Mois = -1;
        while (true)
        {
            //tirer mode urgence
            Passer = false;
            Mois = (Mois + 1) % DonneesClimatiques.TousLesMois.Count();
            while (!Passer)
            {
                Affichage();
                GererEntreeClavier();
            }
            //si mode urgence non =>
            PasserTour();
        }

    }
    public void PasserTour()
    {
        //composants
        //faire grandire
        //huile
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
        Graphique.SauterNLigne(2);
        Console.ForegroundColor = Graphique.Palette["Mois"];
        Console.WriteLine(DonneesClimatiques.TousLesMois[Mois]);
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
                    for (int xGrille = 0; xGrille < XTailleGrille; xGrille++)
                    {
                        //curseur
                        if (CaseSelectionnee[0] == xGrille && CaseSelectionnee[1] == yGrille)
                        {
                            ConsoleColor couleur = ConsoleColor.Red;
                            if (CaseSelectionneePossible)
                                couleur = ConsoleColor.Green;
                            Graphique.TracerPatternLongueurN("█", Graphique.XTailleCase, couleur);
                        }
                        else if (ListParcelle[ParcelleSlectionnee].MatricePlantes[xGrille, yGrille] == null)
                            Graphique.TracerPatternLongueurN(" ", Graphique.XTailleCase);
                        else
                        {
                            ChangerCouleurEtat(1);
                            Console.Write(Graphique.PlantesDesign[ListParcelle[ParcelleSlectionnee].MatricePlantes[xGrille, yGrille].TypePlante][yCase]);
                        }
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
        Console.ForegroundColor = Graphique.Palette["Information"];
        if (MenuSelectionnee == "Planter" && SectionSelectionnee != "Retour")
        {
            Console.Write("Prix");
            Graphique.AfficherDictionnaire(Constantes.PlantesNecessaireConstruction[SectionSelectionnee], Graphique.Palette["Information"]);
        }
        else if (SectionSelectionnee == "Demonter" && CaseSelectionneePossible && CaseSelectionnee[0] != -1 && CaseSelectionnee[1] != -1)
        {
            Console.Write("Recuperer");
            string TypePlantePotentiellementDetruite = ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]].TypePlante;
            Graphique.AfficherDictionnaire(CalculerRemboursementConstruction(Constantes.PlantesNecessaireConstruction[TypePlantePotentiellementDetruite]), Graphique.Palette["Information"]);
        }
        else
            Console.ForegroundColor = Graphique.Palette["Message"];
        Console.WriteLine("\n" + Message);
    }

    //action 
    public void GererEntreeClavier()
    {
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
            case ConsoleKey.Enter:
                ValiderEntre();
                break;
        }
        // Tu peux faire d'autres trucs ici avec la variable `a`
    }

    public bool PeutConstruirePlante(string nomPlante)
    {
        if (!Constantes.PlantesNecessaireConstruction.ContainsKey(nomPlante))
            return false;

        var necessaire = Constantes.PlantesNecessaireConstruction[nomPlante];

        foreach (var composant in necessaire)
        {
            string nom = composant.Key;
            int quantiteRequise = composant.Value;

            if (!Composants.ContainsKey(nom) || Composants[nom] < quantiteRequise)
            {
                return false;
            }
        }

        return true;
    }
    public void ValiderEntre()
    {
        if (SectionSelectionnee == "Retour")
        {
            MenuSelectionnee = PrecedentMenuSelectionnee;
            SectionSelectionnee = Constantes.Menus[MenuSelectionnee].Values.First();

        }
        else if (MenuSelectionnee == "Planter")
        {

            TypePlanteSelectionne = SectionSelectionnee;
            if (PeutConstruirePlante(TypePlanteSelectionne))
            {
                SelectionPlante();
            }
            else
            {
                Message = "Pas assez de composants";
            }

        }
        else if (SectionSelectionnee == "Demonter")
        {
            SelectionPlante();
        }
        else if (SectionSelectionnee == "Passer")
        {
            Passer = true;
        }

        else
        {
            PrecedentMenuSelectionnee = MenuSelectionnee;
            MenuSelectionnee = SectionSelectionnee;
            SectionSelectionnee = Constantes.Menus[MenuSelectionnee].Values.First();
        }
    }
    public void PrendreSectionSuivantePrecedente(bool suivante)
    {
        var valeurs = Constantes.Menus[MenuSelectionnee].Values.ToList();
        int index = valeurs.IndexOf(SectionSelectionnee);

        if (index == -1)
        {
            throw new ArgumentException($"Clé '{SectionSelectionnee}' non trouvée dans le dictionnaire.");
        }
        int indexRenvoye = 0;
        if (suivante)
            indexRenvoye = (index + 1) % valeurs.Count;
        else
            indexRenvoye = (index - 1 + valeurs.Count) % valeurs.Count;
        SectionSelectionnee = valeurs[indexRenvoye];
    }

    //action sur plante

    public bool[,] VerifierPlanter()
    {
        bool[,] emplacementPasPossible = new bool[XTailleGrille, YTailleGrille];

        if (MenuSelectionnee == "Planter")
        {
            foreach (Plante plante in ListParcelle[ParcelleSlectionnee].MatricePlantes)
            {
                if (plante != null)
                {
                    int x = plante.Coord[0];
                    int y = plante.Coord[1];
                    int espacement = plante.EspacementNecessaire;

                    for (int dx = -espacement; dx <= espacement; dx++)
                    {
                        for (int dy = -espacement; dy <= espacement; dy++)
                        {
                            int nx = x + dx;
                            int ny = y + dy;
                            if (nx >= 0 && nx < XTailleGrille && ny >= 0 && ny < YTailleGrille)
                            {
                                emplacementPasPossible[nx, ny] = true;
                            }
                        }
                    }
                }
            }
        }
        return emplacementPasPossible;
    }

    public void ActionPossible(bool emplacementPasPossible)
    {
        if (MenuSelectionnee == "Planter")
        {
            if (emplacementPasPossible)
                CaseSelectionneePossible = false;
            else
                CaseSelectionneePossible = true;
        }
        else if (SectionSelectionnee == "Demonter" || MenuSelectionnee == "Arroser")
        {
            if (ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]] != null)
                CaseSelectionneePossible = true;
            else
                CaseSelectionneePossible = false;
        }
    }

    public void SelectionPlante()
    {
        CaseSelectionnee = [0, 0];
        bool annuler = false;
        bool valider = false;
        string consigne = "fleche pour se deplacer, entree pour valider, e pour annuler";
        bool[,] emplacementPasPossible = VerifierPlanter();
        CaseSelectionneePossible = false;
        do
        {
            ActionPossible(emplacementPasPossible[CaseSelectionnee[0], CaseSelectionnee[1]]);
            Affichage();
            Console.ForegroundColor = Graphique.Palette["Message"];
            Console.WriteLine(consigne);
            ConsoleKeyInfo touche = Console.ReadKey(true);
            switch (touche.Key)
            {
                //Up et Down se déplacer
                case ConsoleKey.UpArrow:
                    CaseSelectionnee = CoordCase(CaseSelectionnee[0], CaseSelectionnee[1], 0, -1);
                    break;

                case ConsoleKey.DownArrow:
                    CaseSelectionnee = CoordCase(CaseSelectionnee[0], CaseSelectionnee[1], 0, 1);
                    break;

                case ConsoleKey.RightArrow:
                    CaseSelectionnee = CoordCase(CaseSelectionnee[0], CaseSelectionnee[1], 1, 0);
                    break;

                case ConsoleKey.LeftArrow:
                    CaseSelectionnee = CoordCase(CaseSelectionnee[0], CaseSelectionnee[1], -1, 0);
                    break;
                //valider
                case ConsoleKey.Enter:
                    if (CaseSelectionneePossible)
                    {
                        if (MenuSelectionnee == "Planter")
                        {
                            Planter(TypePlanteSelectionne);
                        }
                        else if (SectionSelectionnee == "Demonter")
                        {
                            DemonterPlante();
                        }
                        else if (MenuSelectionnee == "Arroser")
                        {
                            ArroserPlante();
                        }
                        valider = true;
                    }
                    break;
                //annuler
                case ConsoleKey.E:
                    annuler = true;
                    break;
            }
            ActionPossible(emplacementPasPossible[CaseSelectionnee[0], CaseSelectionnee[1]]);

        } while (!annuler && !valider);
        CaseSelectionnee = [-1, -1];
        Affichage();
    }
    public int[] CoordCase(int xActu, int yActu, int xTranslation, int yTranslation)
    {
        int[] coord = [xActu, yActu];
        if ((xActu + xTranslation >= 0) && (yActu + yTranslation >= 0) && (xActu + xTranslation < XTailleGrille) && (yActu + yTranslation < YTailleGrille))
            coord = [xActu + xTranslation, yActu + yTranslation];
        return coord;
    }
    public void DemonterPlante()
    {
        var plante = ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]];
        if (plante != null)
        {
            string TypePlanteDetruite = ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]].TypePlante;
            ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]] = null;
            RembourserConstruction(Constantes.PlantesNecessaireConstruction[TypePlanteDetruite]);
            // Exemple : ajouter des composants récupérés
        }
        //selectionnerPlantes
        //retirer
        //ajouter aux dico composants
    }
    public void PayerConstruction(Dictionary<string, int> necessaire)
    {
        //payer cout de construction

        foreach (var composant in necessaire)
        {
            string nom = composant.Key;
            int quantite = composant.Value;

            // Décrémente les composants utilisés
            Composants[nom] -= quantite;
        }
    }
    public void RembourserConstruction(Dictionary<string, int> necessaire)
    {
        //rembourser cout de construction

        foreach (var composant in necessaire)
        {
            string nom = composant.Key;
            int quantite = (int)Math.Round(composant.Value * Constantes.Remboursement);

            // Décrémente les composants utilisés
            Composants[nom] += quantite;
        }
    }
    public Dictionary<string, int> CalculerRemboursementConstruction(Dictionary<string, int> necessaire)
    {
        Dictionary<string, int> remboursement = new Dictionary<string, int>();

        foreach (var composant in necessaire)
        {
            string nom = composant.Key;
            int quantite = (int)Math.Round(composant.Value * Constantes.Remboursement); // ou Floor/Ceiling selon la logique

            remboursement[nom] = quantite;
        }

        return remboursement;
    }


    public void Planter(string type, bool initilisation = false)
    {
        if (CaseSelectionneePossible || initilisation)
        {
            if (!Constantes.PlantesNecessaireConstruction.ContainsKey(type))
            {
                Console.WriteLine("Type de plante inconnu : " + type);
                return;
            }
            if (!initilisation)
                PayerConstruction(Constantes.PlantesNecessaireConstruction[type]);
            if (type == "PoMWier")
            {
                ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]] =
                    new PoMWier(ListParcelle[ParcelleSlectionnee], CaseSelectionnee[0], CaseSelectionnee[1]);
            }
            else if (type == "Margu-ee-rite")
            {
                ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]] =
                    new Marguerite(ListParcelle[ParcelleSlectionnee], CaseSelectionnee[0], CaseSelectionnee[1]);
            }
            else if (type == "Rob-ose")
            {
                ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]] =
                    new Robose(ListParcelle[ParcelleSlectionnee], CaseSelectionnee[0], CaseSelectionnee[1]);
            }
            else if (type == "LierR3")
            {
                ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]] =
                    new Lierre(ListParcelle[ParcelleSlectionnee], CaseSelectionnee[0], CaseSelectionnee[1]);
            }
            else if (type == "Po1rier")
            {
                ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]] =
                    new Poirier(ListParcelle[ParcelleSlectionnee], CaseSelectionnee[0], CaseSelectionnee[1]);
            }
        }
    }

    public void ArroserPlante()
    {
        var plante = ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]];
        if (plante != null)
        {
            //ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]].Arroser();
            //actualiser eau
        }
    }

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


