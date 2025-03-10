using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("SHOOT ##############")]
    public bool shot;
    [SerializeField] float speed;
    [SerializeField] float maxDistanceMultiplier;
    [SerializeField] float minDistanceMultiplier;
    [SerializeField] float distanceSpeedMultiplier;
    Vector2 clickDownPos;
    Vector2 mousePos;
    [Header("AIM RENDERER ############")]
    [SerializeField] LineRenderer aimLineRenderer;
    //[Header("PARTICLES ###############")]
    //public GameObject deathParticles;
    [Header("MENU #####################")]
    [SerializeField] int numberOfClicksToMenu;
    [SerializeField] int currentNumberOfClicks;
    [SerializeField] float clickIntervalCronometer;
    [SerializeField] float intervalLimit;


    GameObject? slider;
    void Start()
    {
        aimLineRenderer.positionCount = 0;
        rb = GetComponent<Rigidbody2D>();
        SetSlider();
        UIManager.onItemSelected += SetSlider;
    }

    void Update()
    {
        if (UIManager.Instance.onUI)
        {
            currentNumberOfClicks = 0;
            return;
        }
        if (!shot && Input.GetMouseButtonDown(0))
        {
            currentNumberOfClicks++;
            if (currentNumberOfClicks >= numberOfClicksToMenu) 
            {
                UIManager.Instance.Menu();
            }
            clickIntervalCronometer = 0;
        }
        clickIntervalCronometer += Time.deltaTime;
        if (clickIntervalCronometer>=intervalLimit)
        {
            currentNumberOfClicks = 0;
        }
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !shot) clickDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonUp(0) && !shot) 
        {
            // Multiplier va a hacer un n�mero entre 0.5 y 1.5 que se obtiene por la magnitud clampeada del vector igual a la diferencia entre clickDownPos y mousePos
            float multiplier = Mathf.Clamp(GF.ClampVector(clickDownPos - mousePos, 0, maxDistanceMultiplier).magnitude, minDistanceMultiplier, maxDistanceMultiplier) ;
            float force = speed * multiplier * distanceSpeedMultiplier;
            Vector2 direction = (clickDownPos - mousePos).normalized;
            if (direction == Vector2.zero) return;
            shot = true;
            rb.AddForce(direction*force, ForceMode2D.Impulse);
        }

        if (Input.GetMouseButton(0) && !shot)
        {
            aimLineRenderer.positionCount = 2;
            aimLineRenderer.SetPosition(0, transform.position);
            aimLineRenderer.SetPosition(1, (Vector2)transform.position + GF.ClampVector(clickDownPos-mousePos, 0, maxDistanceMultiplier));
        }
        else aimLineRenderer.positionCount = 0;
    }
    void SetSlider()
    {
        Item? itemSelected = UIManager.Instance.sliderSelected;
        if (itemSelected != null)
        {
            GameObject? objectSelected = itemSelected.item;
            if (objectSelected != null)
            {
                if (slider!=null) Destroy(slider);
                slider = Instantiate(objectSelected, transform.position, Quaternion.identity, transform);
            }
        }
        else if (slider != null) Destroy(slider);
    }

    private void OnDestroy()
    {
        UIManager.onItemSelected -= SetSlider;
    }
}
