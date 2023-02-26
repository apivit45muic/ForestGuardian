using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip openingSound;
    [SerializeField] private AudioClip getHitSound;
    [SerializeField] private AudioSource _audioSource;

	public Transform player;

	public bool isFlipped = false;

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}
	private void playattackSound(){_audioSource.PlayOneShot(attackSound, 0.6F);}
    private void playwalkSound(){_audioSource.PlayOneShot(walkSound, 3F);}
    private void playopeningSound(){_audioSource.PlayOneShot(openingSound, 0.3F);}
    private void playgetHitSound(){_audioSource.PlayOneShot(getHitSound, 0.3F);}

}