using System.Runtime.InteropServices;

namespace AutoClicker
{
    // The script that simulates mouse input
    static class MouseScript
    {
        // Import the mouse event function from user32
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwdata, int dwextrainfo);

        // The Mouse Event Flags
        public enum MouseEventFlags
        {
            // Left Mouse Button
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,

            // Middle Mouse Button
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,

            // Right Mouse Button
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010
        }

        //Left Mouse Click
        public static void LeftClick(Point p)
        {
            mouse_event((int)(MouseEventFlags.MOUSEEVENTF_LEFTDOWN), p.X, p.Y, 0, 0);
            mouse_event((int)(MouseEventFlags.MOUSEEVENTF_LEFTUP), p.X, p.Y, 0, 0);
        }
        //Middle Mouse Click
        public static void MidleClick(Point p)
        {
            mouse_event((int)(MouseEventFlags.MOUSEEVENTF_MIDDLEDOWN), p.X, p.Y, 0, 0);
            mouse_event((int)(MouseEventFlags.MOUSEEVENTF_MIDDLEUP), p.X, p.Y, 0, 0);
        }
        //Right Mouse Click
        public static void RightClick(Point p)
        {
            mouse_event((int)(MouseEventFlags.MOUSEEVENTF_MIDDLEDOWN), p.X, p.Y, 0, 0);
            mouse_event((int)(MouseEventFlags.MOUSEEVENTF_MIDDLEUP), p.X, p.Y, 0, 0);
        }
    }
}
