using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projet_Boogle
{
    
    public class De
    {
        private Dictionary<char,int> lettres_de = new Dictionary<char, int>();
        private char lettre_visible;

        

        #region Constructeur
        public De()
        {
            
            for(int i=0; i<6; i++)
            {
                int numero_lettre = Jeu.random.Next(0, 26);
                char[] lettres = Jeu.Lettres.Keys.ToArray(); 
                char lettre_choisie = lettres[numero_lettre]; 
                if (lettres_de.ContainsKey(lettre_choisie))
                {
                    lettres_de[lettre_choisie] += 1;
                }
                else
                {
                    lettres_de.Add(lettre_choisie, 1);
                }
                

            }
        }
        #endregion

        #region Propriétés
        public Dictionary<char, int> Lettres_de
        {
            get { return this.lettres_de; }
        }

        public char Lettre_visible
        {
            get { return this.lettre_visible; }
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// la méthode crée un tableau de string avec les lettres des faces du dé
        /// la lettre visible est une lettre random des lettres sur les 6 faces du dé
        /// </summary>
        public void Lance()
        {
            
            char[] lettres = new char[6];
            int index = 0;
            foreach (char lettre in this.lettres_de.Keys) {
                for (int i = 0; i< this.lettres_de[lettre]; i++)
                {
                    lettres[index] = lettre;
                    index++;
                }
            }
            this.lettre_visible = lettres[Jeu.random.Next(0, 6)];
        }

        
        /// <summary>
        /// retourne un string qui indique les 6 lettres sur les faces du dé, et la lettre obtenue après avoir lancé          
        /// le dé
        /// </summary>
        /// <returns>description du dé</returns>
        public string toString()
        {
            string res = "Le dé est composé des faces suivantes : ";
            foreach (char lettre in this.lettres_de.Keys)
            {
                int index = 0;
                for (int i = 0; i< this.lettres_de[lettre]; i++)
                {
                    res += lettre + ", ";
                    index++;
                }
            }
            if (this.lettre_visible == null)
            {
                res += " Il n'y a pas de face visible, le dé n'a pas encore été lancé";
            }
            else
            {
                res += " La face visible est " + this.lettre_visible;
            }
            return res;

        }
        #endregion

    }
}
