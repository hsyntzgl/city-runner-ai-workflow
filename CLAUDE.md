# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project overview

This is a Unity project (Editor version **6000.5.2f1**, i.e. Unity 6) using the **Universal Render Pipeline (URP)**. It currently contains only the stock URP 3D template scaffolding — `Assets/Scenes/SampleScene.unity`, the default `TutorialInfo`/`Readme` assets, and URP quality settings under `Assets/Settings` (`PC_RPAsset`, `Mobile_RPAsset` and their renderer assets). There is no custom gameplay code yet; `Assembly-CSharp.csproj` has no scripts under `Assets` besides the template's `Readme.cs` / `ReadmeEditor.cs`.

This directory is not a git repository yet — if starting real work, consider `git init` and adding a Unity-appropriate `.gitignore` (the project currently has no `.gitignore`, so `Library/`, `Temp/`, `Logs/`, `UserSettings/`, and `obj/` are untracked only by virtue of not being committed).

## Working with this project

There is no CLI build/test pipeline configured — this project is developed through the Unity Editor. Typical workflows:

- Open the project in Unity Editor **6000.5.2f1** (via Unity Hub) to edit scenes, prefabs, and assets.
- C# scripts compile via the Editor (or `dotnet build "My project.slnx"` for a syntax/reference check outside the Editor — this does not run Unity-specific codegen like the Burst compiler or asset pipeline).
- The `com.unity.test-framework` package is installed but no test assemblies exist yet. When adding tests, create an Editor or Runtime `asmdef` referencing `UnityEngine.TestRunner`/`UnityEditor.TestRunner` and run them via Unity's Test Runner window (`Window > General > Test Runner`), since there is no headless CLI entry point set up in this repo.

### Unity MCP

A `unity-mcp` tool integration is available in this environment for driving the live Unity Editor: reading console logs (`Unity_GetConsoleLogs`), compiling/executing C# directly in the Editor (`Unity_RunCommand`), capturing the Scene/Game view (`Unity_Camera_Capture`, `Unity_SceneView_Capture2DScene`, multi-angle capture), and generating assets. `Unity_RunCommand` requires the exact `internal class CommandScript : IRunCommand` shape described in its tool schema — deviating from that (wrong class name/accessibility, top-level statements) fails compilation.

## Architecture notes

- **Render pipeline**: URP, with separate quality tiers for PC and Mobile (`Assets/Settings/PC_RPAsset.asset` + `PC_Renderer.asset`, `Mobile_RPAsset.asset` + `Mobile_Renderer.asset`), plus a shared `DefaultVolumeProfile` and `SampleSceneProfile` for post-processing/volume settings.
- **Key packages** (see `Packages/manifest.json`): `com.unity.render-pipelines.universal` (17.5.0), `com.unity.inputsystem` (1.19.0, new Input System), `com.unity.visualscripting` (1.9.11), `com.unity.timeline`, `com.unity.ai.navigation` (NavMesh), `com.unity.ai.assistant` (Unity's built-in Muse/AI Assistant package — unrelated to Claude), `com.unity.test-framework`, `com.unity.multiplayer.center`.
- As gameplay code is added, prefer organizing under `Assets/Scripts/` (Runtime) with a separate `Assets/Scripts/Editor/` for editor-only tooling, following the existing `TutorialInfo/Scripts/Editor` split — Editor-only scripts must live in a folder named `Editor` (or an `asmdef` restricted to the Editor platform) so they're excluded from player builds.
