using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필요한 네임스페이스
using UnityEngine.UI; // UI를 제어하기 위해 필요한 네임스페이스



public class SceneStarter : MonoBehaviour
{
    // 전환할 씬의 이름
    private string StartScene = "Start";
    public Button StartButton;


    void Start()
    {
        // 버튼 컴포넌트를 가져와서 클릭 이벤트에 메서드 추가
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(OnStartButtonClick);
        }
    }

    // 버튼 클릭 시 호출될 메서드
    void OnStartButtonClick()
    {
        SceneManager.LoadScene(StartScene);
    }
}

