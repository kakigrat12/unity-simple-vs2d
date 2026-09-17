# Source scope and provenance

## Included

- `Assets/Scripts/**/*.cs`: authored multiplayer gameplay, room flow, spawning, inventory, combat, environment, and presentation code.
- `ProjectSettings/ProjectVersion.txt`: original Unity editor version.
- `Packages/manifest.json`: original Unity package dependency declaration; inspected before publication and no credential-like value was found.

## Deliberately excluded

- `Assets/Photon/**`: Photon PUN SDK, demos, editor integrations, and `PhotonServerSettings`.
- `Assets/Joystick Pack/**`, `Assets/LeanTween/**`, `Assets/TextMesh Pro/**`, and other imported/vendor content.
- Scenes, prefabs, sprites, materials, sounds, fonts, shaders, and other content assets.
- All Photon application IDs, server settings, local configuration, and credentials.
- Unity-generated `.meta`, `.csproj`, `.sln`, `Library`, `Temp`, `Logs`, `obj`, and build output.
- Original Git metadata and history.

## Limitations

- This is a code-reading snapshot, not a standalone playable Unity checkout.
- The authored integration code references Photon PUN types, but the SDK and service configuration are intentionally omitted.
- Behaviour depends on serialized scene references and prefabs that are not part of this repository.
- No automated test suite is included.
- The snapshot is presented as a multiplayer systems prototype, not as a production networking framework.
