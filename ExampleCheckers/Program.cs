using Core;
using ExampleGame;
// See https://aka.ms/new-console-template for more information


var logger = new Logger();
var game = new LifecycleManager(logger);
var componentRegistry = new Dictionary<string, Type> {
    {nameof(Position),typeof(Position)},
};
var playerObjectDefinition = new LifecycleObjectDefinition {
    Name= "Piece",
    Components= new string[]{nameof(Position)}
};
var objectBuilder = new LifecycleObjectBuilder(logger, componentRegistry);

game.AddObject(objectBuilder.Build(playerObjectDefinition));
game.Init();
game.Start();

var currentPlayer = "Player 1";
Dictionary<string, Func<string, EventBase>> inputHandlers = new Dictionary<string, Func<string, EventBase>>{
    ["move"] = DoMove,
    ["kill"] = DoKill
};

while(true) {
    game.Tick();
    Console.Write($"{currentPlayer} > ");
    var _eventText = Console.ReadLine();
    if(_eventText == "exit") break;
    var inputAccepted = HandleInput(_eventText);
    if(!inputAccepted) {
        Console.WriteLine("Invalid input");
    }
}



bool HandleInput(string input) {
    var command_args = input.Split(' ').ToArray();
    var command = command_args[0].ToLower();
    var args = command_args[1];
    try {
        EventBase _event = inputHandlers[command](args);

        game.Submit(_event);
        switch(currentPlayer) {
            case "Player 1":
                currentPlayer = "Player 2";
                break;
            case "Player 2":
                currentPlayer = "Player 1";
                break;
        }
        return true;
    } catch( Exception e) {
        return false;
    }

}

EventBase DoMove(string args) {
    var positions = args.Split(';').Select(x => GetCoordinates(x)).ToArray();

    return new PlayerInputEvent {
        TargetTags= new []{currentPlayer},
        TargetPiece= positions[0],
        TargetMove= positions[1]
    };
}

EventBase DoKill(string args) {
    var position = GetCoordinates(args);
    return new KillEvent {
        TargetTags = {},
        TargetPiece = position
    };
}


(int X, int Y) GetCoordinates(string input) {
    var parsed = input.Split(',');
    return (int.Parse(parsed[0]), int.Parse(parsed[1]));
}


record PlayerInputEvent : EventBase {
    public (int X, int Y) TargetPiece {get; set;}
    public (int X, int Y) TargetMove {get; set;}
}

record KillEvent: EventBase {
    public (int X, int Y) TargetPiece {get; set;}

}