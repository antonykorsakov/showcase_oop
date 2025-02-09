using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    public class TetrominoDataTank : TetrominoData
    {
        public static readonly TetrominoDataTank Instance = new();
        public override CellState Type => CellState.Tank;

        protected override ShapeData[] Shapes { get; } =
        {
            new(new byte[,]
            {
                { 0, 1, 0 },
                { 1, 1, 1 },
                { 1, 0, 1 },
            }),
            new(new byte[,]
            {
                { 1, 1, 0 },
                { 0, 1, 1 },
                { 1, 1, 0 },
            }),
            new(new byte[,]
            {
                { 1, 0, 1 },
                { 1, 1, 1 },
                { 0, 1, 0 },
            }),
            new(new byte[,]
            {
                { 0, 1, 1 },
                { 1, 1, 0 },
                { 0, 1, 1 },
            }),
        };
    }
}