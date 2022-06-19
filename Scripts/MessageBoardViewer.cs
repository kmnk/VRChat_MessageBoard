using UnityEngine;

namespace Kmnk.MessageBoard
{
    public class MessageBoardViewer : MonoBehaviour
    {
#pragma warning disable CS0414
        [SerializeField]
        [Tooltip("複数の MessageBoard を設置する際に指定します")]
        int _id = 0;

        [SerializeField]
        [Tooltip("ボード左上のピックアップ ON/OFF を切り替えます")]
        bool _pickupable = true;
#pragma warning restore CS0414
    }
}