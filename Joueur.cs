using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle
{
    internal class Joueur
    {
        private string nom,
        private int score;
        private Dictionary<string, int> motsTrouves;

        public Joueur(string nom, int score, Dictionary<string, int> motsTrouves) 
        {
            this.nom = nom;
            if (nom=="")    //la condition permet de verifier si le nom entre est null ou vide cf learn microsoft pour autres options
            {
                Console.WriteLine("Vous devez avoir un nom");
                return;
            }
            this.score = score;
            this.motsTrouves = motsTrouves;
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

        public string toString()
        {
           Console.Write("Le score de" + this.nom + " est de " + this.score + " grâce aux mots cités suivants");
           foreach(string mot in motsTrouves.Keys)
            {
                Console.Write($"{mot}");
            } 

        }
    }
}
