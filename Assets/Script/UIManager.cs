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
        // ��ư Ŭ�� �� TextureBrush�� TogglePainting �޼��带 ȣ���ϵ��� ����
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
