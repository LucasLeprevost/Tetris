using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class Position
    {
        public int Ligne { get; set; }
        public int Colonne { get; set; }
        public Position(int ligne, int colonne)
        {
            this.Ligne = ligne;
            this.Colonne = colonne;
        }
    }
}
