using UnityEngine;

namespace Kmnk.MessageBoard
{
    public class InputBoard : MonoBehaviour
    {
#pragma warning disable CS0414
        [SerializeField]
        [Tooltip("複数の MessageBoard を設置する際に対象の MessageBoard の id を指定します")]
        int _id = 0;

        [SerializeField]
        [Tooltip("ボード左上のピックアップ ON/OFF を切り替えます")]
        bool _pickupable = true;

        [SerializeField]
        [Tooltip("テンプレートメッセージ")]
        string[] _templateMessages = null;

        [SerializeField]
        [Tooltip("テンプレートメッセージのボタンが入るオブジェクト")]
        Transform _templateMessagesTransform;

        [SerializeField]
        [Tooltip("テンプレートメッセージを増やした時に複製するボタンのオブジェクト")]
        GameObject _templateMessageButtonOrigin;
#pragma warning restore CS0414
    }
}