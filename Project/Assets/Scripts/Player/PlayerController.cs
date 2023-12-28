using System;
using UnityEngine;

namespace TarodevController
{
    /// <summary>
    /// Hey!
    /// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
    /// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
    /// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
    /// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
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


        [SerializeField]
        private PlayerControllerStatistics _stats;

        [SerializeField]
        private Functionnal _functionnal;

        private Rigidbody2D _rb;
        private CapsuleCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
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
            GatherInput();

            UpdateVisuals();

        }

        private void FixedUpdate()
        {
            CheckCollisions();

            HandleJump();
            HandleDirection();
            HandleGravity();

            ApplyMovement();
        }

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
        }

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            // Ground and Ceiling
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

            bool groundHit;

            RaycastHit2D hit = Physics2D.Raycast(_col.bounds.center, Vector2.down,_col.size.y/2 + _stats.GrounderDistance);
            if (hit.collider != null)
            {
                groundHit = true;
            }
            else
            {
                groundHit = false;
            }

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

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
                if (_timeWhenJumpPressed+_stats.JumpBuffer < _time)
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

        private void HandleDirection()
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
                transform.rotation = Quaternion.identity;
        }

        private void UpdateVisuals()
        {
            if (!_stats.tilt)
            {
                transform.rotation = _baseRot;
            }

            if (_frameInput.Move.x > 0)
            {
                _functionnal.visual.transform.localScale = new Vector3(1,1,1);
            }
            else if (_frameInput.Move.x < 0)
            {
                _functionnal.visual.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }

    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }
}