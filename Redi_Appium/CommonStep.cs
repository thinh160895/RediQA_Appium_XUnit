using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;


namespace Redi_Appium
{
    public class SwipeHandler
    {
        private AndroidDriver _driver;

        public SwipeHandler(AndroidDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver), "AndroidDriver cannot be null.");
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
                            endY = startY + 200;
                            break;
                        case "UP":
                            endX = startX;
                            endY = 200;
                            break;
                        case "LEFT":
                            endX = 200;
                            endY = startY;
                            break;
                        case "RIGHT":
                            endX = startX + 200;
                            endY = startY;
                            break;
                        default:
                            throw new ArgumentException("Invalid direction. Please use 'UP', 'DOWN', 'LEFT', or 'RIGHT'.");
                    }

                    // Tạo action để swipe
                    var move = touch.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromSeconds(1));
                    sequence.AddAction(move);

                    var actionsSeq = new List<ActionSequence> { sequence };
                    _driver.PerformActions(actionsSeq);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
