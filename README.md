# MonoSingleton
A class for creating MonoBehaviour that acts as a Singleton, battle tested on multiple live projects.

# Use
Extremely simple to use. Copy the file into your Assets folder somewhere.

Instead of:

public class GameManager : MonoBehaviour

Change to:

public class GameManager : MonoSingleton<GameManager>

Then instead of:

private void Awake()
{
  //Awake code.
}

Use:

public override void Init()
{
  //Awake code.
}

Everything else is the same, you will now only have one instance of your gameobject even when you load other scenes, or create duplicates in the same scene.

To access your gameobject, assuming it's called GameManager, you can use:

GameManager.Instance

Instead of needing to have a reference to the gameobject.

Thats all nice and simple, happy coding.

Jon
