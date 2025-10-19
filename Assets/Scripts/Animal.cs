using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    // --- Encapsulated data (backing fields) ---
    [SerializeField] private string animalName = "Unnamed";
    [SerializeField] private int health = 100;

    // Public properties with validation (Encapsulation)
    public string AnimalName
    {
        get => animalName;
        set => animalName = string.IsNullOrWhiteSpace(value) ? "Unnamed" : value.Trim();
    }

    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }

    // --- Abstraction: high-level verbs every Animal can do ---
    public virtual void Walk(Vector3 input) { }     // default “no-op”
    public virtual void Run(Vector3 input) { }
    public virtual void Jump() { }
    public virtual void Speak() { Debug.Log($"{AnimalName} makes a sound."); }

    // Convenience “daily routine” shows Abstraction nicely
    public void PerformDailyRoutine()
    {
        Speak();
        // could call Walk/Run/Jump in a sequence if desired
    }
}
