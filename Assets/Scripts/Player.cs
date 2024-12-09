using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    GameObject currentFloor;
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] GameObject ReplayButton;
    [SerializeField] GameObject PlayButton;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;
        Time.timeScale = 0f;
        PlayButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Floor")
        {
            if (other.contacts[0].normal == new Vector2(0f,1f))
            {
                Debug.Log("hit floor");
                currentFloor = other.gameObject;
                ModifyHp(1);
            }
        }
        else if(other.gameObject.tag == "Nails")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("hit nails");
                currentFloor = other.gameObject;
                ModifyHp(-3);
            }
        }
        else if (other.gameObject.tag == "Ceiling")
        {
            Debug.Log("hit ceiling");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathLine")
        {
            Debug.Log("you dead");
            Die();
        }
    }

    void ModifyHp(int amount)
    {
        Hp += amount;
        if(Hp <= 0)
        {
            Hp = 0;
            Die();
        }
        if(Hp > 10)
        {
            Hp = 10;
        }
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        for (int i = 0; i < HpBar.transform.childCount; i++)
        {
            if(Hp>i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void Die()
    {
        Time.timeScale = 0f;
        ReplayButton.SetActive(true);
    }

    public void Relay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void Play()
    {
        Time.timeScale = 1f;
        PlayButton.SetActive(false);
    }
}
