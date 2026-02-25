# Unity Tower Defence Game (C#, Unity)

A tower defence project built in Unity using C#. The project implements the core tower defence loop: selecting and placing turrets on build nodes, spawning enemy waves that follow waypoint paths, managing player money and lives, and ending the game when lives reach zero.

This repository also includes turret upgrades, sell logic, basic UI, camera controls, audio, and scene transitions.

---

## Gameplay Overview

- Enemies spawn in waves and follow a waypoint path.
- The player builds turrets on nodes using money.
- Turrets target the nearest enemy within range and attack using either:
  - **Bullets** (with optional explosion radius), or
  - **Laser** (damage over time + slow effect).
- The player earns money by defeating enemies.
- The player loses lives when enemies reach the end of the path.
- The game ends when lives reach zero.

---

## Key Systems Implemented

### Building and Upgrading
- **BuildManager**: selects which turret type is being built and manages node selection.
- **Shop**: turret selection (standard turret, missile launcher, laser beamer).
- **Node**: build placement, upgrade, sell, hover feedback (including “not enough money” colour).
- **NodeUI**: upgrade and sell controls.

### Turrets and Attacking
- **Turret**:
  - Finds a target by tag at intervals (`InvokeRepeating`).
  - Rotates towards target (lock-on rotation).
  - Two attack modes:
    - **Bullet mode**: fires based on fire rate using a bullet prefab.
    - **Laser mode**: uses LineRenderer + particles + light, deals damage over time, applies slow, and plays attack audio.
- **Bullet**:
  - Seeks a target.
  - On hit: spawns impact effect.
  - Optional AoE using `Physics.OverlapSphere` when explosion radius > 0.

### Enemies and Movement
- **Enemy**:
  - HP scales based on rounds survived.
  - HP bar updates via UI fill.
  - Gives money on death and decrements enemies alive counter.
  - Supports slow effect (used by laser turret).
- **EnemyMovement**:
  - Moves along **Waypoints**.
  - Deals damage to the player when reaching the end point.

### Waves and Difficulty
- **WaveSpawner**:
  - Spawns waves continuously with time between waves.
  - Picks random wave configurations and random enemies from a wave list.
  - Spawns a **boss wave every 10 rounds**.
  - Tracks `enemiesAlive`.

- **Waypoints**:
  - Collects waypoint transforms from children into a static list.

### Player and UI
- **PlayerStats**:
  - Tracks money, lives, and rounds survived.
- **MoneyUI / LivesUI**:
  - Displays current money and lives.
- **GameManager / GameOver**:
  - Detects game over when lives reach zero.
  - Shows game over UI and displays rounds survived.

### Controls and UX
- **CameraController**:
  - RTS-style camera panning (WASD and screen edges), zoom using mouse wheel.
  - Optional rotation using Q / E.
  - Toggle camera movement using Escape.
- **PauseMenu**:
  - Toggle pause using P (time scale).
  - Retry and menu navigation.
- **SceneFader / LevelSelector / MainMenu**:
  - Scene transitions (fade in/out).
- **BackgroundMusic / PlayAudio**:
  - Audio playback support.

---

## How to Run

1. Clone the repository
2. Open in Unity Hub
3. Open the main scene
4. Press Play

---

Author
Dan Burciu
