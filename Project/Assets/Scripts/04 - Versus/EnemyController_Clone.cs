using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

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
            public GameObject interactInfo;

            public bool activeCamera;
            public GameObject playerCamera;
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

        
        #endregion

        #region Unity methods
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;

            _baseRot = transform.rotation;

            _functionnal.playerCamera.SetActive(_functionnal.activeCamera);
        }

        private void Update()
        {

            _time += Time.deltaTime;
            GatherInput();

            UpdateVisuals();
            HandleInteraction();
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

            _frameInput.JumpDown = InputManager.instance.jumpPerformed;
            _frameInput.JumpHeld = InputManager.instance.jump.IsPressed();
            _frameInput.Move = InputManager.instance.move.ReadValue<Vector2>();

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

        #region Interacting

        private void HandleInteraction()
        {
            if (_functionnal.interactInfo.activeSelf)
            {
                if (InputManager.instance.interactPerformed)
                {
                    _interactObject.Action();
                }
            }
        }

        public void AllowInteract(InteractObject target)
        {
            _functionnal.interactInfo.SetActive(true);
            _interactObject = target;
        }

        public void DisallowInteract(InteractObject target)
        {
            _functionnal.interactInfo.SetActive(false);
            _interactObject = null;
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