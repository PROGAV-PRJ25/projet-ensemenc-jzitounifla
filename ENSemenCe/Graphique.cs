public static class Graphique
{
    //Texte
    public const string Titre = "ENSemenCe";

    //Palette de couleurs pour les différents composants
    public static readonly Dictionary<string, ConsoleColor> Palette = new Dictionary<string, ConsoleColor>{
            //Pieces 11+1*3+2, dephasage : + si grand, negatif si petit
            { "Titre",ConsoleColor.Green  },
            { "Cloture",ConsoleColor.Magenta  },
            { "Arroseur",ConsoleColor.Cyan },
            { "Palissade",ConsoleColor.White },
            { "TexteMenu",ConsoleColor.White  },
            { "MenuSelectionne",ConsoleColor.White  },
            { "Message",ConsoleColor.Yellow  },
            { "Composants",ConsoleColor.White},
            { "Mois",ConsoleColor.Blue },
            { "Information",ConsoleColor.Green  },
            { "Robots",ConsoleColor.Cyan },
            { "Fleur", ConsoleColor.White},
            { "Fruit",ConsoleColor.White  },
            { "CurseurNon",ConsoleColor.Red  },
            { "CurseurOui",ConsoleColor.Green  },
            { "Creature",ConsoleColor.Red  },
            { "EtatPlante1",ConsoleColor.Red  },
            { "EtatPlante2",ConsoleColor.Yellow  },
            { "EtatPlante3",ConsoleColor.Green  },
    };

    //EQUIPEMENTS PARCELLE

    //Design Robots le niveau
    public static readonly Dictionary<int, Dictionary<string, string>> RobotDesign = new Dictionary<int, Dictionary<string, string>> {
    { 1, new Dictionary<string, string> {
        { "tete", "◉" },
        { "corps", "|" },
        { "pieds", "└┘" }
    }},
    { 2, new Dictionary<string, string> {
        { "tete", "⛶" },
        { "corps", "║" },
        { "pieds", "╚╝" }
    }},
    { 3, new Dictionary<string, string> {
        { "tete", "⌐■-■" },
        { "corps", "▓" },
        { "pieds", "██" }
    }}
    };

    //Design Cloture selon le niveau
    public static readonly Dictionary<int, Dictionary<string, string>> ClotureDesign = new Dictionary<int, Dictionary<string, string>> {
    // Pièces 11+1*3+2, déphasage : + si grand, négatif si petit
    { 1, new Dictionary<string, string> {
        { "horizontal", "-" },
        { "vertical", "|" },
        { "angleHautGauche", "┌"},
        { "angleHautDroit",  "┐" },
        { "angleBasGauche", "└" },
        { "angleBasDroit", "┘" }
    }},

    { 2, new Dictionary<string, string> {
        { "horizontal", "-┼" },
        { "vertical", "|┼" },
        { "angleHautGauche", "┌"},
        { "angleHautDroit",  "┐" },
        { "angleBasGauche", "└" },
        { "angleBasDroit", "┘" }
    }},

    { 3, new Dictionary<string, string> {
        { "horizontal", "▓" },
        { "vertical",   "▒" },
        { "angleHautGauche", "╞" },
        { "angleHautDroit",  "╡" },
        { "angleBasGauche",  "╘" },
        { "angleBasDroit",   "╛" }
}}

};
    //Design Palissade
    public static readonly Dictionary<int, Dictionary<string, string>> PalissadeDesign = new Dictionary<int, Dictionary<string, string>> {
    // Pièces 11+1*3+2, déphasage : + si grand, négatif si petit
    { 1, new Dictionary<string, string> {
        { "horizontal", " " },
        { "vertical", " " },
    }},

    { 2, new Dictionary<string, string> {
        { "horizontal", "- " },
        { "vertical", "| " },
    }},

    { 3, new Dictionary<string, string> {
        { "horizontal", "═ " },
        { "vertical", "║ " },
    }}
};

    //Design Arroseur selon le niveau
    public static readonly Dictionary<int, string> ArroseurDesign = new Dictionary<int, string>
    {
        {1,"."},
        {2,"╬"},
        {3,"¤"},
    };
    //nombre de case entre arroseur en X
    public static readonly Dictionary<int, int> ArroseurFrequenceX = new Dictionary<int, int>{
        {1,2},
        {2,2},
        {3,1},
    };
    //nombre de case entre arroseur en Y
    public static readonly Dictionary<int, int> ArroseurFrequenceY = new Dictionary<int, int>{
        {1,2},
        {2,2},
        {3,1},
    };

    //DESIGN Creature :
    public static readonly Dictionary<int, string[]> CreaturesDesign = new() {
    {1,["        ", " -----► ", "    |   ", "        " ]},
    {2,["        ", "    ►   ", "    |   ", "   __   " ]},
    {3,["||||||||", "| -  - |", "|      |", "| ---- |" ]},
    {4,["   Ø►   ", "    █   ", "   ▄█   ", "   |    " ]},
};

    //DESIGN FEE
    public static string[] FeeDesign = {
    "  /\\*/\\ ",
    " <(o o)>",
    "  \\ ^ / ",
    "  /_~_\\ "
    };

    //DESIGN PLANTES :
    public static readonly Dictionary<string, string[]> PlantesDesign = new() {
    {"Margu-ee-rite", ["❋ ❋ ❋ ❋ "," ❋ ❋ ❋ ❋","❋ ❋ ❋ ❋ "," ❋ ❋ ❋ ❋"]},
    {"Rob-ose", ["   ❀❀❀  ","  ❀ ❀ ❀ ","   ❀❀❀  ","    |   "]},
    {"LierR3",        ["  /\\/\\/ "," /\\   /\\","    /\\/ ","  \\/\\/\\/"]},
    {"PoMWier", ["  ◦⁃⁃◦  "," ◉◦◦◦◦◉ ","  ◦⁃⁃◦  ","   ∐∐   "]},
    {"Po1rier",["   ⌘⌘   ","  ⌘●●⌘  ","   ⌘⌘   ","   ||   "]},
};

    //placement des elements
    public const int YGrilleStart = 3;
    public const int MargeGauche = 10;
    public const int MargeAvantMenu = 10;
    public const int MargeDroite = 20;

    //taille case
    public const int YTailleCase = 4;
    public const int XTailleCase = 2 * YTailleCase;
    public static int YConsole = 0;
    //intercase
    public const int YInterCase = 1;
    public const int XInterCase = 1;

    public static void SauterNLigne(int n)//SAUTE N LIGNE
    {
        for (int i = 0; i < n; i++)
        {
            Console.Write("\n");
        }
    }
    public static void TracerPatternLongueurN(string pattern, int n, ConsoleColor couleur = ConsoleColor.White)//TRACE UN PATTERN SUR UNE LONGUEUR N DE COULEUR
    {
        Console.ForegroundColor = couleur;
        for (int i = 0; i < n / pattern.Length; i++)
        {
            Console.Write(pattern);
        }
        Console.Write(pattern.Substring(0, n % pattern.Length));
    }
    public static void TracerTitreEncadre(string texte, int tailleGrilleCaractere)//TRACE TITRE ENCADRE
    {
        int longueur = MargeGauche + tailleGrilleCaractere + MargeAvantMenu + MargeDroite;
        int intervalLettre = 3;
        ConsoleColor couleur = Palette["Titre"];
        SauterNLigne(2);
        Graphique.TracerPatternLongueurN("-", longueur, couleur);
        SauterNLigne(2);
        //Titre
        Graphique.TracerPatternLongueurN(" ", (longueur - (intervalLettre + 1) * (texte.Length - 1) + 1) / 2, couleur);
        for (int i = 0; i < texte.Length; i++)
        {
            Console.Write(texte[i]);
            Graphique.TracerPatternLongueurN(" ", intervalLettre, couleur);
        }
        //2 eme ligne
        SauterNLigne(2);
        Graphique.TracerPatternLongueurN("-", longueur, couleur);
        SauterNLigne(2);
    }
    public static void TracerClotureCote(int niveauCloture, int yConsole = 0)//TRACE UN MORCEAU DE CLOTURE SELON L EMPLACEMENT DE LA CASE
    {
        Console.ForegroundColor = Palette["Cloture"];
        Console.Write(ClotureDesign[niveauCloture]["vertical"][(yConsole - Graphique.YGrilleStart) % ClotureDesign[niveauCloture]["horizontal"].Length]);
    }
    public static void TracerClotureHautBas(int niveauCloture, int longueur, string hauteur)//TRACE UNE LIGNE HORIZONTAL DE CLOTURE
    {
        Console.ForegroundColor = Graphique.Palette["Cloture"];
        Console.Write(ClotureDesign[niveauCloture]["angle" + hauteur + "Gauche"]);
        TracerPatternLongueurN(ClotureDesign[niveauCloture]["horizontal"], longueur - 2, Graphique.Palette["Cloture"]);
        Console.Write(ClotureDesign[niveauCloture]["angle" + hauteur + "Droit"]);
    }

    public static void TracerInterligneAroseur(bool arroseur, int niveauArroseur, int niveauPalissade, int longueurTotal)//TRACE INTERLIGNE EN PRENANT EN COMPTE LA FREQUENCE DES ARROSEUR ET LES PALISSADES
    {
        int nombreArroseur = longueurTotal / ((Graphique.XTailleCase + 1) * ArroseurFrequenceX[niveauArroseur]);
        if (nombreArroseur != 0)
        {
            for (int i = 0; i < nombreArroseur; i++)
            {
                Console.ForegroundColor = Palette["Palissade"];
                for (int j = 0; j < ArroseurFrequenceX[niveauArroseur] - 1; j++)
                {
                    //palissade entre les intercase
                    TracerPatternLongueurN(PalissadeDesign[niveauPalissade]["horizontal"], Graphique.XTailleCase);
                    //pas de palissade au niveau des intercase
                    Console.Write(" ");
                }
                TracerPatternLongueurN(PalissadeDesign[niveauPalissade]["horizontal"], Graphique.XTailleCase);
                //dessine l arroseur
                if (arroseur)
                {
                    Console.ForegroundColor = Palette["Arroseur"];
                    Console.Write(ArroseurDesign[niveauArroseur]);
                }
                //intercase sans arroseur
                else
                    Console.Write(" ");
            }
        }
        //dessine ce qu il y a apres le dernier arroseur
        Console.ForegroundColor = Palette["Palissade"];
        int nombrePalissadeRestante = (longueurTotal - nombreArroseur * ((Graphique.XTailleCase + 1) * ArroseurFrequenceX[niveauArroseur])) / (Graphique.XTailleCase + 1);
        for (int i = 0; i < nombrePalissadeRestante; i++)
        {
            TracerPatternLongueurN(PalissadeDesign[niveauPalissade]["horizontal"], Graphique.XTailleCase);
            Console.Write(" ");
        }
        //dessine les palissades restantes
        TracerPatternLongueurN(PalissadeDesign[niveauPalissade]["horizontal"], Graphique.XTailleCase);
    }
    public static void TracerPalissadeVertical(int niveauPalissade, int yCase)//TRACE UN MORCEAU DE PALLISSADE A LA VERTICAL SELON LA POSITION
    {
        Console.ForegroundColor = Palette["Palissade"];
        Console.Write(PalissadeDesign[niveauPalissade]["vertical"][yCase % PalissadeDesign[niveauPalissade]["vertical"].Length]);
    }

    public static void AfficherDictionnaire(Dictionary<string, int> dic, ConsoleColor couleur)//AFFICHE UN DICTIONNAIRE AVEC CLE VALEUR SUR UNE MEME LIGNE
    {
        Console.ForegroundColor = couleur;
        foreach (var pair in dic)
        {
            TracerPatternLongueurN(" ", 3, couleur);
            Console.Write(pair.Key + " : " + pair.Value);
        }
    }
    public static void AfficherRobot(int niveau)//AFFICHE LE ROBOT
    {
        Console.ForegroundColor = Palette["Robots"];
        Console.WriteLine("  " + RobotDesign[niveau]["tete"] + "  ");
        Console.WriteLine("  " + RobotDesign[niveau]["corps"] + "  ");
        Console.WriteLine("  " + RobotDesign[niveau]["pieds"] + "  ");
    }

};


//Brouillon design
// _____|❋❋❋ |______
// _____| ❋❋❋|______

// _____|❋ ❋ ❋ ❋ |______
// _____| ❋ ❋ ❋ ❋|______
// _____|❋ ❋ ❋ ❋ |______
// _____| ❋ ❋ ❋ ❋|______

// _____|✿ ✿ |______
// _____| ✿ ✿|______

// _____|[﹥]＜|______
// _____|＜]﹥[|______

// _____|❦☐❦☐|______
// _____|■❦■❦|______

// _____|◎☐◎☐|______
// _____|■◎■◎|______

// _____|  ◦⁃⁃◦  |______
// _____| ◉◦◦◦◦◉ |______
// _____|  ◦⁃⁃◦  |______
// _____|   ∐∐   |______