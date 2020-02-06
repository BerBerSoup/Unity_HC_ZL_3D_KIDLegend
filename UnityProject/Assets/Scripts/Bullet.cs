using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 子彈的傷害
    /// </summary>
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "鼠王")
        {
            other.GetComponent<Player>().Hit(damage);
        }
    }

}
