using System;
using System.Data.SqlClient;
using System.Windows;

namespace SAE
{
    /// <summary>
    /// Permet d'acc�der � la base de donn�es.
    /// Ne doit �tre utilis�e que par la couche mod�le pour impl�menter un CRUD.
    /// </summary>
    class DataAccess
    {
        public SqlConnection connection;

        /// <summary>
        /// Connecte la base de donn�es
        /// </summary>
        public Boolean OpenConnection()
        {
            Boolean ret = false;
            try
            {
                this.connection = new SqlConnection();

                this.connection.ConnectionString =
                "Data Source=srv-jupiter.iut-acy.local;" +
                "Initial Catalog=BT6;" +
                "Integrated Security=SSPI;";

                this.connection.Open();
                if (this.connection.State.Equals(System.Data.ConnectionState.Open))
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Important Message");
            }

            return ret;
        }

        /// <summary>
        /// D�connecte la base de donn�es
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (this.connection.State.Equals(System.Data.ConnectionState.Open))
                {
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Important Message");
            }
        }

        /// <summary>
        /// Donne acc�s � des donn�es en lecture
        /// </summary>
        public SqlDataReader GetData(String getQuery)
        {
            SqlDataReader reader = null;

            try
            {
                SqlCommand command = new SqlCommand(getQuery, this.connection);
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Important Message");
            }

            return reader;
        }

        /// <summary>
        /// Permet d'ins�rer, supprimer ou modifier des donn�es
        /// </summary>
        /// <param name="setQuery">Requ�te SQL permettant d'ins�rer, supprimer ou modifier des donn�es.</param>
        /// <exception cref="System.Exception">D�clench�e si la connexion, l'�criture/modification/suppression en base ou la d�connexion �chouent.</exception> 
        /// <returns>Un bool�en indiquant si des lignes ont �t� ajout�es/supprim�es/modifi�es.</returns>
        public Boolean SetData(String setQuery)
        {
            Boolean ret = false;

            try
            {
                if (this.OpenConnection())
                {
                    int modifiedLines;
                    SqlCommand command = new SqlCommand(setQuery, this.connection);

                    modifiedLines = command.ExecuteNonQuery();

                    if (modifiedLines > 0)
                    {
                        ret = true;
                    }

                    this.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Important Message");
            }

            return ret;
        }
    }


}
