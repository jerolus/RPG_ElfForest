using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
	public string destroyAnimationName;

	private Animator m_animator;
	private Collider2D m_collider;

	private void Start()
    {
		m_animator = gameObject.GetComponent<Animator>();
		m_collider = gameObject.GetComponent<Collider2D>();
	}

    private void Update()
    {
		AnimatorStateInfo animInfo = m_animator.GetCurrentAnimatorStateInfo(0);

		if (animInfo.IsName(destroyAnimationName) && animInfo.normalizedTime >= 1)
		{
			Destroy(gameObject);
		}
    }

	private IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Attack")
		{
			m_animator.Play(destroyAnimationName);
			yield return new WaitForSeconds(0.25f);
			m_collider.enabled = false;
		}
	}
}
