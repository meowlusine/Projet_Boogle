using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle
{
    internal class Dictionnaire
    {
        private string langue = "fr";
        private List<string> liste_mots = new List<string>();



        #region Constructeur
        public Dictionnaire(string langue)
        {
            this.langue = langue;
            this.liste_mots = Program.transformation_Dico(langue);
            this.liste_mots = Program.tri_fusion(liste_mots);
        }
        #endregion

        #region Propriétés
        public List<string> Liste_mots
        {
            get { return this.liste_mots; }
        }

        #endregion

        #region Méthodes
        
        /// <summary>
        /// Une méthode qui renvoie un string qui décrit le dictionnaire selon le nombre de mots par longueur,
        /// le nombre de mots par lettre et enfin par langue
        /// </summary>
        /// <returns>description du dictionnaire</returns>
        public string toString()
        {
            Dictionary<int, int> nb_mots_longueur = new Dictionary<int, int>();
            Dictionary<string, int> nb_mots_lettre = new Dictionary<string, int>();

            foreach (string mot in this.liste_mots)
            {
                int taille = mot.Length;
                if (taille >= 1)
                {
                    string lettre = Convert.ToString(mot[0]);
                    if (nb_mots_longueur.ContainsKey(taille))
                    {
                        nb_mots_longueur[taille]++;
                    }
                    else
                    {
                        nb_mots_longueur.Add(taille, 1);
                    }
                    if (nb_mots_lettre.ContainsKey(lettre))
                    {
                        nb_mots_lettre[lettre]++;
                    }
                    else
                    {
                        nb_mots_lettre.Add(lettre, 1);
                    }
                }    
            }
            string res = "Nombre de mots par lettre : \n";
            foreach (string lettre in nb_mots_lettre.Keys)
            {
                res += lettre + " : " + nb_mots_lettre[lettre] + "\n";
            }
            res += "\nNombre de mots par taille : \n";
            foreach (int taille in nb_mots_longueur.Keys)
            {
                res += taille + " : " + nb_mots_longueur[taille] + "\n";
            }

            return res + "\nLangue : " + this.langue;
        }

        /// <summary>
        /// On recherche si le mot entré en paramètre appartient au dictionnaire de façon récursive
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="liste_mots"></param>
        /// <returns>un booléen qui indique si le mot appartient au dico</returns>
        public bool RechDicoRecursif(string mot, List<string> liste_mots)
        {

            if (liste_mots.Count == 0)
            {
                return false;
            }
            if (liste_mots.Count == 1)
            {
                return string.Compare(mot, liste_mots[0]) == 0;
            }

            int milieu = liste_mots.Count / 2;
            string mot_milieu = liste_mots[milieu];

            
            int comparaison = string.Compare(mot, mot_milieu);

            if (comparaison == 0)
            {
                return true;
            }
                
            else if (comparaison < 0)
            {
               
                var gauche = liste_mots.GetRange(0, milieu);
                return RechDicoRecursif(mot, gauche);
            }
            else
            {
                
                var droite = liste_mots.GetRange(milieu + 1, liste_mots.Count - (milieu + 1));
                return RechDicoRecursif(mot, droite);
            }
        }

        /// <summary>
        /// recherche si le mot entré en paramètre appartient au dico en le parcourant du début à la fin
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="liste"></param>
        /// <returns>un booléen: vrai si le mot appartient au dico, faux sinon</returns>
        public static bool recherche_lineaire(string mot, List<string> liste)
            {
            foreach(string element in liste)
                {
                    if(element == mot)
                    {
                         return true;
                    }
        
                }
                return false;
            }
        #endregion
    }

}
