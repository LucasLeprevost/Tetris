using System.Text;
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
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
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
					Canvas.SetTop(imageControl, ligne * celluleTaille);
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
					imageControls[ligne, colonne].Source = tuilesImg[idTuile];
				}
			}
        }

		private void DessinerBloc(Bloc bloc)
		{
			foreach (Position pos in bloc.PositionsDesTuiles())
			{
				imageControls[pos.Ligne, pos.Colonne].Source = tuilesImg[bloc.Id];
            }
        }

		private void Dessiner(EtatJeu etat)
		{
			DessinerGrille(etat.Grille);
			DessinerBloc(etat.BlocActuel);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
		{

		}

		private void TetrisCanvas_Loaded(object sender, RoutedEventArgs e)
		{
			Dessiner(etatJeu);
        }

		private void BoutonRejouerClick (object sender, RoutedEventArgs e)
		{

		}

    }
}