using System.Collections.Generic;

namespace Tetris
{
    public class BlocT : Bloc
    {
        private readonly Position[][] tuiles = new Position[][]
        {
            new Position[] { new(0, 1), new(1, 0), new(1, 1), new(1, 2) },
            new Position[] { new(0, 1), new(1, 1), new(1, 2), new(2, 1) },
            new Position[] { new(1, 0), new(1, 1), new(1, 2), new(2, 1) },
            new Position[] { new(0, 1), new(1, 0), new(1, 1), new(2, 1) }
        };

        public override int Id => 6;
        protected override Position[][] Tuiles => tuiles;
        protected override Position DecalageDepart => new Position(0, 3);
    }
}