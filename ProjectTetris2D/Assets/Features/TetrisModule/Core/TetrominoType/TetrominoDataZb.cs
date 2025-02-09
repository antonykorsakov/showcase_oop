using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataZb : TetrominoData
    {
        public static readonly TetrominoDataZb Instance = new();
        public override CellState Type => CellState.Zb;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 0, 0, 1 },
                { 1, 1, 1 },
                { 1, 0, 0 },
            }),
            new(new byte[,]
            {
                { 1, 1, 0 },
                { 0, 1, 0 },
                { 0, 1, 1 },
            }),
        };
    }
}