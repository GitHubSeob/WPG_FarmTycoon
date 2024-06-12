using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChickenManager : MonoBehaviour
{
    float walkSpeed;
    int mode;
    // 0 : idle, 1 : turn, 2 : move, 3 : stuck, 4 : turn, 5 : move, 6 : endStuck
    int type;
    // 1 or 2

    int actionCount;
    int rotateAngle;
    Rigidbody rb;
    int dna;

    int age;
    int ageTime;
    int growAge;
    int growTime;
    public GameObject chicken;
    GameObject foodbox;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        Debug.Log(this.gameObject.tag);
        if (this.gameObject.tag == "Chick1")
        {
            type = 1;
            walkSpeed = 0.05f;
        }
        else if (this.gameObject.tag == "Chick2")
        {
            type = 2;
            walkSpeed = 0.1f;
        }

        actionCount = -1;
        rb = GetComponent<Rigidbody>();
        do
        {
            dna = Random.Range(-1, 2);
        } while (dna == 0);
        age = 0;
        ageTime = 0;
        growAge = 10;
        growTime = 60;
        animator = GetComponent<Animator>();
        foodbox = GameObject.Find("FoodBox");
    }

    int getRandom(int n)
    {
        switch (n)
        {
            case 30:
                return Random.Range(5, 25);
            case 60:
                return Random.Range(15, 45);
            case 120:
                return Random.Range(35, 85);
        }

        return 0;
    }

    public void printLog()
    {
        Debug.Log(age);
    }

    //void OnTriggerEnter(Collider collision)
    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.name);
        //if (collision.gameObject.name.Contains("F") || collision.gameObject.name.Contains("C"))
        if (collision.gameObject.name.Contains("F"))
        {
            mode = 4;
            rotateAngle = 3 * dna;
            //rotateAngle = 9 * (Random.Range(0, 2) == 0 ? -1 : 1);
            actionCount = 10;
        }
    }

    //void OnTriggerExit(Collider collision)
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("F"))
        {
            mode = 6;
            actionCount = getRandom(30);
        }
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        if (ageTime < growTime)
        {
            ++ageTime;
        }
        else if (ageTime == growTime)
        {
            if (Random.Range(0, 10) == 0)
            {
                if (foodbox.GetComponent<FoodBox>().decFoodCount())
                {
                    ++age;
                    Debug.Log(this.gameObject.name + " : " + age);
                }
            }
        }
        if (age == growAge)
        {
            if (this.type == 2)
            {
                Debug.Log("Upgrade : " + this.gameObject.name);
                Instantiate(chicken, transform.position, transform.rotation);
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }

        if (actionCount >= 0)
        {
            --actionCount;

            switch (mode)
            {
                case 0:
                    animator.SetTrigger("Stay");
                    break;
                case 1:
                case 4:
                    animator.SetTrigger("Walk");
                    transform.Rotate(0, rotateAngle, 0);
                    break;
                case 2:
                case 5:
                case 6:
                    animator.SetTrigger("Walk");
                    transform.Translate(0, 0, walkSpeed);
                    break;
            }

            return;
        }

        if (mode == 1)
        {
            mode = 2;
            actionCount = getRandom(60);
            return;
        }
        else if (mode == 2)
        {
            mode = 0;
            actionCount = getRandom(120);
            return;
        }

        if (mode == 4)
        {
            mode = 5;
            actionCount = 3;
            return;
        }
        else if (mode == 5)
        {
            mode = 4;
            actionCount = getRandom(30);
            return;
        }
        else if (mode == 6)
        {
            mode = 0;
            actionCount = getRandom(120);
            return;
        }

        mode = Random.Range(0, 2);
        actionCount = getRandom(60);
        if (mode == 1)
        {
            do
            {
                rotateAngle = Random.Range(-9, 10);
            } while (rotateAngle == 0);
            actionCount = 10;
        }
    }
}

