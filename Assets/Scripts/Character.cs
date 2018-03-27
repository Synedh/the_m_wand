using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {
    
    public int CurrentLife = 5;
    public int maxLife = 5;
    public Heart heart;
    public Heart[] hearts;
    public GameObject lifebar;
    Animator animator;
	public Image flash;
	float startFlashDuration = 0;
	float flashDuration = 0.9f;
	float flashIntensity = 0.3f;

	float cameraDuration = 0.9f;
	float initialCameraDuration;
	float powerCamera = 0.5f;
	Vector3 startCameraPosition;
	bool cameraShouldShake = false;
	Transform camera;


    private void Start()
    {
        animator = GetComponent<Animator>();

		//TEST
		flash = GameObject.FindGameObjectWithTag("Flash").GetComponent<Image>();
		camera = Camera.main.transform;
		startCameraPosition = camera.localPosition;
		initialCameraDuration = cameraDuration;
		//!TEST
        hearts = new Heart[maxLife];
        RectTransform rt = (RectTransform)heart.transform;
        float width = rt.rect.width;

        for (int i = 0; i < maxLife; i++)
        {
            Heart h = Instantiate(heart, new Vector2(0, 0), Quaternion.identity);
            h.transform.SetParent(lifebar.transform, false);
            h.transform.position += transform.right * i;
            hearts[i] = h;
            if (i >= CurrentLife)
                hearts[i].shatter();
        }
    }

    public void getHit()
    {
		animator.SetBool("isHurt", true);
		startFlashDuration = 0;
		cameraDuration = 0;
		cameraShouldShake = true;

        for (int i = 0; i < CurrentLife; i++)
        {
            hearts[i].shake();
        }

        if (CurrentLife > 1)
        {
            hearts[CurrentLife - 1].shatter();
            CurrentLife--;
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void stopStagger()
    {
        animator.SetBool("isHurt", false);
    }

    public void stopCast()
    {
        animator.SetBool("spell_cast", false);
        animator.SetBool("spell_charge", false);

    }

    private void Update()
    {		
		if (animator.GetBool ("isHurt")) {
			//Flash
			flash.color = Color.Lerp (new Color (0, 0, 0, 0), new Color (1, 0, 0, flashIntensity), startFlashDuration);
		
			if (startFlashDuration < 1) {
				startFlashDuration += Time.deltaTime / flashDuration;
			} else {
				flash.color = Color.Lerp (new Color (1, 0, 0, flashIntensity), new Color (0, 0, 0, 0), startFlashDuration);
			}

			//Camera
			if (cameraShouldShake) {
				if (cameraDuration < 1) {
					camera.localPosition = startCameraPosition + Random.insideUnitSphere * powerCamera;
					cameraDuration += Time.deltaTime / cameraDuration;
				} else {
					cameraShouldShake = false;
					cameraDuration = initialCameraDuration;
					camera.localPosition = startCameraPosition;
				}
			}
		}
    }
}
