using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class home_geniration : MonoBehaviour
{
    //[SerializeField] manadger_script manadger_script;
    [SerializeField] private spawner_script spawner_script;
    [SerializeField] GameObject camera_center;

    [SerializeField] Color[] colors;

    [SerializeField] private GameObject[] plate_meshes;
    [SerializeField] private GameObject[] plate_ornaments;
    [SerializeField] private GameObject plate;
    [SerializeField] private GameObject wall_coll;
    [SerializeField] private GameObject stairs;

    private float plate_side = 5f;
    private float dist_between_floors = 6.5f;

    int amount_of_floors = 5;
    int amount_spawned_plates = 5;
    [SerializeField] private int ornament_fiequoncy = 5;


    HashSet<Vector3> spawned_plates_poses;

    public void genirate_level()
    {
        amount_of_floors = PlayerPrefs.GetInt("amount_of_floors");
        amount_spawned_plates = PlayerPrefs.GetInt("amount_spawned_plates", amount_spawned_plates);


        spawned_plates_poses = new HashSet<Vector3>();

        float floor_border = (amount_of_floors - 1) * dist_between_floors;
        floor_border /= 2;
        //float middle_floor = floor_border - (floor_border * 2);

        HashSet<GameObject> all_wall_transforms = new HashSet<GameObject>();

        float mid_point = floor_border - dist_between_floors * Mathf.Round((amount_of_floors - 1) / 2);
        Vector3 floor_center_plate_pos = new Vector3(0, floor_border, 0);
        for (float floor_hight = floor_border; floor_hight >= -floor_border; floor_hight -= dist_between_floors)
        {
            floor_center_plate_pos.y = floor_hight;

            HashSet<Vector3> spawned_floor_plates_poses = generate_all_floor_poses(floor_center_plate_pos);

            HashSet<GameObject> wall_transforms = find_edge_transforms(spawned_floor_plates_poses);

            if (floor_hight != -floor_border)
            {
                GameObject random_wall = wall_transforms.ElementAt(Random.Range(0, wall_transforms.Count));

                floor_center_plate_pos = random_wall.transform.position;// + plate_side * -random_wall.transform.forward;

                Vector3 steir_pose = random_wall.transform.position;
                steir_pose.y -= 3;

                Destroy(random_wall);
                wall_transforms.Remove(random_wall);
                GameObject _steirs = Instantiate(stairs, transform);
                _steirs.transform.position = steir_pose;
                _steirs.transform.rotation = random_wall.transform.rotation;
                _steirs.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];

                if (floor_hight == mid_point)
                {
                    camera_center.transform.position = steir_pose;
                }
            }

            spawned_plates_poses.UnionWith(spawned_floor_plates_poses);
            //all_wall_transforms.UnionWith(wall_transforms);
        }


        HashSet<GameObject> spawned_plates = new HashSet<GameObject>();

        foreach (Vector3 pos in spawned_plates_poses)
        {
            GameObject _plate = Instantiate(plate, transform);
            GameObject plate_mash = Instantiate(plate_meshes.ElementAt(Random.Range(0, plate_meshes.Length)));

            plate_mash.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];

            if (Random.Range(0, ornament_fiequoncy) == 1)
            {
                GameObject plate_ornament = Instantiate(plate_ornaments.ElementAt(Random.Range(0, plate_ornaments.Length)));
                plate_ornament.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
                plate_ornament.transform.parent = _plate.transform;
            }

            plate_mash.transform.parent = _plate.transform;

            _plate.transform.position = pos;
            spawned_plates.Add(_plate);
        }

        spawner_script.spawn_dudes(spawned_plates_poses);
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


    private HashSet<GameObject> find_edge_transforms(HashSet<Vector3> spawned_plates_poses)
    {
        HashSet<GameObject> edge_transforms = new HashSet<GameObject>();

        foreach (Vector3 plate_pos in spawned_plates_poses)
        {
            HashSet<Vector3> empty_neibors = find_neibors(plate_pos);
            empty_neibors.ExceptWith(spawned_plates_poses);

            if (empty_neibors.Count > 0)
            {

                foreach (Vector3 empty_cell in empty_neibors)
                {
                    GameObject wall = Instantiate(wall_coll, transform);

                    wall.transform.position = empty_cell;
                    wall.transform.forward = -(empty_cell - plate_pos).normalized;

                    edge_transforms.Add(wall);
                }
            }
        }

        return edge_transforms;
    }
}
