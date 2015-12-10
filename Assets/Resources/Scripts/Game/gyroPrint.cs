using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gyroPrint : MonoBehaviour {

    public Text gyro;
    public Text gyroDistance;
    public Text gyroTrue;

    void Update()
    {
        Vector3 gyroVector = Input.gyro.rotationRate;
        float gyroVectorDistance = Vector3.Distance(Vector3.zero, gyroVector);

        gyro.text = gyroVector.ToString();
        gyroDistance.text = gyroVectorDistance.ToString();

        if (gyroVectorDistance >= 2.5f)
            gyroTrue.text = "True";
        else
            gyroTrue.text = "False";
    }
}
