using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    public Camera mainCamera;
    public RenderTexture PrevRT;
    public RenderTexture CurrentRT;
    public RenderTexture TempRT;
    public Shader DrawShader;
    public Shader RippleShader;
    private Material RippleMat;
    private Material DrawMat;
    private Vector3 lastPos;
    private Material waterMaterial;
    [Range(0,1.0f)]
    public float DrawRedius = 0.2f;
    public int TextureSize = 512;
    void Start()
    {
        mainCamera =Camera.main.GetComponent<Camera>();
        CurrentRT = CreatRT();
        PrevRT = CreatRT();
        TempRT = CreatRT();
        DrawMat = new Material(DrawShader);
        RippleMat = new Material(RippleShader);
        GetComponent<Renderer>().material.mainTexture = CurrentRT;
        waterMaterial = GetComponent<Renderer>().material;

        // 【新增2】关键步骤：把初始的 CurrentRT 赋值给材质的 MainTex
        // 注意：如果你的 ASE 材质暴露的属性名不叫 "_MainTex"，请改成对应的名字（比如 "_BaseMap" 等）
        waterMaterial.SetTexture("MainTex", CurrentRT);

    }

    public RenderTexture CreatRT()
    {
        RenderTexture rt = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.ARGBHalf);
        rt.Create();
        return rt;
    }

    private void DrawAt(float x,float y,float radius)
    {
        Vector2 uv = new Vector2(x, y); 
        DrawMat.SetVector("_Pos", new Vector4(uv.x, uv.y, radius, 0)); 
        DrawMat.SetTexture("_SourceTex", CurrentRT);
        Graphics.Blit(null, TempRT, DrawMat);
        RenderTexture rt = TempRT;
        TempRT = CurrentRT;
        CurrentRT = rt;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.point - lastPos).sqrMagnitude > 0.1f)
                {
                    lastPos = hit.point;
                    DrawAt(hit.textureCoord.x, hit.textureCoord.y, DrawRedius);
                }
            }
        }
        RippleMat.SetTexture("_PrevRT", PrevRT);
        RippleMat.SetTexture("_CurrentRT", CurrentRT);
        Graphics.Blit(null, TempRT, RippleMat);
        Graphics.Blit(TempRT, PrevRT);
        RenderTexture rt = PrevRT;
        PrevRT = CurrentRT;
        CurrentRT = rt;
    }
}
