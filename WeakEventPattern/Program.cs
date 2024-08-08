using System;
using static System.Console;

namespace DotNetDesignPatternDemos.Behavioral.Observer.WeakEventPattern
{
    public class Button
    {
        public event EventHandler Clicked;

        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window
    {
        public Window(Button button)
        {
            button.Clicked += ButtonOnClicked;
        }

        private void ButtonOnClicked(object sender, EventArgs eventArgs)
        {
            WriteLine("Button clicked (Window handler)");
        }

        ~Window()
        {
            WriteLine("Window finalized");
        }
    }

    public class WeakEventHandler
    {
        private WeakReference _targetRef;
        private EventHandler _handler;
        private Action<EventHandler> _unregister;

        public WeakEventHandler(EventHandler handler, Action<EventHandler> unregister)
        {
            _targetRef = new WeakReference(handler.Target);
            _handler = handler;
            _unregister = unregister;
        }

        public void Handler(object sender, EventArgs e)
        {
            var target = _targetRef.Target;
            if (target != null)
            {
                _handler?.Invoke(target, e);
            }
            else
            {
                _unregister?.Invoke(_handler);
            }
        }

        public static EventHandler CreateWeakEventHandler(EventHandler handler, Action<EventHandler> unregister)
        {
            var weh = new WeakEventHandler(handler, unregister);
            return weh.Handler;
        }
    }

    public class Window2
    {
        private readonly Button _button;
        private readonly EventHandler _weakHandler;

        public Window2(Button button)
        {
            _button = button;
            _weakHandler = WeakEventHandler.CreateWeakEventHandler(ButtonOnClicked, h => button.Clicked -= h);
            _button.Clicked += _weakHandler;
        }

        private void ButtonOnClicked(object sender, EventArgs eventArgs)
        {
            WriteLine("Button clicked (Window2 handler)");
        }

        ~Window2()
        {
            WriteLine("Window2 finalized");
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var btn = new Button();
            // var window = new Window(btn);
            var window = new Window2(btn);
            var windowRef = new WeakReference(window);
            btn.Fire();

            WriteLine("Setting window to null");
            window = null;

            FireGC();
            WriteLine($"Window alive? {windowRef.IsAlive}");

            btn.Fire();

            WriteLine("Setting button to null");
            btn = null;

            FireGC();

            WriteLine($"Window alive? {windowRef.IsAlive}");
        }

        private static void FireGC()
        {
            WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            WriteLine("GC is done!");
        }
    }
}
