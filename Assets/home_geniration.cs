using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class home_geniration : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject center_for_camera;

    [SerializeField] private GameObject[] plate_meshes;
    [SerializeField] private GameObject plate;
    [SerializeField] private GameObject wall_coll;
    [SerializeField] private GameObject stairs;

    [SerializeField] private float plate_hight = 0.5f;
    [SerializeField] private float plate_side = 5f;
    [SerializeField] private float dist_between_floors = 5.5f;
    [SerializeField] private int grid_side_amount_of_plates = 10;
    [SerializeField] private int amount_of_floors = 5;

    [SerializeField] private int centralisation_of_random_plate = 2;
    [SerializeField] private int amount_spawned_plates = 5;

    void Start()
    {
        HashSet<Vector3> spawned_plates_poses = new HashSet<Vector3>();

        HashSet<Vector3> center_poses = new HashSet<Vector3>();

        float floor_border = ((amount_of_floors - 1)/2) * dist_between_floors;

        Vector3 floor_center_plate_pos = new Vector3(0, floor_border, 0);
        for (float floor_hight = floor_border; floor_hight >= -floor_border; floor_hight -= dist_between_floors)
        {
            floor_center_plate_pos.y = floor_hight;

            HashSet<Vector3> spawned_floor_plates_poses = generate_all_floor_poses(floor_center_plate_pos);

            floor_center_plate_pos = spawned_floor_plates_poses.ElementAt(Random.Range(0, spawned_floor_plates_poses.Count - 1));

            center_poses.Add(floor_center_plate_pos);

            spawned_plates_poses.UnionWith(spawned_floor_plates_poses);
        }
        

        HashSet<GameObject> spawned_plates = new HashSet<GameObject>();

        foreach (Vector3 pos in spawned_plates_poses)
        {
            GameObject _plate = Instantiate(plate, transform);
            GameObject plate_mash = Instantiate(plate_meshes.ElementAt(Random.Range(0, plate_meshes.Length)));

            plate_mash.transform.parent = _plate.transform;
            _plate.transform.position = pos;
            spawned_plates.Add(_plate);
        }

        foreach (Vector3 pos in center_poses)
        {
            GameObject _steirs = Instantiate(stairs, transform);

            _steirs.transform.position = new Vector3(pos.x, pos.y - 3, pos.z);
        }

        find_wall_poses(spawned_plates_poses);

        spawner.transform.position = center_poses.ElementAt(Random.Range(0, center_poses.Count));
        center_for_camera.transform.position = spawner.transform.position;
    }

    HashSet<Vector3> find_neibors(Vector3 vec)
    {
        float x = vec.x;
        float y = vec.y;
        float z = vec.z;

        HashSet<Vector3> neibor_poses = new HashSet<Vector3>();
        neibor_poses.Add(new Vector3(x + plate_side, y, z));
        neibor_poses.Add(new Vector3(x - plate_side, y, z));
        neibor_poses.Add(new Vector3(x, y, z + plate_side));
        neibor_poses.Add(new Vector3(x, y, z - plate_side));

        //neibor_poses.IntersectWith(posible_values);
        return neibor_poses;
    }

    HashSet<Vector3> generate_all_floor_poses(Vector3 curr_plate_pos)
    {
        HashSet<Vector3> spawned_plates_poses = new HashSet<Vector3>();
        spawned_plates_poses.Add(curr_plate_pos);

        while (spawned_plates_poses.Count < amount_spawned_plates)
        {
            HashSet<Vector3> all_posible_neibors = find_neibors(curr_plate_pos);
            all_posible_neibors.ExceptWith(spawned_plates_poses);

            if (all_posible_neibors.Count != 0)
            {
                curr_plate_pos = all_posible_neibors.ElementAt(Random.Range(0, all_posible_neibors.Count - 1));
                spawned_plates_poses.Add(curr_plate_pos);
            }

            curr_plate_pos = spawned_plates_poses.ElementAt(Random.Range(0, spawned_plates_poses.Count - 1));
        }

        return spawned_plates_poses;
    }

    private HashSet<GameObject> find_wall_poses(HashSet<Vector3> spawned_plates_poses)
    {
        HashSet<GameObject> walls = new HashSet<GameObject>();

        foreach (Vector3 plate_pos in spawned_plates_poses)
        {
            HashSet<Vector3> empty_neibors = find_neibors(plate_pos);
            empty_neibors.ExceptWith(spawned_plates_poses);

            if (empty_neibors.Count > 0)
            {
                foreach (Vector3 empty_cell in empty_neibors)
                {
                    GameObject _wall = Instantiate(wall_coll, transform);
                    _wall.transform.position = (empty_cell + plate_pos) / 2;
                    if (empty_cell.x == plate_pos.x)
                    {
                        walls.Add(_wall);
                    }
                    else
                    {
                        _wall.transform.Rotate(0, 90, 0);
                        walls.Add(_wall);
                    }
                    
                }
            }
        }

        return walls;
    }

    private HashSet<Vector3> get_posible_valuse(float floor_hight)
    {
        float floor_border = ((grid_side_amount_of_plates - 1) / 2) * plate_side;
        HashSet<Vector3> floor_values = new HashSet<Vector3>();
        int i = 0;
        for (float v_z = -floor_border; v_z <= floor_border || i < grid_side_amount_of_plates * grid_side_amount_of_plates; v_z += plate_side, i++)
        {
            for (float v_x = -floor_border; v_x <= floor_border || i < grid_side_amount_of_plates * grid_side_amount_of_plates; v_x += plate_side)
            {
                floor_values.Add(new Vector3(v_x, floor_hight, v_z));

                i++;
            }

        }
        return floor_values;
    }
}
