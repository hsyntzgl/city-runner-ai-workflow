# City Runner — AI-Augmented Unity Prototype

A 3D game prototype built in **Unity (C#)** using an **AI-augmented workflow**: [Claude](https://claude.ai) connected to the Unity Editor through **MCP (Model Context Protocol)**.

The goal was not only to build a game, but also to test how much an AI workflow can help in real Unity development.


## Features

- Third-person character controller (run, idle)
- Animation states with transitions (idle / run)
- Survival stats system: **stamina**, **health**, and **hunger** bars
  - Stamina goes down while running and comes back while idle
  - Hunger goes down over time and affects health
- City environment to explore

![Stats system](stats.gif)

## How AI Was Used

I developed this project together with **Claude**, connected to the Unity Editor with MCP.

**Done with Claude (through MCP):**
- Character controller
- Camera system
- Survival stats system (stamina, health, hunger)
- Animations and animation transitions

**Done by me (without AI):**
- Everything else: city environment setup, scene work, and final fixes
- Reading, fixing, and testing the AI-generated code
- Designing how the game systems should work

## What Worked / What Didn't

- ✅ AI was very fast for repeating editor work (creating objects, adding components)
- ✅ Script drafts saved time as a starting point
- ⚠️ Sometimes the AI code compiled but did not work correctly in play mode — I still had to test every change
- ⚠️ Some detailed work (animator transitions, small adjustments) was faster to do by hand

## Built With

- Unity 6.5(6000.5.2f1)
- C#
- Claude + MCP (Model Context Protocol)
- City environment: CITY 
- Character: Low Poly Character Package

## How to Run

1. Clone the repository
2. Open the project folder in Unity Hub
3. Open the main scene Assets/Scenes/DemoScene.unity and press **Play**

---

*This project is part of my work on AI-augmented game development. More about me: [github.com/hsyntzgl](https://github.com/hsyntzgl)*
