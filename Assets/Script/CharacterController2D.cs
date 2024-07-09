using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Lực nhảy của nhân vật.
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;           // cúi người
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // Độ mượt khi di chuyển
    [SerializeField] private bool m_AirControl = false;                         // Điều khiển nhân vật ở trên không
    [SerializeField] private LayerMask m_WhatIsGround;                          
    [SerializeField] private Transform m_GroundCheck;                           // Check xem player tiếp đất hay chưa
    [SerializeField] private Transform m_CeilingCheck;                          // Check xem có tường phía trên hay không
    [SerializeField] private Collider2D m_CrouchDisableCollider;           

    const float k_GroundedRadius = .2f; 
    private bool m_Grounded;            
    const float k_CeilingRadius = .2f; 
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // Xác định mặt của player
    private Vector3 m_Velocity = Vector3.zero; // vận tốc

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate() 
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        
        if (!crouch)
        {
            // // Kiểm tra nếu có vật thể chạm vào trần phía trên nhân vật bằng cách sử dụng một vòng tròn nhỏ
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //Nếu nhân vật đang tiếp đất hoặc có thể điều khiển khi đang trên không.
        if (m_Grounded || m_AirControl)
        {

            // Nếu cúi xuống
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true; //Đặt trạng thái cúi.
                    OnCrouchEvent.Invoke(true);
                }

                // giảm tốc khi cúi
                move *= m_CrouchSpeed;

                // vô hiệu collider khi cúi
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // bật lại collider khi kh cúi
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Tính toán vận tốc mục tiêu.
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // Cập nhật lại và áp dụng vào nhân vật
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // Nếu nhân vật di chuyển sang phải nhưng đang quay mặt sang trái.
            if (move > 0 && !m_FacingRight)
            {
                
                Flip();
            }
            
            else if (move < 0 && m_FacingRight)
            {
                
                Flip();
            }
        }
        // nếu nhân vật đang tiếp đất và nhảy
        if (m_Grounded && jump)
        {
    
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce)); // thêm lực nhảy
        }
    }


    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}