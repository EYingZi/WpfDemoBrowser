using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drawing
{
    /// <summary>
    /// C14.xaml 的交互逻辑
    /// </summary>
    public partial class C14 : UserControl
    {
        public C14()
        {
            InitializeComponent();
            // VisualLayer
            DrawingVisual v = new DrawingVisual();
            DrawSquare(v, new Point(10, 10), false);
        }

        // VisualLayer
        // Variables for dragging shapes.
        private bool isDragging = false;
        private Vector clickOffset;
        private DrawingVisual selectedVisual;

        // Variables for drawing the selection square.
        private bool isMultiSelecting = false;
        private Point selectionSquareTopLeft;

        private void drawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointClicked = e.GetPosition(drawingSurface);

            if (cmdAdd.IsChecked == true)
            {
                DrawingVisual visual = new DrawingVisual();
                DrawSquare(visual, pointClicked, false);
                drawingSurface.AddVisual(visual);
            }
            else if (cmdDelete.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if (visual != null) drawingSurface.DeleteVisual(visual);
            }
            else if (cmdSelectMove.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if (visual != null)
                {
                    // Calculate the top-left corner of the square.
                    // This is done by looking at the current bounds and
                    // removing half the border (pen thickness).
                    // An alternate solution would be to store the top-left
                    // point of every visual in a collection in the 
                    // DrawingCanvas, and provide this point when hit testing.
                    Point topLeftCorner = new Point(
                        visual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        visual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
                    DrawSquare(visual, topLeftCorner, true);

                    clickOffset = topLeftCorner - pointClicked;
                    isDragging = true;

                    if (selectedVisual != null && selectedVisual != visual)
                    {
                        // The selection has changed. Clear the previous selection.
                        ClearSelection();
                    }
                    selectedVisual = visual;
                }
            }
            else if (cmdSelectMultiple.IsChecked == true)
            {

                selectionSquare = new DrawingVisual();

                drawingSurface.AddVisual(selectionSquare);

                selectionSquareTopLeft = pointClicked;
                isMultiSelecting = true;

                // Make sure we get the MouseLeftButtonUp event even if the user
                // moves off the Canvas. Otherwise, two selection squares could be drawn at once.
                drawingSurface.CaptureMouse();
            }
        }

        // Drawing constants.
        private Brush drawingBrush = Brushes.AliceBlue;
        private Brush selectedDrawingBrush = Brushes.LightGoldenrodYellow;
        private Pen drawingPen = new Pen(Brushes.SteelBlue, 3);
        private Size squareSize = new Size(30, 30);
        private DrawingVisual selectionSquare;

        // Rendering the square.
        private void DrawSquare(DrawingVisual visual, Point topLeftCorner, bool isSelected)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                Brush brush = drawingBrush;
                if (isSelected) brush = selectedDrawingBrush;

                dc.DrawRectangle(brush, drawingPen,
                    new Rect(topLeftCorner, squareSize));
            }
        }

        private void drawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

            if (isMultiSelecting)
            {
                // Display all the squares in this region.
                RectangleGeometry geometry = new RectangleGeometry(
                    new Rect(selectionSquareTopLeft, e.GetPosition(drawingSurface)));
                List<DrawingVisual> visualsInRegion =
                    drawingSurface.GetVisuals(geometry);
                MessageBox.Show(String.Format("You selected {0} square(s).", visualsInRegion.Count));

                isMultiSelecting = false;
                drawingSurface.DeleteVisual(selectionSquare);
                drawingSurface.ReleaseMouseCapture();
            }
        }

        private void ClearSelection()
        {
            Point topLeftCorner = new Point(
                        selectedVisual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        selectedVisual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
            DrawSquare(selectedVisual, topLeftCorner, false);
            selectedVisual = null;
        }

        private void drawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point pointDragged = e.GetPosition(drawingSurface) + clickOffset;
                DrawSquare(selectedVisual, pointDragged, true);
            }
            else if (isMultiSelecting)
            {
                Point pointDragged = e.GetPosition(drawingSurface);
                DrawSelectionSquare(selectionSquareTopLeft, pointDragged);
            }
        }

        private Brush selectionSquareBrush = Brushes.Transparent;
        private Pen selectionSquarePen = new Pen(Brushes.Black, 2);

        private void DrawSelectionSquare(Point point1, Point point2)
        {
            selectionSquarePen.DashStyle = DashStyles.Dash;

            using (DrawingContext dc = selectionSquare.RenderOpen())
            {
                dc.DrawRectangle(selectionSquareBrush, selectionSquarePen,
                    new Rect(point1, point2));
            }
        }

        // CustomPixelShader
        private void chkEffect_Click(object sender, RoutedEventArgs e)
        {
            if (chkEffect.IsChecked != true)
                img.Effect = null;
            else
                img.Effect = new GrayscaleEffect();
        }

        // GenerateBitmap
        private void cmdGenerate_Click(object sender, RoutedEventArgs e)
        {
            // Create the bitmap, with the dimensions of the image placeholder.
            WriteableBitmap wb = new WriteableBitmap((int)img2.Width,
                (int)img2.Height, 96, 96, PixelFormats.Bgra32, null);

            // Define the update square (which is as big as the entire image).
            Int32Rect rect = new Int32Rect(0, 0, (int)img2.Width, (int)img2.Height);

            byte[] pixels = new byte[(int)img2.Width * (int)img2.Height * wb.Format.BitsPerPixel / 8];
            Random rand = new Random();
            for (int y = 0; y < wb.PixelHeight; y++)
            {
                for (int x = 0; x < wb.PixelWidth; x++)
                {
                    int alpha = 0;
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    // Determine the pixel's color.
                    if ((x % 5 == 0) || (y % 7 == 0))
                    {
                        red = (int)((double)y / wb.PixelHeight * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)x / wb.PixelWidth * 255);
                        alpha = 255;
                    }
                    else
                    {
                        red = (int)((double)x / wb.PixelWidth * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)y / wb.PixelHeight * 255);
                        alpha = 50;
                    }

                    int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)blue;
                    pixels[pixelOffset + 1] = (byte)green;
                    pixels[pixelOffset + 2] = (byte)red;
                    pixels[pixelOffset + 3] = (byte)alpha;


                }

                int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;

                wb.WritePixels(rect, pixels, stride, 0);
            }

            // Show the bitmap in an Image element.
            img2.Source = wb;
        }



        private void cmdGenerate2_Click(object sender, RoutedEventArgs e)
        {


            // Create the bitmap, with the dimensions of the image placeholder.
            WriteableBitmap wb = new WriteableBitmap((int)img2.Width,
                (int)img2.Height, 96, 96, PixelFormats.Bgra32, null);

            Random rand = new Random();
            for (int x = 0; x < wb.PixelWidth; x++)
            {
                for (int y = 0; y < wb.PixelHeight; y++)
                {
                    int alpha = 0;
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    // Determine the pixel's color.
                    if ((x % 5 == 0) || (y % 7 == 0))
                    {
                        red = (int)((double)y / wb.PixelHeight * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)x / wb.PixelWidth * 255);
                        alpha = 255;
                    }
                    else
                    {
                        red = (int)((double)x / wb.PixelWidth * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)y / wb.PixelHeight * 255);
                        alpha = 50;
                    }

                    // Set the pixel value.                    
                    byte[] colorData = { (byte)blue, (byte)green, (byte)red, (byte)alpha }; // B G R

                    Int32Rect rect = new Int32Rect(x, y, 1, 1);
                    int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;
                    wb.WritePixels(rect, colorData, stride, 0);

                    //wb.WritePixels(.[y * wb.PixelWidth + x] = pixelColorValue;
                }
            }

            // Show the bitmap in an Image element.
            img2.Source = wb;
        }
    }
}
