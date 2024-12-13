using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle
{
    internal class Plateau
    {
        private De[] des;
        private string[,] lettresPlateau;
        private int taille;

    
        public Plateau( int taille)
        {
            De[] des = new De[taille * taille];
            string[,] lettresPlateau = new string[taille, taille];
            for (int i = 0; i<taille; i++)
            {
                for (int j = 0; j<taille; j++)
                {
                    int indexDe = i * taille + j;
                    des[indexDe].Lance();
                    lettresPlateau[i, j] = des[indexDe].Lettre_visible[0];
                }
            }
            this.des=des;
            this.lettresPlateau=lettresPlateau;
            this.taille=taille;
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
           for(int i=0; i<this.taille; i++)
           {
                for(int j=0; j<this.taille; j++)
                {
                    if (RechercheMot(mot, 0, i, j, new bool[this.taille, this.taille]))
                    {
                        return true;
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
