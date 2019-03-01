using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour {
    public string fileName;
	// Use this for initialization
	void Start () {
        transform.GetComponent<Button>().onClick.AddListener(() => {
            transform.parent.parent.Find("Main Camera").GetComponent<MapEdit>().mapData.SaveToFile(fileName);
        });
    }
	// Update is called once per frame
	void Update () {
		
	}
}
