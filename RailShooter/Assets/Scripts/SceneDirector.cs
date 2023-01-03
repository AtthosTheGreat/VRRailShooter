using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class SceneDirector : MonoBehaviour
{
    [SerializeField]
    private VRCameraSetMovement cameraMovement = null;

    // FIRST PART
    [SerializeField]
    private float firstStartIndex = 0f;

    [SerializeField]
    private float firstEndIndex = 1.55f;

    [SerializeField]
    private int firstSteps = 100;

    [SerializeField]
    private float firstTransitionTime = 2f;

    [SerializeField]
    private ZombieSpawner firstZombieSpawner = null;

    [SerializeField]
    private float firstZombieSpawnerTime = 20f;

    [SerializeField]
    private float firstWaitAfterSpawning = 1f;

    // SECOND PART
    [SerializeField]
    private float secondStartIndex = 1.55f;

    [SerializeField]
    private float secondEndIndex = 9f;

    [SerializeField]
    private int secondSteps = 500;

    [SerializeField]
    private float secondTransitionTime = 2f;

    [SerializeField]
    private ZombieSpawner secondZombieSpawner = null;

    [SerializeField]
    private float secondZombieSpawnerTime = 20f;

    [SerializeField]
    private float secondTimeBeforeWinning = 5f;

    [SerializeField]
    private GameObject winPanel = null;

    private bool wonGame = false;

    void Start()
    {
        StartCoroutine(DirectorCoroutine());
    }

    private void Update()
    {
        if (wonGame)
        {
            Time.timeScale = 0f;
            winPanel.SetActive(true);
            if (WebXRManager.Instance.XRState == WebXRState.VR)
                WebXRManager.Instance.ToggleVR();
        }
    }

    private IEnumerator DirectorCoroutine()
    {
        // first part
        float firstStepTime = firstTransitionTime / firstSteps;
        for (float i = 0; i < firstTransitionTime; i += firstStepTime)
        {
            cameraMovement.point += (firstEndIndex - firstStartIndex) / firstSteps;
            yield return new WaitForSeconds(firstStepTime);
        }
        firstZombieSpawner.isSpawning = true;
        yield return new WaitForSeconds(firstZombieSpawnerTime);
        firstZombieSpawner.isSpawning = false;
        yield return new WaitForSeconds(firstWaitAfterSpawning);

        // second part
        float secondStepTime = secondTransitionTime / secondSteps;
        for(float i = 0; i < secondTransitionTime; i += secondStepTime)
        {
            cameraMovement.point += (secondEndIndex - secondStartIndex) / secondSteps;
            yield return new WaitForSeconds(secondStepTime);
        }
        secondZombieSpawner.isSpawning = true;
        yield return new WaitForSeconds(secondZombieSpawnerTime);
        secondZombieSpawner.isSpawning = false;
        yield return new WaitForSeconds(secondTimeBeforeWinning);

        // WIN UI
        wonGame = true;
    }
}
