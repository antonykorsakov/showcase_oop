using System;
using Features.TetrisModule.Core.TetrominoType;

namespace Features.TetrisModule.Core.Data
{
    public enum CellState : byte
    {
        Empty = 0x0,
        Wall,

        // base tetromino
        I,
        O,
        T,
        L,
        S,
        J,
        Z,

        // extra tetromino
        Tb, // t-big
        Sb, // s-big
        Zb, // z-big
        Tank,
        X,
        W,
        C,

        //
        Error = 0xFF,
    }

    public static class GridCellStateExtensions
    {
        public static TetrominoData CreateTetrominoData(this CellState cellState)
        {
            return cellState switch
            {
                CellState.Empty => throw CreateErrorStateException(cellState),
                CellState.Wall => throw CreateErrorStateException(cellState),

                CellState.I => TetrominoDataI.Instance,
                CellState.O => TetrominoDataO.Instance,
                CellState.T => TetrominoDataT.Instance,
                CellState.J => TetrominoDataJ.Instance,
                CellState.L => TetrominoDataL.Instance,
                CellState.S => TetrominoDataS.Instance,
                CellState.Z => TetrominoDataZ.Instance,

                CellState.Tb => TetrominoDataTb.Instance,
                CellState.Sb => TetrominoDataSb.Instance,
                CellState.Zb => TetrominoDataZb.Instance,
                CellState.Tank => TetrominoDataTank.Instance,
                CellState.X => TetrominoDataX.Instance,
                CellState.W => TetrominoDataW.Instance,
                CellState.C => TetrominoDataC.Instance,

                CellState.Error => throw CreateErrorStateException(cellState),
                _ => throw CreateOutOfRangeException(cellState)
            };
        }

        private static InvalidOperationException CreateErrorStateException(CellState value)
            => new($"Operation cannot be performed on a cell with state: {value}.");

        private static ArgumentOutOfRangeException CreateOutOfRangeException(CellState value)
            => new(nameof(value), value, $"Encountered an unsupported grid cell state: {value}.");
    }
}