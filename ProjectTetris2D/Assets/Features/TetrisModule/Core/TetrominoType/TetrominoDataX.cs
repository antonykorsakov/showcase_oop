using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataX : TetrominoData
    {
        public static readonly TetrominoDataX Instance = new();
        public override CellState Type => CellState.X;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 0, 1, 0 },
                { 1, 1, 1 },
                { 0, 1, 0 },
            }),
        };
    }
}