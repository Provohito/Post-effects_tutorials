using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DitherFilter : SimpleFilter
{
    [SerializeField] private Texture2D _noiseTexture;
    [SerializeField] private Texture2D _rampTexture;

    private EdgeRender _edgeRender;

    protected override void Init()
    {
        _edgeRender = GetComponent<EdgeRender>();
    }
    protected override void UseFilter(RenderTexture src, RenderTexture dst)
    {
        _mat.SetTexture("_NoiseTex", _noiseTexture);
        _mat.SetTexture("_ColorRampTex", _rampTexture);

        RenderTexture big = RenderTexture.GetTemporary(src.width * 2, src.height * 2);
        RenderTexture half = RenderTexture.GetTemporary(src.width / 2, src.height / 2);

        RenderTexture edged = RenderTexture.GetTemporary(src.width, src.height);
        _edgeRender.RenderBySobel(src, edged);
        _mat.SetTexture("_EdgedTex", edged);

        Graphics.Blit(src, big);
        Graphics.Blit(big, half, _mat);
        Graphics.Blit(half, dst);

        RenderTexture.ReleaseTemporary(big);
        RenderTexture.ReleaseTemporary(half);
        RenderTexture.ReleaseTemporary(edged);
    }
}
