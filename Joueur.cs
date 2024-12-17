using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle
{
    internal class Joueur
    {
        private string nom;
        private int score;
        private Dictionary<string, int> motsTrouves;

        public Joueur(string nom)
        {
            this.nom = nom;
            if (nom == "")    //la condition permet de verifier si le nom entre est null ou vide cf learn microsoft pour autres options
            {
                Console.WriteLine("Vous devez avoir un nom");
                return;
            }
            this.score = 0;
            this.motsTrouves = new Dictionary<string, int>();
        }

        public string Nom
        {
            get { return this.nom; }
        }
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }
        public Dictionary<string,int> MotsTrouves{
            get { return this.motsTrouves;}
            set { this.motsTrouves=value;}
            }
        public bool Contain(string mot)
        {
            if (motsTrouves.ContainsKey(mot))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Add_Mot(string mot)
        {
            if (motsTrouves.ContainsKey(mot) == false) // rajouter la condition 'si mot appartient au dictionnaire
            {
                motsTrouves.Add(mot, 1);     // rajouter occurrence du mot
               
            }
            else
            {
                motsTrouves[mot] += 1;
            }
        }

        public int ScoreDuMot(string mot)
        {
            int scoreMot = 0;
            foreach (char lettre in mot.ToUpper())
            {
                string lettreStr = lettre.ToString(); //sinon on a un char au lieu d'un string et on ne peut pas trouver dans dico
                scoreMot += Program.Lettres[lettre][0];

            }

            return scoreMot;
        }

        public string toString()
        {
            string res = "Le score de" + this.nom + " est de " + this.score + " grâce aux mots cités suivants \n"; 
           foreach(string mot in motsTrouves.Keys)
            {
                res += mot+", ";
            }
            return res;

        }
    }
}
