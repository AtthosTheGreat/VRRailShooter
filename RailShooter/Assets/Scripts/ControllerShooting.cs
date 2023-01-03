using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebXR;

public class ControllerShooting : MonoBehaviour
{
    [SerializeField]
    private int damage = 34;

    [SerializeField]
    private WebXRController controller;

    [SerializeField]
    private GameObject deadPanel = null;

    [SerializeField]
    private GameObject muzzleFlashGameObject = null;

    [SerializeField]
    private Transform muzzleFlashLocation;

    [SerializeField]
    private float reloadTime = 0.75f;

    [SerializeField]
    private GameObject hitExplosionGameObject = null;

    [SerializeField]
    private AudioClip gunshotClip = null;

    private float lastShootTime = -100;

    private bool playerDead = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        /*
        if (controller.GetButtonDown(WebXRController.ButtonTypes.ButtonA))
        {
            Debug.Log("SHOOTING!!!");
            var obj = Instantiate(muzzleFlashGameObject, controller.transform.position + controller.transform.forward * 3f, Quaternion.identity, controller.transform);
            Destroy(obj, 0.2f);
        }
        */

        if (controller.GetButtonDown(WebXRController.ButtonTypes.Trigger))
        {
            if (Time.time - lastShootTime >= reloadTime)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    var zombie = hit.transform.GetComponent<ZombieScript>();
                    if (zombie)
                    {
                        zombie.TakeDamage(damage);
                        var explosion = Instantiate(hitExplosionGameObject, hit.point, Quaternion.identity);
                        Destroy(explosion, 1f);
                    }
                }

                AudioSource.PlayClipAtPoint(gunshotClip, transform.position);
                var muzzleFlash = Instantiate(muzzleFlashGameObject, muzzleFlashLocation);
                Destroy(muzzleFlash, 0.18f);
                lastShootTime = Time.time;
            }
        }
    }

    public void PlayerDeath()
    {
        playerDead = true;
        Time.timeScale = 0f;
        deadPanel.SetActive(true);
        if (WebXRManager.Instance.XRState == WebXRState.VR)
            WebXRManager.Instance.ToggleVR();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
