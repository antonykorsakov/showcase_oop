using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataI : TetrominoData
    {
        public static readonly TetrominoDataI Instance = new();
        public override CellState Type => CellState.I;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 1 },
                { 1 },
                { 1 },
                { 1 },
            }),
            new(new byte[,]
            {
                { 1, 1, 1, 1 },
            }),
        };
    }
}