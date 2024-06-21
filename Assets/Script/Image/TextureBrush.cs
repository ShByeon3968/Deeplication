using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ����ϱ� ���� �ʿ��մϴ�.

public class TextureBrush : MonoBehaviour
{
    public Color brushColor = Color.red;
    public int brushSize = 10;
    public bool isPaintingEnabled = false; // ������ ��� Ȱ��ȭ ����
    private bool isPainting = false;
    private List<Vector2> paintPoints = new List<Vector2>();
    private CameraCapture cameraCapture;



    void Update()
    {
        if (!isPaintingEnabled) return; // ������ ����� Ȱ��ȭ���� ���� ��� ����

        if (Application.platform == RuntimePlatform.Android)
        {
            HandleTouchInput();
        }
        else
        {
            HandleMouseInput();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPainting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPainting = false;
        }

        if (isPainting)
        {
            Vector2 uv;
            if (GetMouseUV(out uv))
            {
                Paint(uv);
                paintPoints.Add(uv);
            }
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isPainting = true;
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isPainting = false;
            }

            if (isPainting && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                Vector2 uv;
                if (GetTouchUV(touch, out uv))
                {
                    Paint(uv);
                    paintPoints.Add(uv);
                }
            }
        }
    }

    private bool GetMouseUV(out Vector2 uv)
    {
        uv = Vector2.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                return false;

            uv = hit.textureCoord;
            return true;
        }
        return false;
    }

    bool GetTouchUV(Touch touch, out Vector2 uv)
    {
        uv = Vector2.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out hit))
        {
            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                return false;

            uv = hit.textureCoord;
            return true;
        }
        return false;
    }

    void Paint(Vector2 uv)
    {
        if (cameraCapture.capTexture != null)
        {
            int x = (int)(uv.x * cameraCapture.capTexture.width);
            int y = (int)(uv.y * cameraCapture.capTexture.height);
            for (int i = -brushSize; i < brushSize; i++)
            {
                for (int j = -brushSize; j < brushSize; j++)
                {
                    if (x + i < 0 || x + i >= cameraCapture.capTexture.width || y + j < 0 || y + j >= cameraCapture.capTexture.height)
                        continue;

                    cameraCapture.capTexture.SetPixel(x + i, y + j, brushColor);
                }
            }
            cameraCapture.capTexture.Apply();
        }
        else
        {
            return;
        }
        
    }

    public List<Vector2> GetPaintPoints()
    {
        return paintPoints;
    }

    // ������ ����� ����ϴ� �޼���
    public void TogglePainting()
    {
        isPaintingEnabled = !isPaintingEnabled;
    }
}
