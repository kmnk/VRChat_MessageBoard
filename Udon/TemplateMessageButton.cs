using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace Kmnk.MessageBoard.Udon
{
    public class TemplateMessageButton : UdonSharpBehaviour
    {
        [SerializeField]
        Udon.InputBoard _inputBoard;

        [SerializeField]
        Text _buttonText;

        public void AppendToInput()
        {
            if (!_inputBoard.IsActiveInput())
            {
                _inputBoard.ActivateInput();
            }
            _inputBoard.AppendTextToInput(_buttonText.text);
        }
    }
}