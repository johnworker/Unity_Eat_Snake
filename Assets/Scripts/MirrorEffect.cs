using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorEffect : MonoBehaviour
{
    public Camera mirrorCamera;
    public Material mirrorMaterial;

    private RenderTexture mirrorTexture;

    private void Start()
    {
        // 創建Render Texture並將其設置為mirrorCamera的targetTexture
        mirrorTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mirrorCamera.targetTexture = mirrorTexture;

        // 將Render Texture作為mirrorMaterial的_MainTex屬性
        mirrorMaterial.SetTexture("_MainTex", mirrorTexture);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // 將mirrorTexture中的像素顛倒，從而實現鏡射效果
        Graphics.Blit(mirrorTexture, destination, mirrorMaterial);
    }
}
