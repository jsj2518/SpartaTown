using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputName;

    public void StartGame()
    {
        GameManager.Instance.PlayerName = inputName.text;
        SceneManager.LoadScene("MainRoomScene");
    }
}
