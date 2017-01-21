using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveManager : MonoBehaviour
{

	public int width = 130;
	public int height = 125;
	public Mesh screenMesh;

	void Awake()
	{
		screenMesh = createMesh();
	}

	protected  Mesh createMesh()
	{
		Mesh m = new Mesh();

		//assign vertices
		Vector3[] vertices = new Vector3[width * height];
		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++)
			{
				vertices[x + width * y] = new Vector3(x / (float)(width - 1) - 0.5f, y / (float)(height - 1) - 0.5f); //pos from [0,1] to [-0.5,0.5],
				vertices[x + width * y] *= 2; //from [-0.5,0.5] to [-1,1], so it can be treat as clip space vertex pos in vertex shader directly
			}

		//assign triangle
		int[] triangles = new int[(width - 1) * (height - 1) * 6];
		for (int x = 0; x < width - 1; x++)
			for (int y = 0; y < height - 1; y++)
			{
				//clock-wise triangle A
				triangles[x * 6 + (width - 1) * 6 * y + 0] = x + width * y; //bottomLeft vert
				triangles[x * 6 + (width - 1) * 6 * y + 1] = x + width * (y + 1); //topLeft vert
				triangles[x * 6 + (width - 1) * 6 * y + 2] = (x + 1) + width * (y + 1); //topRightvert
				//clock-wise triangle B
				triangles[x * 6 + (width - 1) * 6 * y + 3] = triangles[x * 6 + (width - 1) * 6 * y + 0]; //bottomLeft vert
				triangles[x * 6 + (width - 1) * 6 * y + 4] = triangles[x * 6 + (width - 1) * 6 * y + 2]; //topRight vert
				triangles[x * 6 + (width - 1) * 6 * y + 5] = triangles[x * 6 + (width - 1) * 6 * y + 0] + 1; //bottomRight vert
			}

		m.vertices = vertices;
		m.triangles = triangles;
		return m;
	}


	RenderTexture mainRT;
	void OnPreRender()
	{
		mainRT = RenderTexture.GetTemporary(Screen.width,Screen.height, 16);
		GetComponent<Camera>().targetTexture = mainRT;//render to RenderTexture, not framebuffer
	}

	public Material distortionMaterial;
	public int passNum = 0;
	void OnPostRender()
	{
		GetComponent<Camera>().targetTexture = null; //now render to framebuffer
		RenderTexture.active = null; //must place before set pass
		//tell the post process Material to use this RenderTexture as main texture's input
		distortionMaterial.mainTexture = mainRT;
		distortionMaterial.SetPass(passNum); //define which pass in shader to use

		//only the first param(mesh) is useful, you can enter anything for other params
		Graphics.DrawMeshNow(screenMesh, Vector3.zero, Quaternion.identity);

		RenderTexture.ReleaseTemporary(mainRT);
	}
}
