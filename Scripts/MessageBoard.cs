using UnityEngine;

namespace Kmnk.MessageBoard
{
    public class MessageBoard : MonoBehaviour
    {
#pragma warning disable CS0414
        [SerializeField]
        [Tooltip("複数の MessageBoard を設置する際に指定します")]
        int _id = 0;

        [SerializeField]
        [Tooltip("インスタンスのオーナー以外には書き込めないモードにします")]
        bool _onlyWorldOwnerMode = false;

        [SerializeField]
        [Tooltip("Message Board 上部のタイトル部分のテキストを指定します")]
        string _title = "";

        [SerializeField]
        [Tooltip("ログの時間部分のフォーマットを指定します")]
        string _timeFormat = "HH:mm:ss";

        [SerializeField]
        [Tooltip("ワールド作成時に最初から入れておくメッセージを指定します")]
        string[] _initialMessages = null;

        [SerializeField]
        [Tooltip("ワールド作成時に最初から入れておくメッセージの名前部分を指定します")]
        string _initialName = "";

        [SerializeField]
        [Tooltip("ワールド作成時に最初から入れておくメッセージの時間部分を指定します")]
        string _initialTime = "--:--:--";
#pragma warning restore CS0414

        public int GetId()
        {
            return _id;
        }
    }
}