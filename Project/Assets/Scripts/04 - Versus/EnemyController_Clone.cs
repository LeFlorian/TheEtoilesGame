using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

namespace TarodevController
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class EnemyController_Clone : MonoBehaviour
    {
        #region Variables
        [System.Serializable]
        public class PlayerControllerStatistics
        {
            [Header("LAYERS")]
            [Tooltip("Set this to the layer your player is on")]
            public LayerMask PlayerLayer;

            [Header("INPUT")]
            [Tooltip("Makes all Input snap to an integer. Prevents gamepads from walking slowly. Recommended value is true to ensure gamepad/keybaord parity.")]
            public bool SnapInput = true;

            [Tooltip("Minimum input required before you mount a ladder or climb a ledge. Avoids unwanted climbing using controllers"), Range(0.01f, 0.99f)]
            public float VerticalDeadZoneThreshold = 0.3f;

            [Tooltip("Minimum input required before a left or right is recognized. Avoids drifting with sticky controllers"), Range(0.01f, 0.99f)]
            public float HorizontalDeadZoneThreshold = 0.1f;

            [Header("MOVEMENT")]
            [Tooltip("The top horizontal movement speed")]
            public float MaxSpeed = 14;

            [Tooltip("The player's capacity to gain horizontal speed")]
            public float Acceleration = 120;

            [Tooltip("The pace at which the player comes to a stop")]
            public float GroundDeceleration = 60;

            [Tooltip("Deceleration in air only after stopping input mid-air")]
            public float AirDeceleration = 30;

            [Tooltip("A constant downward force applied while grounded. Helps on slopes"), Range(0f, -10f)]
            public float GroundingForce = -1.5f;

            [Tooltip("The detection distance for grounding and roof detection"), Range(0f, 0.5f)]
            public float GrounderDistance = 0.05f;

            [Header("JUMP")]
            [Tooltip("The immediate velocity applied when jumping")]
            public float JumpPower = 36;

            [Tooltip("The maximum vertical movement speed")]
            public float MaxFallSpeed = 40;

            [Tooltip("The player's capacity to gain fall speed. a.k.a. In Air Gravity")]
            public float FallAcceleration = 110;

            [Tooltip("The gravity multiplier added when jump is released early")]
            public float JumpEndEarlyGravityModifier = 3;

            [Tooltip("The time before coyote jump becomes unusable. Coyote jump allows jump to execute even after leaving a ledge")]
            public float CoyoteTime = .15f;

            [Tooltip("The amount of time we buffer a jump. This allows jump input before actually hitting the ground")]
            public float JumpBuffer = .2f;

            [Header("VISUALS")]
            public bool tilt = false;
        }

        [System.Serializable]
        public class Functionnal
        {
            public GameObject visual;
        }


        public PlayerControllerStatistics _stats;

        [SerializeField]
        private Functionnal _functionnal;

        [HideInInspector]
        public Rigidbody2D _rb;
        private CapsuleCollider2D _col;
        private FrameInput _frameInput;
        [HideInInspector]
        public Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;

        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        #endregion

        private float _time;

        private Quaternion _baseRot = Quaternion.identity;

        //Collisions
        private float _frameLeftGrounded = float.MinValue;
        private bool _grounded;

        //Jump
        private bool _asPressedJump;
        private float _timeWhenJumpPressed;

        //Traversing plateform
        private bool _asPressedTraversingPlateform;
        private Collider2D _colliderInContact;
        private bool _isTraversing;

        //Interacting
        private InteractObject _interactObject;

        //Brain
        private bool _jumpPerformed;
        private bool _jumpPressed;
        private Vector2 _inputsMovement;

        [SerializeField]
        private float _waitBeforeJump = 1;
        private float _timingJump;

        [SerializeField]
        private float hitingDistance;
        [SerializeField]
        private float forceHiting;
        [SerializeField]
        private float decreaseForceHiting;

        private float _hitingDelay = 0.5f;
        private float _hitingTime;

        //Animation
        public Animator animator;

        #endregion

        #region Unity methods
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;

            _baseRot = transform.rotation;
        }

        private void Update()
        {

            _time += Time.deltaTime;
            Brain();
            GatherInput();

            UpdateVisuals();
        }

        private void FixedUpdate()
        {
            CheckCollisions();

            HandleJump();
            HandleTraversing();
            HandleDirection();
            HandleGravity();

            ApplyMovement();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Plateform")
            {
                collision.collider.isTrigger = true;
            }
        }
        #endregion

        #region Inputs
        private void GatherInput()
        {
            _frameInput = new FrameInput();

            _frameInput.JumpDown = _jumpPerformed;
            _frameInput.JumpHeld = _jumpPressed;
            _frameInput.Move = _inputsMovement;

            if (_stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                _asPressedJump = true;
                _timeWhenJumpPressed = _time;
            }

            if (_frameInput.Move.y < 0)
            {
                _asPressedTraversingPlateform = true;
            }

        }

        private void Brain()
        {
            _jumpPerformed = false;

            PlayerController pc = FindAnyObjectByType<PlayerController>();

            if (pc != null )
            {

                Vector2 diff = pc.transform.position - transform.position;


                if (diff.x < 0)
                {
                    _inputsMovement.x = Mathf.Lerp(_inputsMovement.x, -1, 1 * Time.deltaTime);
                }
                else
                {
                    _inputsMovement.x = Mathf.Lerp(_inputsMovement.x, 1, 1 * Time.deltaTime);
                }

                if (diff.y > 1)
                {
                    if (_timingJump >= _waitBeforeJump)
                    {
                        float rand = UnityEngine.Random.Range(0, 100);

                        _jumpPerformed = rand < 70f;

                        _timingJump = 0;
                    }
                }

                _timingJump += Time.deltaTime;
                _timingJump = Mathf.Clamp(_timingJump, 0, _waitBeforeJump);

                if (Vector2.Distance(pc.transform.position, transform.position) <= hitingDistance)
                {
                    if (_hitingTime >= _hitingDelay)
                    {
                        pc.HitingPlayer(diff, forceHiting, decreaseForceHiting);

                        _hitingTime = 0;
                    }
                }

                _hitingTime += Time.deltaTime;
                _hitingTime = Mathf.Clamp(_hitingTime, 0, _hitingDelay);
            }
        }
        #endregion

        #region Moving, Jumping, Traversing
        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            // Ground
            bool groundHit = false;

            RaycastHit2D hitDown = Physics2D.Raycast(_col.bounds.center, Vector2.down, _col.size.y / 2 + _stats.GrounderDistance);
            if (hitDown.collider != null)
            {
                if (hitDown.collider.tag != "Interactable")
                {
                    if (_isTraversing)
                    {
                        groundHit = false;

                        if (_colliderInContact != hitDown.collider)
                        {
                            if (hitDown.collider.tag == "Plateform")
                                hitDown.collider.isTrigger = false;

                            _isTraversing = false;
                            groundHit = true;
                            _colliderInContact = hitDown.collider;
                        }
                    }
                    else
                    {
                        if (hitDown.collider.tag == "Plateform")
                            hitDown.collider.isTrigger = false;

                        groundHit = true;
                        _colliderInContact = hitDown.collider;
                    }
                }


            }
            else
            {
                groundHit = false;
            }

            //Ceiling
            bool ceilingHit = false;

            RaycastHit2D hitUp = Physics2D.Raycast(_col.bounds.center, Vector2.up, _col.size.y / 2 + _stats.GrounderDistance);
            if (hitUp.collider != null)
            {
                if (hitUp.collider.tag != "Interactable")
                {
                    if (hitUp.collider.tag == "Plateform")
                    {
                        hitUp.collider.isTrigger = true;
                        ceilingHit = false;
                    }
                    else
                    {
                        ceilingHit = true;
                    }
                }
            }


            // Hit a Ceiling
            if (ceilingHit)
            {
                _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);
                _isTraversing = false;
            }

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #region Jumping

        private void HandleJump()
        {
            if (!_grounded)
            {
                if (_timeWhenJumpPressed + _stats.JumpBuffer < _time)
                {
                    _asPressedJump = false;
                }
            }

            if (_grounded && _asPressedJump)
                ExecuteJump();
        }

        private void ExecuteJump()
        {
            _timeWhenJumpPressed = 0;
            _asPressedJump = false;
            _frameVelocity.y = _stats.JumpPower;
            Jumped?.Invoke();
        }

        #endregion

        private void HandleTraversing()
        {
            if (_grounded)
            {
                if (_asPressedTraversingPlateform)
                {
                    _asPressedTraversingPlateform = false;
                    if (_colliderInContact.tag == "Plateform")
                    {
                        _colliderInContact.isTrigger = true;
                        _isTraversing = true;
                    }
                }
            }
        }

        public virtual void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_frameVelocity.y > 0)
                    inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        private void ApplyMovement()
        {
            _rb.velocity = _frameVelocity;
            if (_stats.tilt)
            {
                transform.rotation = Quaternion.identity;
            }
        }

        public void HitingPlayer(Vector2 dir, float force, float deacreseForce)
        {
            force = force + GetComponent<DamageVersus>().AddingDamage()*force;

            StartCoroutine(ExplosionHit(dir, force, deacreseForce));
        }

        public IEnumerator ExplosionHit(Vector2 dir, float force, float deacreseForce)
        {
            _frameVelocity += dir * force;
            yield return new WaitForFixedUpdate();
            float newForce = force - deacreseForce * Time.fixedDeltaTime;

            if (newForce > 0f)
            {
                StartCoroutine(ExplosionHit(dir, newForce, deacreseForce));
            }
        }
        #endregion

        #region Visuals
        private void UpdateVisuals()
        {
            if (!_stats.tilt)
            {
                transform.rotation = _baseRot;
            }

            if (_frameInput.Move.x > 0)
            {
                _functionnal.visual.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_frameInput.Move.x < 0)
            {
                _functionnal.visual.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        #endregion
    }

    /*public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }*/
}