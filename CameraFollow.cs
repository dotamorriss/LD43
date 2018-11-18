using UnityEngine;
using System.Collections;
/// <summary>
/// gba恶魔城宽15格，高10格（同洛克人zero），小地图一格为大地图16tilemap
/// sfc洛克人x系列宽16格，高14格
/// 2018/4/9大改摄像机跟随
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 5f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;
    public Transform[] cameraTriggers;

    private Transform player;

    private void Start()
    {
        FindHero();
    }
    public void FindHero()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
   private void Update()
    {
        if (player == null) return;
        foreach (Transform cameraTrigger in cameraTriggers)
        {
            float sizeX = cameraTrigger.GetComponent<BoxCollider2D>().size.x;
            float sizeY = cameraTrigger.GetComponent<BoxCollider2D>().size.y;

            if (player.position.x > cameraTrigger.transform.position.x - sizeX / 2 &&
            player.position.x < cameraTrigger.transform.position.x + sizeX / 2 &&
            player.position.y > cameraTrigger.transform.position.y - sizeY / 2 &&
            player.position.y < cameraTrigger.transform.position.y + sizeY / 2)
            {
                //Debug.Log(cameraTrigger.name);
                float maxX = 0; float minX = 0; float maxY = 0; float minY = 0;
                float right = cameraTrigger.GetComponent<CameraTrigger>().right;
                if (right == 0)
                {
                    maxX = cameraTrigger.transform.position.x;
                }
                else if (right == 1)
                {
                    maxX = cameraTrigger.transform.position.x + 5.33f;
                }
                else
                {
                    maxX = cameraTrigger.transform.position.x + 5.33f + (right - 1) * 16;
                }
                float left = cameraTrigger.GetComponent<CameraTrigger>().left;
                if (left == 0)
                {
                    minX = cameraTrigger.transform.position.x;
                }
                else if (left == 1)
                {
                    minX = cameraTrigger.transform.position.x - 5.33f;
                }
                else
                {
                    minX = cameraTrigger.transform.position.x - 5.33f - (left - 1) * 16;
                }
                float up = cameraTrigger.GetComponent<CameraTrigger>().up;
                if (up == 0)
                {
                    maxY = cameraTrigger.transform.position.y - 2;
                }
                else if (up == 1)
                {
                    maxY = cameraTrigger.transform.position.y + 10;
                }
                else
                {
                    maxY = cameraTrigger.transform.position.y + 10 + (up - 1) * 16;
                }
                float down = cameraTrigger.GetComponent<CameraTrigger>().down;
                if (down == 0)
                {
                    minY = cameraTrigger.transform.position.y - 2;
                }
                else if (down == 1)
                {
                    minY = cameraTrigger.transform.position.y - 10;
                }
                else
                {
                    minY = cameraTrigger.transform.position.y - 10 - (down - 1) * 16;
                }

                //Debug.Log(maxX + "maxX");
                //Debug.Log(maxY + "maxY");
                //Debug.Log(minX + "minX");
                //Debug.Log(minY + "minY");
                maxXAndY = new Vector2(maxX, maxY);
                minXAndY = new Vector2(minX, minY);
                float targetX = player.position.x;
                targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
                float targetY = player.position.y;
                targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
                Vector3 Destination = new Vector3(targetX, targetY, -10);
                Vector3 PositionNow = Vector3.Lerp(transform.position, Destination, FollowSpeed * Time.deltaTime);
                transform.position = new Vector3(PositionNow.x, PositionNow.y, -10);
                break;
            }

        }
    }
}
