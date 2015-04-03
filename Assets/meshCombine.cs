using UnityEngine;
using UnityEditor;
using System.Collections;

public class meshCombine : MonoBehaviour {

	public GameObject meshToCombine;

	// Use this for initialization
	void Start () {
		OnWizardCreate();
	}
	
	void OnWizardCreate()
	{
		if (meshToCombine != null) {
			MeshFilter[] meshFilters = meshToCombine.GetComponentsInChildren<MeshFilter> ();
			CombineInstance[] combine = new CombineInstance[meshFilters.Length];
			int i = 0;
			while (i < meshFilters.Length) {
				combine[i].mesh = meshFilters[i].sharedMesh;
				combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
				//meshFilters[i].gameObject.active = false;
				i++;
			}
			
			GameObject combinedMesh = meshToCombine;
			

			combinedMesh.AddComponent<MeshFilter>();
			combinedMesh.AddComponent<MeshRenderer>();
			combinedMesh.GetComponent<MeshFilter>().sharedMesh = new Mesh();
			GameObject.Instantiate(combinedMesh.GetComponent<MeshFilter>().sharedMesh);
			combinedMesh.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);

			Mesh m1 = combinedMesh.GetComponent<MeshFilter>().mesh;
			AssetDatabase.CreateAsset(m1, "Assets/pine.asset"); // saves to "assets/"
			AssetDatabase.SaveAssets();
			
			//Object prefab = PrefabUtility.CreateEmptyPrefab( "Assets/" + "CombinedMesh" + ".prefab");
			//PrefabUtility.ReplacePrefab(combinedMesh, prefab, ReplacePrefabOptions.ConnectToPrefab);
			
			
			
		}
	}
}
