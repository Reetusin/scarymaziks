using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 10f;
    public Image scareImage;
    public Button playAgainButton;

    private void Start()
    {
        Cursor.visible = false;
        scareImage.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector3 targetPosition = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ShowPlayAgainButton();
            Cursor.visible = true;
        }
        else if (collision.gameObject.CompareTag("Scare"))
        {
            ShowScareImage();
        }
    }

    private void ShowScareImage()
    {
        scareImage.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    private void ShowPlayAgainButton()
    {
        playAgainButton.gameObject.SetActive(true);
        Cursor.visible = true;
        speed = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
