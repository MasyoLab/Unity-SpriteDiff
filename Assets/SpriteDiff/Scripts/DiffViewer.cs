using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MasyoLab.SpriteDiff
{
    public class DiffViewer : MonoBehaviour
    {
        [SerializeField]
        private RawImage _rawImage = null;

        public void SetTexture2D(Texture texture)
        {
            _rawImage.texture = texture;
            _rawImage.SetNativeSize();
        }
    }
}
