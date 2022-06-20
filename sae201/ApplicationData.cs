using System;
using System.Collections.Generic;
using System.Text;

namespace SAE
{ /// <summary>
  /// Permet de charger les données d'une BD
  /// </summary>
    public class ApplicationData
    {
        /// <summary>
        /// Déclare une liste d'Article
        /// </summary>
        public static List<Article> listeArticle
        {
            get;
            set;
        }
        /// <summary>
        /// Déclare une liste de Magasin
        /// </summary>
        public static List<Magasin> listeMagasin
        {
            get;
            set;
        }
        /// <summary>
        /// Déclare une liste de Commande
        /// </summary>
        public static List<Commande> listeCommande
        {
            get;
            set;
        }
        /// <summary>
        /// Déclare une liste de Type d'Article
        /// </summary>
        public static List<TypeArticle> listeTypeArticle
        {
            get;
            set;
        }
        /// <summary>
        /// Méthode pour affecter aux listes définit précédement les données contenu dans la BD
        /// </summary>
        public static void loadApplicationData()
        {
            //chargement des tables
            Article unArticle = new Article();
            Magasin unMagasin = new Magasin();
            Commande uneCommande = new Commande();
            TypeArticle unType = new TypeArticle();
            listeArticle = unArticle.FindAll();
            listeCommande = uneCommande.FindAll();
            listeMagasin = unMagasin.FindAll();
            listeTypeArticle = unType.FindAll();
        }

    }
}