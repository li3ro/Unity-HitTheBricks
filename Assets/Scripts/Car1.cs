using UnityEngine;

public class Car1 : Car  // INHERITANCE
{
    public void Move() // POLYMORPHISM
    {
        Move(Vector3.forward);
    }

    public override void Stop() // ABSTRACTION
    {
        Speed = 0.1f; // ENCAPSULATION
    }
}
