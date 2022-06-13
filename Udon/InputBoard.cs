using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace Kmnk.MessageBoard.Udon
{
    public class InputBoard : UdonSharpBehaviour
    {
        [SerializeField]
        string[] _templateMessages = null;

        [SerializeField]
        Udon.MessageBoard _messageBoard = null;

        [SerializeField]
        Button _activateButton = null;

        [SerializeField]
        GameObject _inputParent = null;

        [SerializeField]
        InputField _inputField = null;

        [SerializeField]
        Transform _templateMessagesTransform = null;

        [SerializeField]
        GameObject _templateMessageButtonOrigin = null;

        private void Start()
        {
            if (_messageBoard.IsOnlyWorldOwnerMode() && !AmIOwner())
            {
                DestroyImmediate(this);
            }
        }

        public void RegisterMessage()
        {
            _messageBoard.AddMessage(_inputField.text);
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

        public bool IsActiveInput()
        {
            return _inputParent.activeSelf;
        }

        public void AppendTextToInput(string text)
        {
            _inputField.text += text;
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