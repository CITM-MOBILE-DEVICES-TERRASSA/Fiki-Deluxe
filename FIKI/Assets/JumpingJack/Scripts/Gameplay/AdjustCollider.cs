using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class AdjustCollider : MonoBehaviour
{
    public GameObject colliderPrefab;
    private Tilemap tilemap;
    private Camera mainCamera;
    private readonly Dictionary<Vector3Int, GameObject> activeColliders = new Dictionary<Vector3Int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        mainCamera = Camera.main;

        GenerateCollidersInView();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateCollidersInView();

    }
    private void GenerateCollidersInView()
    {

        Vector3 cameraMin = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 cameraMax = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Bounds cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(cameraMin.x, cameraMin.y, 0),
            new Vector3(cameraMax.x, cameraMax.y, 0)
        );

        BoundsInt tileBounds = tilemap.cellBounds;

        
        HashSet<Vector3Int> visibleTiles = new HashSet<Vector3Int>();

        
        for (int x = tileBounds.xMin; x < tileBounds.xMax; x++)
        {
            for (int y = tileBounds.yMin; y < tileBounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cellPosition);

                if (tile != null)
                {
                    Vector3 tileWorldPosition = tilemap.CellToWorld(cellPosition) + tilemap.cellSize / 2;

                    
                    if (cameraBounds.Contains(tileWorldPosition))
                    {
                        visibleTiles.Add(cellPosition);

                        if (!activeColliders.ContainsKey(cellPosition))
                        {
                            GameObject newCollider = Instantiate(colliderPrefab, tileWorldPosition, Quaternion.identity, transform);
                            newCollider.name = $"Collider_{x}_{y}";

                            
                            BoxCollider2D boxCollider = newCollider.GetComponent<BoxCollider2D>();
                            if (boxCollider != null)
                            {
                                boxCollider.size = tilemap.cellSize;
                            }

                            activeColliders[cellPosition] = newCollider;
                        }
                    }
                }
            }
        }

        List<Vector3Int> toRemove = new List<Vector3Int>();
        foreach (var pair in activeColliders)
        {
            if (!visibleTiles.Contains(pair.Key))
            {
                Destroy(pair.Value);
                toRemove.Add(pair.Key);
            }
        }

        
        foreach (var key in toRemove)
        {
            activeColliders.Remove(key);
        }
    }
}
