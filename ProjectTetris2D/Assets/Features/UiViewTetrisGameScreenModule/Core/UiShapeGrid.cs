// using Cysharp.Threading.Tasks;

using System.Threading;
using Cysharp.Threading.Tasks;
using Features.LogModule.Core;
using Features.TetrisModule.Core.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UiViewTetrisGameScreenModule.Core
{
    public class UiShapeGrid : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _grid;

        private RectTransform _gridTransform;
        private GameObject[,] _items;
        private int _width;
        private int _height;

        private bool _isInitialized;

        public void Initialize(Image sourceItem) => InitializeAsync(sourceItem).Forget();

        public void Setup(ShapeData shapeData)
        {
            if (!_isInitialized)
                return;

            ResetItems();
            ActivateShape(shapeData);
            Centering(shapeData);
        }

        private async UniTaskVoid InitializeAsync(Image sourceItem)
        {
            await CreateItems(sourceItem);
            CollectItems();
            ResetItems();

            _isInitialized = true;
        }

        private async UniTask CreateItems(Image sourceItem)
        {
            _gridTransform = _grid.GetComponent<RectTransform>();
            var gridSize = _gridTransform.sizeDelta;
            var cellSize = _grid.cellSize;

            _width = (int)gridSize.x / (int)cellSize.x;
            _height = (int)gridSize.y / (int)cellSize.y;
            int count = _width * _height;

            for (int i = 0; i < count; i++)
            {
                var item = await FactoryAsync.InstantiateAsync(sourceItem, _gridTransform, CancellationToken.None);
                item.gameObject.SetActive(true);
            }

            _grid.enabled = false;

            // manual Rebuild
            _grid.CalculateLayoutInputHorizontal();
            _grid.SetLayoutHorizontal();
            _grid.CalculateLayoutInputVertical();
            _grid.SetLayoutVertical();
        }

        private void CollectItems()
        {
            _items = new GameObject[_width, _height];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    int index = y * _width + x;
                    _items[x, y] = _gridTransform.GetChild(index).gameObject;
                    _items[x, y].name = $"Item #{index} ({x}, {y})";
                }
            }
        }

        private void ResetItems()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _items[x, y].SetActive(false);
                }
            }
        }

        private void ActivateShape(ShapeData shapeData)
        {
            foreach (var pos in shapeData.CellsPositions)
                _items[pos.x, pos.y].SetActive(true);
        }

        private void Centering(ShapeData shapeData)
        {
            var gridSize = _gridTransform.sizeDelta;
            var cellSize = _grid.cellSize;

            var posX = (gridSize.x - cellSize.x * shapeData.Width) / 2f;
            var posY = (gridSize.y - cellSize.y * shapeData.Height) / 2f;

            _gridTransform.localPosition = new Vector3(posX, posY, 0f);
        }
    }
}