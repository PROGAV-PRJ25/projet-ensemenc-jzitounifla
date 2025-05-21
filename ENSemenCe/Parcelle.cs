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
    public void ArroserParcelle()
    {
        int lignes = MatricePlantes.GetLength(0);
        int colonnes = MatricePlantes.GetLength(1);
        for (int i = 0; i < lignes; i++)
        {
            for (int j = 0; j < colonnes; j++)
            {
                Plante plante = MatricePlantes[i, j];
                if (plante != null) //on vérifie s'il y a une plante à cet endroit là 
                {
                    int a = 1;
                    //on pourrra remplacer le int a par : Plante.Arroser(NiveauArroseur);
                }
            }
        }
    }
}