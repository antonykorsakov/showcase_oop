using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataC : TetrominoData
    {
        public static readonly TetrominoDataC Instance = new();
        public override CellState Type => CellState.C;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 1, 1 },
                { 1, 0 },
                { 1, 1 },
            }),
            new(new byte[,]
            {
                { 1, 1, 1 },
                { 1, 0, 1 },
            }),
            new(new byte[,]
            {
                { 1, 1 },
                { 0, 1 },
                { 1, 1 },
            }),
            new(new byte[,]
            {
                { 1, 0, 1 },
                { 1, 1, 1 },
            }),
        };
    }
}