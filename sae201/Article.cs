using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace SAE
{
    /// <summary>
    /// Stocke 4 informations :
    /// 1 chaines : pour le nom de l'article
    /// 1 entier : l'identifiant de l'article
    /// 1 assossiation : pou un type d'article
    /// 1 nombre decimal : le prix unitaire de l'article
    /// </summary>
    public class Article : Crud<Article>
    {
        private int idArticle;
        private TypeArticle type;
        private string libelleArticle;
        private decimal prixUnitaire;
        /// <summary>
        /// Constructeur vide de Article
        /// </summary>
        public Article()
        {
            this.type = new TypeArticle();
        }
        /// <summary>
        /// Constructeur de Article avec tous les paramètres
        /// </summary>
        public Article(int idArticle, string libelleArticle, decimal prixUnitaire, TypeArticle type)
        {
            IdArticle = idArticle;
            LibelleArticle = libelleArticle;
            PrixUnitaire = prixUnitaire;
            Type = type;
        }
        /// <summary>
        /// Propriété de l'id de l'Article
        /// </summary>
        public int IdArticle
        {
            get => this.idArticle; 
            set
            {
                /// <exception cref="ArgumentException"> Envoyée si l'id de l'article est <0 </exception>
                if (value < 0)
                {
                    throw new ArgumentException("idarticle < 0");
                }
                else
                {
                    this.idArticle = value;
                }
            }
        }
        /// <summary>
        /// Propriété du libelle de l'article
        /// </summary>
        public string LibelleArticle
        {
            /// <exception cref="ArgumentException"> Envoyée si il y a pas de nom d'article</exception>
            get => this.libelleArticle; set
            {
                if (value is null)
                {
                    throw new ArgumentException("faut saisir un texte");
                }
                this.libelleArticle = value;
            }
        }
        /// <summary>
        /// Propriété du prixunitaire de l'article
        /// </summary>
        public decimal PrixUnitaire
        {
            /// <exception cref="ArgumentException"> Envoyée si il le prix est <0 € </exception>
            get => this.prixUnitaire; 
            set
            {
                if (value < 0 )
                    throw new ArgumentException("Le prix unitaire ne peut pas être inferieur a 0€");
                this.prixUnitaire = value;
            }
        }
        /// <summary>
        /// Propriété de l'association TypeArticle
        /// </summary>
        public TypeArticle Type
        {
            /// <exception cref="ArgumentException"> Envoyée si il y a pas de type </exception>
            get => this.type;
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("saisir un texte");
                }
                else
                {
                    this.type = value;
                }
            }
        }
        /// <summary>
        /// Méthode pour créer un Article
        /// </summary>
        public void Create()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Méthode pour lire un Article
        /// </summary>
        public void Read()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Méthode pour mettre à jour un Article
        /// </summary>
        public void Update()
        {
            DataAccess access = new DataAccess();


            try
            {
                if (access.OpenConnection())
                {                 
                    access.SetData($"Update ARTICLE SET nomArticle = '{LibelleArticle}', idTypeArticle = '{type.IdTypeArticle}' where idArticle = {IdArticle}");
                    access.CloseConnection();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message, "L'update d'Article à echoué ");
            }
        }
        /// <summary>
        /// Méthode pour supprimer un Article
        /// </summary>
        public void Delete()
        {
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.OpenConnection())
                {
                    reader = access.GetData("DELETE FROM ARTICLE WHERE LIBELLEARTICLE ='" + this.LibelleArticle + "';");
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
        /// Méthode pour extraire les Articles d'une BD
        /// </summary>
        public List<Article> FindAll()
        {
            List<Article> listeArticle = new List<Article>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.OpenConnection())
                {
                    reader = access.GetData("select a.libellearticle, a.prixunitaire, t.libelletype from dbo.ARTICLE a join TYPEARTICLE t on t.idtypearticle = a.idtypearticle;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Article unarticle = new Article();
                            unarticle.libelleArticle = reader.GetString(0);
                            unarticle.prixUnitaire = reader.GetDecimal(1);
                            unarticle.type.LibelleType = reader.GetString(2);

                            listeArticle.Add(unarticle);
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
            return listeArticle;
        }


        public List<Article> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

       
    }
}
