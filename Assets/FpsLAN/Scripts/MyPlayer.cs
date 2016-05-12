using UnityEditor;
using UnityEngine;
using System.Collections;


// Example script with properties.
public class MyPlayer : MonoBehaviour {
	public int damage;
	public int armor;
	public GameObject gun;

	// ...other code...
}


// Custom Editor the "old" way by modifying the script variables directly.
// No handling of multi-object editing, undo, and prefab overrides!
[CustomEditor (typeof(MyPlayer))]
public class MyPlayerEditor : Editor {
	string objNames="";
	public override void OnInspectorGUI () {
		MyPlayer mp = (MyPlayer)target;

		mp.damage = EditorGUILayout.IntSlider ("Damage", mp.damage, 0, 100);
		ProgressBar (mp.damage / 100.0f, "Damage");

		mp.armor = EditorGUILayout.IntSlider ("Armor", mp.armor, 0, 100);
		ProgressBar (mp.armor / 100.0f, "Armor");

		bool allowSceneObjects = !EditorUtility.IsPersistent (target);
		mp.gun = (GameObject)EditorGUILayout.ObjectField ("Gun Object", mp.gun, typeof(GameObject), allowSceneObjects);


		EditorGUI.HelpBox (GUILayoutUtility.GetRect (18, 18, 30, 30), "sdfgdfgsdfdsfasd", MessageType.Info);
		EditorGUI.HelpBox (GUILayoutUtility.GetRect (18, 18, "TextField"), "sdfgdfgsdfdsfasd", MessageType.Info);
	}

	// Custom GUILayout progress bar.
	void ProgressBar (float value, string label) {
		// Get a rect for the progress bar using the same margins as a textfield:
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, label);
		EditorGUILayout.Space ();
	}
}