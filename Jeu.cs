using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Projet_Boogle
{
    class Jeu
    {
        public static Dictionary<string, int[]>? Lettres; // un dictionnaire avec toutes les lettres, leur nombre et leur poids
        static void Main(string[] args)
        {
            string fichierLettre = Program.LireFichier("Lettres.txt");
            Lettres = Program.StringLettresToDico(fichierLettre);

            Console.WriteLine("==== Bienvenue au jeu Boggle ! ==== ");
            Console.WriteLine("Entrer le nombre de joueur : ");
            int nb_joueur = Convert.ToInt32(Console.ReadLine());
            Joueur[] joueurs = new Joueur[nb_joueur];
            for (int i = 0; i < nb_joueur; i++)
            {
                Console.WriteLine("Entrer le nom du joueur n°" + i + 1);
                string nom = Convert.ToString(Console.ReadLine());
                joueurs[i] = new Joueur(nom);
            }

            for (int tour = 1; tour <= 3; tour++)
            {

                Console.WriteLine("____ Tour " + tour + " ! ____");
                for (int joueur = 0; joueur < joueurs.Length; joueur++)
                {
                    Console.WriteLine($"\nC'est au tour du joueur {joueur} !");
                    Console.WriteLine("Vous avez une minute pour jouer.");

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    while (stopwatch.Elapsed.TotalSeconds < 60)
                    {
                        //jeu

                        Thread.Sleep(100);

                    }
                    stopwatch.Stop(); // Arrête le chronomètre


                }
            }
            Console.WriteLine("==== Fin de la partie ! ====");
            Console.WriteLine(" Voici les résultats ;) \n'roulement de tambours'...");
        }
    }
}
