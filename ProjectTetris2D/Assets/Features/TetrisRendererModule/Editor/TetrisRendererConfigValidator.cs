using System.Collections.Generic;
using System.Reflection;
using Features.TetrisModule.Core.Data;
using Features.TetrisRendererModule.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.TetrisRendererModule.Editor
{
    public static class TetrisRendererConfigValidator
    {
        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;


        [MenuItem("Tetris Tools/Validate TetrisRendererConfig")]
        public static void ValidateConfigs()
        {
            var result = Validate();
            var msg = $"--- '{nameof(TetrisRendererConfig)}' {(result ? "correct" : "not correct")} ---";

            if (result)
                Debug.Log(msg);
            else
                Debug.LogError(msg);
        }

        private static bool Validate()
        {
            var tetrisCoreConfig = FindConfig<TetrisCoreConfig>();
            if (tetrisCoreConfig == null)
            {
                Debug.LogError($"Validation failed: '{nameof(TetrisCoreConfig)}' not found.");
                return false;
            }

            var tetrisRendererConfig = FindConfig<TetrisRendererConfig>();
            if (tetrisRendererConfig == null)
            {
                Debug.LogError($"Validation failed: '{nameof(TetrisRendererConfig)}' not found.");
                return false;
            }

            var rendererConfigType = typeof(TetrisRendererConfig);
            var field = rendererConfigType.GetField(TetrisRendererConfig.TetrominoCellsFieldName, BindingAttr);
            if (field == null)
            {
                Debug.LogError(
                    $"Validation failed: Field '{TetrisRendererConfig.TetrominoCellsFieldName}' not found " +
                    $"in '{nameof(TetrisRendererConfig)}'.", tetrisRendererConfig);
                return false;
            }

            var tiles = (Tile[])field.GetValue(tetrisRendererConfig);
            if (tiles == null)
            {
                Debug.LogError("Validation failed: Tiles array is null.", tetrisRendererConfig);
                return false;
            }

            var result = true;
            result &= ValidateUniqueTiles(tetrisRendererConfig, tiles);
            result &= ValidateCount(tetrisCoreConfig, tetrisRendererConfig, tiles.Length);

            return result;
        }

        private static bool ValidateUniqueTiles(TetrisRendererConfig tetrisRendererConfig, Tile[] tiles)
        {
            var result = true;
            var uniqueTiles = new HashSet<Tile>();

            foreach (var tile in tiles)
            {
                if (!uniqueTiles.Add(tile))
                {
                    Debug.LogError($"Validation failed: Duplicate tile found: '{tile.name}'.", tetrisRendererConfig);
                    result = false;
                }
            }

            return result;
        }

        private static bool ValidateCount(TetrisCoreConfig tetrisCoreConfig, TetrisRendererConfig tetrisRendererConfig,
            int limit)
        {
            var result = true;
            for (int i = 0; i < tetrisCoreConfig.ShapesDifficultyLevelsCount; i++)
            {
                var tetrominoTypes = tetrisCoreConfig.GetTetrominoTypesByDifficulty(i);
                var tetrominoTypesCount = tetrominoTypes.Count;

                if (tetrominoTypesCount > limit)
                {
                    Debug.LogError(
                        $"Validation failed for difficulty level {i}: Expected {limit} tetromino colors, " +
                        $"but found {tetrominoTypesCount}.", tetrisRendererConfig);

                    result = false;
                }
            }

            return result;
        }

        private static T FindConfig<T>()
            where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(ScriptableObject)}");

            int count = 0;
            T config = null;

            foreach (var guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

                if (asset is T item)
                {
                    count++;
                    config = item;
                }
            }

            switch (count)
            {
                case > 1:
                    Debug.LogError($"Found more than one {typeof(T)} in the project.");
                    return null;

                case 0:
                    Debug.LogError($"No {typeof(T)} found in the project.");
                    return null;

                default:
                    return config;
            }
        }
    }
}