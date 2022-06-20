using System;
using System.Collections.Generic;
using System.Text;

namespace SAE
{
	// <summary>
	/// Permet d'implémenter le CRUD dans les différentes classes
	/// </summary>
	public interface Crud<T>
	{
	
			/// <summary>
			/// Méthode pour ajouter un  élément
			/// </summary>
			void Create();

			/// <summary>
			/// Méthode pour lire un élément
			/// </summary>
			void Read();

			/// <summary>
			/// Méthode pour mettre à jour un élément
			/// </summary>
			void Update();

			/// <summary>
			/// Méthode pour supprimer un élément
			/// </summary>
			void Delete();

			/// <summary>
			/// Méthode pour extraire les éléments de la BD
			/// </summary>
			List<T> FindAll();

			/// <summary>
			/// Méthode pour extraire les éléments de la BD avec un filtre
			/// </summary>
			List<T> FindBySelection(string criteres);

		
	}
}