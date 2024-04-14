using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MasyoLab.SpriteDiff
{
    public class DiffPresenter : MonoBehaviour
    {
        [SerializeField]
        private DiffViewer _diffViewer = null;

        [SerializeField]
        private Sprite _sprite1 = null;
        [SerializeField]
        private Sprite _sprite2 = null;

        private void Start()
        {
            var diffModel = new DiffModel();
            var texture2D = diffModel.GetDifferenceTexture2D(_sprite1.texture, _sprite2.texture);
            _diffViewer.SetTexture2D(texture2D);
        }
    }
}
