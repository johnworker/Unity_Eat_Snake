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
        // �Ы�Render Texture�ñN��]�m��mirrorCamera��targetTexture
        mirrorTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mirrorCamera.targetTexture = mirrorTexture;

        // �NRender Texture�@��mirrorMaterial��_MainTex�ݩ�
        mirrorMaterial.SetTexture("_MainTex", mirrorTexture);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // �NmirrorTexture���������A�ˡA�q�ӹ�{��g�ĪG
        Graphics.Blit(mirrorTexture, destination, mirrorMaterial);
    }
}
