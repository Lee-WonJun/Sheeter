namespace SheeterCore
open FSharp.Data

module Json=
    type KeyBinding = JsonProvider<"""
    {
        "process": "process",
        "keybindings": [
            {"action":"action", "keys":"keys"},
            
            {"action":"action", "keys":"keys"},
            
            {"action":"action", "keys":"keys"},
    
            {"action":"action", "keys":"keys"}
        ]
    }
    """>
    
    type KeyMap = 
        {
            Action: string;
            Keys: string
        }
    
    let loadKeyMapFile (path:string) =
        let file =
            try
                KeyBinding.Load(path)
            with
                | _ -> KeyBinding.GetSample();

        file.Keybindings
        |> Array.map (fun x -> {Action = x.Action; Keys = x.Keys})