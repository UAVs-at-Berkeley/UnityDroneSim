# Unity Drone Simulator

**A drone created for Unity with realistic drone physics, intended for Reinforcement Learning Simulation.**

*Project AutoQuad, Spring 2018, UAVs @ Berkeley & Machine Learning @ Berkeley*

![alt text](sim_image.png)

---

### Setup Instructions

1. Download Blender: <a>https://www.blender.org/download/</a>
2. Download Unity 2017: <a>https://unity3d.com/get-unity/download/</a>
3. Clone this repo, master
    1. **master**: Currently velocity_control, stable (supports ML-Agents)
    2. velocity_control: for development of realistic velocity control for drone, supports ML-Agents interface
		1. ML-Agents v0.3.1b as of right now (0.3 necessary)
    3. custom_physics: Roll, Pitch, Yaw, Throttle PID control [1]
    4. cubedrone: precursor to velocity control branch
4. Open Up Unity, and select Open and navigate to the root directory of this repository.
5. Press Play to run the simulation
    1. velocity control branch
        1. I: FORWARD + STRAIGHT (default action in player mode)
        2. J: FORWARD + LEFT
        3. L: FORWARD + RIGHT
    2. custom_physics branch (behind on environment and ml-agents code)
        1. I: PITCH FORWARD
        2. K: PITCH BACKWARD
        3. J: ROLL LEFT
        4. L: ROLL RIGHT
        5. W: THROTTLE UP
        6. S: THROTTLE DOWN
        7. A: YAW LEFT
        8. D: YAW RIGHT
6. To Build: File -> Build Settings, Build, and then select the 
    1. Make sure the build settings are all correct for your environment
7. See the <a href="https://github.com/suneelbelkhale/AutoQuad">AutoQuad repository</a> for ML-Agents code to interface with the environment.
    1. Includes sample Imitation Learning and RL approaches

---
### State Spaces

Choose between the following two states by toggling the use_new_state boolean in the DroneAgent.cs script (or in inspector under DroneParent -> DroneAgent).

1. New state space (5 elements in this order)
    1. Heading (direction, normalized -1 to 1)
    2. Distance from target
    3. forward velocity (normalized)
    4. yaw rate (normalized)
    5. collision (1 or 0)
2. Old state (13 elements in this order)
    1. (X,Y,Z) velocity
    2. (X,Y,Z) position
    3. (X,Y,Z) Euler angles
    4. (X,Y,Z) Target Position
    5. collision (1 or 0)

Observations are 128x128 grayscale images.

---
### Members 

#### ML@Berkeley:
*Project Managers:* Suneel Belkhale, Alex Li  
*Project Members:* Gefen Kohavi, Murtaza Dalal, Daniel Ho, Franklin Rice, Allan Levy

##### UAV's@Berkeley:
*Project Managers:* Suneel Belkhale  
*Project Members:* Alex Chan, Kevin Yang, Valmik Prabhu, Isabella Maceda, Erin Song, Dilan Bhalla, Asad Abbasi, Jason Kim

---
### References

[1] For RPY PID Controller code: https://github.com/WebdiverShaka/DroneControl
