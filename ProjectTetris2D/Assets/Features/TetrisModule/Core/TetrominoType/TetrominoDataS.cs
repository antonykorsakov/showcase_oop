using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataS : TetrominoData
    {
        public static readonly TetrominoDataS Instance = new();
        public override CellState Type => CellState.S;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 1, 0 },
                { 1, 1 },
                { 0, 1 },
            }),
            new(new byte[,]
            {
                { 0, 1, 1 },
                { 1, 1, 0 },
            }),
        };
    }
}