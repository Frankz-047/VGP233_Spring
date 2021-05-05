using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playterRb;
    private bool isOnGround = true;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public float jumpForce;
    public float gravityModifier;
    public bool gameOver = false;
    public ParticleSystem explosionParticle;
    public ParticleSystem DirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    // Start is called before the first frame update
    void Start()
    {
        playterRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playterRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            DirtParticle.Stop();
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        if (gameOver)
        {
            DirtParticle.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            DirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            DirtParticle.Stop();
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            Debug.Log("GameOver");
        }
    }
}
