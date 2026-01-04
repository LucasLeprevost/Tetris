using System.Collections.Generic;
using Tetris.metier;

namespace Tetris.metier.Blocs
{
    public class BlocO : Bloc
    {
        private readonly Position[][] tuiles = new Position[][]
        {
            new Position[] { new(0, 0), new(0, 1), new(1, 0), new(1, 1) }
        };

        public override int Id => 4;
        protected override Position[][] Tuiles => tuiles;
        protected override Position DecalageDepart => new Position(0, 4);
    }
}