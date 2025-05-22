public class Parcelle
{
    //Identification de la parcelle 
    public string NomParcelle { get; set; }
    public string TypeParcelle { get; set; }

    //Numero et plantes

    public int IndexParcelle { get; set; }
    public static int NbParcelle { get; set; } = 0; //nombre total de parcelles du joueur > static et augmente à chaque nouvelle parcelle
    public Plante[,] MatricePlantes { get; set; }

    //propriete de la parcelle

    //amelioration
    public int NiveauCloture { get; set; }
    public int NiveauArroseur { get; set; }
    public int NiveauRobots { get; set; }
    public int NiveauPalissade { get; set; }

    //Différentes variables qui changent en fonction de la météo et du mois
    public int ElectrisationVar { get; set; }
    public int EnsoleillementVar { get; set; }
    public int HuileSolVar { get; set; }
    public int TemperatureVar { get; set; }
    public int RadioactiviteVar { get; set; }

    //CONSTRUCTEUR : 
    public Parcelle(int dimX, int dimY, string type)
    {   //
        MatricePlantes = new Plante[dimX, dimY];
        //non ameliore au debut
        NiveauCloture = 1;
        NiveauArroseur = 1;
        NiveauPalissade = 1;
        NiveauRobots = 1;
        TypeParcelle = type;
        IndexParcelle = NbParcelle;

        Console.WriteLine("Comment voulez-vous appeler votre parcelle ? Pressez seulement Entrée pour mettre le nom par défaut.");
        NomParcelle = Console.ReadLine()!;
        if (NomParcelle == "")
        {
            NomParcelle = "Parcelle n°" + IndexParcelle + 1 + "- " + TypeParcelle; //Nom par défaut = "Parcelle n°1 - Potager"
        }
        else NomParcelle = "Parcelle n°" + IndexParcelle + 1 + "- " + NomParcelle; //Nom choisi = "Parcelle n°1 - NomChoisi"
        NbParcelle++;
    }
    public int ArroserParcelle(int boulonsDispos, int mois)
    {
        int lignes = MatricePlantes.GetLength(0);
        int colonnes = MatricePlantes.GetLength(1);
        int qteArrosage = NiveauArroseur * 25 / 100; //Un arroseur niveau 1 arrose de 25%, niveau 2 50% et niveau 3 de 75% de l'huile manquante à la plante. 
        int litreParPlante;
        int nbPlantesArrosees = 0;

        for (int i = 0; i < lignes; i++)
        {
            for (int j = 0; j < colonnes; j++)
            {
                Plante plante = MatricePlantes[i, j];
                if (plante != null) //on vérifie s'il y a une plante à cet endroit là 
                {
                    litreParPlante = Convert.ToInt16(DonneesPlantes.PlantesRessources[plante.TypePlante]["huile"] - plante.NiveauHuile) * qteArrosage;
                    if (litreParPlante > 0)
                    {
                        nbPlantesArrosees++; //s'il faut l'arroser, on incrémente le nombre de plantes à arroser. 
                    }
                }
            }
        }

        int coutArrosage = nbPlantesArrosees * 5;
        if (coutArrosage <= boulonsDispos) //Arrose la parcelle si on a assez de boulons. Sinon, dead. 
        {

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    Plante plante = MatricePlantes[i, j];
                    if (plante != null) //on vérifie s'il y a une plante à cet endroit là 
                    {
                        litreParPlante = Convert.ToInt16(DonneesPlantes.PlantesRessources[plante.TypePlante]["huile"] - plante.NiveauHuile) * qteArrosage;
                        if (litreParPlante < 0)
                        {
                            litreParPlante = 0;
                        }
                        plante.ArroserPlante(litreParPlante, mois, this);
                    }
                }
            }
            return coutArrosage;
        }
        else
        {
            Console.WriteLine("Vous n'avez pas assez de boulons pour effectuer cette action.");
            return 0;
        }
    }
    public void ActuliserPousseMortRecolte(int mois, Monde monde)//LES ACTIONS EFFECTUER A LA FIN DE CHAQUE TOUR SUR LES PLANTE
    {
        int lignes = MatricePlantes.GetLength(0);
        int colonnes = MatricePlantes.GetLength(1);
        for (int i = 0; i < lignes; i++)
        {
            for (int j = 0; j < colonnes; j++)
            {
                if (MatricePlantes[i, j] != null) //on vérifie s'il y a une plante à cet endroit là 
                {
                    //pousse
                    MatricePlantes[i, j].Pousser(mois, this);
                    //recolte
                    if (MatricePlantes[i, j] is ArbreFruit arbreFruit)
                    {
                        // Tu peux maintenant utiliser `arbre` comme un objet ArbreFruit
                        arbreFruit.ProductionTour += 1;
                        if (arbreFruit.ProductionTour == arbreFruit.FrequenceProduction)
                        {
                            monde.FruitsProduits[arbreFruit.TypeFruits] += arbreFruit.NombreFruits + NiveauRobots;
                            arbreFruit.ProductionTour = 0;
                        }
                    }
                    MatricePlantes[i, j].CalculerEtatGeneral(mois, this);
                    //si la plante est morte
                    if (MatricePlantes[i, j].Etat == 0)
                    {
                        MatricePlantes[i, j] = null;
                    }

                }
            }
        }
    }
}