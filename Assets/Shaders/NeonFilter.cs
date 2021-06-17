using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonFilter : SimpleFilter
{
    protected override void UseFilter(RenderTexture src, RenderTexture dst)
    {
        RenderTexture neonText = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);
        RenderTexture thresholdTex = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);
        RenderTexture blurTex = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);

        Graphics.Blit(src, neonText, _mat, 0);
        Graphics.Blit(neonText, thresholdTex, _mat, 1);
        Graphics.Blit(thresholdTex, blurTex, _mat, 2);
        _mat.SetTexture("_SrcTex", neonText);
        Graphics.Blit(blurTex, dst, _mat, 3);

        RenderTexture.ReleaseTemporary(neonText);
        RenderTexture.ReleaseTemporary(thresholdTex);
        RenderTexture.ReleaseTemporary(blurTex);
    }
}
