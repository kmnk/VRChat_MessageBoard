using System;
using System.Globalization;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace Kmnk.MessageBoard.Udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class MessageBoard : UdonSharpBehaviour
    {
        [SerializeField]
        bool _onlyWorldOwnerMode = false;

        [SerializeField]
        string _title = "";

        [SerializeField]
        string _timeFormat = "HH:mm:ss";

        [SerializeField]
        string[] _initialMessages = null;

        [SerializeField]
        string _initialName = "";

        [SerializeField]
        string _initialTime = "--:--:--";

        [SerializeField]
        Text _titleText = null;

        [SerializeField]
        GameObject _logLinesParent = null;

        [SerializeField]
        GameObject _inputLine = null;

        [SerializeField]
        Button _activateButton = null;

        [SerializeField]
        GameObject _inputParent = null;

        [SerializeField]
        InputField _inputField = null;

        [UdonSynced]
        private string[] _times = null;

        [UdonSynced]
        private string[] _names = null;

        [UdonSynced]
        private string[] _messages = null;

        private LogLine[] _logLines = null;

        private void Start()
        {
            _titleText.text = _title;

            _logLines = _logLinesParent.GetComponentsInChildren<LogLine>();
            InitializeLogLines();

            if (!AmIOwner() && _onlyWorldOwnerMode)
            {
                _inputLine.gameObject.SetActive(false);
            }

            if (AmIOwner())
            {
                InitializeUdonSyncedFields();
                RequestSerialization();
            }

            DisplayAllLogLines();
        }

        public override void OnDeserialization()
        {
            if (!HasAllLogLinesInitialized()) { return; }
            if (!HasAllUdonSyncedFieldInitialized()) { return; }
            DisplayAllLogLines();
        }

        public bool IsOnlyWorldOwnerMode()
        {
            return this._onlyWorldOwnerMode;
        }

        public void RegisterMessage()
        {
            AddMessage(_inputField.text);
        }

        public void DeactivateInput()
        {
            _activateButton.gameObject.SetActive(true);
            _inputParent.SetActive(false);
        }

        public void ActivateInput()
        {
            _activateButton.gameObject.SetActive(false);
            _inputParent.SetActive(true);
            _inputField.text = string.Empty;
            _inputField.Select();
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

        private void InitializeUdonSyncedFields()
        {
            var l = _logLines.Length;
            _times = new string[l];
            _names = new string[l];
            _messages = new string[l];

            for (var i = 0; i < l; i++)
            {
                if (_initialMessages != null && _initialMessages.Length > i)
                {
                    _messages[i] = _initialMessages[i];
                    _names[i] = _initialName;
                    _times[i] = _initialTime;
                }
                else
                {
                    _messages[i] = string.Empty;
                    _names[i] = string.Empty;
                    _times[i] = string.Empty;
                }
            }
        }


        private bool HasAllUdonSyncedFieldInitialized()
        {
            if (_times == null) { return false; }
            if (_names == null) { return false; }
            if (_messages == null) { return false; }
            return true;
        }

        public void AddMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) { return; }
            if (!HasAllUdonSyncedFieldInitialized()) { return; }

            TakeOwner();

            var l = _logLines.Length;
            for (var i = l-1; i > 0; i--) {
                _times[i] = _times[i - 1];
                _names[i] = _names[i - 1];
                _messages[i] = _messages[i - 1];
            }

            _times[0] = FormatDateTime(DateTime.UtcNow);
            _names[0] = Networking.LocalPlayer.displayName;
            _messages[0] = message;

            RequestSerialization();
            DisplayAllLogLines();
        }

        private void DisplayAllLogLines()
        {
            if (!HasAllUdonSyncedFieldInitialized()) { return; }
            if (!HasAllLogLinesInitialized()) { return; }

            for (var i = 0; i < _logLines.Length; i++)
            {
                _logLines[i].Display(_messages[i], _names[i], _times[i]);
            }
        }

        private string FormatDateTime(DateTime dateTime)
        {
            return dateTime
                .ToLocalTime()
                .ToString(_timeFormat, CultureInfo.InvariantCulture);
        }

        #region base
        private bool AmIOwner()
        {
            return Networking.IsOwner(gameObject);
        }

        private void TakeOwner()
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
        #endregion
    }
}