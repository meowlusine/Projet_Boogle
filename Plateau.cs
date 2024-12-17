using System;

using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle
{
    internal class Plateau
    {
        private De[] des;
        private char[,] lettresPlateau;
        private int taille;
        private Dictionnaire dico;
        private List<string> mot_trouvés_plateau;

        
        public Plateau( int taille, Dictionnaire dico)
        {
            this.des = new De[taille * taille];
            this.lettresPlateau = new char[taille, taille];

            //création d'un dictionnaire avec les lettres de l'alphabet et le nombre de fois qu'elles apparaissent sur le plateau
           
            Dictionary<char, int> apparence_lettre = new Dictionary<char, int>();
            for (char lettre = 'A'; lettre <= 'Z'; lettre++)
            {
                apparence_lettre[lettre] = 0;
            }


            for (int i = 0; i<taille; i++)
            {
                for (int j = 0; j<taille; j++)
                {
                    De de = new De();
                    de.Lance();
                    while(Jeu.Lettres[de.Lettre_visible][1] <= apparence_lettre[de.Lettre_visible])
                    {
                        de.Lance();
                    }
                    int indexDe = i * taille + j;
                    des[indexDe] = de;
                    lettresPlateau[i, j] = de.Lettre_visible;
                    apparence_lettre[de.Lettre_visible]++;
                }
            }
            this.dico = dico;
            this.taille=taille;
            this.mot_trouvés_plateau = new List<string>();
        }

        
        
        /// <summary>
        /// retourne le plateau écrit 
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string a = "";
            for(int i=0; i<taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    a += lettresPlateau[i, j];
                    a += " ";
                }
                a += "\n";
            }
            return a;
        }

        public bool Test_Plateau(string mot)
        {
            if(mot.Length < 2)
            {
                return false;
            }
            foreach(string mot_trouve in this.mot_trouvés_plateau)
            {
                if(mot_trouve == mot)
                {
                    return false;
                }
            }
            if (dico.RechDicoRecursif(mot,dico.Liste_mots))
            {
                for (int i = 0; i < this.taille; i++)
                {
                    for (int j = 0; j < this.taille; j++)
                    {
                        if (RechercheMot(mot, 0, i, j, new bool[this.taille, this.taille]))
                        {
                            this.mot_trouvés_plateau.Add(mot);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool RechercheMot(string mot, int indiceLettre, int i, int j, bool[,] visite)
        {
            if(i<0 || j<0 || i>=this.taille || j>=this.taille || visite[i,j])
            {
                return false;
            }

            if (this.lettresPlateau[i, j] != mot[indiceLettre])
            {
                return false;
            }

            if (indiceLettre == mot.Length - 1)
            {
                return true;
            }

            visite[i, j] = true;

            if(RechercheMot(mot,indiceLettre+1, i-1, j-1, visite)
               || RechercheMot(mot, indiceLettre+1, i-1, j, visite)
               || RechercheMot(mot, indiceLettre+1, i-1, j+1, visite)
               || RechercheMot(mot, indiceLettre + 1, i, j-1, visite)
               || RechercheMot(mot, indiceLettre + 1, i, j +1, visite)
               || RechercheMot(mot, indiceLettre + 1, i + 1, j -1, visite)
               || RechercheMot(mot, indiceLettre + 1, i +1, j, visite)
               || RechercheMot(mot, indiceLettre + 1, i +1, j + 1, visite))
            {
                return true;
            }

            visite[i, j] = false;
            return false;
        }
       
    }
}
