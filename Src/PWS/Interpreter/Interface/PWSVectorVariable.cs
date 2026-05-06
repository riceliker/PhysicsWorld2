using Godot;
using System;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class PWSVectorList
    {
        
    }
    public class PWSVector
    {
        public float x {set;get;}
        public float y {set;get;}
        public float z {set;get;}
        public PWSVector(float x, float y, float z)
        {
            this.x = x; this.y = y; this.z = z;
        }
        public static PWSVector add(PWSVector a, PWSVector b)
        {
            return new PWSVector(
                a.x + b.x,
                a.y + b.y,
                a.z + b.z
            );
        }
        public static PWSVector sub(PWSVector a, PWSVector b)
        {
            return new PWSVector(
                a.x - b.x,
                a.y - b.y,
                a.z - b.z
            );
        }
        public static PWSVector multiply(PWSVector v, float t)
        {
            return new PWSVector(
                v.x * t,
                v.y * t,
                v.z * t
            );
        }
        public static float dot(PWSVector a, PWSVector b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
        public static PWSVector cross(PWSVector a, PWSVector b)
        {
            return new PWSVector(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );
        }
        public float magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }
        public PWSVector normalize()
        {
            float len = magnitude();
            if (len < 0.00001f) 
                return new PWSVector(0, 0, 0);

            return new PWSVector(
                x / len,
                y / len,
                z / len
            );
        }
        public static PWSVector zero()
        {
            return new PWSVector(0, 0, 0);
        }
        public static PWSVector unit()
        {
            return new PWSVector(1, 1, 1);
        }
        public static float Distance(PWSVector a, PWSVector b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}
