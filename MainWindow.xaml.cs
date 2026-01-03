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

namespace Tetris
{

	public partial class MainWindow : Window
	{
		private readonly ImageSource[] tuilesImg = new ImageSource[]
		{
			new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/TileCyan.png",UriKind.Relative)),
			new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
		};

		private readonly ImageSource[] blockImage = new ImageSource[]
		{
			new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/Block-I.png",UriKind.Relative)),
			new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
			new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
		};

		private readonly Image[,] imageControls;
		private readonly int delaiMax = 700;
		private readonly int delaiMin = 75;
		private readonly int diminutionDuDelais = 25;

		private EtatJeu etatJeu = new EtatJeu();

		public MainWindow()
		{
			InitializeComponent();
			imageControls = MiseEnPLaceJeuCanvas(etatJeu.Grille);
		}

		private Image[,] MiseEnPLaceJeuCanvas(GrilleJeu grille)
		{
			Image[,] imageControls = new Image[grille.Lignes, grille.Colonnes];
			int celluleTaille = 25;

			for (int ligne = 0; ligne < grille.Lignes; ligne++)
			{
				for ( int colonne = 0; colonne < grille.Colonnes; colonne++)
				{
					Image imageControl = new Image
					{
						Width = celluleTaille,
						Height = celluleTaille
					};

					Canvas.SetTop(imageControl, (ligne-2) * celluleTaille + 10);

					Canvas.SetLeft(imageControl, colonne * celluleTaille);
					TetrisCanvas.Children.Add(imageControl);
					imageControls[ligne, colonne] = imageControl;
				}
			}
			return imageControls;
		}

		private void DessinerGrille(GrilleJeu grille)
		{
			for (int ligne = 0; ligne < grille.Lignes; ligne++)
			{
				for (int colonne = 0; colonne < grille.Colonnes; colonne++)
				{
					int idTuile = grille[ligne, colonne];
                    imageControls[ligne, colonne].Opacity = 1;
                    imageControls[ligne, colonne].Source = tuilesImg[idTuile];
				}
			}
		}

		private void DessinerBloc(Bloc bloc)
		{
			foreach (Position pos in bloc.PositionsDesTuiles())
			{
				imageControls[pos.Ligne, pos.Colonne].Opacity = 1.0;
                imageControls[pos.Ligne, pos.Colonne].Source = tuilesImg[bloc.Id];
			}
		}

		private void DessinerProchainBloc(FileDeBloc fileDeBlocs)
		{
			Bloc prochainBloc = fileDeBlocs.BlocSuivant;
			ImgSuivante.Source = blockImage[prochainBloc.Id];
		}

		private void DessinerBlocReserve(Bloc blocReserve)
		{
			if (blocReserve != null)
				ImgReserve.Source = blockImage[blocReserve.Id];
			else
				ImgReserve.Source = blockImage[0];
		}

		private void DessinerBlocFantome(Bloc bloc)
		{
			int distanceChute = etatJeu.DistanceChuteTuile();

			foreach (Position pos in bloc.PositionsDesTuiles())
			{
				imageControls[pos.Ligne + distanceChute, pos.Colonne].Opacity = 0.25; ;
				imageControls[pos.Ligne + distanceChute, pos.Colonne].Source = tuilesImg[bloc.Id];
			}

		}

		private void Dessiner(EtatJeu etat)
		{
			DessinerGrille(etat.Grille);
			DessinerBlocFantome(etat.BlocActuel);
			DessinerBloc(etat.BlocActuel);
			DessinerProchainBloc(etat.File);
			DessinerBlocReserve(etat.BlocEnReserve);
			ScoreText.Text = $"Score: {etat.Score}";
		}


		private async Task JeuBouclePrincipale()
		{
			int delai;
			
			Dessiner(etatJeu);

			while (!etatJeu.Perdu)
			{
				delai = Math.Max(delaiMin, delaiMax - (etatJeu.Score * diminutionDuDelais));
				await Task.Delay(delai);
				etatJeu.DeplacerBlocBas();
				Dessiner(etatJeu);
			}

			MenuJeuFini.Visibility = Visibility.Visible;
			TexteScoreFinale.Text = $"Score Final: {etatJeu.Score}";
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (etatJeu.Perdu)
				return;

			switch (e.Key)
			{
				case Key.Left:
					etatJeu.DeplacerBlocGauche();
					break;
				case Key.Right:
					etatJeu.DeplacerBlocDroite();
					break;
				case Key.Down:	
					etatJeu.DeplacerBlocBas();
					break;
				case Key.Up:
					etatJeu.TournerBloc('G');
					break;
				case Key.Z:
					etatJeu.TournerBloc('D');
					break;
				case Key.C:
					etatJeu.EchangerBlocReserve();
					break;
				case Key.Space:
					etatJeu.ChuteInstantanee();
					break;
				default:
					return;
			}

			Dessiner(etatJeu);
		}

		private async void TetrisCanvas_Loaded(object sender, RoutedEventArgs e)
		{
			await JeuBouclePrincipale();
		}

		private async void BoutonRejouerClick (object sender, RoutedEventArgs e)
		{
			etatJeu = new EtatJeu();
			MenuJeuFini.Visibility = Visibility.Hidden;
			await JeuBouclePrincipale();
		}

	}
}