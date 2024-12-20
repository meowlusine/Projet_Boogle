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
    /// <summary>
    /// classe permet de créer un joueur
    /// </summary>

    internal class Joueur
    {
        private string nom;
        private int score;
        private Dictionary<string, int> motsTrouves;

        #region Constructeur
        public Joueur(string nom)
        {
            this.nom = nom;
            if (nom == "")    
            {
                Console.WriteLine("Vous devez avoir un nom");
                return;
            }
            this.score = 0;
            this.motsTrouves = new Dictionary<string, int>();
        }

        #endregion

        #region Propriétés
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

        #endregion


        #region Méthodes 
        /// <summary>
        /// La fonction Contain permet de déterminer si le mot entré a déjà été trouvé par le joueur
        /// </summary>
        /// <param name="mot"></param>
        /// <returns> un booléen qui renvoie true si le mot a déjà été trouvé par le joueur, false sinon</returns>                  
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
        /// <summary>
        /// la fonction Add_Mot ajoute le mot entré en paramètre à la liste des mots trouvés 
        /// s'il n'a pas encore été trouvé, s'il a déjà été trouvé lors d'un tours précédent, 
        /// son occurrence augmente de 1
        /// enfin, on ajoute les points gagnés par ce mot au score total du joueur
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot(string mot)
        {
            if (motsTrouves.ContainsKey(mot) == false) 
            {
                motsTrouves.Add(mot, 1);     
               
            }
            else
            {
                motsTrouves[mot] += 1;
            }
            this.score += ScoreDuMot(mot);
        }

        
        /// <summary>
        /// Dans la class Jeu, nous avons un dictionnaire contenant les lettres en clé et une matrice contenant le poids
        /// et leur occurence. Pour chaque lettre du mot, on récupère le poids coorespondant et on l'additionne au 
        /// score du mot
        /// </summary>
        /// <param name="mot"></param>
        /// <returns>le score du mot en fonction du poids de chaque lettre</returns>
        public int ScoreDuMot(string mot)
        {
            int scoreMot = 0;
            foreach (char lettre in mot.ToUpper())
            {
                scoreMot += Jeu.Lettres[lettre][0];

            }

            return scoreMot;
        }

        /// <summary>
        /// une méthode indiquant le nom du joueur, so score et les mots trouvés
        /// </summary>
        /// <returns>un string avec les informations demandées</returns>
        public string toString()
        {
            string res = "Le score de " + this.nom + " est de " + this.score + " grâce aux mots cités suivants \n"; 
            foreach(string mot in motsTrouves.Keys)
            {
                res += mot+", ";
            }
            return res;

        }

        #region GenererNuageDeMot

        /// <summary>
        /// génère un nuage de mot avec les mots trouvés pas le joueur au cours de la partie
        /// </summary>
        public void GenererNuageDeMots()
        {
           
            int largeur = 800;
            int hauteur = 600;

            
            Bitmap image = new Bitmap(largeur, hauteur);
            Graphics g = Graphics.FromImage(image);

            
            g.Clear(Color.White);

           

            Brush[] couleurs = {
        new SolidBrush(Color.FromArgb(128, 0, 128)), 
        new SolidBrush(Color.FromArgb(186, 85, 211)), 
        new SolidBrush(Color.FromArgb(138, 43, 226)), 
        new SolidBrush(Color.FromArgb(255, 20, 147)), 
        new SolidBrush(Color.FromArgb(255, 105, 180)), 
        new SolidBrush(Color.FromArgb(255, 182, 193)), 
        new SolidBrush(Color.FromArgb(255, 105, 180)), 
        new SolidBrush(Color.FromArgb(219, 112, 147))  
    };

           
            List<Rectangle> positions = new List<Rectangle>();

            
            List<KeyValuePair<string, int>> motsFrequences = MotsTrouves.ToList();
            motsFrequences.Sort((x, y) => y.Value.CompareTo(x.Value)); 

            
            int maxTentatives = 200; 
            int minSpacing = 10; 

            foreach (var mot in motsFrequences)
            {
               
                int taillePolice = 40 + mot.Value * 17; 
                System.Drawing.Font font = new System.Drawing.Font("Arial", taillePolice);

                
                Brush couleur = couleurs[Jeu.random.Next(couleurs.Length)];

               
                SizeF tailleMot = g.MeasureString(mot.Key, font);
                int motLargeur = (int)tailleMot.Width;
                int motHauteur = (int)tailleMot.Height;

                
                bool trouvePlace = false;
                int tentatives = 0;

                while (!trouvePlace && tentatives < maxTentatives)
                {
                    
                    int x = Jeu.random.Next(minSpacing, largeur - motLargeur - minSpacing);
                    int y = Jeu.random.Next(minSpacing, hauteur - motHauteur - minSpacing);

                    Rectangle position = new Rectangle(x, y, motLargeur, motHauteur);

                    
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
                        
                        int rotationAngle = Jeu.random.Next(0, 2) == 0 ? 0 : 90; 
                        g.TranslateTransform(position.X + motLargeur / 2, position.Y + motHauteur / 2); 
                        g.RotateTransform(rotationAngle); 

                       
                        g.DrawString(mot.Key, font, couleur, new PointF(-motLargeur / 2, -motHauteur / 2)); 

                        
                        g.RotateTransform(-rotationAngle); 
                        g.TranslateTransform(-(position.X + motLargeur / 2), -(position.Y + motHauteur / 2)); 

                        positions.Add(position); 
                        trouvePlace = true; 
                    }

                    tentatives++;
                }
            }

           
            string path = $"{Nom}_nuage_mots.png";
            image.Save(path, ImageFormat.Png);
           

            
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });

            
            g.Dispose();
            image.Dispose();
        }
        #endregion

        #endregion
    }
}
