using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectPlace : MonoBehaviour {
    public Transform usingTag;
    public GameObject canvas;
    public float screenW;
    public float screenH;
    // Use this for initialization
    void Start () {
        canvas = transform.parent.gameObject;
        screenW = canvas.transform.GetComponent<RectTransform>().sizeDelta.x;
        screenH = canvas.transform.GetComponent<RectTransform>().sizeDelta.y;
        usingTag = transform.parent.Find("UsingTag");
        usingTag.transform.localPosition = new Vector3(screenW/2 - MapEdit.UNIT_LENGTH/2, - screenH/2 + MapEdit.UNIT_LENGTH / 2);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)&&RectTransformUtility.RectangleContainsScreenPoint(transform.GetComponent<RectTransform>(),Input.mousePosition)) {
            usingTag.transform.GetComponent<RawImage>().color = transform.GetComponent<RawImage>().color;
        }

    }
}
