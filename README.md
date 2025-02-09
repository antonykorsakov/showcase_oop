# Antony Korsakov - Unity Developer  

🎯 **CV: [Unity Developer AKorsakov](https://github.com/antonykorsakov/showcase/blob/main/Documentation/CV%20Unity%20Developer%20AKorsakov.pdf)**  
🔗 **LinkedIn:** [Antony Korsakov](https://www.linkedin.com/in/antonykorsakov/)  
📽 **Performance:** [Unity Meetup: Animator and Playables API](https://www.youtube.com/watch?v=824FOYwCV1k)  

🌐 **Live Demo:** [WebGL Build](https://antonykorsakov.github.io/showcase/)  
💻 **Source Code:** [GitHub Repository](https://github.com/antonykorsakov/showcase/tree/main/ProjectTetris2D)  

# Table of Contents
- [Introduction](#introduction)
- [Code style](#code-style)
- [Project description](#project-description)
  - [Architecture](#architecture)
  - [Gameplay Features](#gameplay-features)
    - [Tetris Logic & Visualization](#tetris-logic--visualization)
    - [JSON Saving & Loading (for Mobile & WebGl)](#json-saving--loading-for-mobile--webgl)
    - [Firebase Analytics (for Mobile & WebGl)](#firebase-analytics-for-mobile--webgl)
	- [Tetromino Randomization](#tetromino-randomization)
  - [UI Features](#ui-features)
    - [Splash Screen](#splash-screen)
	- [Universal Aspect Ratio Support](#universal-aspect-ratio-support)
	- [UI Particle System](#ui-particle-system)
	- [Texture Scrolling (Custom Shader)](#texture-scrolling-custom-shader)
	- [Localization](#localization)
  - [Unity Editor Tools & Extensions](#unity-editor-tools--extensions)
    - [Editor Tools](#editor-tools)
	- [Unit Tests](#unit-tests)
	- [Zenject Extension](#zenject-extension)
	- [Unity Localization Extension](#unity-localization-extension)
  - [Optimizations](#optimizations)
  - [Project terminology](#project-terminology)

# Introduction
Showcase is a project demonstrating my hard skills in Unity. UML diagrams, clean code, efficient solutions and attention to details.

<div style="display: flex; align-items: center;">
    <img src="Documentation/Images/splashscreen.png?raw=true" alt="SplashScreen" width="400" height="715"/>
    <img src="Documentation/Images/gameplay.png?raw=true" alt="Gameplay" width="400" height="715"/>
</div>

# Code style

## Class member ordering (StyleCop)
- Constants and static fields
- Serialized fields
- Non-serialized fields
- Constructor
- Properties
- Events (e.g., `event Action`)
- Unity methods (e.g., `Awake`, `Start`, `Update`, `OnDestroy`, etc.)
- Custom methods

## Class member modifier ordering (StyleCop)
- Public
- Internal
- Protected
- Private

## Extra project rules
<details>
	<summary>Extra code style rules</summary>

- General Convention
  - **_camelCase** should be used for private fields.
  - **snake_case** should be used for `JsonProperty` attribute
    ```csharp
    [JsonProperty("lines_count")] private int _linesCount;
    ```
  - **PascalCase** should be used for all other identifiers.
    - and tests should follow the Arrange-Act-Assert (AAA) pattern
	  ```csharp
	  [Test]
	  public void TurnOnAccelerate_ShouldChangeDelayToFastFallDelay()
	  {
        // Arrange
        // Act
        // Assert
      }
	  ```
- Event Naming Convention
  - Use verbs in the present continuous tense (`-ing`) for events that represent an ongoing action that has just started, is in progress.
  - Use verbs in the past tense (`-ed`) for events that represent a completed action or change.
  - The suffix "Event" should be capitalized.
- Class Convention
  - Each class must be in its own file. Avoid defining classes inside other classes.
  - All classes must use the `sealed` keyword whenever the syntax permits it. The `sealed` modifier can only be omitted if inheritance is explicitly required to implement or modify the behavior of a feature.
</details>

[jump to Table of Contents](#table-of-contents)

# Project description

## Architecture
<img alt="Features" align="right" src="Documentation/Images/features.png?raw=true"/>

The project follows a `Feature-Based Architecture`, incorporates `DDD (Domain-Driven Design)`, and applies `Clean Architecture`, enabling the separation of code into independent, scalable modules.
All custom features are registered as singletons in the `DI container`. This implementation essentially follows the `Service Locator pattern`, which is achieved using the [Zenject plugin](https://github.com/Mathijs-Bakker/Extenject).

Each core feature is isolated and does not have direct knowledge of or interaction with other features. Features communicate only through behavior scripts located in the `Behavior folders`.

The entire implementation is explicit and does not rely on an `Event Bus` (this architecture does not use the `SignalBus` from `Zenject plugin`).

Folders structure for unity project features:
```plaintext
<FeatureName>/
  ├── Behavior/
  ├── Binding/
  ├── Content/
  ├── Core/
  │   └── Features.<FeatureName>.Core.asmdef
  ├── Editor/
  │   └── Features.<FeatureName>.Editor.asmdef
  ├── Settings/
  ├── Shaders/
  ├── Plugins/
  ├── Tests/
  │   ├── EditMode/
  │   │   └── Features.<FeatureName>.Tests.EditMode.asmdef
  │   └── PlayMode/
  │       └── Features.<FeatureName>.Tests.PlayMode.asmdef
```

| Folder | Description |
|:---|---|
| Behavior | Contains scripts and components responsible for the feature's behavior and interaction with other features. |
| Binding  | Holds files for registering the feature's dependencies in a DI container (e.g., Zenject) or ServiceLocator. |
| Content  | e.g., `Animation`, `Material`, `Texture`, etc. |
| Core     | Includes core scripts and foundational code for the feature, along with its assembly definition file. |
| Editor   | Contains scripts and tools designed to extend or customize the Unity Editor for the feature, such as property drawers, custom inspectors, and editor-specific utilities. |
| Settings | Configuration of feature, e.g. `ScriptableObject` |
| Shaders  | Contains custom shaders and shader-related utilities used by the feature to handle rendering and visual effects. |
| Plugins  | Holds additional scripts and libraries, such as JavaScript libraries (e.g., `.jslib`), external plugins, and third-party tools integrated into the project. |
| Tests    | Contains unit tests for the feature, organized into EditMode and PlayMode subfolders with assembly definition files. |

Key Advantages
- Easy extension: Features can be added or modified with minimal effort.
- Clean structure: Clear organization reduces code dependencies.
- Testability: Independent features are easy to unit test without affecting the rest of the project.
- Bug isolation: Issues can be quickly identified and resolved due to the modular structure.

[jump to Table of Contents](#table-of-contents)

## Gameplay Features
The project implements core Tetris gameplay mechanics with a focus on modularity and scalability.

### Tetris Logic & Visualization
The base Tetris logic and its visualization are separated into two independent modules. This separation improves ease of maintenance.

The **TetrisModule** handles all core gameplay logic, such as piece movement, rotation, line clearing, and game state.
<img src="Documentation/TetrisModule/UML.png?raw=true" alt="TetrisModule UML"/>

The **TetrisRendererModule** is fully decoupled and responsible for rendering the gameplay.
<img src="Documentation/TetrisRendererModule/UML.png?raw=true" alt="TetrisRendererModule UML"/>

### JSON Saving & Loading (for Mobile & WebGl)
Progress saving uses the [Newtonsoft JSON](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@latest/) for serialization across all platforms. The saving method differs based on the platform:
- JsonDeviceController (Desktop & Mobile): Progress is saved to local files using the System.IO library.
- JsonWebController (WebGL): Progress is stored in the browser's localStorage, implemented via JavaScript.
<img src="Documentation/JsonModule/UML.png?raw=true" alt="JsonModule UML"/>

### Firebase Analytics (for Mobile & WebGl)
For analytics, the popular solution `Firebase Analytics` is used. It is not just integrated into the project, but added via the `Unity Package Manager`. This approach ensures that the project's core (`Assets/` folder) remains clean and avoids cluttering with third-party packages.

Tarball archives can be downloaded via the following [links](https://developers.google.com/unity/archive#firebase).

- AnalyticsDeviceController (Desktop & Mobile)
- AnalyticsWebController (WebGL)
<img src="Documentation/AnalyticsModule/UML.png?raw=true" alt="AnalyticsModule UML"/>

### Tetromino Randomization
To avoid frequent repetitions of the same pieces, the randomization system uses a [shuffle algorithm](https://learn.microsoft.com/en-us/dotnet/api/system.random.shuffle?view=net-9.0). This ensures providing a fair and balanced experience for players. Implementation details can be found in `RandomShuffle.cs`.

[jump to Table of Contents](#table-of-contents)

## UI Features

### Splash Screen
The game includes two splash screens:
- Default Unity SplashScreen
- Custom SplashScreen, implemented as a UI-overlay.
This implementation allows asynchronous loading of all necessary resources and a smooth transition into the game with a fade-animation.

### Universal Aspect Ratio Support
- Support for screens with various aspect ratios, from narrow 21:9 (e.g., `Sony Xperia 1 V`) to widescreen monitors.
- SplashScreen is also adapted for all aspect ratios.
- 4K resolution support, focusing on modern devices and scaling down textures for older devices (instead of scaling up).

### UI Particle System
The <a href="https://github.com/mob-sakai/ParticleEffectForUGUI.git">ui particle plugin</a> is used to integrate the Particle System into the UI, enabling efficient use of particle effects in the interface.

<img src="Documentation/Images/particlesystem.png?raw=true" alt="Particle System"/>

### Texture Scrolling (Custom Shader)
A custom shader is implemented for the pause menu, which smoothly shifts the background along UV coordinates, using a mask for enhanced visual effects.

<img src="Documentation/Images/shader-scrolling.gif?raw=true" alt="Shader Scrolling"/>

### Localization
All in-game text is managed through a localization module, simplifying the addition of new languages. Currently, three languages are available for demonstration: English, Italian and Polish.

[jump to Table of Contents](#table-of-contents)

## Unity Editor Tools & Extensions

### Editor Tools
Tool for adding a keystore for the Android platform. It automatically runs when the editor is launched in Android platform mode, but can also be triggered manually.

<img src="Documentation/Images/editor_keystore.png?raw=true" alt="Setup Keystore"/>

A tool for checking the current configurations in the project. It is accessible from the top menu, allowing easy access for project validation.

<img src="Documentation/Images/editor_validator.png?raw=true" alt="Config Validator"/>

### Unit Tests
Unit testing is integrated into the project to ensure stability and correctness. ([Test Framework](https://docs.unity3d.com/Packages/com.unity.test-framework@latest/), [Moq](https://docs.unity3d.com/Packages/nuget.moq@latest/))

<img src="Documentation/Images/editor_tests.png?raw=true" alt="Unit Tests"/>

### Zenject Extension
A custom script for extending the `Zenject plugin`. It removes empty references from `ProjectContext`, helps to inject install scripts, and sorts everything alphabetically to make it easier to find modules and pass settings.

<div style="display: flex; align-items: center;">
	<img src="Documentation/Images/editor_zenject1.png?raw=true" alt="Zenject Extension 1"/>
	<img src="Documentation/Images/editor_zenject2.png?raw=true" alt="Zenject Extension 2"/>
</div>

### Unity Localization Extension
A script added to Unity `GameObjects` that, with a simple button click, automatically updates the object to ensure that text is refreshed when the localization is changed.

[jump to Table of Contents](#table-of-contents)

## Optimizations
- No `Instantiate`, only `InstantiateAsync`
- No `UnityEngine.UI.Text`, only `TextMeshPro`
- URP
- One image atlas
- Tile images (`UnityEngine.Tilemaps`)
- `System.Text.StringBuilder`

## Additional features
- [new InputSystem](https://docs.unity3d.com/Packages/com.unity.inputsystem@latest/)

## Project terminology
- **Tetromino**: A geometric shape composed of four squares, connected orthogonally. More information can be found [here](https://en.wikipedia.org/wiki/Tetromino).

| Abbreviation | Full name |
|---|---|
| Beh     | Behavior |
| Btn     | Button |
| MS      | Milliseconds |
| TComp   | TextComponent (e.g., `private TMP_Text _scoreTComp`) |
| Tetro   | Tetromino |
| UEditor | UnityEditor.Editor |
| UObject | UnityEngine.Object |

[jump to Table of Contents](#table-of-contents)