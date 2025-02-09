using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.TetrisRendererModule.Core
{
    [CreateAssetMenu(fileName = nameof(TetrisRendererConfig),
        menuName = "Configs/" + nameof(TetrisRendererConfig), order = 'T')]
    public class TetrisRendererConfig : ScriptableObject, ITetrisRendererConfig
    {
        public const string TetrominoCellsFieldName = nameof(_tetrominoCells);

        [SerializeField] private TetrisGridRenderer _view;
        [SerializeField] private Tile _emptyCell;
        [SerializeField] private Tile _wallCell;
        [SerializeField] private Tile _errorCell;

        [SerializeField] private Tile[] _tetrominoCells = Array.Empty<Tile>();

        public TetrisGridRenderer View => _view;
        public Tile EmptyTile => _emptyCell;
        public Tile WallTile => _wallCell;
        public Tile ErrorTile => _errorCell;

        public Tile GetTetrominoTile(int index)
        {
            if (index < 0)
                return _errorCell;

            index = index % _tetrominoCells.Length;
            return _tetrominoCells[index];
        }
    }
}