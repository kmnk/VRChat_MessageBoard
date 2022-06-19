using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace Kmnk.MessageBoard.Udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class MessageBoardViewer : UdonSharpBehaviour
    {
        [SerializeField]
        Udon.MessageBoard _messageBoard = null;

        [SerializeField]
        Text _titleText = null;

        [SerializeField]
        GameObject _logLinesParent = null;

        private LogLine[] _logLines = null;

        private void Start()
        {
            _titleText.text = _messageBoard.GetTitle();

            _logLines = _logLinesParent.GetComponentsInChildren<LogLine>();
            InitializeLogLines();

            _messageBoard.AddEventListener(this);
        }

        public void OnDisplayAllLogLines()
        {
            DisplayAllLogLines();
        }

        private void InitializeLogLines()
        {
            foreach (var l in _logLines)
            {
                l.Initialize();
            }
        }

        private bool HasAllLogLinesInitialized()
        {
            foreach (var l in _logLines)
            {
                if (!l.HasInitialized()) { return false; }
            }
            return true;
        }

        private void DisplayAllLogLines()
        {
            if (!HasAllLogLinesInitialized()) { return; }

            var messages = _messageBoard.GetMessages();
            var names = _messageBoard.GetNames();
            var times = _messageBoard.GetTimes();

            for (var i = 0; i < _logLines.Length; i++)
            {
                _logLines[i].Display(messages[i], names[i], times[i]);
            }
        }
    }
}