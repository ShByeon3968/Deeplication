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
        // 카메라 접근 및 WebCamTexture 초기화
        webCamTexture = new WebCamTexture();
        display.texture = webCamTexture;
        webCamTexture.Play();
    }

    public void CapturePhoto()
    {
        // 캡처한 사진을 Texture2D로 변환
        capTexture = new Texture2D(webCamTexture.width, webCamTexture.height);
        capTexture.SetPixels(webCamTexture.GetPixels());
        capTexture.Apply();

        // 사진을 RawImage에 표시
        display.texture = capTexture;

        // 캡처 후 카메라 중지 (선택 사항)
        webCamTexture.Stop();

        StartCoroutine(SendPhotoToServer(capTexture));
    }

    private IEnumerator SendPhotoToServer(Texture2D photo)
    {
        // Texture2D를 PNG로 인코딩
        byte[] imageBytes = photo.EncodeToPNG();

        // 웹 요청을 위한 폼 데이터 생성
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageBytes, "photo.png", "image/png");

        // 서버로 이미지 전송
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
                // 서버 응답 처리
                var response = www.downloadHandler.data;
                // 필요 시 response 데이터 처리
            }
        }
    }
}
