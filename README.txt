Unity Card Game Project

Overview

This project implements a modular, extensible card game system in Unity.
It is structured around separation of concerns, dependency injection (IoC), and scalable UI components. The architecture is designed to make it easy to add new deck types, rules, or UI layouts without rewriting core logic.



Project Structure

Assets/
 ├── Scripts/
 │   ├── Game/            # Core game logic and managers
 │   ├── Decks/           # Implementations of specific deck types
 │   ├── IOC/             # Simple Inversion of Control (DI container)
 │   └── ...              # Supporting files (UI, Data, etc.)
 ├── Prefabs/             # Reusable UI and card prefabs
 ├── Scenes/              # Unity scenes
 ├── Images/              # Card and UI images



Architectural Choices

1. Dependency Injection (IOC)
A lightweight DI container (DIContainer, SceneDIContainer, DIBehaviour) was introduced.
This decouples game systems (e.g., GameManager, DeckService, DataManager) from their concrete implementations.

Trade-off: Slightly more upfront complexity, but allows flexible swapping of implementations (e.g., test doubles, different deck rules).

2. Data & Game Layer Separation
DataManager handles persistence and abstract data access.
DeckService manages deck creation, shuffling, and drawing cards.
Abstract deck contracts (IDeck, AbstractDeck) define shared behavior.

Trade-off: Some duplication across deck implementations (Standard52CardDeck, PinochleDeck), but this makes extending new deck types straightforward.

3. UI Abstraction
IHandUI interface with multiple implementations (GridHandUI, FanHandUI, RowUI, SpotUI).
CardUI represents visual cards, while ScreenUI coordinates UI screens.

Trade-off: More components to manage, but enables flexible layout switching without modifying game logic.

4. Game Flow Management
Bootstrapper initializes core systems.
GameManager orchestrates high-level state transitions.
CardDealer automates dealing cards between players and UI layers.

Trade-off: Requires careful orchestration in Bootstrapper, but keeps game loop logic clear and testable.



Extensibility

- New Deck Types: Inherit from AbstractDeck or implement IDeck.
- New UIs: Implement IHandUI and plug into the UI system.
- Rules & Variants: Extend DeckService or wrap game state changes inside GameManager.




Notes

- Scalability: The architecture supports multiple card games sharing the same base system.
- Testing: The IoC setup simplifies mocking dependencies for unit tests.
- Performance: Minimal overhead since Unity’s built-in update loop is leveraged, and DI is lightweight.




Next Steps (Potential Improvements)

- Add async/await handling for animations or networked play.
- Introduce unit tests for DeckService and GameManager.
- Expand DataManager to support persistence beyond runtime (e.g., JSON, PlayerPrefs).
- Optimize card UI rendering for large-scale games.


