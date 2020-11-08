# Sheeter
![sheeter](https://user-images.githubusercontent.com/10369528/98460692-f6fffc00-21e9-11eb-9588-10a30836eeaf.png)

Cheat sheet viewer of the current application

## What is Sheeter?
Sheeter is a utility for quickly checking a program's shortcut.

Now just remember one shortcut

Inspired by Microsoft's PowerToys and Intelli J's "Search Everywhere."

![resultr](https://user-images.githubusercontent.com/10369528/98461423-4f85c800-21ef-11eb-95b2-3bb64a797c85.gif)

## How to Use
#### Requirements
1. install dotnet core 3.0 runtime

#### Download And Setting
1. Download from the [GitHub releases page](https://github.com/Lee-WonJun/Sheeter/releases/tag/0.1.0).
2. Add Json File to keymap folder
 - File Name : process name
 - ext : json
 - contents
 ```json
 {
    "icon": "intellj.png", //Currently no meaning
    "keybindings": [
        {
            "key": "shift shift",
            "command": "Search Everything"
        }
        //...
     ]
  }
 ```
 
 3. run Sheeter.exe
 4. Press the win + scroll lock key to overlay
 
 #### Note 
 When you open the overlay, you will see the process name.
 
 
 #### Close
 - Alt F4 on overlay
 - Exit button in Tray
 ![image](https://user-images.githubusercontent.com/10369528/98461848-f3bd3e00-21f2-11eb-8e83-99797182aef7.png)


## Project Goal
![Goal](https://user-images.githubusercontent.com/10369528/98461548-4e08cf80-21f0-11eb-8cf7-e3b0f0585756.png)

1. Overlay Keymap
2. Search Keymap (Not yet)
3. Setting (Not yet)
4. Icon Tab (Not yet)


## Open Source License
[FSharp.Data](https://github.com/fsharp/FSharp.Data) (Apache 2.0)
