using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataJ : TetrominoData
    {
        public static readonly TetrominoDataJ Instance = new();
        public override CellState Type => CellState.J;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 0, 1 },
                { 0, 1 },
                { 1, 1 },
            }),
            new(new byte[,]
            {
                { 1, 0, 0 },
                { 1, 1, 1 },
            }),
            new(new byte[,]
            {
                { 1, 1 },
                { 1, 0 },
                { 1, 0 },
            }),
            new(new byte[,]
            {
                { 1, 1, 1 },
                { 0, 0, 1 },
            }),
        };
    }
}