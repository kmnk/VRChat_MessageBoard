using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace Kmnk {
    public class LogLine : UdonSharpBehaviour
    {
        [SerializeField]
        private Text _messageText = null;

        [SerializeField]
        private Text _nameText = null;

        [SerializeField]
        private Text _timeText = null;

        public void Initialize()
        {
			this._messageText.text = string.Empty;
			this._nameText.text = string.Empty;
			this._timeText.text = string.Empty;
        }

        public bool HasInitialized()
        {
            if (this._messageText == null) { return false; }
            if (this._nameText == null) { return false; }
            if (this._timeText == null) { return false; }
            return true;
        }

        public void Display(string message, string name, string time)
        {
            this._messageText.text = message;
            this._nameText.text = name;
            this._timeText.text = time;
        }
    }
}