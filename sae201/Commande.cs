using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;



namespace SAE
{
    /// <summary>
    /// Stocke 5 informations :
    /// 1 chaines : le commentaire
    /// 1 entier : le nombre de quantite de l'article commander
    /// 2 assossiation : pour un magasin et un article
    /// 1 Date : la date de la commande
    /// </summary>
    public class Commande : Crud<Commande>
    {
        //private int idCommande;
        private Magasin unMagasin;
        private Article unArticle;
        private DateTime date;
        private int quantite;
        private string commente;
        /// <summary>
        /// Constructeur de Commande avec les associations
        /// </summary>
        public Commande()
        {
            this.unMagasin = new Magasin();
            this.unArticle = new Article();
        }

        /// <summary>
        /// Constructeur de Commande avec tous les paramètres
        /// </summary>
        public Commande(Magasin unMagasin, Article unArticle, DateTime date, int quantite, string commente)
        {
            //IdCommande = idCommande;
            UnMagasin = unMagasin;
            UnArticle = unArticle;
            Date = date;
            Quantite = quantite;
            Commente = commente;
        }
 
        /// <summary>
        /// Propriété de l'id de la Commande
        /// </summary>
        /*public int IdCommande
        {
            get => idCommande; set
            {
                if (value < 0)
                {
                    throw new ArgumentException("idCommande < 0");
                }
                else
                {
                    this.idCommande = value;
                }
            }
        }*/
        /// <summary>
        /// Propriété de l'association Magasin
        /// </summary>
        public Magasin UnMagasin { get => unMagasin; set => unMagasin = value; }
        /// <summary>
        /// Propriété de l'association Article
        /// </summary>
        public Article UnArticle { get => unArticle; set => unArticle = value; }
        /// <summary>
        /// Propriété de la date de la Commande
        /// </summary>
        public DateTime Date { get => date; set => date = value; }
        /// <summary>
        /// Propriété de la quantite de la Commande
        /// </summary>
        public int Quantite
        {
            get => quantite; set
            {
                /// <exception cref="ArgumentException"> Envoyée si la quantité est <0 </exception>
                if (value < 0)
                {
                    throw new ArgumentException("quantite < 0");
                }
                else
                {
                    this.quantite = value;
                }
            }
        }
        /// <summary>
        /// Propriété de du commentaire de la Commande
        /// </summary>
        public string Commente
        {
            /// rajoute si il y a un commentaire 
            get => commente; 
            
            set
            {
                if (String.IsNullOrEmpty(value))
                    this.commente = "Pas de commentaire";
                else
                    this.commente = value;
            }
        }
        /// <summary>
        /// Méthode pour créer une commande
        /// </summary>
        public void Create()
        {
            DataAccess access = new DataAccess();
            access.SetData($"INSERT INTO COMMANDE(idMagasin, idArticle, date, commente, quantite) VALUES({this.unMagasin.IdMagasin}, {this.unArticle.IdArticle}, '{this.Date}', '{this.Commente}','{this.Quantite}',)");
        }
        /// <summary>
        /// Méthode pour lire une commande
        /// </summary>
        public void Read()
        {
            DataAccess access = new DataAccess();
            access.GetData("select * from COMMANDE c join MAGASIN m on m.IDMAGASIN = c.IDMAGASIN join article a on a.IDARTICLE = c.IDARTICLE");
        }
        /// <summary>
        /// Méthode pour mettre à jour une commande
        /// </summary>
        public void Update()
        {
            List<Commande> listeGroupes = new List<Commande>();
            DataAccess access = new DataAccess();
          


            try
            {
                if (access.OpenConnection())
                {
                   /*reader = access.GetData("Update dbo.COMMANDE set libelleMagasin = '" + LibelleMagasin + "' where n_groupe =" + this.libelleMedicament);
                    reader.Read();
                    reader.Close();
                    access.CloseConnection();*/
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
        }
        /// <summary>
        /// Méthode pour supprimer une commande
        /// </summary>
        public void Delete()
        {
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.OpenConnection())
                {
                    reader = access.GetData("DELETE FROM COMMANDE WHERE DATE ='" + this.Date + "';");
                    reader.Read();
                    reader.Close();

                    access.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
        }
        /// <summary>
        /// Méthode pour extraire les commandes d'une BD
        /// </summary>
        public List<Commande> FindAll()
        {
            List<Commande> listeCommande = new List<Commande>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.OpenConnection())
                {
                    reader = access.GetData("Select m.LIBELLEMAGASIN,a.LIBELLEARTICLE,c.DATE,c.COMMENTE,c.QUANTITE FROM COMMANDE c join MAGASIN m on m.IDMAGASIN = c.IDMAGASIN join ARTICLE a on a.IDARTICLE = c.IDARTICLE ");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Commande unecommande = new Commande();
                            unecommande.Quantite = reader.GetInt32(4);
                            unecommande.Date = reader.GetDateTime(2);
                            unecommande.unMagasin.LibelleMagasin = reader.GetString(0);
                            unecommande.unArticle.LibelleArticle = reader.GetString(1);
                            if (reader.GetValue(3) is string)
                            {
                                unecommande.Commente = reader.GetString(3);
                            }
                            else
                            {
                                unecommande.Commente = null;
                            }
                            listeCommande.Add(unecommande);
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Aucune donnée trouvée.", "Important Message");
                    }
                    reader.Close();
                    access.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
            return listeCommande;
        }
        /// <summary>
        /// Méthode pour extraire les commandes d'une BD avec un filtre
        /// </summary>
        public List<Commande> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

       
    }
}
