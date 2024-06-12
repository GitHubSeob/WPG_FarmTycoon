using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AnimalManager : MonoBehaviour
{
    [SerializeField]
    float walkSpeed;
    int mode;
    // 0 : idle, 1 : turn, 2 : move, 3 : stuck, 4 : turn, 5 : move, 6 : endStuck
    [SerializeField]
    bool isChild;
    [SerializeField]
    string animalType;

    int actionCount;
    int rotateAngle;
    Rigidbody rb;
    int dna;

    int age;
    int ageTime;
    int growAge;
    int growTime;
    public GameObject upgradeTarget;
    GameObject foodbox;

    Animator animator;
    [SerializeField]
    float walkAnimationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
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
        foodbox = GameObject.Find(animalType + "FoodBox");
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
        slaugher();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("F"))
        {
            mode = 4;
            rotateAngle = 5 * dna;
            actionCount = 20;
        }
    }

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

    void slaugher()
    {
        GameObject go = GameObject.Find("ProductionInterface");
        GameObject general = GameObject.Find("GeneralAnimal");
        if (this.gameObject.tag == "Chick1")
        {
            go.GetComponent<ProductionInterface>().updateChickenCount(1);
            --general.GetComponent<General>().chickenCount;
        }
        else if (this.gameObject.tag == "Horse1")
        {
            go.GetComponent<ProductionInterface>().updateHorseCount(1);
            --general.GetComponent<General>().horseCount;
        }
        else if (this.gameObject.tag == "Deer1")
        {
            go.GetComponent<ProductionInterface>().updateDeerCount(1);
            --general.GetComponent<General>().deerCount;
        }
        else
        {
            return;
        }
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag.Contains("2"))
        {
            if (ageTime < growTime)
            {
                ++ageTime;
            }
            else if (ageTime == growTime)
            {
                if (Random.Range(0, 10) == 0)
                {
                    if (foodbox.GetComponent<FoodBox>().subFoodCount())
                    {
                        ++age;
                        Debug.Log(this.gameObject.name + " : " + age);
                    }
                }
            }
            if (age == growAge)
            {
                if (isChild)
                {
                    Debug.Log("Upgrade : " + this.gameObject.name);
                    Instantiate(upgradeTarget, transform.position, transform.rotation);
                    this.gameObject.SetActive(false);
                    Destroy(this.gameObject);
                }
            }
        }

        if (actionCount >= 0)
        {
            --actionCount;

            switch (mode)
            {
                case 0:
                    animator.speed = 1;
                    animator.SetTrigger("Stay");
                    break;
                case 1:
                case 4:
                    animator.speed = walkAnimationSpeed;
                    animator.SetTrigger("Walk");
                    transform.Rotate(0, rotateAngle, 0);
                    break;
                case 2:
                case 5:
                case 6:
                    animator.speed = walkAnimationSpeed;
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
