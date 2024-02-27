using System.Linq;
using UnityEngine;

public class Motion : MonoBehaviour
{
    private const double G = 6.674e10 - 11;
    private Motion[] objects;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 acceleration;
    [SerializeField] private Transform pos;
    [SerializeField] private bool collided = false;
    public Vector3 Position => pos.position;

    public float mass = 1;

    private void Awake()
    {
        pos = gameObject.GetComponent<Transform>();
    }

    void Start()
    {
        objects =  GetComponentInParent<ObjectRegister>().allobjects.Where(x => x.Position - Position != Vector3.zero).ToArray();
        foreach (Motion motion in objects)
        {
            print($"{this.name} : {motion.name}");
        }
        
    }

    void EvalGravity()
    {
        Vector3 total = Vector3.zero;
        foreach (Motion planet in objects)
        {
            Vector3 r = Position - planet.Position;
            float r2 = (r).sqrMagnitude;
            float f = -(planet.mass) / (r2);
            total += r.normalized * f;
        }

        acceleration = total;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!collided)
        {
            EvalGravity();
            velocity += acceleration * Time.fixedDeltaTime;
            pos.position += velocity * Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        collided = true;
    }
}
