# SimpleVS2D — Multiplayer C# Portfolio Snapshot

SimpleVS2D is a networked 2D action prototype with rooms, teams, synchronized player spawning, master-client loot population, weapons, health, inventory, and match presentation built on Photon PUN.

This repository is intentionally code-first. It contains authored C# and minimal Unity/package version metadata, but not scenes, prefabs, artwork, audio, Photon SDK sources, server settings, application IDs, or build artifacts.

## Engineering highlights

- Lobby and room lifecycle with custom room properties and scene synchronization.
- Master-client team allocation followed by buffered synchronization and client-side spawning.
- Master-owned procedural loot population using Photon room objects and instantiation data.
- Multiplayer combat flow across weapons, hit boxes, health, death events, kill messages, and team-aware UI.
- Inventory abstractions for weapons, ammunition, consumables, hidden objects, and presentation.
- Focused ownership, camera, audio, animation, and lifecycle components around network entities.

## Code map

```text
Assets/Scripts/
├── NetworkLoading/  Lobby, rooms, teams, player spawn, loot spawn, and scene flow
├── Inventory/       Item contracts, data, cells, panels, weapons, and ammunition
├── ThingsSkripts/   World-item pickup and discard behaviour
├── UnusialScripts/  Match bootstrap, team spawn metadata, and gameplay utilities
└── *.cs              Movement, combat, health, camera, UI, and environment systems
```

## Representative network flow

1. [`Lobby`](Assets/Scripts/NetworkLoading/Lobby.cs) connects to Photon and exposes room discovery/creation.
2. [`RoomSettings`](Assets/Scripts/NetworkLoading/RoomSettings.cs) describes match constraints through room properties.
3. The master client uses [`SpawnPlayer`](Assets/Scripts/NetworkLoading/SpawnPlayer.cs) to allocate teams and distribute spawn state.
4. [`SpawnLoot`](Assets/Scripts/NetworkLoading/SpawnLoot.cs) creates room-owned items for all clients.
5. [`WeaponFire`](Assets/Scripts/WeaponFire.cs), [`Bullet`](Assets/Scripts/Bullet.cs), and [`Health`](Assets/Scripts/Health.cs) form the combat event path.

## Review notes

The source is preserved as a living multiplayer prototype: it contains exploratory code and original naming while demonstrating the breadth of systems required to move from lobby to a playable network match. See [SOURCE_SCOPE.md](SOURCE_SCOPE.md) for the exact publication boundary.

## Unity version

Created with Unity `2020.3.21f1`. The code can be reviewed directly on GitHub. Reconstructing a playable build requires the omitted scenes, prefabs, Photon PUN SDK, and a separately configured Photon application.
