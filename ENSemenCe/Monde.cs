using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

public class Monde
{
    public int Mois { get; set; }

    //composants 
    public Dictionary<string, int> Composants { get; set; }

    //selection
    public int ParcelleSlectionnee { get; set; }
    public string MenuSelectionnee { get; set; }
    public string SectionSelectionnee { get; set; }
    public string TypePlanteSelectionne { get; set; }

    //grille
    public int XTailleGrille { get; set; }
    public int YTailleGrille { get; set; }
    public List<Parcelle> ListParcelle { get; set; }

    //selection
    public int[] CaseSelectionnee = [-1, -1];//Curseur
    public bool CaseSelectionneePossible = false;//action possible selon l emplacement du curseur

    //urgence
    public Creature CreatureMonde { get; set; }
    public string Origin { get; set; }//menu de retour : change selon le mode (menuGeneral ou menuUrgence)
    public bool Urgence { get; set; }

    //autre
    public string Message { get; set; }
    public string Information { get; set; }
    public string ButAmelioration { get; set; }
    public bool Passer { get; set; }//variable pour passer un tour
    public Dictionary<string, int> FruitsProduits { get; set; }
    Random Rnd = new Random();
    public Monde(int dimX, int dimY, int coefficientPieceDepart = 5) //CONSTRUCTEUR MONDE
    {
        //taille de la grille
        XTailleGrille = dimX;
        YTailleGrille = dimY;

        //menu de depart
        MenuSelectionnee = "MenuGeneral";
        SectionSelectionnee = "Planter";
        TypePlanteSelectionne = Constantes.Menus["Planter"].Values.First();

        //parcelle et mois de depart
        ListParcelle = [new Parcelle(XTailleGrille, YTailleGrille, "Potager")];
        ParcelleSlectionnee = 0;
        Mois = 0;
        InitialisationPlante();

        //creature
        CreatureMonde = null;

        //label
        Message = "";
        Information = "";
        ButAmelioration = "Economise de l'huile lors de l'arrosage";

        //initialisation des composants et fruits
        Composants = new Dictionary<string, int>
        {
            {"boulons", 10*coefficientPieceDepart},
            {"plaque", 10*coefficientPieceDepart},
            {"tige", 10*coefficientPieceDepart},
            {"vis", 10*coefficientPieceDepart}
        };
        FruitsProduits = new Dictionary<string, int>
        {
            { "graines lumineuses", 0 },     // Marguerite
            { "pétales d'acier", 0 },        // Robose
            { "gousses grimpantes", 0 },     // Lierre
            { "pomwies", 0 },                // PoMWier
            { "poires hybrides", 0 }         // Poirier
        };

        //ecran affichage : hauteur sans les informations du haut et du bas
        Graphique.YConsole = dimY * Graphique.YTailleCase + Graphique.YGrilleStart + 10;
        FruitsProduits = new Dictionary<string, int>
        {
            { "graines lumineuses", 0 },     // Marguerite
            { "pétales d'acier", 0 },        // Robose
            { "gousses grimpantes", 0 },     // Lierre
            { "pomwies", 0 },                // PoMWier
            { "poires hybrides", 0 }         // Poirier
        };
    }
    public void InitialisationPlante() //ON PLANTE QQUES MARGU-EE-RITES POUR COMMENCER LE JEU
    {
        CaseSelectionnee[1] = 0;
        for (int i = 0; i < XTailleGrille; i += 2)
        {
            CaseSelectionnee[0] = i;
            Planter("Margu-ee-rite", true);//Plante a l emplacement du curseur
        }
        CaseSelectionnee[0] = -1;
        CaseSelectionnee[0] = -1;
    }

    //Fonctions pour le déroulement des tours
    public void Jouer() //LA BOUCLE GÉNÉRALE DE JEU !! LÀ QU'À LIEU LE TOUR
    {
        Passer = false;
        Mois = -1;
        //boucle de jeu pour defilement d un mois
        while (true)
        {
            Passer = false;
            Mois = (Mois + 1) % DonneesClimatiques.TousLesMois.Count();
            ModeUrgenceTirage();
            //boucle pour un mois
            while ((!Passer) || Urgence)
            {
                Affichage();
                GererEntreeClavier();
            }
            PasserTour();
        }
    }
    public void PasserTour() //PASSER LE TOUR : ACTUALISE CHAQUE PLANTE DE CHAQUE PARCELLE
    {
        foreach (Parcelle parcelle in ListParcelle)
        {
            parcelle.ActuliserPousseMortRecolte(Mois, this);
        }
    }

    //Navigation et touches
    public void ValiderEntre()//GESTION DES ACTIONS A EXECUTER QUAND ON TAPE ENTREE (Sauf pour la selection des cases)
    {
        //condition critique
        if (SectionSelectionnee == "Retour au menu principal")
        {
            MenuSelectionnee = Origin;
            SectionSelectionnee = Constantes.Menus[MenuSelectionnee].Values.First();
        }
        //chaque action valide declenche un tour de la creature en mode urgence => TourModeUrgence
        else if (MenuSelectionnee == "Planter")
        {

            TypePlanteSelectionne = SectionSelectionnee;
            //si on peut constuire la plante
            if (PeutConstruire(Constantes.PlantesNecessaireConstruction[TypePlanteSelectionne]))
            {
                SelectionCase();
            }
            else
            {
                Message = "Pas assez de composants";
            }

        }
        else if (MenuSelectionnee == "Acheter")
        {
            //si on peut acheter les composants
            if (Composants["boulons"] >= Constantes.PackAchat * Constantes.CoutBoulons[SectionSelectionnee])
            {
                Acheter();
                TourModeUrgence();
            }
        }
        else if (SectionSelectionnee == "Demonter")
        {
            SelectionCase();
        }
        else if (MenuSelectionnee == "Ameliorer")
        {
            //si on a assez pour ameliorer et que le niveau n est pas maximum
            if (Ameliorer())
            {
                TourModeUrgence();
            }
        }
        else if (SectionSelectionnee == "Passer au mois suivant")
        {
            Passer = true;
        }
        else if (SectionSelectionnee == "PasserAction")
        {
            TourModeUrgence();
        }
        else if (SectionSelectionnee == "VendreRecolte")
        {
            VendreRecolte();
            TourModeUrgence();
        }
        else if (MenuSelectionnee == "Pieges")
        {
            if (PiegePossibleUtile())//si niveau du piege insuffisant ou pas assez de composants
            {
                SelectionCase();
            }
        }
        else if (SectionSelectionnee == "ArroserUnePlante")
        {
            SelectionCase();
        }
        else if (SectionSelectionnee == "ArrosageAutomatique")
        {
            //RAJOUTER UNE CONDITION SI POSSIBLE
            ArroserParcelleSelectionnee();
        }
        else
        {
            //Change de menu
            MenuSelectionnee = SectionSelectionnee;
            SectionSelectionnee = Constantes.Menus[MenuSelectionnee].Values.First();
        }
    }
    public void GererEntreeClavier()//DEPLACEMENTS DANS LES MENUS
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
    }
    public void PrendreSectionSuivantePrecedente(bool suivante)//RETOURNE LE PROCHAIN MENU
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
    public void SelectionCase()//SELECTIONNE UNE CASE
    {
        CaseSelectionnee = [0, 0];
        bool annuler = false;
        bool valider = false;
        string consigne = "fleche pour se deplacer, entree pour valider, e pour annuler";
        CaseSelectionneePossible = false;
        do
        {
            //endroit ou l on peut pas planter
            bool[,] emplacementPasPossible = VerifierPlanter();
            //actualise l etat du curseur en fonction de l action
            ActionPossible(emplacementPasPossible[CaseSelectionnee[0], CaseSelectionnee[1]]);
            Affichage();
            //recuperer touche
            Console.ForegroundColor = Graphique.Palette["Message"];
            Console.WriteLine(consigne);
            ConsoleKeyInfo touche = Console.ReadKey(true);
            switch (touche.Key)
            {
                //Up et Down deplace coordonner du curseur
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
                        else if (SectionSelectionnee == "ArroserUnePlante")
                        {
                            ArroserPlante(Mois);
                        }
                        else if (MenuSelectionnee == "Pieges")
                        {
                            PayerConstruction(Constantes.PiegeNecessaireConstruction[SectionSelectionnee]);
                            ModeUrgenceFin();//Creature piege, fin du mode urgence
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
        CaseSelectionnee = [-1, -1];//retire le curseur
        if (valider)
            TourModeUrgence();//chaque action valide declenche un tour de la creature en mode urgence => TourModeUrgence
        Affichage();
    }
    public void ActionPossible(bool emplacementPasPossible)//ACTUALISE L ETAT DE L ACTION SELON L EMPLACEMENT DU CURSEUR ET L ACTION EN COURS
    {
        if (MenuSelectionnee == "Planter")
        {
            //si les espacements sont respecte
            if (emplacementPasPossible)
                CaseSelectionneePossible = false;
            else
                CaseSelectionneePossible = true;
        }
        else if (SectionSelectionnee == "Demonter" || SectionSelectionnee == "ArroserUnePlante")
        {
            //si le curseur est sur une plante
            if (ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]] != null)
                CaseSelectionneePossible = true;
            else
                CaseSelectionneePossible = false;
        }
        else if (MenuSelectionnee == "Pieges")
        {
            //si le curseur est sur la creature
            if (CreatureMonde.X == CaseSelectionnee[0] && CreatureMonde.Y == CaseSelectionnee[1])
                CaseSelectionneePossible = true;
            else
                CaseSelectionneePossible = false;
        }
    }
    public int[] CoordCase(int xActu, int yActu, int xTranslation, int yTranslation)//ACTUALISE LA POSITION DU CURSEUR
    {
        int[] coord = [xActu, yActu];
        //verifie si on tente pas de se deplacer en dehors de la grille
        if ((xActu + xTranslation >= 0) && (yActu + yTranslation >= 0) && (xActu + xTranslation < XTailleGrille) && (yActu + yTranslation < YTailleGrille))
            coord = [xActu + xTranslation, yActu + yTranslation];
        return coord;
    }

    //Mode urgence
    public void ModeUrgenceTirage()//DECLENCHE OU PAS LE MODE URGENCE
    {
        double proba = Rnd.NextDouble(); // nombre entre 0.0 et 1.0
        Origin = "MenuGeneral";
        if (proba <= Constantes.ProbaModeUrgence)
        {
            int niveau = Rnd.Next(1, 5); // 1 inclus, 5 exclus → donc entre 1 et 4
            Urgence = true;
            FaireApparaitreCreature(niveau);//pioche une creature et la place sur la grille si le niveau de la cloture est insuffisant
            if (Urgence)//si la creature a passe la cloture
            {
                Origin = "MenuUrgence";
                MenuSelectionnee = "MenuUrgence";
                SectionSelectionnee = Constantes.Menus[MenuSelectionnee].Values.First();
            }
            else
                ModeUrgenceFin();
        }
    }
    public void ModeUrgenceFin()//REVIENT EN MODE NORMAL
    {
        Urgence = false;
        CreatureMonde = null;
        //revient au menu normal
        Origin = "MenuGeneral";
        MenuSelectionnee = "MenuGeneral";
        SectionSelectionnee = Constantes.Menus[MenuSelectionnee].Values.First();
    }
    public void FaireApparaitreCreature(int niveau)//CREER LA CREATURE SI LE NIVEAU DE LA CLOTURE EST INSUFFISANT
    {
        //la creature ne passe pas la cloture
        if (ListParcelle[ParcelleSlectionnee].NiveauCloture > niveau)
        {
            Urgence = false;
        }
        if (niveau == 1)
        {
            CreatureMonde = new CreatureNiveau1(this, "1");
        }
        else if (niveau == 2)
        {
            CreatureMonde = new CreatureNiveau2(this, "2");
        }
        else if (niveau == 3)
        {
            CreatureMonde = new CreatureNiveau3(this, "3");
        }
        else if (niveau == 4)
        {
            CreatureMonde = new CreatureNiveau4(this, "4");
        }
        if (Urgence)
        {
            //initialise la creature
            CreatureMonde.PositionnerSurBord();
            CreatureMonde.Manger();
        }
    }
    public void TourModeUrgence()//ENCLENCHE LE TOUR DE LA CREATURE ET LA SUPPRIME SI BESOIN
    {
        if (Urgence)
        {
            //bouge la creature
            CreatureMonde.TourCreature();
            if (CreatureMonde.NombreActions == 0)//si la creature n a plus d action fin du mode urgence
            {
                Thread.Sleep(Constantes.TempsDpt * 3);
                ModeUrgenceFin();
            }
        }
    }
    //Affichage
    public void Affichage()//AFFICHE TOUT LE JEU
    {
        //taille de la grille en caracteres
        int yTailleGrilleCaractere = (Graphique.YInterCase + Graphique.YTailleCase) * YTailleGrille + 1;
        int xTailleGrilleCaractere = (Graphique.XInterCase + Graphique.XTailleCase) * XTailleGrille + 1;
        AffichageHaut(xTailleGrilleCaractere, yTailleGrilleCaractere);
        AffichageGrille(xTailleGrilleCaractere, yTailleGrilleCaractere);
        AffichageBas();
    }
    public void AffichageHaut(int xTailleGrilleCaractere, int yTailleGrilleCaractere)//AFFICHE TOUTES LES INFORMATIONS AU DESSUS DE LA GRILLE
    {
        //titre
        Console.Clear();
        Graphique.TracerTitreEncadre(Graphique.Titre, xTailleGrilleCaractere);
        Graphique.SauterNLigne(2);
        //mois
        Console.ForegroundColor = Graphique.Palette["Mois"];
        Console.WriteLine(DonneesClimatiques.TousLesMois[Mois]); //écrit infos sur le climat 
        //composants
        Console.ForegroundColor = Graphique.Palette["Composants"];
        Graphique.AfficherDictionnaire(Composants, Graphique.Palette["Composants"]);
        Console.WriteLine("");
        //fruits
        Console.ForegroundColor = Graphique.Palette["Fruit"];
        Graphique.AfficherDictionnaire(FruitsProduits, Graphique.Palette["Fruit"]);
        //parcelle
        Console.WriteLine("\n" + ListParcelle[ParcelleSlectionnee].NomParcelle);
        //mode urgence
        if (Urgence)
        {
            Console.ForegroundColor = Graphique.Palette["Creature"];
            Console.WriteLine("\n Mode URGENCE, creature niveau " + CreatureMonde.Niveau + ",  action restante " + CreatureMonde.NombreActions);
        }
        else
            Graphique.SauterNLigne(2);
    }
    public void AffichageGrille(int xTailleGrilleCaractere, int yTailleGrilleCaractere)//AFFICHE LA PARTIE PRINCIPAL DU JEU
    {
        //parcours dans une case
        int yCase = 0;
        //coordonnee absolue en caractere selon y
        int yGrille = 0;
        //parcours
        int ligneArroseur = 0;
        bool estGrille = true;
        for (int yConsole = 0; yConsole < Graphique.YConsole; yConsole++)
        {
            estGrille = true;
            //espace sur le cote
            Graphique.TracerPatternLongueurN(" ", Graphique.MargeGauche);
            //haut du cadre
            if (yConsole == Graphique.YGrilleStart)
                Graphique.TracerClotureHautBas(ListParcelle[ParcelleSlectionnee].NiveauCloture, xTailleGrilleCaractere, "Haut");
            //base du cadre
            else if (yConsole == Graphique.YGrilleStart - 1 + yTailleGrilleCaractere)
                Graphique.TracerClotureHautBas(ListParcelle[ParcelleSlectionnee].NiveauCloture, xTailleGrilleCaractere, "Bas");
            //grille
            else if (Graphique.YGrilleStart < yConsole && yConsole < Graphique.YGrilleStart - 1 + yTailleGrilleCaractere)
            {
                Graphique.TracerClotureCote(ListParcelle[ParcelleSlectionnee].NiveauCloture, yConsole);
                //interCase
                if ((yConsole - Graphique.YGrilleStart) % (Graphique.YTailleCase + Graphique.YInterCase) == 0)
                {
                    ligneArroseur++;
                    if (ligneArroseur % Graphique.ArroseurFrequenceY[ListParcelle[ParcelleSlectionnee].NiveauArroseur] == 0)
                    {
                        Graphique.TracerInterligneAroseur(arroseur: true, ListParcelle[ParcelleSlectionnee].NiveauArroseur, ListParcelle[ParcelleSlectionnee].NiveauPalissade, longueurTotal: xTailleGrilleCaractere - 2);
                    }
                    else
                        Graphique.TracerInterligneAroseur(arroseur: false, ListParcelle[ParcelleSlectionnee].NiveauArroseur, ListParcelle[ParcelleSlectionnee].NiveauPalissade, longueurTotal: xTailleGrilleCaractere - 2);
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
                            ConsoleColor couleur = Graphique.Palette["CurseurNon"];
                            if (CaseSelectionneePossible)
                                couleur = Graphique.Palette["CurseurOui"];
                            Graphique.TracerPatternLongueurN("█", Graphique.XTailleCase, couleur);
                        }
                        //creature
                        else if (CreatureMonde != null && CreatureMonde.X == xGrille && CreatureMonde.Y == yGrille)
                        {
                            Console.ForegroundColor = Graphique.Palette["Creature"];
                            Console.Write(CreatureMonde.Design[yCase]);
                        }
                        //case vide
                        else if (ListParcelle[ParcelleSlectionnee].MatricePlantes[xGrille, yGrille] == null)
                            Graphique.TracerPatternLongueurN(" ", Graphique.XTailleCase);
                        else
                        {
                            //dessine la plante en fonction de son nombre de tour
                            if (Graphique.YTailleCase - yCase <= ListParcelle[ParcelleSlectionnee].MatricePlantes[xGrille, yGrille].AffichageTour)
                            {
                                Console.ForegroundColor = Graphique.Palette["EtatPlante" + ListParcelle[ParcelleSlectionnee].MatricePlantes[xGrille, yGrille].Etat];
                                Console.Write(Graphique.PlantesDesign[ListParcelle[ParcelleSlectionnee].MatricePlantes[xGrille, yGrille].TypePlante][yCase]);
                            }
                            //la plante n est pas assez grande
                            else
                                Graphique.TracerPatternLongueurN(" ", Graphique.XTailleCase);
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
            //trace le menu sur le cote
            if (Constantes.Menus[MenuSelectionnee].ContainsKey(yConsole))
            {
                //conserve l alignement meme si le menu depasse de la grille
                if (!estGrille)
                    Graphique.TracerPatternLongueurN(" ", xTailleGrilleCaractere);
                Graphique.TracerPatternLongueurN(" ", Graphique.MargeAvantMenu);
                //si c est la section selectionnnee la mettre en bleu
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
        //dessine le robot
        Graphique.AfficherRobot(ListParcelle[ParcelleSlectionnee].NiveauRobots);
    }
    public void AffichageBas()//AFFICHE LES AUTRES INFORMATIONS SELON LE MENU
    {
        Console.ForegroundColor = Graphique.Palette["Information"];
        if (MenuSelectionnee == "Planter" && SectionSelectionnee != "Retour au menu principal")
        {
            Console.Write("Prix ");
            Graphique.AfficherDictionnaire(Constantes.PlantesNecessaireConstruction[SectionSelectionnee], Graphique.Palette["Information"]);
            Console.WriteLine("");
            //affiche les informations de la plante
            var info = ArbreFruitRegistry.Infos[SectionSelectionnee];
            Console.WriteLine($"{info.Nom} produit {info.NombreFruits} {info.TypeFruits} tous les {info.FrequenceProduction} cycles. A besoin {info.EspacementNecessaire} espace vide autour");
            Console.WriteLine(Constantes.PlantesDescription[SectionSelectionnee]);
        }
        else if (SectionSelectionnee == "Demonter" && CaseSelectionneePossible && CaseSelectionnee[0] != -1 && CaseSelectionnee[1] != -1)
        {
            Console.Write("Recuperer");
            string TypePlantePotentiellementDetruite = ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]].TypePlante;
            //affiche le remboursement selon la plante
            Graphique.AfficherDictionnaire(CalculerRemboursementConstruction(Constantes.PlantesNecessaireConstruction[TypePlantePotentiellementDetruite]), Graphique.Palette["Information"]);
        }
        else if (MenuSelectionnee == "Ameliorer" && SectionSelectionnee != "Retour au menu principal")
        {
            Console.Write("Prix " + ButAmelioration);
            int niveau = NiveauAmelioration() + 1;
            //affiche cout de l amelioration si pas au niveau max
            if (niveau < 4)
                Graphique.AfficherDictionnaire(Constantes.AmeliorationNecessaireConstruction[SectionSelectionnee + niveau], Graphique.Palette["Information"]);
            else
                Console.Write("   Niveau maximum");
        }
        else if (MenuSelectionnee == "Acheter" && SectionSelectionnee != "Retour au menu principal")
        {
            //affiche prix du composant
            int prix = Constantes.PackAchat * Constantes.CoutBoulons[SectionSelectionnee];
            Console.WriteLine("Prix en boulons pour un pack de " + Constantes.PackAchat);
            Console.WriteLine(prix);
        }
        else if (MenuSelectionnee == "Pieges" && SectionSelectionnee != "Retour au menu principal")
        {
            bool possible = false;
            char niveauPiegeSelectionne = SectionSelectionnee[SectionSelectionnee.Length - 1];
            int niveauPiegeSelectionneInt = int.Parse(niveauPiegeSelectionne.ToString());
            //si le niveau du piege est suffisant pour la creature
            if (CreatureMonde.Niveau <= niveauPiegeSelectionneInt)
            {
                Console.WriteLine("Prix du piege :");
                Graphique.AfficherDictionnaire(Constantes.PiegeNecessaireConstruction[SectionSelectionnee], Graphique.Palette["Information"]);
            }
            else
                Console.WriteLine("Creature de niveau trop eleve"); ;
        }
        //autre
        else
        {
            Console.ForegroundColor = Graphique.Palette["Message"];
            Console.WriteLine("\n" + Message);
        }
        Console.WriteLine();
    }

    //ACTIONS
    //planter

    public bool[,] VerifierPlanter()//VERIFIE SI LA CASE EST DISPONIBLE
    {
        bool[,] emplacementPasPossible = new bool[XTailleGrille, YTailleGrille];

        if (MenuSelectionnee == "Planter")
        {
            emplacementPasPossible = new bool[XTailleGrille, YTailleGrille];

            // Parcours de toutes les cases du terrain
            for (int x = 0; x < XTailleGrille; x++)
            {
                for (int y = 0; y < YTailleGrille; y++)
                {
                    // On commence par dire que la case est libre (possible)
                    bool interdit = false;

                    // Vérifie la distance avec chaque plante déjà plantée
                    foreach (Plante planteExistante in ListParcelle[ParcelleSlectionnee].MatricePlantes)
                    {
                        if (planteExistante != null)
                        {
                            int dx = Math.Abs(planteExistante.Coord[0] - x);
                            int dy = Math.Abs(planteExistante.Coord[1] - y);

                            // Espacement minimum entre la plante existante et la nouvelle
                            int espacementMin = Math.Max(planteExistante.EspacementNecessaire, ArbreFruitRegistry.Infos[SectionSelectionnee].EspacementNecessaire);

                            // Si la case est trop proche d'une plante existante, interdit de planter ici
                            if (dx <= espacementMin && dy <= espacementMin)
                            {
                                interdit = true;
                                break;
                            }
                        }
                    }

                    // Marque la case dans le tableau
                    emplacementPasPossible[x, y] = interdit;
                }
            }
        }
        return emplacementPasPossible;
    }
    public bool PeutConstruire(Dictionary<string, int> necessaire)//VERIFIE LES RESSOURCES POUR LA CONSTRUCTION
    {
        foreach (var composant in necessaire)
        {
            string nom = composant.Key;
            int quantiteRequise = composant.Value;
            //si il manque des composants ou que le composant n existe pas
            if (!Composants.ContainsKey(nom) || Composants[nom] < quantiteRequise)
            {
                return false;
            }
        }

        return true;
    }
    public void Planter(string type, bool initilisation = false) //PLANTER UNE NOUVELLE PLANTE SUR LA PARCELLE
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
    public void PayerConstruction(Dictionary<string, int> necessaire)//ACTUALISE COMPOSANTS APRES ACHAT
    {
        //payer cout de construction

        foreach (var composant in necessaire)
        {
            string nom = composant.Key;
            int quantite = composant.Value;

            // retire les composants utilisés
            Composants[nom] -= quantite;
        }
    }

    //demonter
    public void DemonterPlante()//SUPPRIME PLANTE ET REMBOURSE
    {
        var plante = ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]];
        if (plante != null)
        {
            string TypePlanteDetruite = ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]].TypePlante;
            ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]] = null;
            RembourserConstruction(Constantes.PlantesNecessaireConstruction[TypePlanteDetruite]);
        }
    }
    public void RembourserConstruction(Dictionary<string, int> necessaire)//ACTUALISE COMPOSANTS APRES REMBOURSEMENT
    {
        //rembourser cout de construction

        foreach (var composant in necessaire)
        {
            string nom = composant.Key;
            int quantite = (int)Math.Round(composant.Value * Constantes.Remboursement);

            // Retire les composants utilisés
            Composants[nom] += quantite;
        }
    }
    public Dictionary<string, int> CalculerRemboursementConstruction(Dictionary<string, int> necessaire)//AFFICHE REMBOURSEMENT
    {
        Dictionary<string, int> remboursement = new Dictionary<string, int>();

        foreach (var composant in necessaire)
        {
            string nom = composant.Key;
            int quantite = (int)Math.Round(composant.Value * Constantes.Remboursement);

            remboursement[nom] = quantite;
        }

        return remboursement;
    }

    //ameliorer
    public int NiveauAmelioration()//RETOURNE LE NIVEAU DE L ITEM SELECTIONNE
    {
        int niveau = 1;
        if (SectionSelectionnee == "Arroseurs")
        {
            niveau = ListParcelle[ParcelleSlectionnee].NiveauArroseur;
        }
        else if (SectionSelectionnee == "Clotures")
        {
            niveau = ListParcelle[ParcelleSlectionnee].NiveauCloture;
        }
        else if (SectionSelectionnee == "Palissades")
        {
            niveau = ListParcelle[ParcelleSlectionnee].NiveauPalissade;
        }
        else if (SectionSelectionnee == "Robots-Travailleurs")
        {
            niveau = ListParcelle[ParcelleSlectionnee].NiveauRobots;
        }
        return niveau;
    }
    public bool Ameliorer() //POUR AMÉLIORER LE MATÉRIEL D'UNE PARCELLE
    {
        bool amelioration = false;
        //verifie le materiel selectionne et si celui-ci n est pas au maximum l'ameliore
        if ((SectionSelectionnee == "Arroseurs") && ListParcelle[ParcelleSlectionnee].NiveauArroseur < 3)
        {
            ListParcelle[ParcelleSlectionnee].NiveauArroseur += 1;
            ButAmelioration = "Economise de l'huile lors de l'arrosage";
            amelioration = true;
        }
        else if (SectionSelectionnee == "Clotures" && ListParcelle[ParcelleSlectionnee].NiveauCloture < 3)
        {
            ListParcelle[ParcelleSlectionnee].NiveauCloture += 1;
            ButAmelioration = "Reduit les risques d'imprevus";
            amelioration = true;
        }
        else if ((SectionSelectionnee == "Palissades") && ListParcelle[ParcelleSlectionnee].NiveauPalissade < 3)
        {
            ListParcelle[ParcelleSlectionnee].NiveauPalissade += 1;
            ButAmelioration = "Protege vos plantes";
            amelioration = true;
        }
        else if ((SectionSelectionnee == "Robots-Travailleurs") && ListParcelle[ParcelleSlectionnee].NiveauRobots < 3)
        {
            ListParcelle[ParcelleSlectionnee].NiveauRobots += 1;
            ButAmelioration = "Augmente les récolotes";
            amelioration = true;
        }
        return amelioration;
    }

    //marche
    public void Acheter()//ACHETER DES COMPOSANTS AU MARCHE
    {
        //achat par pack
        Composants["boulons"] -= Constantes.PackAchat * Constantes.CoutBoulons[SectionSelectionnee];
        Composants[SectionSelectionnee] += Constantes.PackAchat;
    }
    public void VendreRecolte()//VENDRE SA RECOLTE POUR DES BOULONS
    {
        {
            int totalArgent = 0;
            foreach (var fruit in FruitsProduits.Keys.ToList())
            {
                int quantite = FruitsProduits[fruit];
                int gainUnitaire = Constantes.FruitsGain.ContainsKey(fruit) ? Constantes.FruitsGain[fruit] : 0;

                totalArgent += quantite * gainUnitaire;
                // Réinitialiser le stock à 0
                FruitsProduits[fruit] = 0;
            }
            Composants["boulons"] += totalArgent;
        }
    }

    //piege
    public bool PiegePossibleUtile()
    {
        bool possible = false;
        char niveauPiegeSelectionne = SectionSelectionnee[SectionSelectionnee.Length - 1];
        int niveauPiegeSelectionneInt = int.Parse(niveauPiegeSelectionne.ToString());
        //si on a assez de composants pour le piege et que le piege est de niveau suffisant contre la creature
        if ((CreatureMonde.Niveau <= niveauPiegeSelectionneInt) && PeutConstruire(Constantes.PiegeNecessaireConstruction[SectionSelectionnee]))
        {
            possible = true;
        }
        return possible;
    }
    //En cours
    public void ArroserPlante(int mois) //ARROSER LA PLANTE SÉLECTIONNÉE
    {
        var plante = ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]];
        if (plante != null)
        {
            int litresSouhaites = plante.PreparerArrosagePlante();
            if (Composants["boulons"] < litresSouhaites * Constantes.CoutBoulons["litreHuile"])
                Console.WriteLine("Vous n'avez pas assez de boulons pour faire cette action.");
            else
            {
                Composants["boulons"] = -litresSouhaites * Constantes.CoutBoulons["litreHuile"];
                plante.ArroserPlante(litresSouhaites, mois, ListParcelle[ParcelleSlectionnee]);
            }
        }
    }
    public void ArroserTout() //ARROSER TOUTES LES PLANTES
    {
        int coutArroserTout = 0;
        foreach (Parcelle parcelle in ListParcelle)
        {
            coutArroserTout += parcelle.CalculerCoutArrosageParcelle(Composants["boulons"], Mois); //on calcule le cout pour tout arroser
        }
        if (Composants["boulons"] >= coutArroserTout)
        {  //si on a assez de boulons pour tout arroser 
            Console.WriteLine("Cette action vous coûtera " + coutArroserTout + " boulons. Voulez-vous la réaliser ? Entrée pour valider, E pour annuler"); //vérifier que la personne veut bien arroser avec le prix
            ConsoleKeyInfo touche = Console.ReadKey(true);
            while ((touche.Key != ConsoleKey.Enter) || (touche.Key != ConsoleKey.E)) //s'assurer qu'ils pressent pas la mauvaise touche
            {
                Console.WriteLine("Erreur : mauvaise touche. Veuillez presser entrée pour valider, E pour annuler.");
                touche = Console.ReadKey(true);

            }
            if (touche.Key == ConsoleKey.Enter)
            {
                foreach (Parcelle parcelle in ListParcelle) parcelle.ArroserParcelle(Mois); //arroser les parcelles
                Composants["boulons"] -= coutArroserTout; //faire payer
            }
            else Console.WriteLine("Action annulée.");
        }
        else Console.WriteLine("Vous n'avez pas assez de boulons pour réaliser cette action. Action annulée.");
    }
    public void ArroserParcelleSelectionnee() //ARROSER LA PARCELLE
    {
        //selectionnerParcelle
        int index = 0;
        int coutArroser = ListParcelle[index].CalculerCoutArrosageParcelle(Composants["boulons"], Mois);

        //Si on a assez de boulons, proposer d'arroser en montrant le prix 
        if (Composants["boulons"] >= coutArroser)
        {
            Console.WriteLine("Cette action vous coûtera " + coutArroser + " boulons. Voulez-vous la réaliser ? Entrée pour valider, E pour annuler"); //vérifier que la personne veut bien arroser avec le prix
            ConsoleKeyInfo touche = Console.ReadKey(true);
            while ((touche.Key != ConsoleKey.Enter) || (touche.Key != ConsoleKey.E)) //s'assurer qu'ils pressent pas la mauvaise touche
            {
                Console.WriteLine("Erreur : mauvaise touche. Veuillez presser entrée pour valider, E pour annuler.");
                touche = Console.ReadKey(true);

            }
            if (touche.Key == ConsoleKey.Enter)
            {
                ListParcelle[index].ArroserParcelle(Mois); //arroser la parcelle
                Composants["boulons"] -= coutArroser; //faire payer
            }
            else Console.WriteLine("Action annulée.");
        }

        //Sinon, annuler l'action. 
        else Console.WriteLine("Vous n'avez pas assez de boulons pour réaliser cette action. Action annulée.");

    }
    public void EclairerPlante()
    {
        var plante = ListParcelle[ParcelleSlectionnee].MatricePlantes[CaseSelectionnee[0], CaseSelectionnee[1]];
        if (plante != null)
        {
            //Console.WriteLine("Souhaitez-vous utiliser une p")

        }
    }
}
