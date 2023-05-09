using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private CameraController cameraReference;
    [SerializeField] private HeaderAttribute playerController;
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private HealthBarController barravida;
    [SerializeField] private int vidaactual;
    private bool lowLifeMusicPlayed = false;











    [Header("InputSystem")]
    Vector2 rawInputMovement;
    Vector3 mouseposition;





    [SerializeField] private AudioSource audiosource;
    private void Start() {
        GetComponent<HealthBarController>().onHit += cameraReference.CallScreenShake;
    }

    void Movimiento()
    {
        myRBD2.velocity = rawInputMovement * velocityModifier;
    }
    void Apuntar()
    {
        Vector2 distance = mouseposition - transform.position;
        Debug.DrawRay(transform.position, distance * rayDistance, Color.red);
    }

    private void Update() {
        vidaactual = barravida.currentValue;

        Movimiento();

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Apuntar();


        if (vidaactual <= 25)
        {
            LowLife();
        }

        if(vidaactual == 0)
        {
            Destroy(gameObject);
        }
        



    }
    private void LowLife()
    {
        
        if (!lowLifeMusicPlayed)
        {
            audiosource.Play();
            lowLifeMusicPlayed = true;
        }
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }


    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector2(inputMovement.x, inputMovement.y);
    }

    public void OnAim(InputAction.CallbackContext value)
    {
        Vector2 direccionmouse = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());
        mouseposition = direccionmouse;
        Debug.Log(mouseposition);
    }

    public void OnFire(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Vector2 distance = mouseposition - transform.position;
            BulletController myBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            myBullet.SetUpVelocity(distance.normalized, 6, soundScriptableObject);
        }
    }


}
