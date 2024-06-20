using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class home_geniration : MonoBehaviour
{
    [SerializeField] private GameObject plate;

    [SerializeField] private float plate_hight = 0.5f;
    [SerializeField] private float plate_side = 5f;
    [SerializeField] private float dist_between_plates = 5f;
    [SerializeField] private int grid_side_amount_of_plates = 10;
    [SerializeField] private int grid_hight_amount_of_floors = 5;

    [SerializeField] private int centralisation_of_random_plate = 2;
    [SerializeField] private int amount_spawned_plates = 5;

    public HashSet<Vector3> floor_values;
    private float floor_border;


    void Start()
    {
        floor_border = ((grid_side_amount_of_plates - 1) / 2) * plate_side;

        floor_values = new HashSet<Vector3>();

        float floor_hight = 0;

        int i = 0;
        for (float v_z = -floor_border; v_z <= floor_border || i < grid_side_amount_of_plates * grid_side_amount_of_plates; v_z += plate_side, i++)
        {
            for (float v_x = -floor_border; v_x <= floor_border || i < grid_side_amount_of_plates * grid_side_amount_of_plates; v_x += plate_side)
            {
                floor_values.Add(new Vector3(v_x, floor_hight, v_z));

                i++;
            }

        }

        HashSet<Vector3> spawned_plates_poses = new HashSet<Vector3>();

        float almost_center = floor_border - Mathf.Round(grid_side_amount_of_plates / 2) * plate_side;
        Vector3 curr_plate_pos = new Vector3(almost_center, floor_hight, almost_center);

        while (spawned_plates_poses.Count < amount_spawned_plates)
        {
            HashSet<Vector3> all_posible_neibors = find_neibors(curr_plate_pos, floor_values);
            all_posible_neibors.ExceptWith(spawned_plates_poses);

            if (all_posible_neibors.Count != 0)
            {
                curr_plate_pos = all_posible_neibors.ElementAt(Random.Range(0, all_posible_neibors.Count - 1));
                spawned_plates_poses.Add(curr_plate_pos);
            }

             curr_plate_pos = spawned_plates_poses.ElementAt(Random.Range(0, spawned_plates_poses.Count - 1));
            
        }

        HashSet<GameObject> spawned_plates = new HashSet<GameObject>();

        foreach (Vector3 pos in spawned_plates_poses)
        {
            GameObject _plate = Instantiate(plate);
            _plate.transform.position = pos;
            spawned_plates.Add(_plate);
        }
    }

    HashSet<Vector3> find_neibors(Vector3 vec, HashSet<Vector3> posible_values)
    {
        float x = vec.x;
        float y = vec.y;
        float z = vec.z;

        HashSet<Vector3> neibor_poses = new HashSet<Vector3>();
        neibor_poses.Add(new Vector3(x + plate_side, y, z));
        neibor_poses.Add(new Vector3(x - plate_side, y, z));
        neibor_poses.Add(new Vector3(x, y, z + plate_side));
        neibor_poses.Add(new Vector3(x, y, z - plate_side));

        neibor_poses.IntersectWith(posible_values);
        return neibor_poses;
    }
}
