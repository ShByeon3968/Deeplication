using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button CaptureButton;
    [SerializeField] private Button regionSelector;
    [SerializeField] private CameraCapture captureManager;
    [SerializeField] public TextureBrush textureBrush;

    // Start is called before the first frame update
    void Start()
    {
        CaptureButton.onClick.AddListener(captureManager.CapturePhoto);
        // 버튼 클릭 시 TextureBrush의 TogglePainting 메서드를 호출하도록 설정
        regionSelector.onClick.AddListener(textureBrush.TogglePainting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //regionSelector.GetComponentInChildren<Text>().text = textureBrush.isPaintingEnabled ? "Disable Painting" : "Enable Painting";
    }
}
