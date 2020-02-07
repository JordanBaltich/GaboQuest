//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CreateSphere : MonoBehaviour
//{

//    float R = 277;

//    float rotationX = 0;
//    float rotationY = 0;
//    float velocityX = 0;
//    float velocityY = 0;
//    float pushBack = 0;
//    float phi;
//    float ga;
//    public int kMaxPoints = 100000;
//    public int nbrPoints = 2000;

//    public SpherePoint[] pts;
//    bool addPoints = true;

//    void Start()
//    {
//        phi = (Mathf.Sqrt(5) + 1) / 2 - 1; // golden ratio
//        ga = phi * 2 * Mathf.PI;           // golden angle

//        SpherePoint[] pts = new SpherePoint[kMaxPoints];
//    }


//    // To add: Give each point a heading and velocity, adjust based on distance/heading to nearby points.
//    //
//    class SpherePoint
//    {
//        float lat;
//        float lon;
//        SpherePoint(float lat, float lon)
//        {
//            this.lat = lat;
//            this.lon = lon;
//        }
//    };





//    void initSphere()
//    {
//        for (int i = 1; i <= Mathf.Min(nbrPoints, pts.Length); ++i)
//        {
//            float lon = ga * i;
//            lon /= 2 * Mathf.PI; lon -= floor(lon); lon *= 2 * Mathf.PI;
//            if (lon > Mathf.PI) lon -= 2 * Mathf.PI;

//            // Convert dome height (which is proportional to surface area) to latitude
//            float lat = Mathf.Asin(-1 + 2 * i / (float)nbrPoints);

//            pts[i] = new SpherePoint(lat, lon);
//        }
//    }

//    //void setup()
//    //{
//    //    // size(1024, 768, P3D); 
//    //    size(600, 600, P3D);
//    //    R = .8 * height / 2;

//    //    initSphere();

//    //    colorMode(HSB, 1);
//    //    background(0);


//    //}

//    void draw()
//    {
//        if (addPoints)
//        {
//            nbrPoints += 1;
//            nbrPoints = Mathf.Min(nbrPoints, kMaxPoints);
//            initSphere();
//        }

//        //background(0);
//        //smooth();

//        renderGlobe();

//        rotationX += velocityX;
//        rotationY += velocityY;
//        velocityX *= 0.95;
//        velocityY *= 0.95;

//        //// Implements mouse control (interaction will be inverse when sphere is  upside down)
//        //if (mousePressed)
//        //{
//        //    velocityX += (mouseY - pmouseY) * 0.01;
//        //    velocityY -= (mouseX - pmouseX) * 0.01;
//        //}
//    }


//    void renderGlobe()
//    {
//        pushMatrix();
//        translate(width / 2.0, height / 2.0, pushBack);

//        float xRot = radians(-rotationX);
//        float yRot = radians(270 - rotationY - millis() * .01);
//        rotateX(xRot);
//        rotateY(yRot);


//        strokeWeight(3);

//        float elapsed = millis() * .001;
//        float secs = floor(elapsed);

//        for (int i = 1; i <= min(nbrPoints, pts.Length); ++i)
//        {
//            float lat = pts[i].lat;
//            float lon = pts[i].lon;

//            pushMatrix();
//            rotateY(lon);
//            rotateZ(-lat);
//            float lum = (cos(pts[i].lon + PI * .33 + yRot) + 1) / 2;
//            stroke(.5, .5 * lum, .2 + lum * .8);

//            point(R, 0, 0);

//            popMatrix();
//        }

//        popMatrix();

//    }

//    //void mouseClicked()
//    //{
//    //    addPoints = !addPoints;
//    //}
//}
