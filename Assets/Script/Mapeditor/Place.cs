using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Place : MonoBehaviour {
    readonly Color [] COLOR_LIST = {Color.white, Color.blue, Color.green, Color.yellow,Color.red,Color.black };
    GameObject heightText;
    public MapData mapData;
    bool ifIn = false;
    public int pos;
    public int posX, posY;
    public GameObject M_normal, M_water, M_wood, M_wind, M_fire;
    public GameObject[] tempIn = new GameObject[MapData.MAX_HEIGHT];
    public  static bool isSetStartPoint;
    void Start() {
        heightText = transform.Find("UIText(Clone)").gameObject;
        mapData = transform.parent.parent.Find("Main Camera").GetComponent<MapEdit>().mapData;

        Debug.Log(mapData.DataArr[1].height);
        mapData.DataArr[pos].x = posX;
        mapData.DataArr[pos].y = posY;
        M_normal = transform.parent.parent.Find("Main Camera").GetComponent<MapEdit>().M_normal;
        M_water = transform.parent.parent.Find("Main Camera").GetComponent<MapEdit>().M_water;
        M_wood = transform.parent.parent.Find("Main Camera").GetComponent<MapEdit>().M_wood;
        M_wind = transform.parent.parent.Find("Main Camera").GetComponent<MapEdit>().M_wind;
        M_fire = transform.parent.parent.Find("Main Camera").GetComponent<MapEdit>().M_fire;
        isSetStartPoint = false;
        SynText();
    }

    // Update is called once per frame
    void Update() {
        if (!isSetStartPoint) {
            if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(transform.GetComponent<RectTransform>(), Input.mousePosition)) {
                OnClickL();
                ifIn = true;
                SynText();
            }
            if (Input.GetMouseButtonDown(1) && RectTransformUtility.RectangleContainsScreenPoint(transform.GetComponent<RectTransform>(), Input.mousePosition)) {
                OnClickR();
                SynText();
            }
            if (Input.GetMouseButtonUp(0)) {
                ifIn = false;
            }
            if (Input.GetMouseButton(0) && RectTransformUtility.RectangleContainsScreenPoint(transform.GetComponent<RectTransform>(), Input.mousePosition) && !ifIn) {
                ifIn = true;
                OnClickL();
                SynText();
            }
        }
        else {
            if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(transform.GetComponent<RectTransform>(), Input.mousePosition)) {
                if (transform.GetComponent<RawImage>().color != Color.gray) {
                    transform.GetComponent<RawImage>().color = Color.gray;
                }
                else {
                    transform.GetComponent<RawImage>().color = COLOR_LIST[(int)mapData.DataArr[pos].kind];
                }
                mapData.DataArr[pos].isStart = !mapData.DataArr[pos].isStart;
            }
        }
    }

    Place_Kind GetKind(Color gcolor) {
        for (int i = 0; i < MapData.KIND_NUM; i++) {
            if (COLOR_LIST[i] == gcolor) {
                return (Place_Kind)i;
            }
        }
        return (Place_Kind)0;
    }
    public void OnClickL() {
        Color tempColor = transform.parent.Find("UsingTag").GetComponent<RawImage>().color;
        mapData.DataArr[pos].kind = GetKind(tempColor);
        if (transform.GetComponent<RawImage>().color != tempColor) {
            transform.GetComponent<RawImage>().color = tempColor;
            if (mapData.DataArr[pos].height != 0) {
                for (int i = 0; i < mapData.DataArr[pos].height; i++) {
                    GameObject.Destroy(tempIn[i]);
                }
                for (int i = 0; i < mapData.DataArr[pos].height; i++) {
                    GotoPosition(mapData.DataArr[pos], i);
                    tempIn[i] = InstantiateMap(mapData.DataArr[pos].kind);
                }
                return;
            }
        }
        Add();
    }
    void OnClickR() {
        if (mapData.DataArr[pos].height > 0) {
            Delete();
        }
    }
    void Delete() {
        for (int i = 0; i < mapData.DataArr[pos].height; i++) {
            GameObject.Destroy(tempIn[i]);
        }
        mapData.DataArr[pos].height--;
        for (int i = 0; i < mapData.DataArr[pos].height; i++) {
            GotoPosition(mapData.DataArr[pos], i);
            tempIn[i] = InstantiateMap(mapData.DataArr[pos].kind);
        }
        if (mapData.DataArr[pos].height == 0) {
            transform.GetComponent<RawImage>().color = COLOR_LIST[(int)Place_Kind.PLACE_NONE];
        }
    }
    void Add() {
        for (int i = 0; i < mapData.DataArr[pos].height; i++) {
            GameObject.Destroy(tempIn[i]);
        }
        if (mapData.DataArr[pos].height < MapData.MAX_HEIGHT) {
            mapData.DataArr[pos].height++;
        }
        for (int i = 0; i < mapData.DataArr[pos].height; i++) {
            GotoPosition(mapData.DataArr[pos], i);
            tempIn[i] = InstantiateMap(mapData.DataArr[pos].kind);
        }
    }
    void SynText() {
        heightText.GetComponent<Text>().text = mapData.DataArr[pos].height.ToString();
    }
    public void GotoPosition(MapUnit ld, int lheight) {
        switch (ld.kind) {
            case Place_Kind.PLACE_NORMAL:
                M_normal.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
            case Place_Kind.PLACE_WATER:
                M_water.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
            case Place_Kind.PLACE_WOOD:
                M_wood.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
            case Place_Kind.PLACE_WIND:
                M_wind.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
            case Place_Kind.PLACE_FIRE:
                M_fire.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
        }
    }
    public GameObject InstantiateMap(Place_Kind lkind) {
        switch (lkind) {
            case Place_Kind.PLACE_NORMAL:
                return Instantiate(M_normal);
            case Place_Kind.PLACE_WATER:
                return Instantiate(M_water);
            case Place_Kind.PLACE_WOOD:
                return Instantiate(M_wood);
            case Place_Kind.PLACE_WIND:
                return Instantiate(M_wind);
            case Place_Kind.PLACE_FIRE:
                return Instantiate(M_fire);
        }
        return null;
    }
}
