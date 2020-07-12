using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats {

    public static int stages, lives;

    public static int Stages {
        get {
            return stages;
        }
        set {
            stages=value;
        }
    }

    public static int Lives {
        get {
            return lives;
        }
        set {
            lives=value;
        }
    }
}
