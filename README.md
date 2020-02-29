# Notes


## Disable colliders on death animation

It's important to disable colliders on death otherwise strange things can
happen, like double collisions.

```
foreach (Collider2D c in GetComponentsInChildren<Collider2D>()) {
	c.enabled = false;
}
```	

# Distinguishing between colliders

You can put colliders on child objects and then use tags to distinguish them.

```
foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
{
    if (c.tag == "MushroomHead")
    {
        headCollider = c;
    }
}
```

In `OnTriggerEnter2D` you can find out which collider collided with something like:

```
if (headCollider.IsTouching(otherCollider))
{
    HandleDeath(player);
}
```
