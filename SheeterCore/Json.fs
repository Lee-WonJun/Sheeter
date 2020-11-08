namespace SheeterCore
open FSharp.Data
open System.Windows.Media.Imaging
open System.IO
open System

module Json=
    type KeyBinding = JsonProvider<"""
    {
       "icon": "name",
       "keybindings": [
           {
               "key": "key",
               "command": "command"
           },
           {
               "key": "key",
               "command": "command"
           },
           {
               "key": "key",
               "command": "command"
           }
       ]
    }
    """>

    type KeyMap = 
        {
            Command: string;
            Key: string
        }

    type KeyMapFile = {Icon:string; KeyMaps: KeyMap[]}
    
    let loadKeyMapFile (path:string) =
        try
            Some (KeyBinding.Load(path))
        with
            | _ -> None;
        |> function
            | Some file -> 
                let keymap = 
                    file.Keybindings
                    |> Array.map (fun x -> {Command = x.Command; Key = x.Key})
                {Icon = file.Icon; KeyMaps = keymap}
            | None -> {Icon = ""; KeyMaps = Array.empty }
