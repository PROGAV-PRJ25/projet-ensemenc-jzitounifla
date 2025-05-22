// See https://aka.ms/new-console-template for more information

Console.Clear();

//Initialisation : 
Console.WriteLine("\n\n\n\n\t\t\t\t\t\tBienvenue dans ENSemenCe !");
Console.WriteLine("Pour votre confort, veuillez étendre la console au maximum, et dézoomer un petit peu. Merci ! \nUne fois que cela est fait, PRESSEZ ENTREE");
ConsoleKeyInfo touche = Console.ReadKey();
while (touche.Key != ConsoleKey.Enter)
    touche = Console.ReadKey();


Console.Clear();
Console.WriteLine("\n\n\n\n\t\t\t\t\t\tBienvenue dans ENSemenCe ! \n\n\t\t\t\t\t\t D'abord, expliquons rapidement le jeu : ");
Console.WriteLine("\nAlors que toute créature vivante a déserté ce monde décharné, les robots qui le peuplent se sont pris de passion pour le jardinage. Depuis que ce nouveau hobby s'est développé, de nombreux mini robots-plantes sont apparus. Parmi eux, le Rob-ose par exemple, ou le Pomwier, qui donne même des fruits !! ");
Console.WriteLine("\nVous êtes un droïde jardinier en herbe (ou plutot en métal), et vous voulez vous aussi vous lancer dans le jardinage mécanique. Pour ce faire, vous avez commandé le kit ENSemenCe, qui vous offre les premiers outils d'un jardinier ! Profitez de votre nouveau potager, et faites attention à bien prendre soin de vos plantes... ");
System.Threading.Thread.Sleep(1000);

Console.WriteLine("\n\nPRESSEZ ENTREE POUR AFFICHER LES RÈGLES");
touche = Console.ReadKey();
while (touche.Key != ConsoleKey.Enter)
    touche = Console.ReadKey();

//AFFICHER LES RÈGLES
Console.Clear();
Console.WriteLine("\n\n\n\n\t\t\t\t\t\tBienvenue dans ENSemenCe !\n\n\n");
Console.WriteLine("\t\t\t\t——————————*****—————————— ENSEMENCE ——————————*****—————————— ");
Console.WriteLine("\t\t\t\t                       RÈGLES DU JEU\n\n\n");
Console.WriteLine("- Les plantes robotiques ont un état des besoins en huile, en électricité et en UV bien précis. Selon le respect de ces conditions, elles ont un état entre 0 et 3. Si l'état de la plante tombe à 0, elle meurt.");
Console.WriteLine("- Vous pouvez récolter le fruit de votre labeur sous la forme de fruits métalliques, fleurs de ferraille... et les vendre au marché pour récupérer des ressources.");
Console.WriteLine("- N'hésitez pas à économiser votre butin !! Quelque fois surgit un individu étrange dont le cœur semble battre, et qui vient piller votre ferraille si précieuse. Vous aurez besion d'un piège pour l'attraper, et pour en acheter un vous aurez besoin de suffisemment de boulons ! ");
Console.WriteLine("- De même, il arrive que vos bébés robotiques soit attaqués par un virus qui leur fait vriller les circuits. Allez donc acheter un antivirus au supermaché.");

System.Threading.Thread.Sleep(5000);

Console.WriteLine("\n\nPRESSEZ ENTREE POUR COMMENCER LE JEU");
touche = Console.ReadKey();
while (touche.Key != ConsoleKey.Enter)
    touche = Console.ReadKey();

Console.Clear();
//rajouter une sécurité sur la taille du plateau
Monde m1 = new Monde(dimX: 10, dimY: 6, coefficientPieceDepart: 10);
m1.Jouer();