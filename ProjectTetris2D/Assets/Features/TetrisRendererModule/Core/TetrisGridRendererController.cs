using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Features.LogModule.Core;
using Features.TetrisModule.Core.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.TetrisRendererModule.Core
{
    public sealed class TetrisGridRendererController : ITetrisGridRendererController
    {
        private const int XOffset = 4;
        private const int YOffset = 10;

        private readonly ITetrisRendererConfig _config;
        private TetrisGridRenderer _tetrisGridRenderer;
        private IReadOnlyList<CellState> _permittedStates;

        public TetrisGridRendererController(ITetrisRendererConfig config)
        {
            _config = config;
        }

        public async UniTask EnableRendererAsync()
        {
            if (_tetrisGridRenderer == null)
                _tetrisGridRenderer = await FactoryAsync.InstantiateAsync(_config.View, null, CancellationToken.None);

            _tetrisGridRenderer.gameObject.SetActive(true);
        }

        public void PrepareGridRenderer(int width, int height, IReadOnlyList<CellState> permittedStates)
        {
            _permittedStates = permittedStates;
            Log.Print($"Permitted States: {string.Join(" , ", permittedStates)};");

            for (int x = -XOffset; x < width + XOffset; x++)
            {
                for (int y = -YOffset; y < height; y++)
                {
                    var tile = _config.WallTile;
                    SetCell(x, y, tile);
                }

                for (int y = height; y < height + YOffset; y++)
                {
                    var tile = _config.EmptyTile;
                    SetCell(x, y, tile);
                }
            }
        }

        public void UpdateGridRenderer(int width, int height, Func<int, int, CellState> getCellState)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var cellState = getCellState(x, y);
                    var tile = FindTile(cellState);
                    SetCell(x, y, tile);
                }
            }
        }

        private void SetCell(int x, int y, Tile gridCell)
        {
            var tilemap = _tetrisGridRenderer.Tilemap;
            tilemap.SetTile(new Vector3Int(x, y), gridCell);
        }

        private Tile FindTile(CellState currentState)
        {
            switch (currentState)
            {
                case CellState.Empty:
                    return _config.EmptyTile;

                case CellState.Wall:
                    return _config.WallTile;

                case CellState.Error:
                    return _config.ErrorTile;
            }

            for (var index = 0; index < _permittedStates.Count; index++)
            {
                var cellState = _permittedStates[index];
                if (currentState == cellState)
                    return _config.GetTetrominoTile(index);
            }

            return _config.ErrorTile;
        }
    }
}