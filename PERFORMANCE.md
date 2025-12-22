# Tempo-Endless-Runner

Keep the game smooth (60 FPS) on mobile devices by reducing CPU, memory, and GPU load.

## Key Optimizations

- **Object pooling**: Reuse platforms, obstacles, and coins instead of `Instantiate`/`Destroy` to avoid GC spikes.  
- **Simple physics**: Use `Rigidbody2D` with basic forces, velocity and simple colliders for stable movement.  
- **Lightweight UI**: Use `TextMeshPro`, update HUD only when values change, and keep layout simple.  
- **Mobile controls**: Simple on screen joystick/ keyboard input;
- **Data‑driven design**: Use `ScriptableObject` for game modes and items to keep code clean.
## Build Size
<img width="483" height="321" alt="Screenshot 2025-12-22 at 6 35 41 PM" src="https://github.com/user-attachments/assets/586dabc1-aa05-4b82-9da0-6c1a9a53bd28" />

## Memory & Assets

- Use compressed textures and small sprite atlases.  
- Object prefabs to spawned objects to keep memory stable during gameplay.

## Testing

- Tested in Unity Profiler.  
- Target: 55–60 FPS, low GC alloc, stable memory.

## Trade‑offs

- Slightly higher memory (from pooling) for much smoother performance.  
- Simple visuals and controls to ensure good performance on low‑end devices.
