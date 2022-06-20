using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SAE
{
    /// <summary>
    /// Stocke 7 informations :
    /// 6 chaines : pour le nom du magasin, l'adresse, la ville, le code postal, la region et le pays
    /// 1 entier : l'identifiant du magasin
    /// </summary>
    public class Magasin : Crud<Magasin>
    {

        private int idMagasin; 
        private string libelleMagasin;
        private string adresseM;
        private string ville;
        private string codePostal;
        private string regions;
        private string pays;
        /// <summary>
        /// Constructeur vide de Commande
        /// </summary>
        public Magasin()
        {
        }
        /// <summary>
        /// Constructeur de Commande avec tous les paramètres
        /// </summary>
        public Magasin(int idMagasin, string libelleMagasin, string adresseM, string ville, string codePostal, string regions, string pays)
        {
            IdMagasin = idMagasin;
            LibelleMagasin = libelleMagasin;
            AdresseM = adresseM;
            Ville = ville;
            CodePostal = codePostal;
            Regions = regions;
            Pays = pays;
        }
        /// <summary>
        /// Propriété de l'id du magasin
        /// </summary>
        public int IdMagasin
        {
            /// <exception cref="ArgumentException"> Envoyée si l'id du magasin est <0 </exception>
            get => idMagasin; 
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("idMagasin < 0");
                }
                else
                {
                    this.idMagasin = value;
                }
            }
        }
        /// <summary>
        /// Propriété de l'adrese du magasin
        /// </summary>
        public string AdresseM
        {
            /// <exception cref="ArgumentException"> Envoyée si il y a pas l'adresse </exception>
            get => adresseM;
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("erreur adresse");
                }
                else
                {
                    this.adresseM = value;
                }
            }
        }
        /// <summary>
        /// Propriété de la ville du magasin
        /// </summary>
        public string Ville
        {
            get => ville;
            set
            {
                /// <exception cref="ArgumentException"> Envoyée si il y a pas de ville </exception>
                if (value is null)
                {
                    throw new ArgumentException("erreur ville");
                }
                else
                {
                    this.adresseM = value;
                }
            }
        }
        /// <summary>
        /// Propriété du code postal du magasin
        /// </summary>
        public string CodePostal
        {
            /// <exception cref="ArgumentException"> Envoyée si le code postal ne serait pas composé de 5 chiffres</exception>
            get => codePostal;
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("erreur code");
                }
                else
                {
                    this.codePostal = value;
                }
            }
        }
        /// <summary>
        /// Propriété de la region du magasin
        /// </summary>
        public string Regions
        {
            get => regions;
            set
            {
                /// <exception cref="ArgumentException"> Envoyée si il y a pas de region </exception>
                if (value is null)
                {
                    throw new ArgumentException("erreur region");
                }
                else
                {
                    this.regions = value;
                }
            }
        }
        /// <summary>
        /// Propriété du pays du magasin
        /// </summary>
        public string Pays
        {
            /// <exception cref="ArgumentException"> Envoyée si il y a pas de pays </exception>
            get => pays;
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("erreur pays");
                }
                else
                {
                    this.pays = value;
                }
            }
        }
        /// <summary>
        /// Propriété du libelle du magasin
        /// </summary>
        public string LibelleMagasin
        {
            get => libelleMagasin;
            set
            {
                /// <exception cref="ArgumentException"> Envoyée si il y a pas de nom de magasin </exception>
                if (value is null)
                {
                    throw new ArgumentException("erreur libelle magasin");
                }
                else
                {
                    this.libelleMagasin = value;
                }
            }
        }
        /// <summary>
        /// Méthode pour créer un magasin
        /// </summary>
        public void Create()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Méthode pour lire un magasin
        /// </summary>
        public void Read()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Méthode pour mettre à jour un magasin
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Méthode pour supprimer un magasin
        /// </summary>
        public void Delete()
        {
            DataAccess access = new DataAccess();

            try
            {
                if (access.OpenConnection())
                {
                    SqlCommand command = new SqlCommand($"DELETE FROM [IUT-ACY\\inzoudih].MAGASIN WHERE NOMMAGASIN = {this.LibelleMagasin}", access.connection);
                    command.ExecuteNonQuery();
                    access.CloseConnection();
                }
            }
            catch (Exception)
            {

                System.Windows.MessageBox.Show("Vous avez bien supprimer la commande !", "Important", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// Méthode pour extraire les magasins d'une BD
        /// </summary>
        public List<Magasin> FindAll()
        {
            List<Magasin> listeMagasin = new List<Magasin>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.OpenConnection())
                {
                    reader = access.GetData("select * from dbo.MAGASIN;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Magasin unmagasin = new Magasin();
                            unmagasin.adresseM = reader.GetString(1);
                            unmagasin.libelleMagasin = reader.GetString(2);
                            unmagasin.ville = reader.GetString(3);
                            unmagasin.codePostal = reader.GetString(4);
                            unmagasin.regions = reader.GetString(5);
                            unmagasin.pays = reader.GetString(6);


                            listeMagasin.Add(unmagasin);
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
            return listeMagasin;
        }
        /// <summary>
        /// Méthode pour extraire les magasins d'une BD avec un filtre
        /// </summary>
        public List<Magasin> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }


    }
}
