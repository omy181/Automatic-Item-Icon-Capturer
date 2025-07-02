# Automatic-Item-Icon-Capturer

An automatic sprite generator for 3D inventory items.

This system was developed for a personal 3D game project featuring numerous interactable items and a UI-based inventory. In this setup, when a player picks up an item, a 2D icon must represent it in the UI.

Creating a unique hand-drawn sprite for every item was time-consuming and required artistic skill. My initial workaround was to take manual screenshots of 3D models, resize them, and use them as icons — but the process was tedious.

So I built a better solution: **automation**.

![image](https://github.com/user-attachments/assets/c8b3e8ce-ff7c-4513-a5c6-18686a2958cd)

---

## How to Use

Each item is a `ScriptableObject` of type `Item`.
A new **"Render Icons"** button is added to Unity's top menu.

When clicked, it automatically captures and saves a sprite icon for each `Item` in the `GameIcons` folder.

![image](https://github.com/user-attachments/assets/57753c88-3fad-4179-ac91-8d46d20a2d47)

Each `Item` object includes customizable capture settings:

* **Offset**
* **Rotation**
* **Scale**

These can be adjusted and recaptured as needed.

![image](https://github.com/user-attachments/assets/608c63cb-8c78-429f-a93b-568b208fdb1f)

---

## How It Works

When capture starts, a dedicated **capture scene** is loaded.
This scene contains predefined lighting and camera setup.

All `Item` GameObjects are spawned with their configured transform values. Each object is rendered, and its icon is saved automatically to the `GameIcons` folder.

![image](https://github.com/user-attachments/assets/2f19c46b-6b36-4380-92dc-79b7b6179547)

---
