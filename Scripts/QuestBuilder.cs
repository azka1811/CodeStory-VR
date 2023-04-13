using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
public class QuestBuilder : MonoBehaviour
{
    string path;
    string jsonString;
    QuestJSON currentJson;

    public GameObject questItems;

    public PrefabContainer prefabs;

    public float time = 120;

    // Start is called before the first frame update
    bool makeOne = false;
    void Start()
    {
        prefabs = GetComponent<PrefabContainer>();

        path = Application.dataPath + "/sample.json";
        currentJson = readJSON(path);
        print(currentJson.arithematic_operators);
    }
    // Update is called once per frame
    void Update()
    {
        if (time < 0)
        {
            makeOne = true;
            time = 0;
        }
        else
        {
            questItems.transform.parent.gameObject.GetComponent<Quest>().questNameText.text = getTimeFormatted(time);
        }
        if (makeOne)
        {
            buildQuest();
            makeOne = false;
        }
        else if (time > 0)
        {
            time -= Time.deltaTime;

        }
    }

    public QuestJSON readJSON(string path)
    {
        jsonString = File.ReadAllText(path);
        QuestJSON json = JsonUtility.FromJson<QuestJSON>(jsonString);
        return json;
    }

    void makeConst(GameObject obj, string value)
    {
        TextMeshPro[] texts = obj.GetComponentsInChildren<TextMeshPro>();
        foreach (TextMeshPro t in texts)
        {
            t.text = value;
        }
    }

    void setPosition(GameObject obj)
    {
        obj.transform.rotation = questItems.transform.rotation;
        float xrs = -13f; //2
        float xre = 6f; //11
        float zrs = -1.5f;
        float zre = 10f;

        float ox = questItems.transform.position.x;
        float oz = questItems.transform.position.z;

        float[] coords = { 0f, 0f, 0f };

        int items = questItems.transform.childCount;

        int iter = 0;

        float dx, dz, x, z, y;

        dx = Random.Range(xrs, xre);
        dz = Random.Range(zrs, zre);
        x = ox + dx;
        z = oz + dz;
        y = questItems.transform.position.y + 3f;

        while (iter < 20000)
        {
            iter++;
            bool done = true;
            for (int j = 0; j < items; j++)
            {
                GameObject check = questItems.transform.GetChild(j).gameObject;

                Bounds bounds1 = check.GetComponent<Collider>().bounds;

                Bounds bounds2 = obj.GetComponent<Collider>().bounds;

                bool intersecting = bounds1.Intersects(bounds2);

                if (obj != check && intersecting)
                {
                    dx = Random.Range(xrs, xre);
                    dz = Random.Range(zrs, zre);
                    x = ox + dx;
                    z = oz + dz;
                    obj.transform.position = new Vector3(x, y, z);
                    done = false;
                    if (iter % 100 == 0)
                        Debug.Log("retrying" + iter);
                }

            }
            if (done)
            {
                obj.transform.position = new Vector3(x, y, z);
            }
        }
        obj.transform.position = new Vector3(x, y, z);
    }

    void buildQuest()
    {
        string[] ath_ops = currentJson.arithematic_operators.Split(",");
        int complexity = currentJson.complexity;
        int conditions = currentJson.conditions;
        string[] constants = currentJson.constants.Split(",");
        string[] conditional = currentJson.conditional.Split(",");
        string expected_output = currentJson.expected_output;
        int loops = currentJson.loops;
        string quest_prompt = currentJson.quest_prompt;
        int reward = currentJson.reward;
        string type = currentJson.type;
        int variables = currentJson.variables;
        int connectors = currentJson.connectors;

        Quest plaQuest = questItems.transform.parent.gameObject.GetComponent<Quest>();

        plaQuest.reward = reward;
        plaQuest.type = type.Trim();
        plaQuest.questRequirement = quest_prompt;
        plaQuest.expectedVal[0] = expected_output;
        plaQuest.complexity = complexity;
        plaQuest.questHint = "This is a personalized quest!";

        plaQuest.setEverythingPLA();

        if (ath_ops.Length > 0)
        {

            for (int i = 0; i < ath_ops.Length; i++)
            {
                ath_ops[i] = ath_ops[i].Trim();
                print(i + ath_ops[i]);

                if (ath_ops[i] == "ADD")
                {
                    GameObject op = Instantiate(prefabs.add_op, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    op.transform.parent = questItems.transform;
                    op.name = "ADD_OP_" + i;
                    setPosition(op);

                }
                else if (ath_ops[i] == "MUL")
                {
                    GameObject op = Instantiate(prefabs.mul_op, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    op.transform.parent = questItems.transform;
                    op.name = "MUL_OP_" + i;
                    setPosition(op);

                }
                else if (ath_ops[i] == "DIV")
                {
                    GameObject op = Instantiate(prefabs.div_op, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    op.transform.parent = questItems.transform;
                    op.name = "DIV_OP_" + i;
                    setPosition(op);

                }
                else if (ath_ops[i] == "SUB")
                {
                    GameObject op = Instantiate(prefabs.sub_op, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    op.transform.parent = questItems.transform;
                    op.name = "SUB_OP_" + i;
                    setPosition(op);

                }
            }

        }
        for (int i = 0; i < variables; i++)
        {
            GameObject op = Instantiate(prefabs.var, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            op.transform.parent = questItems.transform;
            op.name = "VAR_" + i;
            setPosition(op);
        }

        for (int i = 0; i < constants.Length; i++)
        {
            string val = constants[i].Trim();
            GameObject op = Instantiate(prefabs.constant, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            op.transform.parent = questItems.transform;
            op.transform.localScale = new Vector3(1, 1, 1);
            op.name = "CONST_" + i;
            makeConst(op, val);
            setPosition(op);
        }

        for (int i = 0; i < connectors; i++)
        {
            GameObject op = Instantiate(prefabs.connector, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            op.transform.parent = questItems.transform;
            op.name = "CONN_" + i;
            op.transform.rotation = Quaternion.identity;
            setPosition(op);
        }
        plaQuest.Freeze();
    }
    public string getTimeFormatted(float time)
    {
        int mins = (int)(time / 60);
        string minsStr = "";
        if (mins < 10)
        {
            minsStr = "0" + mins;
        }
        else
        {
            minsStr = "" + mins;
        }
        int secs = (int)(time - (mins * 60));
        string secsStr = "";
        if (secs < 10)
        {
            secsStr = "0" + secs;
        }
        else
        {
            secsStr = "" + secs;
        }
        return minsStr + ":" + secsStr;
    }
}

[System.Serializable]
public class QuestJSON
{
    public string arithematic_operators;
    public int complexity;
    public string conditional;
    public int conditions;
    public string constants;
    public string expected_output;
    public int id;
    public int loops;
    public string quest_prompt;
    public int reward;
    public string type;
    public int variables;
    public int connectors;

}
