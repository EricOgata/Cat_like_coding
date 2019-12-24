using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    [Header("Cube Properties")]
    [SerializeField] Transform pointPrefab;
    [Range(10, 100)] 
    [SerializeField] int resolution = 10;
    [SerializeField] GraphFunctionName function;
    Transform[] points;
    static GraphFunction[] functions = {
        SineFunction, Sine2DFunction,
        MultiSineFunction, MultiSine2DFunction,
        SquareFunction, CubeFunction,
        Ripple,
        Cylinder,
        Sphere,
        Torus,
        Test
    };

    private void Awake() {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++) {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    private void Update() {
        float t = Time.time;
        GraphFunction f = functions[(int)function];
        float step = 2f / resolution;
        for(int i = 0, z = 0; z < resolution; z++) {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++) {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }
    }

    const float pi = Mathf.PI;

    static Vector3 SineFunction(float x, float z, float t) {
        //return Mathf.Sin(pi * (x + t));
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t)); // a formula modifica o eixo Y.
        p.z = z;
        return p;
    }

    static Vector3 MultiSineFunction(float x, float z, float t) {
        Vector3 p;
        p.x = x;
        // Modifica o eixo Y
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t) {
        // f(x, z, t) = sin(pi(x + z + t))
        //return Mathf.Sin(pi * (x + z + t));

        // f(x, z, t) = ( sin(pi(x + t)) + sin(pi(z + t)) ) / 2
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(pi * (x + t));
        p.y *= 0.5f;// Using *0.5 instead of /2f
        p.z = z;
        return p;
    }

    static Vector3 MultiSine2DFunction(float x, float z, float t) {
        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        p.y += Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    static Vector3 SquareFunction(float x, float z, float t) {
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = (x * x) * Mathf.Sin(pi * t);
        return p;
    }

    static Vector3 CubeFunction(float x, float z, float t) {
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = (x * x * x) * Mathf.Sin(pi * t);
        return p;
    }

    static Vector3 Ripple(float x, float z, float t) {
        Vector3 p;
        float d = Mathf.Sqrt(x * x + z * z); // hipotenusa a² + b² = c²
        p.x = x;
        p.z = z;
        p.y = Mathf.Sin(pi * (4f * d - t));
        p.y /= 1f + 10f * d; // definindo a amplitude do efeito (1/10D), onde D = Distância da origem. Ou seja, quanto mais próximo da origem, maior o efeito.
        return p;
    }

    static Vector3 Test(float x, float z, float t) {
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = (x + z);
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t) {
        //float r = 2f; // Raio do círculo
        //float r = 1f + Mathf.Sin(6f*pi*u)*0.5f; // Raio = 1 + (sin(6 * pi * u))/5 (Raio dependendo do valor de U)
        //float r = 1f + Mathf.Sin(2f * pi * v) * 0.5f; // Raio = 1 + (sin(2 * pi * v))/5 (Raio dependendo do valor de v)
        float r = 0.8f + Mathf.Sin(pi * (3f * u + 1f * v + t)) * 0.2f;
        Vector3 p;
        p.x = r * Mathf.Sin(pi * u);
        p.y = v;
        p.z = r * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 Sphere(float u, float v, float t) {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(pi * 0.5f * v);
        p.x = s * Mathf.Sin(pi * u);
        p.y = r * Mathf.Sin(pi * 0.5f * v);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 Torus(float u, float v, float t) {
        Vector3 p;
        //float r1 = 1f;
        //float r2 = 0.5f;
        float r1 = 0.65f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(pi * (4f * v + t)) * 0.05f;
        float s = r2 * Mathf.Cos(pi * v) + r1;
        p.x = s * Mathf.Sin(pi * u);
        p.y = r2 * Mathf.Sin(pi * v);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }
}
