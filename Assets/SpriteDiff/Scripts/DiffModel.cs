using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace MasyoLab.SpriteDiff
{
    public class DiffModel
    {
        /// <summary>
        /// 2つのTexture2Dを比較して、差分がある場所を書き込んだTexture2Dを出力する
        /// </summary>
        /// <param name="texture1"></param>
        /// <param name="texture2"></param>
        /// <returns></returns>
        public Texture2D GetDifferenceTexture2D(Texture2D texture1, Texture2D texture2)
        {
            // 出力用Texture2D
            var diffTexture = new Texture2D(texture1.width, texture1.height, TextureFormat.ARGB32, false);

            // RenderTextureを作成して書き込む
            var renderTextureA = RenderTexture.GetTemporary(diffTexture.width, diffTexture.height);
            Graphics.Blit(texture1, renderTextureA);
            var renderTextureB = RenderTexture.GetTemporary(diffTexture.width, diffTexture.height);
            Graphics.Blit(texture2, renderTextureB);

            // Texture2Dを作成してRenderTextureから書き込む
            //現在のRenderTextureをキャッシュする
            var preRT = RenderTexture.active;

            RenderTexture.active = renderTextureA;
            var textureA = new Texture2D(diffTexture.width, diffTexture.height);
            textureA.ReadPixels(new Rect(0, 0, diffTexture.width, diffTexture.height), 0, 0);
            textureA.Apply();

            RenderTexture.active = renderTextureB;
            var textureB = new Texture2D(diffTexture.width, diffTexture.height);
            textureB.ReadPixels(new Rect(0, 0, diffTexture.width, diffTexture.height), 0, 0);
            textureB.Apply();

            // キャッシュしたRenderTextureを元に戻す
            RenderTexture.active = preRT;

            // RenderTextureの解除
            RenderTexture.ReleaseTemporary(renderTextureA);
            RenderTexture.ReleaseTemporary(renderTextureB);

            // 比較用
            var pixelsA = textureA.GetPixels();
            var pixelsB = textureB.GetPixels();

            for (int i = 0; i < Mathf.Min(pixelsA.Length, pixelsB.Length); i++)
            {
                var pixelA = pixelsA[i];
                var pixelB = pixelsB[i];
                Color color = Color.clear;

                if (pixelA != pixelB)
                {
                    color = Color.white;
                }
                pixelsA[i] = color;
            }

            diffTexture.SetPixels(pixelsA);
            diffTexture.Apply();

            return diffTexture;
        }
    }
}
