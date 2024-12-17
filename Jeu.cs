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
        public static Dictionary<char, int[]>? Lettres; // un dictionnaire avec toutes les lettres, leur nombre et leur poids


        static void Main(string[] args)
        {
            

            string fichierLettre = Program.LireFichier("Lettres.txt");
            Lettres = Program.StringLettresToDico(fichierLettre);

            // Creation des joueurs 

            Console.WriteLine("==== Bienvenue au jeu Boggle ! ==== ");
            Console.WriteLine("Entre le nombre de joueur : ");
            int nb_joueur = Convert.ToInt32(Console.ReadLine());
            Joueur[] joueurs = new Joueur[nb_joueur];
            for (int i = 0; i < nb_joueur; i++)
            {
                Console.WriteLine("Entre le nom du joueur n°" + (i + 1));
                string nom = Convert.ToString(Console.ReadLine());
                joueurs[i] = new Joueur(nom);
            }

            // choix de la langue par le joueur 

            Console.WriteLine("Choisis la langue ('fr' pour français et 'en' pour anglais) : ");
            string langue = Convert.ToString(Console.ReadLine());
            while(langue != "fr" && langue != "en")
            {
                Console.WriteLine("entrée non valide");
                Console.WriteLine("Choisis la langue ('fr' pour français et 'en' pour anglais) : ");
                langue = Convert.ToString(Console.ReadLine());
            }

            // choix de la taille du plateau

            Console.WriteLine("Choisis la taille du plateau : ");
            int taille = Convert.ToInt32(Console.ReadLine());
            



            for (int tour = 1; tour <= 3; tour++)
            {

                Console.WriteLine("____ Tour " + tour + " ! ____");
                for (int joueur = 0; joueur < joueurs.Length; joueur++)
                {
                    Console.WriteLine($"\nC'est au tour de " + joueurs[joueur].Nom +" !");
                    Console.WriteLine("Tu as 1 min pour jouer.");

                    // creation du plateau 
                    Dictionnaire dico = new Dictionnaire(langue);
                    Plateau plateau = new Plateau(taille, dico);
                    Console.WriteLine(plateau.toString());

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    while (stopwatch.Elapsed.TotalSeconds < 60)
                    {
                        
                        Console.WriteLine("Ecris un mot");
                        string mot = Convert.ToString(Console.ReadLine());

                        if (plateau.Test_Plateau(mot.ToUpper()))
                        {
                            Console.WriteLine("valide");
                            joueurs[joueur].Add_Mot(mot);

                        }
                        else
                        {
                            Console.WriteLine("non valide");
                        }

                        Thread.Sleep(100);


                    }
                    stopwatch.Stop(); // Arrête le chronomètre


                }
            }

            Console.WriteLine("==== Fin de la partie ! ====");
            Console.WriteLine(" Voici les résultats ;) \n'roulement de tambours'...");
            Thread.Sleep(2000);
            int max = 0;
            string j_max = "";
            foreach(Joueur joueur in joueurs)
            {
                if(joueur.Score> max)
                {
                    max = joueur.Score;
                    j_max = joueur.Nom;
                }
                Console.WriteLine(joueur.toString());
            }
            Console.WriteLine("Et le grand gagnant est " + j_max + " !!!! ouaiiiiiiiiiis bravo wouwouuuuuuWOUUUUU");
            foreach (Joueur joueur in joueurs)
            {
                joueur.GenererNuageDeMots();
            }
        }
    }
}
