using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public sealed class TetrominoDataZ : TetrominoData
    {
        public static readonly TetrominoDataZ Instance = new();
        public override CellState Type => CellState.Z;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 0, 1 },
                { 1, 1 },
                { 1, 0 },
            }),
            new(new byte[,]
            {
                { 1, 1, 0 },
                { 0, 1, 1 },
            }),
        };
    }
}