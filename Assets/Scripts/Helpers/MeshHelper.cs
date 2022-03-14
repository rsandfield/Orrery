using UnityEngine;

public static class MeshHelper
{
    public static void InvertNormals(this Mesh mesh)
    {
        Vector3[] normals = mesh.normals;
        for(int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        int[] tris = mesh.GetTriangles(0);
        for(int i = 0; i < tris.Length; i += 3)
        {
            int temp = tris[i];
            tris[i] = tris[i + 1];
            tris[i + 1] = temp;
        }
        mesh.SetTriangles(tris, 0);
    }
}