using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class LandscapeGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public GameObject[] objects;
    public Terrain terrain;
    public int objectsPerType = 3;

    public Transform player;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        GenerateTerrain();
        Bake();
    }

    void Start()
    {
        offsetX = Random.Range(1, 999);
        offsetY = Random.Range(1, 999);

        PlaceObjects();
        InitializePathVisualizer();
    }

    private void Bake()
    {
        NavMeshSurface navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
    }

    void GenerateTerrain()
    {
        Terrain terrainComponent = GetComponent<Terrain>();
        terrainComponent.terrainData = GenerateTerrainData(terrainComponent.terrainData);
    }

    TerrainData GenerateTerrainData(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, height, width);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, width];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / width * scale + offsetY;
                heights[x, y] = Mathf.PerlinNoise(xCoord, yCoord);
            }
        }
        return heights;
    }

    void PlaceObjects()
    {
        TerrainData terrainData = terrain.terrainData;
        
        foreach (GameObject obj in objects)
        {
            for (int i = 0; i < objectsPerType; i++)
            {
                Vector3 position = GetRandomPosition(terrainData);

                if (NavMesh.SamplePosition(position, out NavMeshHit hit, 1000000000.0f, NavMesh.AllAreas))
                {
                    Instantiate(obj, hit.position, Quaternion.identity);
                }
            }
        }
    }

    Vector3 GetRandomPosition(TerrainData terrainData)
    {
        float x = Random.Range(0, terrainData.size.x);
        float z = Random.Range(0, terrainData.size.z);
        float y = terrainData.GetHeight((int)x, (int)z);
        return new Vector3(x, y, z);
    }

    void InitializePathVisualizer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
    }

    void Update()
    {
        Transform closestObject = FindClosestObject();
        if (closestObject != null)
        {
            VisualizePath(player.position, closestObject.position);
        }
    }

    Transform FindClosestObject()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Collectible");
        Transform closest = null;
        float minDistance = float.MaxValue;

        foreach (GameObject obj in allObjects)
        {
            float distance = Vector3.Distance(player.position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = obj.transform;
            }
        }

        return closest;
    }

    void VisualizePath(Vector3 start, Vector3 end)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(start, end, NavMesh.AllAreas, path))
        {
            lineRenderer.positionCount = path.corners.Length;
            lineRenderer.SetPositions(path.corners);
        }
    }
}
