using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.TetrisRendererModule.Core
{
    public class TetrisGridRenderer : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;

        public Tilemap Tilemap => _tilemap;
    }
}