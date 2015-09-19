﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using MonoMac.AppKit;
using MacBridge.CoreGraphics;

namespace WinApi
{
    public static partial class Win32
    {
        const string NotImplemented = "Win32 method not implemented on Mac: ";

        public static IntPtr WindowFromPoint(POINT p)
        {
            Size displaySize;
            XplatUI.GetDisplaySize(out displaySize);
            p.Y = displaySize.Height - p.Y;

            var screenLocation = p.ToCGPoint();

            int wnum = NSWindow.WindowNumberAtPoint(screenLocation, 0);
            var window = NSApplication.SharedApplication.WindowWithWindowNumber(wnum);
            if (window != null)
            {
                var windowLocation = window.ConvertScreenToBase(screenLocation);
                //var contentViewLocation = window.ContentView.ConvertPointFromView(windowLocation, null);
                var view = window.ContentView.HitTest(windowLocation);

                if (view != null)
                {
                    var hwnd = Hwnd.GetHandleFromWindow(view.Handle);
                    if (hwnd != IntPtr.Zero)
                        return hwnd;
                }
            }

            return IntPtr.Zero;
        }

        public static IntPtr GetWindow(IntPtr hWnd, uint uCmd)
        {
            //TODO:
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static void ShowWindow(IntPtr hWnd, int nCmdShow)
        {
            // TODO:
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
        }

        public static void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags)
        {
            if (0 == (uFlags & (uint)XplatUIWin32.SetWindowPosFlags.SWP_NOZORDER))
                XplatUI.SetZOrder(hWnd, hWndInsertAfter, false, false);

            int x_, y_, w_, h_, clw_, clh_;
            XplatUI.GetWindowPos(hWnd, false, out x_, out y_, out w_, out h_, out clw_, out clh_);
           
            bool move = 0 != (uFlags & (uint)XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE);
            if (move)
            {
                x = x_;
                y = y_;
            }

            bool size = 0 != (uFlags & (uint)XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
            if (size)
            {
                cx = w_;
                cy = h_;
            }

            if (move || size) 
                XplatUI.SetWindowPos(hWnd, x, y, cx, cy);

            bool show = 0 != (uFlags & (uint)XplatUIWin32.SetWindowPosFlags.SWP_SHOWWINDOW);
            bool hide = 0 != (uFlags & (uint)XplatUIWin32.SetWindowPosFlags.SWP_HIDEWINDOW);
            bool activate = 0 != (uFlags & (uint)XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE);

            if (show || hide || activate)
                XplatUI.SetVisible(hWnd, show || !hide, activate);

            bool redraw = 0 != (uFlags & (uint)XplatUIWin32.SetWindowPosFlags.SWP_NOREDRAW);
            if (redraw)
                XplatUI.UpdateWindow(hWnd);

            // TODO: Handle remaining flags
        }

        public static int DestroyIcon(IntPtr hIcon)
        {
            // TODO:
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return 0;
        }

        public static bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            XplatUI.PostMessage(hWnd, (Msg)msg, wParam, lParam);
            return true;
        }

        public static bool SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            XplatUI.SendMessage(hWnd, (Msg)msg, wParam, lParam);
            return true;
        }

        public static IntPtr SetFocus(IntPtr hWnd)
        {
            var prev = XplatUI.GetFocus();
            XplatUI.SetFocus(hWnd);
            return prev;
        }

        public static IntPtr GetFocus()
        {
            return XplatUI.GetFocus();
        }

        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        public static bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i)
        {
            // TODO;
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return false;
        }

        public static int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount)
        {
            // TODO;
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return 0;
        }

        public static IntPtr GetForegroundWindow()
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static IntPtr GetDesktopWindow()
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static IntPtr GetShellWindow()
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static int GetWindowRect(IntPtr hwnd, out RECT rc)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            rc = new RECT();
            return 0;
        }

        // Apple says:
        // Don't flash the taskbar button if the only thing the user has to do is activate the program,
        // read a message, or see a change in status.
        public static bool FlashWindow(IntPtr hwnd, bool bInvert)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return true;
        }

        public static IntPtr GetWindowDC(IntPtr handle)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static IntPtr ReleaseDC(IntPtr handle, IntPtr hDC)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static int GetClassName(IntPtr hwnd, char[] className, int maxCount)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return 0;
        }

        public static IntPtr GetWindow(IntPtr hwnd, int uCmd)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static bool IsWindowVisible(IntPtr hwnd)
        {
            return XplatUI.IsVisible(hwnd);
        }

        public static int GetClientRect(IntPtr hwnd, ref RECT lpRect)
        {
            var window = Hwnd.ObjectFromHandle(hwnd);
            lpRect = new RECT(window.ClientRect);
            return 1;
        }

        public static int GetClientRect(IntPtr hwnd, [In, Out] ref Rectangle rect)
        {
            var window = Hwnd.ObjectFromHandle(hwnd);
            rect = window.ClientRect;
            return 1;
        }

        public static bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint)
        {
            XplatUI.SetWindowPos(hwnd, X, Y, nWidth, nHeight);
            if (bRepaint)
                XplatUI.UpdateWindow(hwnd);
            return true;
        }

        public static bool UpdateWindow(IntPtr hwnd)
        {
            XplatUI.UpdateWindow(hwnd);
            return true;
        }

        public static bool InvalidateRect(IntPtr hwnd, ref Rectangle rect, bool bErase)
        {
            XplatUI.Invalidate(hwnd, rect, bErase);
            return true;
        }

        public static bool ValidateRect(IntPtr hwnd, ref Rectangle rect)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return true;
        }

        public static bool GetWindowRect(IntPtr hWnd, [In, Out] ref Rectangle rect)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return false;
        }

        public static IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static int GetWindowLongPtr32(IntPtr hWnd, int nIndex)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return 0;
        }

        public static IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static int SetWindowLongPtr32(IntPtr hWnd, int nIndex, int dwNewLong)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return 0;
        }

        public static IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static uint TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return 0;
        }

        public static int EnableMenuItem(IntPtr hMenu, SC uIDEnableItem, MF uEnable)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return 0;
        }

        public static IntPtr SetCursor(IntPtr hcur)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static IntPtr LoadCursor(IntPtr hInstcance, SystemCursor hcur)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return IntPtr.Zero;
        }

        public static bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, BlendFlags dwFlags)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return false;
        }

        public static bool EnableWindow(IntPtr hWnd, bool bEnable)
        {
            XplatUI.EnableWindow(hWnd, bEnable);
            return true;
        }

        public static IntPtr SetActiveWindow(IntPtr handle)
        {
            var prev = XplatUI.GetActive();
            XplatUI.Activate(handle);
            return prev;
        }

        public static bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            return false;
        }

        public static bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl)
        {
            Debug.WriteLine(NotImplemented + MethodBase.GetCurrentMethod().Name);
            lpwndpl = new WINDOWPLACEMENT();
            return false;
        }

        public static int ScrollWindowEx(IntPtr hWnd, int dx, int dy, IntPtr prcScroll, IntPtr prcClip, IntPtr hrgnUpdate, IntPtr prcUpdate, uint flags)
        {
            var control = Control.FromHandle(hWnd);
            var rect = ((RECT)Marshal.PtrToStructure(prcScroll, typeof(RECT))).ToRectangle();

            // Change Bounds origin of every NSView whose Control's frame intersects with a given rect.
            foreach (Control child in control.Controls) {
                if (child.Bounds.IntersectsWith(rect))
                {
                    var window = Hwnd.ObjectFromHandle(child.Handle);
                    var view = (NSView)MonoMac.ObjCRuntime.Runtime.GetNSObject(window.ClientWindow);
                    var origin = view.Bounds.Origin;
                    origin.Y -= dy;
                    origin.X += dx;
                    view.SetBoundsOrigin(origin);
                }
            }
            return 1;
        }
    }
}