using System;
using Moq;
using NUnit.Framework;
using Features.TetrisModule.Core.Data;
using Features.TetrisRendererModule.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.TetrisRendererModule.Tests.EditMode
{
    public class TetrisRendererTests
    {
        // private TetrisGridRendererController _controller;
        // private Mock<ITetrisRendererConfig> _mockConfig;
        // private Mock<TetrisGridRenderer> _mockGridRenderer;
        // private Mock<Tilemap> _mockTilemap;
        //
        // [SetUp]
        // public void SetUp()
        // {
        //     _mockConfig = new Mock<ITetrisRendererConfig>();
        //     _mockGridRenderer = new Mock<TetrisGridRenderer>();
        //     _mockTilemap = new Mock<Tilemap>();
        //
        //     // Подготовим GridRenderer с Tilemap
        //     _mockGridRenderer.Setup(gr => gr.Tilemap).Returns(_mockTilemap.Object);
        //
        //     // Подготовим конфиг с фейковыми данными
        //     _mockConfig.Setup(cfg => cfg.View).Returns(_mockGridRenderer.Object);
        //
        //     // Инициализируем контроллер с моками
        //     _controller = new TetrisGridRendererController(_mockConfig.Object);
        // }
        //
        // [Test]
        // public void EnableRenderer_ShouldCreateGridAndActivateGameObject()
        // {
        //     // Act
        //     _controller.EnableRenderer();
        //
        //     // Assert
        //     _mockGridRenderer.Verify(gr => gr.gameObject.SetActive(true), Times.Once);
        //     _mockConfig.Verify(cfg => cfg.View, Times.Once); // Проверка, что View было вызвано один раз
        // }
        //
        // [Test]
        // public void UpdateGridRenderer_ShouldUpdateCellsBasedOnState()
        // {
        //     // Arrange
        //     int width = 3;
        //     int height = 3;
        //     Func<int, int, GridCellState> getCellState = (x, y) => GridCellState.Empty;
        //
        //     // Act
        //     _controller.UpdateGridRenderer(width, height, getCellState);
        //
        //     // Assert
        //     // Проверим, что для каждой ячейки был вызван SetCell с ожидаемыми значениями
        //     _mockTilemap.Verify(tm => tm.SetTile(It.IsAny<Vector3Int>(), It.IsAny<Tile>()),
        //         Times.Exactly(width * height));
        // }
        //
        // [Test]
        // public void UpdateGridRenderer_ShouldSetCorrectTilesForEachCell()
        // {
        //     // Arrange
        //     int width = 2;
        //     int height = 2;
        //     var expectedTile = new Tile();
        //     Func<int, int, GridCellState> getCellState = (x, y) => GridCellState.Empty;
        //
        //     // Настроим конфиг, чтобы возвращать правильную плитку для каждого типа состояния
        //     var cellData = new CellData();
        //     _mockConfig.Setup(cfg => cfg.GetTetrominoData(It.IsAny<GridCellState>())).Returns(cellData);
        //     cellData.GetType().GetProperty("_gridCell")?.SetValue(cellData, expectedTile);
        //
        //     // Act
        //     _controller.UpdateGridRenderer(width, height, getCellState);
        //
        //     // Assert
        //     _mockTilemap.Verify(tm => tm.SetTile(It.IsAny<Vector3Int>(), expectedTile), Times.Exactly(width * height));
        // }
    }
}