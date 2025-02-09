using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataW : TetrominoData
    {
        public static readonly TetrominoDataW Instance = new();
        public override CellState Type => CellState.W;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 1, 0, 0 },
                { 1, 1, 0 },
                { 0, 1, 1 },
            }),
            new(new byte[,]
            {
                { 0, 1, 1 },
                { 1, 1, 0 },
                { 1, 0, 0 },
            }),
            new(new byte[,]
            {
                { 1, 1, 0 },
                { 0, 1, 1 },
                { 0, 0, 1 },
            }),
            new(new byte[,]
            {
                { 0, 0, 1 },
                { 0, 1, 1 },
                { 1, 1, 0 },
            }),
        };
    }
}