namespace Tetris
{
	public class EtatJeu
	{
		private Bloc blocActuel = null!;

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


		public GrilleJeu Grille { get; }
		public FileDeBloc File { get; }
		public bool Perdu { get; private set; }
		public int Score { get; private set; }
		public Bloc BlocEnReserve { get; private set; }
		public bool PeutEchanger { get; private set; }


		public EtatJeu()
		{
			Grille = new GrilleJeu(22, 10);
			File = new FileDeBloc();
			BlocActuel = File.ObtenirBlocDifferent();
			PeutEchanger = true;
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
			{
				BlocActuel.TournerSensAntihoraire();
				if (!BlocPeutTenir())
					BlocActuel.TournerSensHoraire();
			}
			else
			{
				BlocActuel.TournerSensHoraire();

				if (!BlocPeutTenir())
					BlocActuel.TournerSensAntihoraire();
			}
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

		public void EchangerBlocReserve()
		{
			if (!PeutEchanger)
				return;

			if (BlocEnReserve == null)
			{
				BlocEnReserve = BlocActuel;
				BlocActuel = File.ObtenirBlocDifferent();
			}
			else
			{
				Bloc temp = BlocActuel;
				BlocActuel = BlocEnReserve;
				BlocEnReserve = temp;
			}
			PeutEchanger = false;
		}

		private void PlacerBloc()
		{
			foreach (Position p in BlocActuel.PositionsDesTuiles())
				Grille[p.Ligne, p.Colonne] = BlocActuel.Id;

			Score += Grille.NettoyerLignesPleines();

			if (EstDefaite())
				Perdu = true;
			else
			{
				BlocActuel = File.ObtenirBlocDifferent();
				PeutEchanger = true;
			}
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


		public int DistanceChuteTuile ()
		{
			int distanceChute = Grille.Lignes;
			int distanceTuile;

			foreach ( Position p in BlocActuel.PositionsDesTuiles())
			{
				distanceTuile = 0;
				while (Grille.EstVide(p.Ligne + distanceTuile + 1, p.Colonne))
					distanceTuile++;

				if (distanceTuile < distanceChute)
					distanceChute = distanceTuile;
			}

			return distanceChute;
		}

		public void ChuteInstantanee()
		{
			BlocActuel.Deplacer(DistanceChuteTuile(), 0);
			PlacerBloc();
		}



	}
}