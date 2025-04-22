public class Parcelle
{
    //Numero et plantes
    public Plante[,] MatricePlantes { get; set; }
    public int IndexParcelle { get; set; }
    public static int NbParcelle { get; set; } = 0;

    //propriete de la parcelle

    //amelioration
    public int NiveauCloture { get; set; }
    public int NiveauArroseur { get; set; }
    public int NiveauPanneau { get; set; }
    public int NiveauPalissade { get; set; }
    public int NbRobot { get; set; }
    public Parcelle(int dimX, int dimY)
    {   //
        MatricePlantes = new Plante[dimX, dimY];
        //non ameliore au debut
        NiveauCloture = 1;
        NiveauArroseur = 1;
        NiveauPanneau = 1;
        NiveauPalissade = 1;
        NbRobot = 0;

        IndexParcelle = NbParcelle;
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
                if (plante != null)
                {
                    int a = 1;
                    //Plante.Arroser(NiveauArroseur);
                }
            }
        }
    }
}