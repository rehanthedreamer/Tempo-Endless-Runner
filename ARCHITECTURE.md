# Tempo-Endless-Runner
Data driven Endless Running Game

Overview
A 2D endless runner built in Unity 6000.0.58f2, optimized for mobile. Gameplay is data‑driven, with multiple game modes, a store, settings, and a clean UI flow.

Key features:
  *   Configurable gameplay via ScriptableObject.
  *   Main menu → Game Mode Select → Gameplay → Game Over → Store → Settings.
  *   Player input (keyboard, onscreen joystick (New input system) ).
  *   UI HUD for current run distance and coin.
  *   UI BUD for screen navigation.
  *   Save system for player best distance covered, coin earned and power ups.
  *   Object pooling for performance (Platform, obstacle, coin).

**This project follows the basic SOLID principles to keep the code clean, maintainable, and easy to extend.

1. Single Responsibility Principle (SRP)
	Each class has one clear job:
		PlayerController → handles player input and movement.
		GameManager → manages game mode and config.
		SaveService → loads and saves progress.
		ScreenManager → handles UI page navigation and updates.
	This makes code easier to test, debug, and modify.

2. Open/Closed Principle (OCP)
	Core systems are open for extension but closed for modification:
		New game modes are added by creating new GameModeConfig assets, not by changing GameModeManager.
	This lets you add features without breaking existing code.

3. Liskov Substitution Principle (LSP)
	Where inheritance is used (e.g., PoolableObject), subclasses can be used in place of the base class without breaking behavior.
	For example, any object that inherits PoolableObject can be safely spawned/returned by the pool manager.

5. Dependency Inversion Principle (DIP)
	UI updates are driven by events (e.g., OnScoreChanged), not by directly calling player code.

Core Systems

Game Mode System
 * GameModeConfig (ScriptableObject) holds settings for each mode (Easy, Hard).
 * GameManager applies the config data to game dificulty, obstacle spawn rates, platform spawn difficulty and coins.
 * Main menu lets the player choose a mode; gameplay changes based on the selected config.

Player & Input

*  	PlayerController handles jump, double jump using Rigidbody2D.
* 	On screen joystick and binded to keyboard controle as well
*  	Keyboard input  Space > Jump and double jump;
                  Tab > Active shield power up
*  Input uses Unity’s new Input System with actions: Jump, Shield.
*  Supports keyboard, mouse, and touch.

Save & Progress
 * SaveService uses PlayerPrefs to persist in JSON format:
 * Best distance and total coins.
 * Unlocked modes and purchased power‑ups.
 * Game Over screen shows current run stats and best stats from all runs.
	

Object Pooling

	* PlatformManager, ObstacleSpawner, CoinSpawner use Queue<PoolableObject> to reuse objects.
	* Objects are spawned from the pool and returned when off‑screen and game over.
	* Reduces Instantiate/Destroy calls for better mobile performance.

UI System

	* Screens: Menu, Game Mode Select, Gameplay, Game Over, Store, Settings, HUD, BUD.
	* MenuBUD handles navigation between pages.
	* HUD shows current distance and coins; Game Over shows current and best distance.

Store System

	* StoreDataConfig (ScriptableObject) defines store items.
	* StoreScreen holds the item list and handles purchases with coin.
	* Power‑ups (Shield, Double Jump, Coin multiplier) are applied in gameplay.

Settings System

	* Settings page toggles: Music, SFX.
	* Button to reset all progress (calls SaveService.ResetAll()).
	* Settings are saved in SaveService.

Data‑Driven Design

	* All gameplay is driven by ScriptableObject:
	* GameModeConfig → Game speed difficulty, platform difficulty , obstacle spawn rate.
	* StoreDataConfig → store items, prices, icons.
	* PlatformData / ObstacleData / CoinData which prefabs to spawn with required parameters
	* Easy to tune balance or add new modes/items without changing code.

Design Patterns Used

1. Singleton Pattern
	* Used for core managers:
	GameManager
	ParallaxManager
	PlatformManager
	ScreenManager
	SoundManager
	StoreManager
	SaveService (static)
	ObstacleSpawner
	CoinSpawner
	Ensures one instance exists and is easily accessible from anywhere.

2. ScriptableObject-Driven Configuration
	* All gameplay data (modes, store, platforms, obstacles, coins, gameplay mode config) is defined in ScriptableObject assets.
	* Keeps data separate from code, making it easy to:
	* Tune values in the Editor.
	* Add new modes or items without touching scripts.

3. Object Pooling=
	* Platforms, obstacles, and collectibles are reused via Queue<PoolableObject>.
	* Avoids expensive Instantiate/Destroy calls during gameplay, improving mobile performance.

4. Event-Based Communication
	* Systems communicate via simple events (e.g., OnPlayerDeath, OnGameModeChanged, OnCoinAdd, OnStateChanged).
	* Reduces tight coupling between managers (e.g., doesn’t directly have to call ; it raises an event).

5. State Machine (Simple)
	* Game has a simple state machine:
	* inMenu, inGame, GameOver;
	* Player State
	* Running, jumping and dead
	* Controls and physics behavior change based on state.

6. Manager Pattern
	* Dedicated managers for:
	* GameManager → controls game mode and config.
	* ScreenManager → handles page navigation and UI updates.
	* SaveService → handles loading/saving progress.
	* Keeps related logic grouped and easy to extend.

7. Data-Driven UI
	* UI elements (labels, icons, colors) are configured in GameModeConfig and ItemData.
	* StoreManager reads these assets to update buttons, descriptions, and visuals.

 

How to Extend

	* Add a New Game Mode
	* Create a new GameModeConfig asset with custom values.
	* Add a New Power‑up
	* Create a new ItemData asset for the power‑up.
	* In StoreManager, add logic to apply the effect (e.g., PlayerController.Instance.ActivatePowerUp()).

Add a New UI Page

	* Create a new Canvas with the page (e.g., “Leaderboard”).
	* Add a method and button in MdnuBUD (e.g., ShowLeaderboard()).
	
Use dynamic object pooling for particles and effects.

