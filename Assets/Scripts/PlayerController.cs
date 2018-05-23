using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody m_RigidBody;
	private Animator m_Animator;

    public float maxSpeed = 5.0f;
    private bool facingRight = false;
    private bool facingDown = true;

    // Use this for initialization
    void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
		m_Animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        //Valeur entre -1 et 1 selon intentsité de frappe sur l'axe horizontal
        float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");
        //Fonction responsable du mouvement
        Debug.Log("facing Down :" + v);
        Debug.Log("facing Right :" + h);
        MovePlayer(h, v);
    }

	void MovePlayer( float h, float v )
    {
        m_RigidBody.velocity = new Vector3(h * maxSpeed, 0, v * maxSpeed);

        SetBool_H_V(h, v);

    }

    void SetBool_V(float v)
    {
        if (v > 0)
        {
            m_Animator.SetBool("GoUp", true);
            m_Animator.SetBool("GoDown", false);
        }
        else if (v < 0)
        {
            m_Animator.SetBool("GoUp", false);
            m_Animator.SetBool("GoDown", true);
        }
        else
        {
            m_Animator.SetBool("GoUp", false);
            m_Animator.SetBool("GoDown", false);
        }

    }

    void SetBool_H_V(float h, float v)
    {
        if (h > 0)
        {
            m_Animator.SetBool("GoLeft", false);
            m_Animator.SetBool("GoRight", true);
            SetBool_V(v);
        }
        else if (h < 0)
        {
            m_Animator.SetBool("GoLeft", true);
            m_Animator.SetBool("GoRight", false);
            SetBool_V(v);
        }
        else
        {
            m_Animator.SetBool("GoLeft", false);
            m_Animator.SetBool("GoRight", false);
            SetBool_V(v);
        }
    }

    void FlipH()
    {
        facingRight = !facingRight;
        
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    void FlipV()
    {
        facingDown = !facingDown;

        Vector3 s = transform.localScale;
        s.y *= -1;
        transform.localScale = s;
    }
}
