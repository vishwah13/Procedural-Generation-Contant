using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    public Material _material;
    
    private Vector3[] vertices;
    private int[] triangles;

    public int xSize;
    public int zSize;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GetComponent<MeshRenderer>().material = _material;

        CreateShape();
        
        UpdateMesh();
    }
    
    void CreateShape()
    {
       vertices = new Vector3[(xSize + 1) * (zSize + 1)];

       for (int i = 0, z = 0; z <= zSize; z++)
       {
           for (int x = 0; x <= xSize; x++)
           {
               //generate noise
               float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
               
               vertices[i] = new Vector3(x,y,z);
               i++;
           }
           
       }
       //number of triangles present in grid
       triangles = new int[xSize * zSize *6];

       //loop through xSize and create a quad
       int vert = 0;
       int tris = 0;

       for (int z = 0; z < zSize; z++)
       {
           for (int x = 0; x < xSize; x++)
           {
           
               triangles[tris + 0] = vert + 0;
               triangles[tris + 1] = vert + xSize + 1;
               triangles[tris + 2] = vert +1;
               triangles[tris + 3] = vert + 1;
               triangles[tris + 4] = vert + xSize + 1;
               triangles[tris + 5] = vert + xSize + 2;

               vert++;
               tris += 6;
           }
           vert++;
       }

    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if(vertices == null) return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i],.1f);
        }
    }
}
