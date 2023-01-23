using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInputs))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    protected const float MinMoveDistance = 0.001f;
    protected const float ShellRadius = 0.01f;
    
    [SerializeField] private float _speedHorizontal;
    [SerializeField] private float _speedVertical;
    
    private PlayerInputs _inputs;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb2d;

    public float MinGroundNormalY = .65f;
    public float GravityModifier = 1f;
    public Vector2 Velocity;
    public LayerMask LayerMask;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    
    private void Awake()
    {
        _inputs = GetComponent<PlayerInputs>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask);
        contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        targetVelocity = new Vector2( _inputs.MoveLeftRight*_speedHorizontal, 0);
        
        if (_inputs.IsJump && grounded)
        {
            Velocity.y = _speedVertical;
        }

        if (_inputs.MoveLeftRight > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_inputs.MoveLeftRight < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
    
    private void FixedUpdate()
    {
        Velocity += Physics2D.gravity * (GravityModifier * Time.deltaTime);
        Velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        
        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true); 
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _rb2d.Cast(move, contactFilter, hitBuffer, distance + ShellRadius);

            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > MinGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rb2d.position = _rb2d.position + move.normalized * distance;
    }
}
