using System;
using System.IO;
using Projet_Boogle;

class Program
{
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

    public static Dictionary<string, int[]>? Lettres; // un dictionnaire avec toutes les lettres, leur nombre et leur poids

    public static void TestDe()
    {
        string fichierLettre = LireFichier("Lettres.txt");
        Lettres = StringLettresToDico(fichierLettre);

        De dé = new De();
        dé.Lance();
        Console.WriteLine(dé.toString());
        Console.ReadLine();
    }
    public static void Main(string[] args)
    {
        //TestDe();
    }
}