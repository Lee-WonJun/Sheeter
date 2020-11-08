namespace SheeterCore

open System.Runtime.InteropServices
open System.Diagnostics
open System

module Hook =
    [<DllImport("user32.dll")>]
    extern IntPtr GetForegroundWindow();
    
    [<DllImport("user32.dll", SetLastError = true)>]
    extern UInt32 GetWindowThreadProcessId(nativeint hWnd, UInt32& lpdwProcessId);

    [<DllImport("user32.dll")>]
    extern bool RegisterHotKey(nativeint hWnd, int id, int fsModifiers, int vk);
          
    [<DllImport("user32.dll")>]
    extern bool UnregisterHotKey(nativeint hWnd, int id);

    let GetActiveProcess () =
        let hWnd = GetForegroundWindow();
        let mutable procId = 0u;
        GetWindowThreadProcessId(hWnd, &procId) |> ignore;
        let proc = Process.GetProcessById((int)procId);
        (proc.MainWindowTitle,proc.ProcessName)
        
