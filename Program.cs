using System;
using System.Collections.Generic;
using System.IO;
using Projet_Boogle;

class Program
{

    //Ce fichier contient des méthodes utiles pour la traitement des fichiers et les différents types de tris demandés
   

    #region Methodes de mise en forme des fichiers 
    public static string LireFichier(string fichier)
    {
        string fichierString = File.ReadAllText(fichier);
        return fichierString;
    }

    /// <summary>
    /// transforme le string de lettre en un dictionnaire de la forme {'A':[4,3],...}
    /// avec en clé la lettre et en valeur un tableau de int contenant  le poids et le nombre de la lettre
    /// </summary>
    /// <param name="fichierLettre"> le texte extrait de Lettre.txt</param>
    /// <returns>Dictionnaire des lettres</returns>
    public static Dictionary<char, int[]> StringLettresToDico(string fichierLettre)
    {
        Dictionary<char, int[]> Lettres = new Dictionary<char, int[]>();

        
        string[] chaque_ligne = fichierLettre.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        foreach (string ligne in chaque_ligne)
        {
            
            string[] parties = ligne.Split(';');
            char lettre = Convert.ToChar(parties[0]); 
            int poids = Convert.ToInt32(parties[1]); 
            int nombre = Convert.ToInt32(parties[2]); 

            Lettres.Add(lettre, new int[] { poids, nombre }); 

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

        
        List<string> liste_mots = new List<string>();
        if (langue == "fr")
        {
            liste_mots = Program.LireFichier("MotsPossiblesFR.txt").Split(" ").ToList(); 
        }
        else
        {
            liste_mots = Program.LireFichier("MotsPossiblesEN.txt").Split(" ").ToList();
        }

        return liste_mots;
    }

    #endregion


    #region Tris

    #region Tri selection

    /// <summary>
    /// tri une liste de string 
    /// </summary>
    /// <param name="liste_mots"></param>
    /// <returns>void</returns>
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

    /// <summary>
    /// tri une liste de string
    /// </summary>
    /// <param name="liste_mots"></param>
    /// <param name="premier">0</param>
    /// <param name="dernier">taille de la liste</param>
    /// <returns>void</returns>
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


    /// <summary>
    /// tri une liste de string
    /// </summary>
    /// <param name="liste_mots"></param>
    /// <returns>void</returns>
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

    

}
