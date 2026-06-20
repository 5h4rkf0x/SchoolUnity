using UnityEngine;

public class Cubelinrenderer : MonoBehaviour
{
    private CubeManager cubeManager;
    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        cubeManager = GetComponentInParent<CubeManager>();
    }

    private void Start()
    {
        line.positionCount = 2;
        line.material = new Material(Shader.Find("Sprites/Default"));
    }

    private void Update()
    {
        if (cubeManager == null)
        {
            cubeManager = GetComponentInParent<CubeManager>();
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, cubeManager.transform.position);
    }
}
