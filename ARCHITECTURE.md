# Tempo-Endless-Runner
Data driven Endless Running Game

Overview
A 2D endless runner built in Unity 6000.0.58f2, optimized for mobile. Gameplay is data‑driven, with multiple game modes, a store, settings, and a clean UI flow.

Key features:

*Configurable gameplay via ScriptableObject.

*Main menu → Game Mode Select → Gameplay → Game Over → Store → Settings.

*Player input (keyboard, onscreen joystick (New input system) ).

*UI HUD for current run distance and coin.

*UI BUD for screen navigation.

*Save system for player best distance covered, coin earned and power ups.

*Object pooling for performance (Platform, obstacle, coin).

Core Systems
Game Mode System

GameModeConfig (ScriptableObject) holds settings for each mode (Easy, Normal, Hard).

GameModeManager applies the config to player speed, spawn rates, and UI.

Main menu lets the player choose a mode; gameplay changes based on the selected config.

Player & Input

PlayerController handles movement, jump, dash, and double jump using Rigidbody2D.

Input uses Unity’s new Input System with actions: Move, Jump, Dash.

Supports keyboard, mouse, and touch (tap to jump, swipe to move).

Save & Progress

SaveService uses PlayerPrefs to persist:

Best distance, best score, total coins.

Unlocked modes and purchased power‑ups.

Game Over screen shows current run stats and best stats from all runs.

Object Pooling

PlatformManager, ObstacleSpawner, CoinSpawner use Queue<PoolableObject> to reuse objects.

Objects are spawned from the pool and returned when off‑screen.

Reduces Instantiate/Destroy calls for better mobile performance.

UI System

Pages: Main Menu, Game Mode Select, Gameplay, Game Over, Store, Settings.

UIManager handles navigation between pages.

HUD shows current distance, score, and coins; Game Over shows current and best stats.

Store System

ItemData (ScriptableObject) defines store items (coins, lives, power‑ups).

ShopManager holds the item list, player currency, and handles purchases.

Power‑ups (Shield, Double Jump, Dash, Magnet) are applied in gameplay.

Settings System

Settings page toggles: Music, SFX, Vibration.

Button to reset all progress (calls SaveService.ResetAll()).

Settings are saved in PlayerPrefs.

Data‑Driven Design
All gameplay is driven by ScriptableObject:

GameModeConfig → player speed, spawn rates, difficulty.

ItemData → store items, prices, icons.

PlatformData / ObstacleData → which prefabs to spawn.

Easy to tune balance or add new modes/items without changing code.

Design Patterns Used
1. Singleton Pattern

Used for core managers:

PlayerController.Instance

GameModeManager.Instance

SaveService (static)

UIManager.Instance

Ensures one instance exists and is easily accessible from anywhere.

2. ScriptableObject-Driven Configuration

All gameplay data (modes, items, platforms, obstacles) is defined in ScriptableObject assets.

Keeps data separate from code, making it easy to:

Tune values in the Editor.

Add new modes or items without touching scripts.

Support localization and modding later.

3. Object Pooling

Platforms, obstacles, and collectibles are reused via Queue<PoolableObject>.

Avoids expensive Instantiate/Destroy calls during gameplay, improving mobile performance.

4. Event-Based Communication

Systems communicate via simple events (e.g., OnPlayerDeath, OnGameModeChanged).

Reduces tight coupling between managers (e.g., Player doesn’t directly call UI; it raises an event).

5. State Machine (Simple)

Player has a simple state machine:

Idle, Running, Jumping, Dashing, Dead.

Controls and physics behavior change based on state.

6. Manager Pattern

Dedicated managers for:

GameModeManager → controls game mode and config.

UIManager → handles page navigation and UI updates.

SaveService → handles loading/saving progress.

Keeps related logic grouped and easy to extend.

7. Data-Driven UI

UI elements (labels, icons, colors) are configured in GameModeConfig and ItemData.

UIManager reads these assets to update buttons, descriptions, and visuals.

How to Extend
Add a New Game Mode

Add a new value to GameMode enum.

Create a new GameModeConfig asset with custom values.

Add a button in the main menu that calls GameModeManager.Instance.SetGameMode(newMode).

Add a New Power‑up

Add a new ItemType (e.g., SpeedBoost, Shield).

Create a new ItemData asset for the power‑up.

In ShopManager.GrantItem, add logic to apply the effect (e.g., PlayerController.Instance.ActivatePowerUp()).

Add a New UI Page

Create a new Canvas with the page (e.g., “Leaderboard”).

Add a method in UIManager (e.g., ShowLeaderboard()).

Add a button in the main menu or pause menu to open it.

Add Mobile Optimizations

Add device‑specific settings (e.g., lower quality on low‑end devices).

Use object pooling for particles and effects.

Use Addressables later to reduce APK size.