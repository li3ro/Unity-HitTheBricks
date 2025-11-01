using UnityEngine;

public abstract class Car : MonoBehaviour // ABSTRACTION
{
    private float speed = 10f; // Encapsulated field

    public float Speed  // ENCAPSULATION
    {
        get { return speed; }
        protected set
        {
            if (value >= 0)
                speed = value;
        }
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public abstract void Stop(); // ABSTRACTION
}
