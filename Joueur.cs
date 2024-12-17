using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;

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
            this.score += ScoreDuMot(mot);
        }

        public int ScoreDuMot(string mot)
        {
            int scoreMot = 0;
            foreach (char lettre in mot.ToUpper())
            {
                scoreMot += Jeu.Lettres[lettre][0];

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

        /// <summary>
        /// génère un nuage de mot avec les mots trouvés pas le joueur au cours de la partie
        /// </summary>
        public void GenererNuageDeMots()
        {
            // Dimensions de l'image du nuage de mots
            int largeur = 800;
            int hauteur = 600;

            // Créer l'image avec les dimensions spécifiées
            Bitmap image = new Bitmap(largeur, hauteur);
            Graphics g = Graphics.FromImage(image);

            // Définir le fond blanc
            g.Clear(Color.White);

            // Choisir une police de base et des couleurs dans les tons violets et roses
            Random rand = new Random();
            System.Drawing.Font[] polices = {
        new System.Drawing.Font("Arial", 12),
        new System.Drawing.Font("Arial", 16),
        new System.Drawing.Font("Arial", 20),
        new System.Drawing.Font("Arial", 24),
        new System.Drawing.Font("Arial", 30)
    };

            Brush[] couleurs = {
        new SolidBrush(Color.FromArgb(128, 0, 128)), // Violet
        new SolidBrush(Color.FromArgb(186, 85, 211)), // Orchidée
        new SolidBrush(Color.FromArgb(138, 43, 226)), // Bleu violet
        new SolidBrush(Color.FromArgb(255, 20, 147)), // Deep pink (rose profond)
        new SolidBrush(Color.FromArgb(255, 105, 180)), // Hot pink (rose vif)
        new SolidBrush(Color.FromArgb(255, 182, 193)), // Rose pâle
        new SolidBrush(Color.FromArgb(255, 105, 180)), // Rose vif
        new SolidBrush(Color.FromArgb(219, 112, 147))  // Medium violet red
    };

            // Liste pour enregistrer les positions des mots
            List<Rectangle> positions = new List<Rectangle>();

            // Pour éviter la superposition, on va d'abord générer les tailles de mots et leur fréquence
            List<KeyValuePair<string, int>> motsFrequences = MotsTrouves.ToList();
            motsFrequences.Sort((x, y) => y.Value.CompareTo(x.Value)); // Trier par fréquence décroissante

            // Variables pour essayer de placer les mots sans chevauchement
            int maxTentatives = 200; // Augmenter les tentatives pour une meilleure chance de placement
            int minSpacing = 10; // Espacement minimum entre les mots

            foreach (var mot in motsFrequences)
            {
                // La taille de la police est basée sur la fréquence du mot
                int taillePolice = 40 + mot.Value * 17; // La taille varie en fonction de la fréquence
                System.Drawing.Font font = new System.Drawing.Font("Arial", taillePolice);

                // Choisir une couleur au hasard parmi celles définies
                Brush couleur = couleurs[rand.Next(couleurs.Length)];

                // Calculer la largeur et la hauteur du mot avec cette police
                SizeF tailleMot = g.MeasureString(mot.Key, font);
                int motLargeur = (int)tailleMot.Width;
                int motHauteur = (int)tailleMot.Height;

                // Essayer de positionner le mot sans chevauchement
                bool trouvePlace = false;
                int tentatives = 0;

                while (!trouvePlace && tentatives < maxTentatives)
                {
                    // Générer une position aléatoire proche des autres mots
                    int x = rand.Next(minSpacing, largeur - motLargeur - minSpacing);
                    int y = rand.Next(minSpacing, hauteur - motHauteur - minSpacing);

                    Rectangle position = new Rectangle(x, y, motLargeur, motHauteur);

                    // Vérifier si le mot chevauche un mot déjà placé
                    bool overlap = false;
                    foreach (var pos in positions)
                    {
                        if (position.IntersectsWith(pos))
                        {
                            overlap = true;
                            break;
                        }
                    }

                    if (!overlap)
                    {
                        // Appliquer la transformation (horizontal ou vertical)
                        int rotationAngle = rand.Next(0, 2) == 0 ? 0 : 90; // 0° pour horizontal, 90° pour vertical
                        g.TranslateTransform(position.X + motLargeur / 2, position.Y + motHauteur / 2); // Déplacer le centre
                        g.RotateTransform(rotationAngle); // Rotation du graphique

                        // Dessiner le mot avec la position et la rotation
                        g.DrawString(mot.Key, font, couleur, new PointF(-motLargeur / 2, -motHauteur / 2)); // Position relative au centre

                        // Restaurer la transformation (annuler la rotation et translation)
                        g.RotateTransform(-rotationAngle); // Annuler la rotation
                        g.TranslateTransform(-(position.X + motLargeur / 2), -(position.Y + motHauteur / 2)); // Annuler la translation

                        positions.Add(position); // Ajouter la position du mot
                        trouvePlace = true; // Le mot est placé sans chevauchement
                    }

                    tentatives++;
                }

                // Si après plusieurs tentatives on n'a pas trouvé de place, on abandonne ce mot
                if (tentatives >= maxTentatives)
                {
                    Console.WriteLine($"Impossible de placer le mot : {mot.Key}");
                }
            }

            // Enregistrer l'image dans un fichier
            string path = $"{Nom}_nuage_mots.png";
            image.Save(path, ImageFormat.Png);
            Console.WriteLine($"Nuage de mots généré et enregistré sous {path}");

            // Ouvrir l'image automatiquement
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });

            // Libérer les ressources
            g.Dispose();
            image.Dispose();
        }
    }
}
