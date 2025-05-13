public static class Constantes
{
  public static readonly Dictionary<string, Dictionary<int, string>> Menus = new Dictionary<string, Dictionary<int, string>> {
    // Pièces 11+1*3+2, déphasage : + si grand, négatif si petit
    { "MenuGeneral", new Dictionary<int, string> {
        { 6, "Planter" },
        { 8, "Arroser" },
        { 14, "Retour"},
    }},
     { "Planter", new Dictionary<int, string> {
        { 6, "Planter1" },
        { 8, "Plante2" },
        { 14, "Plante3"},
    }}};


  //TYPES DE PLANTES : 

  //Durées de vie
  public static readonly Dictionary<string, int> PlantesDureeVie = new Dictionary<string, int> { //Leur durée de vie
      {"Margu-ee-rite", 4},
      {"Rob-ose", 12},
      {"LierR3", 12*10},
      {"PoMWier", 12*5},
      {"Po1rier",12*5}
    };

  //Description
  public static readonly Dictionary<string, string> PlantesDescription = new Dictionary<string, string> {
      {"Margu-ee-rite", "Cette jolie herbe folle éguaira votre potager. Attention toutefois à ne pas la laisser trop pousser… Elle pourrait bien envahir tout l’espace, et étouffer vos autres plantes."},
      {"Rob-ose", "Fleur iconique, la rob-ose est toutefois dure à entretenir. Elle est très appréciée à la vente mais attention à sa santé fragile, vous pourriez tout perdre !!"},
      {"LierR3", "Certes envahissant, ne négligez pas pour autant le LieR3 ! Ses longues lianes offrent de solides tiges de métal que vous saurez sans doute utiliser à votre avantage..."},
      {"PoMWier", "Fiable et constant, le pommier vous offre ses fruits tout au long de l'année. Certes, il est loin d'être le plus rare des arbres fruitiers, mais prenez bien soin de lui !! Un jour, un pommier pourrait vous illumier... "},
      {"Po1rier","Bien que ses fruits soient parfois gâtés, quand ils sont bons ils sont très appréciés, et se font à toutes les sauces. Rien de tel qu'un petit poirier pour mettre de l'originalité dans votre jardin !"}
    };

  public static readonly Dictionary<string, string> PlantesType = new Dictionary<string, string> {
      {"Margu-ee-rite", "Plante envahissante"},
      {"Rob-ose", "Fleur"},
      {"LierR3", "Plante envahissante"},
      {"PoMWier", "Arbre fruitier"},
      {"Po1rier","Arbre fruitier"}
    };

  public static readonly Dictionary<string, Dictionary<string, int>> PlantesNecessaireConstruction = new Dictionary<string, Dictionary<string, int>> {
      {"Margu-ee-rite", new Dictionary<string, int> {
        {"boulons", 0},
        {"plaque", 0},
        {"tige", 0},
        {"vis", 0}
      }},

      {"Rob-ose", new Dictionary<string, int> {
        {"boulons", 10},
        {"plaque", 0},
        {"tige", 1},
        {"vis", 1}
      }},
      {"LierR3", new Dictionary<string, int> {
        {"boulons", 10},
        {"plaque", 0},
        {"tige", 3},
        {"vis", 1}
      }},
      {"PoMWier", new Dictionary<string, int> {
        {"boulons", 20},
        {"plaque", 2},
        {"tige", 1},
        {"vis", 3}
      }},
      {"Po1rier",new Dictionary<string, int> {
        {"boulons", 25},
        {"plaque", 2},
        {"tige", 1},
        {"vis", 4}
      }}
    };
}