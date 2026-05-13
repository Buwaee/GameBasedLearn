# CROSSWORD: Learn and Play

A relaxing educational word puzzle game built in Unity, designed to help players expand their English vocabulary through interactive word search challenges.

---

## Team: Word Builders

### Team Members

| Name | Role | GitHub |
|---|---|---|
| Efe Ali Işık | Game Designer / UI Designer | @username |
| Ebrar Baha Özüpek | Lead Programmer / Level Designer | @Buwaee |
| Tolga Diğdioğlu | Quality Assurance / Tester | @username |

---

## Game Design Document (GDD)

### Game Name
**CROSSWORD: Learn and Play**

### Genre
2D Educational Puzzle (Word Search)

### Elevator Pitch
CROSSWORD: Learn and Play is a calm educational word puzzle game where players uncover advanced English vocabulary by finding hidden words within a 10x10 letter grid. Using only the meaning of each word as a clue, players actively recall and apply their vocabulary knowledge. The soft green aesthetic, simple drag-to-select controls, and stress-free atmosphere make the game ideal for focused learning and language growth.

### Game Introduction
The game presents players with a 10x10 grid filled with letters and a list of ten English word definitions on the side. Players must read each definition, recall the corresponding vocabulary word, and locate it within the grid by clicking and dragging across letters in any of eight directions (horizontal, vertical, or diagonal). When a word is found correctly, its letters and the matching meaning in the list are highlighted with a unique color, providing immediate visual feedback. The game ends when all ten words have been discovered.

### Core Loop
Read meaning → Recall vocabulary word → Search the grid → Drag-select letters → Receive feedback → Repeat until all words are found

### Controls
- **Left Mouse Button (Hold + Drag)** → Select a sequence of letters in a straight line
- **Release Mouse Button** → Submit the selection for word check

### Win Condition
The player completes the puzzle when all ten target vocabulary words have been correctly identified within the grid. A win screen appears and returns the player to the main menu.

### Art Style
Soft, watercolor-inspired illustration with a calming green color palette. Minimalist UI design with clear typography and gentle visual feedback.

---

## Educational Concept (Critical Section)

### Pedagogical Goal
The pedagogical goal of CROSSWORD: Learn and Play is to **expand the player's English vocabulary** and strengthen **reading comprehension, word recognition, spelling accuracy, and contextual understanding** of advanced English terms. Players are introduced to meaningful words that are useful in academic, professional, and everyday communication. Each puzzle session encourages players to learn definitions, identify word-meaning relationships, and remember spelling patterns through repeated interaction.

### Applied Learning Theory: Cognitivism
The game primarily applies the learning theory of **Cognitivism**. Cognitivism focuses on how people process, organize, store, and recall information through active mental engagement. In CROSSWORD: Learn and Play, players are not passively shown words and definitions; instead, they must **actively connect** definitions with vocabulary terms, **recognize patterns** between clues and answers, and **reinforce memory** through repeated puzzle-solving.

This aligns directly with Craik and Lockhart's **Levels of Processing** model, which suggests that information processed at a deeper semantic level (understanding meaning) is retained better than information processed only at a surface level. By requiring players to interpret definitions and retrieve the correct word, the game encourages deep semantic processing rather than rote memorization. The act of visually scanning the grid for the recalled word further strengthens word-form recognition, while the immediate feedback supports the formation of mental schemas linking definitions to spellings.

### How Game Mechanics Facilitate Learning
The game mechanics directly facilitate learning through interactive problem-solving:

- **Definition-first clue presentation:** Players see only the meaning, never the word itself. This forces active recall, the strongest predictor of long-term retention according to retrieval practice research.
- **Visual word search:** Locating the recalled word in the grid reinforces spelling and letter-pattern recognition.
- **Immediate visual feedback:** Each correctly found word is highlighted with a unique color, both in the grid and in the meaning list. This positive reinforcement strengthens the association between definition and word.
- **Multi-directional search:** Words appear in eight different directions, requiring flexible thinking and preventing pattern shortcuts that bypass vocabulary recall.
- **Stress-free pacing:** No time pressure or penalties for wrong attempts, allowing players to focus on understanding rather than reflexive guessing.

For example, when the player reads the clue *"persistence and determination,"* they must mentally retrieve the word **TENACITY**, then visually locate the eight-letter sequence within the grid. This dual process — semantic retrieval followed by visual confirmation — engages both the linguistic and visuospatial cognitive systems, leading to stronger encoding in long-term memory.

---

## Vocabulary Featured in the Game

| Word | Meaning |
|---|---|
| Tenacity | Persistence and determination |
| Aberrant | Deviating from the norm |
| Acuity | Sharpness of thought or perception |
| Zenith | Highest point |
| Ascertain | Find out with certainty |
| Discern | To recognize or perceive clearly |
| Nuance | Subtle distinction or variation |
| Scrutiny | Close examination |
| Obsolete | No longer useful or modern |
| Venerate | To deeply respect |

---

## "What I Did" — Individual Contributions

### Efe Ali Işık — Game Designer / UI Designer
- Designed the main menu and win screen UI layouts
- Sourced and generated background images and visual assets using AI tools
- Curated the vocabulary list (10 advanced English words with definitions)
- Designed the color palette and overall visual identity
- Wrote the README.md and Game Design Document

### Ebrar Baha Özüpek — Lead Programmer / Level Designer
- Implemented the puzzle data system using ScriptableObjects
- Wrote the word placement generator supporting 8-directional placement (horizontal, vertical, diagonal, both forward and reverse)
- Built the cell, grid, and clue panel system with Unity UI
- Scripted the mouse drag-and-select input system with line-snapping
- Implemented the cell highlighting and color-coding feedback system
- Built the win condition detection and scene management
- Configured Build Settings, Player Settings, and resolved Input System compatibility issues

### Tolga Diğdioğlu — Quality Assurance / Tester
- Tested word placement edge cases (long words such as ASCERTAIN and SCRUTINY)
- Verified UI scaling and layout consistency across different resolutions
- Tested win flow, restart functionality, and scene transitions
- Reported and helped resolve the Input System / Input Manager compatibility issue
- Tested vocabulary list integration and meaning display

---

## Technical Details

- **Engine:** Unity 6 LTS (2D Universal Render Pipeline)
- **Language:** C#
- **Target Resolution:** 1536 x 1024
- **Platform:** Windows Standalone
- **External Tools:** TextMeshPro, AI-generated visual assets

---

## How to Run

1. Clone the repository
2. Open the project folder in Unity Hub (Unity 6 LTS or compatible)
3. Open `Assets/Scenes/MainMenu.unity`
4. Press Play in the Unity Editor
5. Or build the project for Windows: `File > Build Profiles > Build`

---

## Notes

This project was developed for the Game Based Learning course as a 2-week team prototype. The focus was on demonstrating a complete game loop with educational pedagogy, polished UI, and version-controlled development.
