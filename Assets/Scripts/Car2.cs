using UnityEngine;

public class Car2 : Car // INHERITANCE
{
    public void Move() // POLYMORPHISM
    {
        Move(Vector3.up);
    }

    public override void Stop() // ABSTRACTION
    {
        Speed = 0.1f; // ENCAPSULATION
    }
}
