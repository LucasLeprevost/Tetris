using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tetris.metier
{
	public class GrilleJeu
	{
		private readonly int[,] grille ;

		public int Lignes { get; }
		public int Colonnes { get; }

		public GrilleJeu(int lignes, int colonnes)
		{
			this.Lignes = lignes;
			this.Colonnes = colonnes;
			grille = new int[lignes, colonnes];
		}

		public int this[int ligne, int colonne]
		{
			get { return grille[ligne, colonne]; }
			set { grille[ligne, colonne] = value; }
		}

		public bool EstDedans(int ligne, int colonne)
		{
			return ligne >= 0 && ligne < Lignes && colonne >= 0 && colonne < Colonnes;
		}

		public bool EstVide(int ligne, int colonne)
		{
			return EstDedans(ligne,colonne) && grille[ligne, colonne] == 0;
		}

		public bool EstPleine(int ligne)
		{
			for (int col = 0; col < Colonnes; col++)
			{
				if (grille[ligne, col] == 0)
					return false;
			}
			return true;
		}

		public bool EstVideLigne(int ligne)
		{
			for (int col = 0; col < Colonnes; col++)
			{
				if (grille[ligne, col] != 0)
					return false;
			}
			return true;
		}

		public void EffacerLigne(int ligne)
		{
			for (int col = 0; col < Colonnes; col++)
			{
				grille[ligne, col] = 0;
			}
		}

		public void DeplacerLignesVersLeBas(int ligne, int nbLignes)
		{
			for (int col = 0; col < Colonnes; col++)
			{
				grille[ligne + nbLignes, col] = grille[ligne, col];
				grille[ligne, col] = 0;
			}
		}

		public int NettoyerLignesPleines()
		{
			int nettoye = 0;

			for (int ligne = Lignes - 1; ligne >= 0; ligne--)
			{
				if (EstPleine(ligne))
				{
					EffacerLigne(ligne);
					nettoye++;
				}
				else if (nettoye > 0)
				{
					DeplacerLignesVersLeBas(ligne, nettoye);
                }
            }

			return nettoye;
        }
	}
}
