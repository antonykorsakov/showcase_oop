using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataL : TetrominoData
    {
        public static readonly TetrominoDataL Instance = new();
        public override CellState Type => CellState.L;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 1, 0 },
                { 1, 0 },
                { 1, 1 },
            }),
            new(new byte[,]
            {
                { 1, 1, 1 },
                { 1, 0, 0 },
            }),
            new(new byte[,]
            {
                { 1, 1 },
                { 0, 1 },
                { 0, 1 },
            }),
            new(new byte[,]
            {
                { 0, 0, 1 },
                { 1, 1, 1 },
            }),
        };
    }
}