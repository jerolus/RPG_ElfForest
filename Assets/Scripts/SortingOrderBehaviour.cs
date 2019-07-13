using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderBehaviour : MonoBehaviour
{
	public bool fixInRuntime = false;

	private SpriteRenderer m_sprite;

    private void Start()
    {
		m_sprite = gameObject.GetComponent<SpriteRenderer>();
		m_sprite.sortingLayerName = "Player";
		m_sprite.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }

    private void Update()
    {
        if(fixInRuntime)
		{
			m_sprite.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
		}
    }
}
