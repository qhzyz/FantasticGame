using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutlineEffect : MonoBehaviour {

    public Camera objectCamera = null;
    public Color outlineColor = Color.green;
    Camera mainCamera;
    RenderTexture depthTexture;
    RenderTexture occlusionTexture;
    RenderTexture strechTexture;
    RenderTexture outputTexture;
    RenderTexture inputTexture;
    Material m;
    [SerializeField]
    List<GameObject> renderObjects = new List<GameObject>();
    // Use this for initialization
    void Start() {
        mainCamera = Camera.main;
        mainCamera.depthTextureMode = DepthTextureMode.Depth;
        objectCamera.depthTextureMode = DepthTextureMode.None;
        objectCamera.cullingMask = 1 << LayerMask.NameToLayer("Outline");
        objectCamera.fieldOfView = mainCamera.fieldOfView;
        objectCamera.clearFlags = CameraClearFlags.Color;
        objectCamera.projectionMatrix = mainCamera.projectionMatrix;
        objectCamera.nearClipPlane = mainCamera.nearClipPlane;
        objectCamera.farClipPlane = mainCamera.farClipPlane;
        objectCamera.targetTexture = depthTexture;
        objectCamera.aspect = mainCamera.aspect;
        objectCamera.orthographic = false;
        objectCamera.enabled = false;

        m = new Material(Shader.Find("ShapeOutline/DoNothing"));
    }

    // Update is called once per frame
    void Update() {

    }

    void OnRenderImage(RenderTexture srcTex, RenderTexture dstTex) {
        outputTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
        inputTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
        Graphics.Blit(srcTex, inputTexture, m);
        for (int i = 0; i < renderObjects.Count; i++) {
            depthTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 24, RenderTextureFormat.Depth);
            occlusionTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
            strechTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);

            int orgLayer = renderObjects[i].layer;
            renderObjects[i].layer = LayerMask.NameToLayer("Outline");

            objectCamera.targetTexture = depthTexture;
            objectCamera.RenderWithShader(Shader.Find("ShapeOutline/Depth"), string.Empty);

            Material mat = new Material(Shader.Find("ShapeOutline/Occlusion"));
            mat.SetColor("_OutlineColor", outlineColor);
            Graphics.Blit(depthTexture, occlusionTexture, mat);

            mat = new Material(Shader.Find("ShapeOutline/StrechOcclusion"));
            mat.SetColor("_OutlineColor", outlineColor);
            Graphics.Blit(occlusionTexture, strechTexture, mat);


            mat = new Material(Shader.Find("ShapeOutline/MultiMix"));
            mat.SetColor("_OutlineColor", outlineColor);
            mat.SetTexture("_occlusionTex", occlusionTexture);
            mat.SetTexture("_strechTex", strechTexture);
            Graphics.Blit(inputTexture, outputTexture, mat);

            RenderTexture.ReleaseTemporary(depthTexture);
            RenderTexture.ReleaseTemporary(occlusionTexture);
            RenderTexture.ReleaseTemporary(strechTexture);
            renderObjects[i].layer = orgLayer;

            Graphics.Blit(outputTexture, inputTexture, m);
        }
        Graphics.Blit(outputTexture, dstTex, m);

        RenderTexture.ReleaseTemporary(inputTexture);
        RenderTexture.ReleaseTemporary(outputTexture);
    }
}