using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEdit : MonoBehaviour {
    public Color [] COLOR_LIST = {Color.white, Color.blue, Color.green, Color.yellow,Color.red,Color.black };
    public const int UNIT_LENGTH = 50;
    float screenW = 0;
    float screenH = 0;

    public GameObject canvas;
    public bool ifLoadFromFile = false;
    public string fileName;
    public int width = 0;
    public int height = 0;
    GameObject Place;
    GameObject Select;
    Transform setStartButton;
    
    public GameObject M_normal, M_water, M_wood, M_wind, M_fire;
    public MapData mapData;

    // Use this for initialization
    void Start () {
        screenW = canvas.transform.GetComponent<RectTransform>().sizeDelta.x;
        screenH = canvas.transform.GetComponent<RectTransform>().sizeDelta.y;
        mapData = transform.GetComponent<MapData>();
        Place = (GameObject)Resources.Load("mapEditor_place");
        Select = (GameObject)Resources.Load("mapEditor_select");
        Select.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(UNIT_LENGTH,UNIT_LENGTH);
        M_normal = (GameObject)Resources.Load("normalPlace");
        M_water = (GameObject)Resources.Load("waterPlace");
        M_wood = (GameObject)Resources.Load("woodPlace");
        M_wind = (GameObject)Resources.Load("windPlace");
        M_fire = (GameObject)Resources.Load("firePlace");


        InitSelectBoard();
        if (ifLoadFromFile)
            InitPlaceBoardFromFile();
        else
            InitPlaceBoard();

    }
    void InitPlaceBoard() {
        GameObject UIText = (GameObject)Resources.Load("UIText");
        mapData.width = height;
        mapData.length = width;
        float unitWidth = (screenW - UNIT_LENGTH) / width;
        float unitHeight = (screenH - UNIT_LENGTH) / (height + 2);
        if (unitWidth < unitHeight) { unitHeight = unitWidth; }
        if (unitWidth > unitHeight) { unitWidth = unitHeight; }

        float position_x = -screenW / 2 + unitWidth / 2;
        float position_y = screenH / 2 - unitHeight / 2;
        Transform setStartButton = transform.parent.Find("Canvas").Find("SetStartPoint");
        setStartButton.GetComponent<SetStartPoint>().places = new GameObject[height*width];
        for (int i = 0; i < height; i++) {
            for (int j = 0; j <width; j++)
            {
                Place.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(unitWidth, unitHeight);
                //Place.transform.localPosition = new Vector3(position_x + j * (unitWidth+1.5f), position_y-i*(unitHeight+1.5f), 0);
                Place.transform.localPosition = new Vector3(position_x + (width-1) * (unitWidth + 1.5f) -  j * (unitWidth+1.5f), position_y-i*(unitHeight+1.5f), 0);
                Place.transform.GetComponent<RawImage>().color = COLOR_LIST[(int)Place_Kind.PLACE_NONE];

                GameObject tempPlace = Instantiate(Place, transform.parent.Find("Canvas"));
                tempPlace.transform.GetComponent<Place>().posX = j;
                tempPlace.transform.GetComponent<Place>().posY = i;
                tempPlace.transform.GetComponent<Place>().pos = i * width + j;
                setStartButton.GetComponent<SetStartPoint>().places[i * width + j] = tempPlace;
                setStartButton.GetComponent<SetStartPoint>().count++;

                GameObject tempText = Instantiate(UIText);
                tempText.transform.SetParent(tempPlace.transform);
                tempText.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
    void InitPlaceBoardFromFile()
    {
        GameObject UIText = (GameObject)Resources.Load("UIText");
        mapData.LoadFromFile(fileName);

        width = mapData.length;
        height = mapData.width;
        float unitWidth = (screenW - UNIT_LENGTH) / width;
        float unitHeight = (screenH - UNIT_LENGTH) / (height + 2);
        if (unitWidth < unitHeight) { unitHeight = unitWidth; }
        if (unitWidth > unitHeight) { unitWidth = unitHeight; }

        float position_x = -screenW / 2 + unitWidth / 2;
        float position_y = screenH / 2 - unitHeight / 2;
        Transform setStartButton = transform.parent.Find("Canvas").Find("SetStartPoint");
        setStartButton.GetComponent<SetStartPoint>().places = new GameObject[height * width];

        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                GameObject tempPlace = Instantiate(Place, transform.parent.Find("Canvas"));
                tempPlace.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(unitWidth, unitHeight);
                tempPlace.transform.localPosition = new Vector3(position_x + (width - 1) * (unitWidth + 1.5f) - j * (unitWidth + 1.5f), position_y - i * (unitHeight + 1.5f), 0);
                tempPlace.transform.GetComponent<RawImage>().color = COLOR_LIST[(int)Place_Kind.PLACE_NONE];
                tempPlace.transform.GetComponent<Place>().posX = j;
                tempPlace.transform.GetComponent<Place>().posY = i;
                tempPlace.transform.GetComponent<Place>().pos = i * width + j;

                setStartButton.GetComponent<SetStartPoint>().places[i * width + j] = tempPlace;
                setStartButton.GetComponent<SetStartPoint>().count++;

                GameObject tempText = Instantiate(UIText);
                tempText.transform.SetParent(tempPlace.transform);
                tempText.transform.localPosition = new Vector3(0, 0, 0);

                tempPlace.GetComponent<RawImage>().color = COLOR_LIST[(int)mapData.DataArr[i * width + j].kind];
                for (int k = 0; k < mapData.DataArr[i * width + j].height; k++) {
                    switch (mapData.DataArr[i * width + j].kind) {
                        case Place_Kind.PLACE_NORMAL:
                            M_normal.transform.position = new Vector3(mapData.DataArr[i * width + j].x * MapData.UNIT_SIZE,
                                                                      k * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                                      mapData.DataArr[i * width + j].y * MapData.UNIT_SIZE);
                            tempPlace.GetComponent<Place>().tempIn[k]= Instantiate(M_normal);
                            break;
                        case Place_Kind.PLACE_WATER:
                            M_water.transform.position = new Vector3(mapData.DataArr[i * width + j].x * MapData.UNIT_SIZE,
                                                                      k * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                                      mapData.DataArr[i * width + j].y * MapData.UNIT_SIZE);
                            tempPlace.GetComponent<Place>().tempIn[k] = Instantiate(M_water);
                            break;
                        case Place_Kind.PLACE_WOOD:
                            M_wood.transform.position = new Vector3(mapData.DataArr[i * width + j].x * MapData.UNIT_SIZE,
                                                                      k * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                                      mapData.DataArr[i * width + j].y * MapData.UNIT_SIZE);
                            tempPlace.GetComponent<Place>().tempIn[k] = Instantiate(M_wood);
                            break;
                        case Place_Kind.PLACE_WIND:
                            M_wind.transform.position = new Vector3(mapData.DataArr[i * width + j].x * MapData.UNIT_SIZE,
                                                                      k * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                                      mapData.DataArr[i * width + j].y * MapData.UNIT_SIZE);
                            tempPlace.GetComponent<Place>().tempIn[k] = Instantiate(M_wind);
                            break;
                        case Place_Kind.PLACE_FIRE:
                            M_fire.transform.position = new Vector3(mapData.DataArr[i * width + j].x * MapData.UNIT_SIZE,
                                                                      k * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                                      mapData.DataArr[i * width + j].y * MapData.UNIT_SIZE);
                            tempPlace.GetComponent<Place>().tempIn[k] = Instantiate(M_fire);
                            break;
                    }
                }
            }
        }
    }

    void InitSelectBoard()
    {
        float position_x = -screenW / 2 + UNIT_LENGTH/2;
        float position_y = -screenH / 2 + UNIT_LENGTH/2;
        for(int i=0;i<MapData.KIND_NUM;i++)
        {
            Select.transform.localPosition = new Vector3(position_x + i * UNIT_LENGTH, position_y, 0);
            switch ((Place_Kind)i)
            {
                case Place_Kind.PLACE_NORMAL:
                    Select.transform.GetComponent<RawImage>().color = COLOR_LIST[(int)Place_Kind.PLACE_NORMAL];
                    break;
                case Place_Kind.PLACE_WATER:
                    Select.transform.GetComponent<RawImage>().color = COLOR_LIST[(int)Place_Kind.PLACE_WATER];
                    break;
                case Place_Kind.PLACE_WOOD:
                    Select.transform.GetComponent<RawImage>().color = COLOR_LIST[(int)Place_Kind.PLACE_WOOD];
                    break;
                case Place_Kind.PLACE_WIND:
                    Select.transform.GetComponent<RawImage>().color = COLOR_LIST[(int)Place_Kind.PLACE_WIND];
                    break;
                case Place_Kind.PLACE_FIRE:
                    Select.transform.GetComponent<RawImage>().color = COLOR_LIST[(int)Place_Kind.PLACE_FIRE];
                    break;
            }
            Instantiate(Select, transform.parent.Find("Canvas"));
        }
    }

	// Update is called once per frame
	void Update () {
    }
}
