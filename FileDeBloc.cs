using System;
using System.Collections.Generic;
using System.Text;
using Tetris.Blocs;

namespace Tetris
{
	public class FileDeBloc
	{
		private readonly Bloc[] blocs = new Bloc[]
		{
			new BlocI(),
			new BlocJ(),
			new BlocL(),
			new BlocO(),
			new BlocS(),
			new BlocT(),
			new BlocZ()
		};

		private readonly Random random = new Random();

		public Bloc BlocSuivant {  get; private set; }

		public FileDeBloc()
		{
			BlocSuivant = ObtenirBlocAleatoire();
		}

		private Bloc ObtenirBlocAleatoire()
		{
			return blocs[random.Next(blocs.Length)];
		}

		public Bloc ObtenirBlocDifferent()
		{
			Bloc blocActuel = BlocSuivant;

			do
			{
				BlocSuivant = ObtenirBlocAleatoire();
			} while (BlocSuivant.Id == blocActuel.Id);

			return blocActuel;
		}

	}
}
