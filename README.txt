
For this project I decided to implement a simple MVC design. ( DataManager, ScreenUI, GameManager)

I choose MVC over MVVM, for its quicker implementation in this project. It just seems like a good fit.

The deck and other data is accessible through the DataManager.
All UI implementation is accessible through the ScreenUI
The GameManager controls the game logic for dealing the cards.

I also implemented hints of Observer for listing for events like when to Deal or Reset.

I approached the structure using an Object Oriented approach. This allowed me to quickly prototype up the structures and behavior I needed.

I would have liked to implement Dependency Injection, but thought that it was over kill for this project. 

I did use interfaces for the HandUI types, allowing me to treat the different Hand Layouts the same, and allowing for implementations of different layouts in the future.


I'm not the biggest fan of the nested coroutines, but you can't get the transform refs off the main thread using await Tasks and delegating out the movement to a Move component seems less appealing.


I am sure that there is much room for improvement, but I wanted to keep it to around 4 hours.

 