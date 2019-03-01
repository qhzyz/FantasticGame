using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStartPoint : MonoBehaviour {
    readonly Color[] COLOR_LIST = { Color.white, Color.blue, Color.green, Color.yellow, Color.red, Color.black };
    public GameObject[] places;
    public int count=0;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) &&this== RectTransformUtility.RectangleContainsScreenPoint(transform.GetComponent<RectTransform>(), Input.mousePosition)) {

            if (Place.isSetStartPoint == false) {
                transform.GetComponent<Image>().color = Color.red;
                for (int i = 0; i < count; i++) {
                    if (places[i].GetComponent<Place>().mapData.DataArr[i].isStart) {
                        places[i].GetComponent<RawImage>().color = Color.gray;
                    }
                }
            }
            else {
                transform.GetComponent<Image>().color = Color.white;
                for (int i = 0; i < count; i++) {
                    places[i].GetComponent<RawImage>().color = COLOR_LIST[(int)places[i].GetComponent<Place>().mapData.DataArr[i].kind];
                }
            }
            Place.isSetStartPoint = !Place.isSetStartPoint;
        }
    }
}
