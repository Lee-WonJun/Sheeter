namespace SheeterCore

open System.Runtime.InteropServices;
open System.Diagnostics;
open System


module Hook =
    type LowLevelKeyboardProc = delegate of int * nativeint * nativeint -> nativeint

    [<DllImport("user32.dll")>]
    extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, nativeint hInstance, UInt32 threadId);
    
    [<DllImport("user32.dll")>]
    extern bool UnhookWindowsHookEx(nativeint hInstance);
    
    [<DllImport("user32.dll")>]
    extern IntPtr CallNextHookEx(nativeint idHook, int nCode, int wParam, nativeint lParam);
    
    [<DllImport("kernel32.dll")>]
    extern IntPtr LoadLibrary(string lpFileName);
    
    [<DllImport("user32.dll")>]
    extern IntPtr GetForegroundWindow();
    
    [<DllImport("user32.dll", SetLastError = true)>]
    extern UInt32 GetWindowThreadProcessId(nativeint hWnd, UInt32& lpdwProcessId);

    let WH_KEYBOARD_LL = 13;
    let WM_KEYDOWN = 0x100;
    let VK_CODE = 145u;

    let GetActiveProcess () =
        let hWnd = GetForegroundWindow();
        let mutable procId = 0u;
        GetWindowThreadProcessId(hWnd, &procId) |> ignore;
        let proc = Process.GetProcessById((int)procId);
        (proc.MainWindowTitle,proc.ProcessName)

