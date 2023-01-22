using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter(Collision other)
    {
        //사라지는 기준 정확하게 바꾸기
        if (other.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 1);
        }
        else if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
