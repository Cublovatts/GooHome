using UnityEngine;

public enum WALL_TYPE { normal, sticky, slippy, bouncy }

public class Wall : MonoBehaviour
{
    public WALL_TYPE type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player;
        bool isPlayer = collision.gameObject.TryGetComponent<Player>(out player);

        switch (type)
        {
            case WALL_TYPE.sticky:
                if (isPlayer)
                    player.Stick(gameObject);
                break;

            case WALL_TYPE.slippy:
                break;

            case WALL_TYPE.normal:
            default:
                break;
        }
    }
}
