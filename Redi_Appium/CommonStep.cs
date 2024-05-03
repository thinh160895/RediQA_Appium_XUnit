using Microsoft.VisualStudio.TestPlatform.Utilities;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using Xunit.Abstractions;
using static OpenQA.Selenium.Interactions.PointerInputDevice;


namespace Redi_Appium
{
    public class SwipeHandler
    {
        private AndroidDriver _driver;
        private readonly ITestOutputHelper _output;

        public SwipeHandler(AndroidDriver driver, ITestOutputHelper output)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver), "AndroidDriver cannot be null.");
            _output = output;
        }
        public void Swipe(string direction, int times)
        {

            for (int i = 0; i < times; i++)
            {

                try
                {
                    var touch = new PointerInputDevice(PointerKind.Touch, "finger");
                    var sequence = new ActionSequence(touch);


                    // Điểm bắt đầu của swipe
                    var startX = _driver.Manage().Window.Size.Width / 2;
                    var startY = _driver.Manage().Window.Size.Height / 2;

                    // Số lượng pixel để swipe theo hướng tương ứng
                    var endX = 200;
                    var endY = 200;

                    switch (direction.ToUpper())
                    {
                        case "DOWN":
                            endX = startX;
                            endY = startY + startY;
                            break;
                        case "UP":
                            endX = startX;
                            endY = startY - startY;
                            break;
                        case "LEFT":
                            endX = startX - startX;
                            endY = startY;
                            break;
                        case "RIGHT":
                            endX = startX + startX;
                            endY = startY;
                            break;
                        default:
                            throw new ArgumentException("Invalid direction. Please use 'UP', 'DOWN', 'LEFT', or 'RIGHT'.");
                    }

                    // Tạo action để swipe
                    var move = touch.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromSeconds(1));
                    sequence
                        .AddAction(touch.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.FromSeconds(1)))
                        .AddAction(touch.CreatePointerDown(MouseButton.Touch))
                        .AddAction(touch.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromSeconds(1)))
                        .AddAction(touch.CreatePointerUp(MouseButton.Touch))
                        ;

                    var actionsSeq = new List<ActionSequence> { sequence };
                    _driver.PerformActions(actionsSeq);
                }
                catch (Exception ex)
                {
                    _output.WriteLine(ex.ToString());
                }
            }
        }
    }
}
