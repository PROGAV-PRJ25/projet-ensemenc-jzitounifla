public static class Constantes
{
    public static readonly Dictionary<string, Dictionary<int, string>> Menus = new Dictionary<string, Dictionary<int, string>> {
    // Pièces 11+1*3+2, déphasage : + si grand, négatif si petit
    { "MenuGeneral", new Dictionary<int, string> {
        { 6, "Planter" },
        { 8, "Arroser" },
        { 14, "Retour"},
    }}};


    //Tous les types de plantes
    public static readonly Dictionary<string, int> PlantesDureeVie = new Dictionary<string, int> { //Leur durée de vie
      {"Margu-ee-rite", 4},
      {"Rob-ose", 12},
      {"LierR3", 12*10},
      {"PoMWier", 12*5},
      {"Po1rier",12*5}



    };

}