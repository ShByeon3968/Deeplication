using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CameraCapture : MonoBehaviour
{
    public RawImage display;
    private WebCamTexture webCamTexture;

    public Texture2D capTexture;

    void Start()
    {
        // ī�޶� ���� �� WebCamTexture �ʱ�ȭ
        webCamTexture = new WebCamTexture();
        display.texture = webCamTexture;
        webCamTexture.Play();
    }

    public void CapturePhoto()
    {
        // ĸó�� ������ Texture2D�� ��ȯ
        capTexture = new Texture2D(webCamTexture.width, webCamTexture.height);
        capTexture.SetPixels(webCamTexture.GetPixels());
        capTexture.Apply();

        // ������ RawImage�� ǥ��
        display.texture = capTexture;

        // ĸó �� ī�޶� ���� (���� ����)
        webCamTexture.Stop();

        StartCoroutine(SendPhotoToServer(capTexture));
    }

    private IEnumerator SendPhotoToServer(Texture2D photo)
    {
        // Texture2D�� PNG�� ���ڵ�
        byte[] imageBytes = photo.EncodeToPNG();

        // �� ��û�� ���� �� ������ ����
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageBytes, "photo.png", "image/png");

        // ������ �̹��� ����
        using (UnityWebRequest www = UnityWebRequest.Post(ServerManager.localServerIP, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Image uploaded successfully!");
                // ���� ���� ó��
                var response = www.downloadHandler.data;
                // �ʿ� �� response ������ ó��
            }
        }
    }
}
