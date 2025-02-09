using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataO : TetrominoData
    {
        public static readonly TetrominoDataO Instance = new();
        public override CellState Type => CellState.O;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 1, 1 },
                { 1, 1 },
            }),
        };
    }
}