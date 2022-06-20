using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SAE
{
    /// <summary>
    /// Stocke 2 informations :
    /// 1 chaines : pour le nom du type d'article
    /// 1 entier : l'identifiant du type de l'article
    /// </summary>
    public class TypeArticle : Crud<TypeArticle>
    {
        private int idTypeArticle;
        private string libelletype;

        public TypeArticle()
        {
        }

        public TypeArticle(int idTypeArticle, string libelletype)
        {
            IdTypeArticle = idTypeArticle;
            LibelleType = libelletype;
        }

        public int IdTypeArticle {
            /// <exception cref="ArgumentException"> Envoyée si l'id du type de article est <0 </exception>
            get => idTypeArticle;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("idTypeArticle < 0");
                }
                else
                {
                    this.idTypeArticle = value;
                }
            }
        }
        public string LibelleType {
            /// <exception cref="ArgumentException"> Envoyée si il y a pas de nom pour le type</exception>
            get => libelletype;
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("saisir un texte");
                }
                else
                {
                    this.libelletype = value;
                }
            }
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public List<TypeArticle> FindAll()
        {
            List<TypeArticle> listeTypeArticle = new List<TypeArticle>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.OpenConnection())
                {
                    reader = access.GetData("select * from dbo.TYPEARTICLE;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TypeArticle untype = new TypeArticle();
                            untype.IdTypeArticle = reader.GetInt32(0);
                            untype.LibelleType = reader.GetString(1);


                            listeTypeArticle.Add(untype);
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
            return listeTypeArticle;
        }

        public List<TypeArticle> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
