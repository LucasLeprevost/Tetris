namespace Tetris
{
	public class EtatJeu
	{
		private Bloc blocActuel;

		public GrilleJeu Grille { get; }
		public FileDeBloc File { get; }
		public bool Perdu { get; private set; }
		public int Score { get; private set; }

		public Bloc BlocActuel
		{
			get => blocActuel;
			private set
			{
				blocActuel = value;
				blocActuel.Reinitialiser();

				for (int i = 0; i < 2; i++)
				{
					blocActuel.Deplacer(1, 0);
					if (!BlocPeutTenir())
						blocActuel.Deplacer(-1, 0);
				}
			}
		}

		public EtatJeu()
		{
			Grille = new GrilleJeu(22, 10);
			File = new FileDeBloc();
			BlocActuel = File.ObtenirBlocDifferent();
		}

		private bool BlocPeutTenir()
		{
			foreach (Position p in BlocActuel.PositionsDesTuiles())
			{
				if (!Grille.EstVide(p.Ligne, p.Colonne))
					return false;
			}
			return true;
		}

		public void TournerBloc(char sens)
		{
			if (sens == 'G')
				BlocActuel.TournerSensAntihoraire();
            else
                BlocActuel.TournerSensHoraire();

			if (!BlocPeutTenir())
				BlocActuel.AnnulerRotation();
		}

		public void DeplacerBlocGauche()
		{
			BlocActuel.Deplacer(0, -1);

			if (!BlocPeutTenir())
				BlocActuel.Deplacer(0, 1);

		}

		public void DeplacerBlocDroite()
		{
			BlocActuel.Deplacer(0, 1);

			if (!BlocPeutTenir())
				BlocActuel.Deplacer(0, -1);
			
		}

		private bool EstDefaite()
		{
			return !(Grille.EstVideLigne(0) && Grille.EstVideLigne(1));
        }

		private void PlacerBloc()
		{
			foreach (Position p in BlocActuel.PositionsDesTuiles())
				Grille[p.Ligne, p.Colonne] = BlocActuel.Id;

			Score += Grille.NettoyerLignesPleines();

			if (EstDefaite())
				Perdu = true;
			else
				BlocActuel = File.ObtenirBlocDifferent();
		}

		public void DeplacerBlocBas()
		{
			BlocActuel.Deplacer(1, 0);

			if (!BlocPeutTenir())
			{
				BlocActuel.Deplacer(-1, 0);
				PlacerBloc();
			}
		}
	}
}