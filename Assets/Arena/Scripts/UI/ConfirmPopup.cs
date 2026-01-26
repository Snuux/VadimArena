using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Arena.Scripts.UI
{
    public class ConfirmPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _messageText;

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        public void ShowMessage(string text) => _messageText.text = text;

        public IEnumerator WaitConfirm(KeyCode keyForConfirm)
        {
            yield return new WaitWhile(() => Input.GetKeyDown(keyForConfirm) == false);
        }
    }
}
