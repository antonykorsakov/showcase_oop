using UnityEngine.Tilemaps;

namespace Features.TetrisRendererModule.Core
{
    public interface ITetrisRendererConfig
    {
        TetrisGridRenderer View { get; }
        Tile EmptyTile { get; }
        Tile WallTile { get; }
        Tile ErrorTile { get; }

        Tile GetTetrominoTile(int index);
    }
}