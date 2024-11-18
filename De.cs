using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projet_Boogle
{
    internal class De
    {
        // le dé est un dictionnaire de la forme {"C":1;"J":2;...} avec en clé un string de la lettre et en valeur
        // le nombre de fois qu'apparait la lettre sur le dé 

        private Dictionary<string,int> lettres_de = new Dictionary<string, int>();
        private string lettre_visible;

        Random random = new Random();

        #region Constructeur
        public De()
        {
            
            for(int i=0; i<6; i++)
            {
                int numero_lettre = random.Next(0, 26);
                string[] lettres = Program.Lettres.Keys.ToArray(); // met toutes les lettres dans un tableau de string
                string lettre_choisie = lettres[numero_lettre]; // choisie une lettre random 
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
        public Dictionary<string, int> Lettres_de
        {
            get { return this.lettres_de; }
        }

        public string Lettre_visible
        {
            get { return this.lettre_visible; }
        }
        #endregion

        #region Méthodes
        public void Lance()
        {
            // on crée un tableau de string avec les lettres des faces du dé
            string[] lettres = new string[6];
            int index = 0;
            foreach (string lettre in this.lettres_de.Keys) {
                for (int i = 0; i< this.lettres_de[lettre]; i++)
                {
                    lettres[index] = lettre;
                    index++;
                }
            }
            this.lettre_visible = lettres[random.Next(0, 6)];
        }

        public string toString()
        {
            string res = "Le dé est composé des faces suivantes : ";
            foreach (string lettre in this.lettres_de.Keys)
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
