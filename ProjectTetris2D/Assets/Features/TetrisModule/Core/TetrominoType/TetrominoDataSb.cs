using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataSb : TetrominoData
    {
        public static readonly TetrominoDataSb Instance = new();
        public override CellState Type => CellState.Sb;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 1, 0, 0 },
                { 1, 1, 1 },
                { 0, 0, 1 },
            }),
            new(new byte[,]
            {
                { 0, 1, 1 },
                { 0, 1, 0 },
                { 1, 1, 0 },
            }),
        };
    }
}