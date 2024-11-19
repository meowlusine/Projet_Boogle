using System;
using System.IO;
using Projet_Boogle;

class Program
{
    public static Dictionary<string, int[]>? Lettres; // un dictionnaire avec toutes les lettres, leur nombre et leur poids

    #region Methodes de mise en forme des fichiers 
    public static string LireFichier(string fichier)
    {
        string fichierString = File.ReadAllText(fichier);
        return fichierString;
    }

    /// <summary>
    /// transforme le string de lettre en un dictionnaire de la forme {"A":[4,3],...}
    /// avec en clé la lettre et en valeur un tableau de int contenant le nombre et le poids de la lettre
    /// </summary>
    /// <param name="fichierLettre"> le texte extrait de Lettre.txt</param>
    /// <returns>Dictionnaire des lettres</returns>
    public static Dictionary<string, int[]> StringLettresToDico(string fichierLettre)
    {
        Dictionary<string, int[]> Lettres = new Dictionary<string, int[]>();
        string[] chaque_lettre = fichierLettre.Split("\n");
        foreach (string lettre in chaque_lettre)
        {
            int[] nombre_poids = new int[2];
            nombre_poids[0] = Convert.ToInt32(lettre[2]);
            nombre_poids[1] = Convert.ToInt32(lettre[4]);
            Lettres.Add(Convert.ToString(lettre[0]), nombre_poids);
        }
        return Lettres;
    }


    /// <summary>
    ///  En fonction de la langue, extrait le contenu du fichier de mots correpspondant et le met sous 
    ///  forme de liste de string
    /// </summary>
    /// <param name="langue"> la langue utilisée ( "fr" ou "en")</param>
    /// <returns>Liste de string d'un dictionnaire de mots ( fr ou en ) </returns>
    public static List<string> transformation_Dico(string langue)
    {

        // en fonction de la langue, on prend le fichier correspondant et on le transforme en List de mots
        List<string> liste_mots = new List<string>();
        if (langue == "fr")
        {
            liste_mots = Program.LireFichier("MotsPossiblesFR.txt").Split(" ").ToList(); // string -> string[] -> List<string>
        }
        else
        {
            liste_mots = Program.LireFichier("MotsPossiblesEN.txt").Split(" ").ToList();
        }

        return liste_mots;
    }

    #endregion

    #region Tests
    public static void TestDe()
    {
        string fichierLettre = LireFichier("Lettres.txt");
        Lettres = StringLettresToDico(fichierLettre);

        De dé = new De();
        dé.Lance();
        Console.WriteLine(dé.toString());
        Console.ReadLine();
    }

    public static void Test_tris()
    {
        List<string> liste_mots = new List<string> { "banane", "pomme", "orange", "kiwi", "ananas", "fraise", "cerise", "abricot", "mangue", "raisin", "poire", "grenade", "figue", "prune", "datte" };
        List<string> liste_mots_selection = new List<string>(liste_mots);
        List<string> liste_mots_rapide = new List<string>(liste_mots);
        List<string> liste_mots_fusion = new List<string>(liste_mots);

        tri_selection(liste_mots_selection);
        foreach (string s in liste_mots_selection)
        {
            Console.Write(s + ", ");
        }
        Console.WriteLine();

        tri_rapide(liste_mots_rapide, 0, liste_mots_rapide.Count - 1);
        foreach (string s in liste_mots_rapide)
        {
            Console.Write(s + ", ");
        }
        Console.WriteLine();

        liste_mots_fusion = tri_fusion(liste_mots_fusion);
        foreach (string s in liste_mots_fusion)
        {
            Console.Write(s + ", ");
        }
        Console.WriteLine();

        Console.ReadLine();
    }

    public static void TestDictionnaire()
    {
        Dictionnaire dicoFr = new Dictionnaire("fr");
        Console.WriteLine(dicoFr.toString());
        string motCherche1 = "CHAMPIGNONS";
        string motCherche2 = "CITRON";
        Console.WriteLine($"Recherche du mot '{motCherche1}' : " + dicoFr.RechDicoRecursif(motCherche1, dicoFr.Liste_mots));
        Console.WriteLine($"Recherche du mot '{motCherche2}' : " + dicoFr.RechDicoRecursif(motCherche2, dicoFr.Liste_mots));


        Dictionnaire dicoEn = new Dictionnaire("en");
        Console.WriteLine("\n=== Test de la méthode toString() pour l'anglais ===");
        Console.WriteLine(dicoEn.toString());
        Console.ReadLine();
    }



    #endregion

    #region Tris

    #region Tri selection
    public static void tri_selection(List<string> liste_mots)
    {
        for (int i = 0; i < liste_mots.Count - 1; i++)
        {
            int minimum = i;
            for (int j = i + 1; j < liste_mots.Count; j++)
            {   
                if (string.Compare(liste_mots[j], liste_mots[minimum]) < 0)
                {   
                    minimum = j;
                }      
            }
            if (minimum != i)
            {
                string c = liste_mots[minimum];
                liste_mots[minimum] = liste_mots[i];
                liste_mots[i] = c;
            }
        }
    }

    #endregion

    #region Tri rapide
    public static int partitionner(List<string> liste_mots, int premier, int dernier){
        string pivot = liste_mots[dernier];
        int i = premier-1;
        for(int j=premier; j<=dernier-1; j++)
        {
            if (string.Compare(liste_mots[j], pivot)<0)
            {
                i++;
                string c = liste_mots[i];
                liste_mots[i] = liste_mots[j];
                liste_mots[j] = c;
            }
        }
        string d = liste_mots[dernier];
        liste_mots[dernier] = liste_mots[i + 1];
        liste_mots[i + 1] = d;
        return i + 1;
    }

    public static void tri_rapide(List<string> liste_mots, int premier, int dernier)
    { 
        if (premier < dernier)
        {
            int part = partitionner(liste_mots, premier, dernier);
            tri_rapide(liste_mots, premier, part - 1);
            tri_rapide(liste_mots, part+1, dernier);
        }
    }

    #endregion

    #region Tri fusion
    public static List<string> fusion(List<string> tab1, List<string> tab2)
    {
        List<string> res = new List<string>();
        int i = 0;
        int j = 0;
        while(i < tab1.Count && j < tab2.Count)
        {
            if (string.Compare(tab1[i], tab2[j]) <= 0)
            {
                res.Add(tab1[i]);
                i++;
            }
            else
            {
                res.Add(tab2[j]);
                j++;
            }
        }
        res.AddRange(tab1.GetRange(i, tab1.Count - i));
        res.AddRange(tab2.GetRange(j, tab2.Count - j));

        return res;
    }

    public static List<string> tri_fusion(List<string> liste_mots)
    {
        if ( liste_mots.Count <=1)
        {
            return liste_mots;
        }
        else
        {
            int milieu = liste_mots.Count / 2;
            List<string> tab1 = liste_mots.GetRange(0, milieu); 
            List<string> tab2 = liste_mots.GetRange(milieu, liste_mots.Count - milieu); 
            return fusion(tri_fusion(tab1), tri_fusion(tab2));
        }
    }
    #endregion

    #endregion

    

    public static void Main(string[] args)
    {
        //TestDe();
        //Test_tris();
        //TestDictionnaire();
    }
}