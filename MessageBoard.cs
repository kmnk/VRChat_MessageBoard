using System;
using System.Globalization;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace Kmnk {
    public class MessageBoard : UdonSharpBehaviour {

        [SerializeField]
        private bool _onlyWorldOwnerMode = false;

        [SerializeField]
        private string _title = "";

        [SerializeField]
        private string _timeFormat = "HH:mm:ss";

        [SerializeField]
        private Text _titleText = null;

        [SerializeField]
        private string[] _initialMessages = null;

        [SerializeField]
        private string _initialName = "";

        [SerializeField]
        private string _initialTime = "--:--:--";

        [SerializeField]
        private GameObject _logLinesParent = null;

        [SerializeField]
        private Button _activateButton = null;

        [SerializeField]
        private GameObject _inputParent = null;

        [SerializeField]
        private InputField _inputField = null;

        [UdonSynced]
        private string[] _times = null;

        [UdonSynced]
        private string[] _names = null;

        [UdonSynced]
        private string[] _messages = null;

        private LogLine[] _logLines = null;

        private void Start()
        {
            this._titleText.text = this._title;

            this._logLines = _logLinesParent.GetComponentsInChildren<LogLine>();
            this.InitializeLogLines();
            this.InitializeActivateButton();

            if (this.AmIOwner())
            {
                this.InitializeUdonSyncedFields();
                this.RequestSerialization();
            }

            this.DisplayAllLogLines();
        }

        public override void OnDeserialization()
        {
            if (!this.HasAllLogLinesInitialized()) { return; }
            if (!this.HasAllUdonSyncedFieldInitialized()) { return; }
            this.DisplayAllLogLines();
        }

        public void RegisterMessage()
        {
            this.AddMessage(this._inputField.text);
            this.DisplayAllLogLines();
        }

        public void DeactivateInput()
        {
            this._activateButton.gameObject.SetActive(true);
            this._inputParent.SetActive(false);
        }

        public void ActivateInput()
        {
            this._activateButton.gameObject.SetActive(false);
            this._inputParent.SetActive(true);
            this._inputField.text = string.Empty;
            this._inputField.Select();
        }

        private void InitializeLogLines()
        {
            foreach (var l in this._logLines)
            {
                l.Initialize();
            }
        }

        private void InitializeActivateButton()
        {
            if (!this._onlyWorldOwnerMode) { return; }
            if (!this.AmIOwner())
            {
                // if not owner on initialize, you are not world owner
                this._activateButton.gameObject.SetActive(false);
            }
        }

        private bool HasAllLogLinesInitialized()
        {
            foreach (var l in this._logLines)
            {
                if (!l.HasInitialized()) { return false; }
            }
            return true;
        }

        private void InitializeUdonSyncedFields()
        {
            var l = this._logLines.Length;
            this._times = new string[l];
            this._names = new string[l];
            this._messages = new string[l];

            for (var i = 0; i < l; i++)
            {
                if (_initialMessages != null && _initialMessages.Length > i)
                {
                    this._messages[i] = this._initialMessages[i];
                    this._names[i] = this._initialName;
                    this._times[i] = this._initialTime;
                }
                else
                {
                    this._messages[i] = string.Empty;
                    this._names[i] = string.Empty;
                    this._times[i] = string.Empty;
                }
            }
        }

        private bool HasAllUdonSyncedFieldInitialized()
        {
            if (this._times == null) { return false; }
            if (this._names == null) { return false; }
            if (this._messages == null) { return false; }
            return true;
        }

        private void AddMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) { return; }
            if (!this.HasAllUdonSyncedFieldInitialized()) { return; }

            this.TakeOwner();

            var l = this._logLines.Length;
            for (var i = l-1; i > 0; i--) {
                this._times[i] = this._times[i - 1];
                this._names[i] = this._names[i - 1];
                this._messages[i] = this._messages[i - 1];
            }

            this._times[0] = this.FormatDateTime(DateTime.UtcNow);
            this._names[0] = Networking.LocalPlayer.displayName;
            this._messages[0] = message;

            this.RequestSerialization();
        }

        private void DisplayAllLogLines()
        {
            if (!this.HasAllUdonSyncedFieldInitialized()) { return; }
            if (!this.HasAllLogLinesInitialized()) { return; }

            for (var i = 0; i < this._logLines.Length; i++)
            {
                this._logLines[i].Display(
                    this._messages[i],
                    this._names[i],
                    this._times[i]
                );
            }
        }

        private bool AmIOwner()
        {
            return Networking.IsOwner(this.gameObject);
        }

        private void TakeOwner()
        {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        }

        private string FormatDateTime(DateTime dateTime)
        {
            return dateTime
                .ToLocalTime()
                .ToString(this._timeFormat, CultureInfo.InvariantCulture);
        }
    }
}