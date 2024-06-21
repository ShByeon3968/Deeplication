using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �ʿ��� ���ӽ����̽�
using UnityEngine.UI; // UI�� �����ϱ� ���� �ʿ��� ���ӽ����̽�



public class SceneStarter : MonoBehaviour
{
    // ��ȯ�� ���� �̸�
    private string StartScene = "Start";
    public Button StartButton;


    void Start()
    {
        // ��ư ������Ʈ�� �����ͼ� Ŭ�� �̺�Ʈ�� �޼��� �߰�
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(OnStartButtonClick);
        }
    }

    // ��ư Ŭ�� �� ȣ��� �޼���
    void OnStartButtonClick()
    {
        SceneManager.LoadScene(StartScene);
    }
}

