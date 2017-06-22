// GLKT - Benjamin Vedrenne

using UnityEditor;
using UnityEngine;
using System.Collections;


// Simple script that randomizes the rotation of the Selected GameObjects
// and lets you see which objects are currently selected
public class RenameAssets : EditorWindow {
	
	public float rotationAmount = 0.33F;
	public string selected = "";

	public bool RenameAndIncrement = false;
	public string BaseName = "MyObject_";	
	public int StartNumber = 0;
	public int Increment = 1;
	public bool addPrefix = false;
	public string prefix = "prefix_";
	public bool addSufix = false;
	public string sufix = "_sufix";
	public bool SetFirstLetterUpperCase = false;
	public bool SetFirstLetterLowerCase = false;
	public bool searchAndReplace = false;
	public string toSearch = "";
	public string toReplace = "";


	void OnGUI() {
		
		string count = ""+Selection.objects.Length;
		EditorGUILayout.LabelField("selected assets:", count);

		EditorGUILayout.Space();

		RenameAndIncrement = EditorGUILayout.Toggle("Rename and increment", RenameAndIncrement);
		BaseName = EditorGUILayout.TextField("base name", BaseName);
		StartNumber = EditorGUILayout.IntField("Start number", StartNumber);
		Increment = EditorGUILayout.IntField("Increment", Increment);

		EditorGUILayout.Space();

		addPrefix = EditorGUILayout.Toggle("addPrefix", addPrefix);
		prefix = EditorGUILayout.TextField("prefix:", prefix);

		EditorGUILayout.Space();

		addSufix = EditorGUILayout.Toggle("addSufix", addSufix);
		sufix = EditorGUILayout.TextField("sufix:", sufix);

		EditorGUILayout.Space();

		SetFirstLetterUpperCase = EditorGUILayout.Toggle("first char Uppercase", SetFirstLetterUpperCase);
		SetFirstLetterLowerCase = EditorGUILayout.Toggle("first char lowercase", SetFirstLetterLowerCase);

		EditorGUILayout.Space();
		searchAndReplace = EditorGUILayout.Toggle("search and replace", searchAndReplace);
		toSearch = EditorGUILayout.TextField("search :", toSearch);
		toReplace = EditorGUILayout.TextField("replace by:", toReplace);

		EditorGUILayout.Space();

		if (GUILayout.Button("Rename selected assets"))
			doRenameAssets();

		EditorGUILayout.Space();

		if (GUILayout.Button("Close"))
			Close();
	}

	void OnInspectorUpdate() {
		Repaint();
	}

	[MenuItem("GLKT/rename asset")]
	static void RenameAssetsWindow() {
		RenameAssets window = new RenameAssets();
		window.ShowUtility();
		window.minSize = new Vector2(400,400);
	}

	void doRenameAssets() {
		Object[] objects = Selection.objects;

		if (Selection.objects == null)
			return;

		int digit = StartNumber;

		foreach(Object obj in Selection.objects) {

			string path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

			string name = obj.name;

			if(SetFirstLetterLowerCase || SetFirstLetterUpperCase){
				char firstChar = name[0];
				string newChar = ""+firstChar;
				if(SetFirstLetterLowerCase){
					newChar = newChar.ToLower();
				}else{
					newChar = newChar.ToUpper();
				}
				name = name.Remove(0,1);
   				name = name.Insert(0,newChar);
			}
			if(addPrefix){
				name = prefix + name;
			}
			if(addSufix){
				name = name + sufix;
			}
			if(RenameAndIncrement){
				name = BaseName + digit;
				digit += Increment;
			}

			if(searchAndReplace){
				if(name.Contains(toSearch)){
				     //genString = "<name>";
				     name = name.Replace(toSearch, toReplace);
				     //print(genString);
				     //genString = strTest;
				     //strTest = "has changed their name to " + strTest;
				     //currentPlayer.name = genString;

				    /* genString = toSearch;
				     name.Replace(toSearch, "");
				     print(genString);
				     genString = strTest;
				     strTest = "has changed their name to " + strTest;
				     currentPlayer.name = genString;*/
				}
			}

			AssetDatabase.RenameAsset(path,name);
		}
	}
}