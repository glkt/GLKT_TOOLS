// GLKT - Benjamin Vedrenne

using UnityEditor;
using UnityEngine;
using System.Collections;


// Simple script that randomizes the rotation of the Selected GameObjects
// and lets you see which objects are currently selected
public class CreatePrefabs : EditorWindow {
	

	[MenuItem ("GLKT/Create Prefabs")]
	static void CreatePrefabsUI()
	{
	    Transform[] transforms = Selection.transforms;
	    foreach (Transform t in transforms) {
	        Object prefab = EditorUtility.CreateEmptyPrefab("Assets/"+t.gameObject.name+".prefab");
	        EditorUtility.ReplacePrefab(t.gameObject, prefab, ReplacePrefabOptions.ConnectToPrefab);
	    }
	}

}

