using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.Blocs
{
	public abstract class Bloc
	{
		protected abstract Position[][] Tuiles { get; }
		protected abstract Position DecalageDepart { get; }
		public abstract int Id { get; }

		private int etatRotation;
		private Position positionBlocDansGrille;

		public Bloc()
		{
			positionBlocDansGrille = new Position(DecalageDepart.Ligne, DecalageDepart.Colonne);
		}

		public IEnumerable<Position> PositionsDesTuiles()
		{
			foreach (Position p in Tuiles[etatRotation])
			{
				yield return new Position(p.Ligne + positionBlocDansGrille.Ligne, p.Colonne + positionBlocDansGrille.Colonne);
			}
		}

		public void TournerSensHoraire()
		{
			etatRotation = (etatRotation + 1) % Tuiles.Length;
		}

        public void TournerSensAntihoraire()
        {
			if (etatRotation == 0)
				etatRotation = Tuiles.Length - 1;
			else
				etatRotation = (etatRotation - 1) ; 
        }

        public void AnnulerRotation()
		{
			if (etatRotation == 0)
				etatRotation = Tuiles.Length - 1;
			else
				etatRotation--;
		}

		public void Deplacer(int lignes, int colonnes)
		{
			positionBlocDansGrille.Ligne += lignes;
			positionBlocDansGrille.Colonne += colonnes;
		}

		public void Reinitialiser()
		{
			etatRotation = 0;
			positionBlocDansGrille.Ligne = DecalageDepart.Ligne;
			positionBlocDansGrille.Colonne = DecalageDepart.Colonne;
		}
	}
}
