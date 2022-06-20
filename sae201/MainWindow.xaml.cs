using SAE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sae201
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Article> LesArticles { get; }
        public ObservableCollection<Commande> LesCommandes { get; }
        public ObservableCollection<TypeArticle> LesTypesArticles { get; }
        public ObservableCollection<Magasin> LesMagasins { get; }
        public MainWindow()
        {
            LesArticles = new ObservableCollection<Article>();
            LesCommandes = new ObservableCollection<Commande>();
            LesTypesArticles = new ObservableCollection<TypeArticle>();
            LesMagasins = new ObservableCollection<Magasin>();
            InitializeComponent();
            ApplicationData.loadApplicationData();

            dg.ItemsSource = ApplicationData.listeCommande;
            dg.ItemsSource = ApplicationData.listeMagasin;
            dg.ItemsSource = ApplicationData.listeArticle;
            listeMagasin.ItemsSource = ApplicationData.listeMagasin;
            listeArticle.ItemsSource = ApplicationData.listeArticle;
            listeTypeArticle.ItemsSource = ApplicationData.listeTypeArticle;

            

            this.DataContext = this;
        }
        /// <summary>
        /// Interaction Bouton Supprimer
        /// </summary>
        private void supprimer_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mes = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (mes == MessageBoxResult.Yes)
            {
                ((Commande)dg.SelectedItem).Delete();
                ApplicationData.listeCommande.Remove((Commande)this.dg.SelectedItem);
                dg.Items.Refresh();
                dg.SelectedItem = 0;
            }
            
        }
        /// <summary>
        /// Interaction Bouton Ajouter
        /// </summary>
        private void ajouter_Click(object sender, RoutedEventArgs e)
        {
            Magasin mag = ((Magasin)this.listeMagasin.SelectedItem);
            Article art = ((Article)this.listeArticle.SelectedItem);
            String dateChoisi = this.choixDate.SelectedDate.Value.ToString();
            String commentaire = (String)this.textCommentaire.Text;
            int quantite = int.Parse(this.textQuantite.Text);          
            Commande cmd = new Commande(mag,art,DateTime.Parse(dateChoisi),quantite,commentaire);
            cmd.Create();
        }
        /// <summary>
        /// Interaction Bouton Modifier
        /// </summary>
        private void modifier_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
