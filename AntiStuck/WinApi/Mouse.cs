using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiStuck.WinApi
{
    public class Mouse
    {
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public static void MoveTo(int x, int y)
        {
            NativeImport.mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint)x, (uint)y, 0, 0);
        }

        internal static void MouseMove(int x, int y)
        {
            NativeImport.SetCursorPos(x, y);
        }

        private static void MouseEvent(uint mouseEvent, uint x, uint y)
        {
            NativeImport.mouse_event(mouseEvent, x, y, 0, 0);
        }

        internal static async Task MouseLeftDown()
        {
            MouseEvent(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y);
            await Task.Delay(25);
        }
        
        internal static async Task MouseLeftUp()
        {
            MouseEvent(MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y);
            await Task.Delay(25);
        }

        public static async Task MouseClickLeft()
        {
            await MouseLeftDown();
            await Task.Delay(50);
            await MouseLeftUp();
        }
        internal static void MouseRightDown()
        {
            MouseEvent(MOUSEEVENTF_RIGHTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y);
        }
        internal static void MouseRightUp()
        {
            MouseEvent(MOUSEEVENTF_RIGHTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y);
        }

        /// <summary>
        /// Performs a mouse right click.
        /// </summary>

        internal static void MouseClickRight()
        {
            MouseRightDown();
            MouseRightUp();
        }
    }
}