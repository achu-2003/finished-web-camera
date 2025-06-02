using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace webcamproject
{
    public enum FlipState
    {
        Normal,
        Horizontal,
        Vertical,
        Both
    }

    public partial class webcamera : Form
    {
        private System.Windows.Forms.Timer cameraCheckTimer;
        private int lastCameraCount = 0;
        private List<string> availableCameras = new List<string>();

        private int rotationAngleStream = 0;
        private bool _streaming;
        private Capture capture;
        
        private string currentCameraName = "camera1";
        private Dictionary<string, CameraSettings> _cameraSettings = new Dictionary<string, CameraSettings>();
        private Rectangle roiRect =  Rectangle.Empty;
        private Rectangle previousRoiRect = Rectangle.Empty;

        private int cameraWidth = 0;
        private int cameraHeight = 0;

        private readonly Color SelectedButtonColor = Color.LightGreen;
        private readonly Color NormalButtonColor = SystemColors.Control;

        private StringBuilder errorLogBuffer = new StringBuilder();
        private DateTime currentLogDate = DateTime.Today;

        private float zoomFactor = 1.0f;
        private PointF zoomCenter = PointF.Empty;
        private const float ZoomIncrement = 0.1f;
        private const float MinZoom = 0.5f;

        private int brightness = 50;
        private int saturation = 50;
        private int contrast = 50;
        private int sharpness = 50;


        private const float MAX_ZOOM_FACTOR = 4.0f;

        private Stopwatch fpsStopwatch = new Stopwatch();
        private int frameCount = 0;

        private bool _wasStreamingBeforeDisconnect = false;

        [Serializable]
        public class CameraSettingsCollection
        {
            public List<CameraSettingEntry> Entries { get; set; } = new List<CameraSettingEntry>();
        }

        [Serializable]
        public class CameraSettingEntry
        {
            public string CameraName { get; set; }
            public CameraSettings Settings { get; set; }
        }

        [Serializable]
        public class CameraSettings
        {
            public int BrightnessValue { get; set; }
            public bool FlipHorizontal { get; set; }
            public bool FlipVertical { get; set; }
            public int RotationAngleStream { get; set; }
            public Rectangle RoiRect { get; set; } = Rectangle.Empty;
            public Rectangle PreviousRoiRect { get; set; } = Rectangle.Empty;
            public double HStartPercent { get; set; } = 0;
            public double HEndPercent { get; set; } = 0;
            public double VStartPercent { get; set; } = 0;
            public double VEndPercent { get; set; } = 0;
            public int CameraWidth { get; set; }
            public int CameraHeight { get; set; }
            public float ZoomFactor { get; set; } = 1.0f;
            public float ZoomCenterX { get; set; }
            public float ZoomCenterY { get; set; }          
            public int Brightness { get; set; }
            public int Contrast { get; set; }
            public int Saturation { get; set; }
            public int Sharpness { get; set; }
            public int ZoomLevel { get; set; }
        }
        private Timer _frameTimer;

        private void InitializeFrameTimer()
        {
            _frameTimer = new Timer { Interval = 10 };
            _frameTimer.Tick += (s, e) => streaming(s, e);
        }
        private void InitializePictureBox()
        {
            streampbox1.SizeMode = PictureBoxSizeMode.StretchImage;
            streampbox1.MouseWheel += Streampbox1_MouseWheel;
            streampbox1.MouseEnter += (s, e) => streampbox1.Focus();
        }
        private string GetErrorLogDirectory()
        {
            string basePath = @"C:\Users\KML\Desktop\form c#\webcamproject";

            string errorLogPath = Path.Combine(basePath, "error list");

            if (!Directory.Exists(errorLogPath))
            {
                Directory.CreateDirectory(errorLogPath);
            }
            return errorLogPath;
        }
        private Bitmap ApplyZoom(Bitmap source, float zoomFactor, PointF zoomCenter)
        {
            if (zoomFactor == 1.0f || source == null)
                return (Bitmap)source.Clone();

            Bitmap zoomed = new Bitmap(source.Width, source.Height);

            using (Graphics g = Graphics.FromImage(zoomed))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Use the provided zoom center (calculated to maintain cursor position)
                PointF center = zoomCenter == PointF.Empty ?
                    new PointF(source.Width / 2f, source.Height / 2f) :
                    zoomCenter;

                // Calculate source rectangle
                float srcWidth = source.Width / zoomFactor;
                float srcHeight = source.Height / zoomFactor;
                float srcX = center.X - srcWidth / 2;
                float srcY = center.Y - srcHeight / 2;

                // Clamp to image boundaries
                srcX = Math.Max(0, Math.Min(srcX, source.Width - srcWidth));
                srcY = Math.Max(0, Math.Min(srcY, source.Height - srcHeight));
                srcWidth = Math.Min(srcWidth, source.Width - srcX);
                srcHeight = Math.Min(srcHeight, source.Height - srcY);

                // Draw zoomed portion
                g.DrawImage(source,
                    new Rectangle(0, 0, source.Width, source.Height),
                    new RectangleF(srcX, srcY, srcWidth, srcHeight),
                    GraphicsUnit.Pixel);
            }
            return zoomed;
        }

        private void Streampbox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!_streaming || streampbox1.Image == null) return;

            // Get current mouse position in image coordinates
            PointF imagePoint = ScreenToImageCoordinates(e.Location);

            // Store old zoom factor
            float oldZoom = zoomFactor;

            // Adjust zoom factor
            zoomFactor = e.Delta > 0
                 ? Math.Min(zoomFactor + ZoomIncrement, MAX_ZOOM_FACTOR)
                 : Math.Max(zoomFactor - ZoomIncrement, MinZoom);

            // Reset if fully zoomed out
            if (Math.Abs(zoomFactor - 1.0f) < 0.01f)
            {
                zoomFactor = 1.0f;
                zoomCenter = PointF.Empty;
            }
            else
            {
                // Calculate new zoom center to keep cursor position stable
                if (oldZoom != 1.0f)
                {
                    // Adjust existing center to maintain the point under cursor
                    zoomCenter = new PointF(
                        imagePoint.X - (imagePoint.X - zoomCenter.X) * (oldZoom / zoomFactor),
                        imagePoint.Y - (imagePoint.Y - zoomCenter.Y) * (oldZoom / zoomFactor));
                }
                else
                {
                    // First zoom - center at cursor position
                    zoomCenter = imagePoint;
                }
            }
            if (_streaming)
            {
                _frameTimer.Stop();
                _frameTimer.Start();
            }
            // Sync zoom bar and label
            int zoomBarValue = (int)(((Math.Max(zoomFactor, 1.0f) - 1.0f) / (MAX_ZOOM_FACTOR - 1.0f)) * 100.0f);
            zoombar.Value = Math.Min(zoombar.Maximum, Math.Max(zoombar.Minimum, zoomBarValue));
            zoom0lb.Text = zoomBarValue.ToString();
        }
        
        private RectangleF GetImageDisplayArea()
        {
            if (streampbox1.Image == null || streampbox1.ClientRectangle.Width == 0)
                return RectangleF.Empty;

            float imageRatio = (float)streampbox1.Image.Width / streampbox1.Image.Height;
            float controlRatio = (float)streampbox1.Width / streampbox1.Height;

            float width, height;
            if (imageRatio > controlRatio)
            {
                width = streampbox1.Width;
                height = width / imageRatio;
            }
            else
            {
                height = streampbox1.Height;
                width = height * imageRatio;
            }

            float x = (streampbox1.Width - width) / 2;
            float y = (streampbox1.Height - height) / 2;

            return new RectangleF(x, y, width, height);
        }

        private PointF ScreenToImageCoordinates(Point screenPoint)
        {
            if (streampbox1.Image == null) return PointF.Empty;

            RectangleF displayArea = GetImageDisplayArea();
            if (displayArea.Width == 0 || displayArea.Height == 0)
                return PointF.Empty;

            // Convert screen coordinates to image coordinates
            float x = (screenPoint.X - displayArea.X) * (streampbox1.Image.Width / displayArea.Width);
            float y = (screenPoint.Y - displayArea.Y) * (streampbox1.Image.Height / displayArea.Height);

            // Clamp to image boundaries
            x = Math.Max(0, Math.Min(x, streampbox1.Image.Width));
            y = Math.Max(0, Math.Min(y, streampbox1.Image.Height));

            return new PointF(x, y);
        }

        private string GetDailyLogFilePath()
        {
            return Path.Combine(GetErrorLogDirectory(), $"{DateTime.Today:yyyy-MM-dd}.txt");
        }

        private void ResetNavigationButtons()
        {
            setupbtn.BackColor = NormalButtonColor;
            infobtn.BackColor = NormalButtonColor;
            all.BackColor = NormalButtonColor;
        }

        public void errormassage(string message)
        {
            try
            {
                // Check if date has changed (new day)
                if (DateTime.Today != currentLogDate)
                {
                    currentLogDate = DateTime.Today;
                    errorLogBuffer.Clear();   
                }

                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";

                // Add to current session buffer
                errorLogBuffer.Append(logEntry);

                
                File.AppendAllText(GetDailyLogFilePath(), logEntry);

                // Only update display if in error view
                if (txtErrorDisplay.Visible && infobtn.BackColor == SelectedButtonColor)
                {
                    UpdateErrorDisplay();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error logging: {ex.Message}");
            }
        }

        private void UpdateErrorDisplay()
        {
            if (txtErrorDisplay.InvokeRequired)
            {
                txtErrorDisplay.Invoke(new Action(UpdateErrorDisplay));
                return;
            }
             
            txtErrorDisplay.Text = $"=== Current Session Errors ===\n\n{errorLogBuffer.ToString()}";
            txtErrorDisplay.SelectionStart = txtErrorDisplay.Text.Length;
            txtErrorDisplay.ScrollToCaret();
        }       
        public webcamera()
        {
            InitializeComponent();
            InitializeFrameTimer();
            streampbox1.MouseWheel += Streampbox1_MouseWheel;
            cameraCheckTimer = new System.Windows.Forms.Timer();
            cameraCheckTimer.Interval = 1000; // Check every second
            cameraCheckTimer.Tick += CheckForCameraChanges;
            cameraCheckTimer.Start();

            // Enable mouse wheel events on the PictureBox
            streampbox1.MouseEnter += (s, e) => streampbox1.Focus();
            brighscroll.Scroll += (sender, e) =>
            {
                brightness = brighscroll.Value;
                brightness0lb.Text = brightness.ToString();

            };

            saturationscroll.Scroll += (sender, e) =>
            {
                saturation = saturationscroll.Value;
                saturation0lb.Text = saturation.ToString();
            };

            contrastscroll.Scroll += (sender, e) =>
            {
                contrast = contrastscroll.Value;
                contrast0lb.Text = contrast.ToString();
            };

            zoombar.Scroll += (sender, e) =>
            {

                zoomFactor = 1.0f + (zoombar.Value / 100.0f) * (MAX_ZOOM_FACTOR - 1.0f);
                zoom0lb.Text = zoombar.Value.ToString();
            };
            fliph2.Click += fliph2_Click;
            flip1v.Click += flip1v_Click;
            UpdateFlipButtons();
        }

        private void webcamera_Load(object sender, EventArgs e)
        {
            // Initialize UI components
            InitializePictureBox();
            InitializeFrameTimer();
 
            // Set up event handlers
            streampbox1.MouseWheel += Streampbox1_MouseWheel;
            streampbox1.MouseEnter += (s, e2) => streampbox1.Focus();

            // Initialize scrollbars
            brighscroll.Minimum = 0;
            brighscroll.Maximum = 100;
            brighscroll.Value = 50;
            brightness0lb.Text = "50";

            contrastscroll.Minimum = 0;
            contrastscroll.Maximum = 100;
            contrastscroll.Value = 50;
            contrast0lb.Text = "50";

            saturationscroll.Minimum = 0;
            saturationscroll.Maximum = 100;
            saturationscroll.Value = 50;
            saturation0lb.Text = "50";

            zoombar.Minimum = 0;
            zoombar.Maximum = 100;
            zoombar.Value = 0;
            zoom0lb.Text = "0";

            // Set up scroll events
            brighscroll.Scroll += AdjustmentScroll_Scroll;
            contrastscroll.Scroll += AdjustmentScroll_Scroll;
            saturationscroll.Scroll += AdjustmentScroll_Scroll;
            zoombar.Scroll += Trackbar_Scroll;
            InitializeCameraDetection();
            // Initialize camera selection
            PopulatePhysicalCameras();
            cameraselectbox.SelectedIndexChanged += cameraselectbox_SelectedIndexChanged;

            // Initialize error logging
            try
            {
                errorLogBuffer.Clear();
                currentLogDate = DateTime.Today;
                errormassage("Application initialized successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing logging system: {ex.Message}");
                errorLogBuffer.Clear();
            }

            ResetNavigationButtons();
            all.BackColor = SelectedButtonColor; 

            // Initialize UI visibility states
            InitializeUIVisibility();

            CheckCameraConnection();
            fpsStopwatch.Start();

            try
            {
                if (crtorwrongpb.BackColor == Color.Green)
                {
                    capture = new Capture(0);
                    cameraWidth = capture.Width;
                    cameraHeight = capture.Height;
                    getcwidth.Text = cameraWidth.ToString();
                    getchight.Text = cameraHeight.ToString();
                }
                LoadAppState();

                // Initialize camera collection
                cameracollection.Items.Add("camera1");
                cameracollection.Items.Add("camera2");
                cameracollection.Items.Add("camera3");
                cameracollection.Items.Add("camera4");
                cameracollection.SelectedItem = currentCameraName;
                cameracollection.SelectedIndexChanged += cameracollection_SelectedIndexChanged;

                if (_cameraSettings.TryGetValue(currentCameraName, out var settings))
                {
                    this.Invoke((MethodInvoker)delegate {
                        UpdateUIControls(settings);
                        UpdateRotationButtonText();
                        UpdateFlipButtons();
                        UpdateRoiTextBoxes();
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing capture device: {ex.Message}");
                errormassage($"Error initializing capture device: {ex.Message}");
                onoffbtn.Enabled = false;
                SetConnectionStatus(false);
                getcwidth.Text = "0";
                getchight.Text = "0";
                fpslb2.Text = "0";

            }
            getcwidth.Text = "0";
            getchight.Text = "0";
            fpslb2.Text = "0";
        }

        private void InitializeUIVisibility()
        {
            errorloglb.Visible = false;
            clearlogbtn.Visible = false;
            savelogbtn.Visible = false;
            txtErrorDisplay.Visible = false;

            camerasettinglb.Visible = false;
            rotatestreambtn.Visible = false;
            saveparameter.Visible = false;
            switchcamera.Visible = false;
            cameracollection.Visible = false;

            label1.Visible = false;
            startroi.Visible = false;
            endroi.Visible = false;
            roi1.Visible = false;
            hstarttb.Visible = false;
            hendtb.Visible = false;
            roi2.Visible = false;
            vstarttb.Visible = false;
            vendtb.Visible = false;
            setroi.Visible = false;

            _streaming = false;

            adjustlb.Visible = false;
            saturationscroll.Visible = false;
            brighscroll.Visible = false;
            saturationlb.Visible = false;
            brightlb.Visible = false;
            contrastlb.Visible = false;
            contrastscroll.Visible = false;
            zoombar.Visible = false;
            zoombar.Visible = false;
            zoomlb.Visible = false;
            adjustclrear.Visible = false;

            brightsvalue1.Visible = false;
            brightevalue.Visible = false;
            saturationsvalue1.Visible = false;
            saturationevalue.Visible = false;
            contrastsvalue1.Visible = false;
            contrastevalue.Visible = false;
            zoomsvalue1.Visible = false;
            zoomevalue.Visible = false;

            brightness0lb.Visible = false;
            saturation0lb.Visible = false;
            contrast0lb.Visible = false;
            zoom0lb.Visible = false;

            fliph2.Visible = false;
            flip1v.Visible = false;
            fpslb.Visible = true;
            fpslb2.Visible = true;
        }

        private void Trackbar_Scroll(object sender, EventArgs e)
        {
            if (!_streaming) return;

            // Update variables based on which trackbar changed
            if (sender == brighscroll)
            {
                brightness = brighscroll.Value;
                brightness0lb.Text = brightness.ToString();
            }
            else if (sender == contrastscroll)
            {
                contrast = contrastscroll.Value;
                contrast0lb.Text = contrast.ToString();
            }
            else if (sender == saturationscroll)
            {
                saturation = saturationscroll.Value;
                saturation0lb.Text = saturation.ToString();
            }
            else if (sender == zoombar)
            {
                zoomFactor = 1.0f + (zoombar.Value / 50f);
                zoomCenter = new PointF(streampbox1.Width / 2f, streampbox1.Height / 2f);
                zoom0lb.Text = zoombar.Value.ToString();
            }

            SaveCurrentCameraSettings(currentCameraName);
            SaveAppState();
            // Force immediate update
            if (_streaming)
            {
                _frameTimer.Stop();
                _frameTimer.Start();
            }

        }
        private void AdjustmentScroll_Scroll(object sender, EventArgs e)
        {
            if (sender == brighscroll)
            {
                brightness = brighscroll.Value;
                brightness0lb.Text = brightness.ToString();
            }
            else if (sender == contrastscroll)
            {
                contrast = contrastscroll.Value;
                contrast0lb.Text = contrast.ToString();
            }
            else if (sender == saturationscroll)
            {
                saturation = saturationscroll.Value;
                saturation0lb.Text = saturation.ToString();
            }
            if (_streaming)
            {
                _frameTimer.Stop();
                _frameTimer.Start();
            }
        }

        private void streaming(object sender, EventArgs e)
        {
            Mat frame = null;
            Image<Bgr, byte> img = null;
            Bitmap processedFrame = null;

            if (!_streaming || capture == null) return;

            try
            {
                frame = capture.QueryFrame();

                if (frame == null)
                {
                    string errorMsg = "Camera disconnected abruptly - no frame received";
                    SetConnectionStatus(false, true);
                    errormassage(errorMsg);
                    Debug.WriteLine(errorMsg);
                    StopStreaming();
                    return;

                }

                img = frame.ToImage<Bgr, byte>();
                if (img == null)
                {
                    string errorMsg = "Frame conversion failed (null image)";
                    errormassage(errorMsg);
                    throw new Exception(errorMsg);
                }

                using (var originalFrame = img.Bitmap)
                {
                    processedFrame = (Bitmap)originalFrame.Clone();
                }

                // Apply transformations
                processedFrame = ApplyAllTransformations(processedFrame);

                UpdateStreamDisplay(processedFrame);
 
               // === FPS Tracking ===
                frameCount++;

                if (fpsStopwatch.ElapsedMilliseconds >= 1000)
                {
                    int fps = frameCount;
                    frameCount = 0;
                    fpsStopwatch.Restart();
                     
                    if (fpslb2.InvokeRequired)
                    {
                        fpslb2.Invoke(new Action(() => fpslb2.Text = $"{fps} fps"));
                    }
                    else
                    {
                        fpslb2.Text = $"{fps} fps";
                    }
                }
            }
            catch (AccessViolationException avEx)
            {
                string errorMsg = $"Memory access violation: {avEx.Message}";
                SetConnectionStatus(false, true);
                errormassage(errorMsg);
                StopStreaming();
            }
            catch (NullReferenceException nullEx)
            {
                string errorMsg = $"Null reference in streaming: {nullEx.Message}";
                SetConnectionStatus(false, true);
                errormassage(errorMsg);
                StopStreaming();
            }
            catch (OutOfMemoryException memEx)
            {
                string errorMsg = $"Out of memory: {memEx.Message}";
                errormassage(errorMsg);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                string errorMsg = $"Unexpected streaming error: {ex.Message}";
                SetConnectionStatus(false, true);
                errormassage(errorMsg);
                StopStreaming();
            }
            finally
            {
                frame?.Dispose();
                img?.Dispose();

                // Attempt auto-reconnect
                if (!_streaming && capture == null)
                {
                    Task.Delay(1000).ContinueWith(_ =>
                    {
                        this.Invoke((Action)(() =>
                        {
                            try
                            {
                                capture = new Capture(GetCameraIndex(currentCameraName));
                                if (_streaming) _frameTimer.Start();
                                errormassage("Camera reconnected successfully");
                            }
                            catch (Exception reconnectEx)
                            {
                                errormassage($"Reconnection failed: {reconnectEx.Message}");
                            }
                        }));
                    });
                }
            }
        }

        private Bitmap ApplyAllTransformations(Bitmap originalFrame)
        {
            if (originalFrame == null) return null;

            Bitmap result = (Bitmap)originalFrame.Clone();

            try
            {
                // 1. Apply basic image adjustments (brightness, contrast, saturation)
                if (brightness != 50 || contrast != 50 || saturation != 50 || sharpness != 50)
                {
                    Bitmap adjusted = ApplyImageAdjustments(result, brightness, contrast, saturation, sharpness);
                    result.Dispose();
                    result = adjusted;
                }

                // 2. Apply ROI (Region of Interest) FIRST before rotation
                if (roiRect != Rectangle.Empty && roiRect.Width > 0 && roiRect.Height > 0)
                {
                    Bitmap roiResult = new Bitmap(result.Width, result.Height);

                    using (Graphics g = Graphics.FromImage(roiResult))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(result,
                            new Rectangle(0, 0, result.Width, result.Height),
                            roiRect,
                            GraphicsUnit.Pixel);
                    }
                    result.Dispose();
                    result = roiResult;
                }

                // 3. Apply rotation (AFTER ROI)
                if (rotationAngleStream != 0)
                {
                    result = RotateImage(result, rotationAngleStream);
                }

                // 4. Apply flip transformations
                bool flipH = false, flipV = false;
                if (_cameraSettings.TryGetValue(currentCameraName, out var settings))
                {
                    flipH = settings.FlipHorizontal;
                    flipV = settings.FlipVertical;
                }

                if (flipH || flipV)
                {
                    Bitmap flipped = new Bitmap(result.Width, result.Height);
                    using (Graphics g = Graphics.FromImage(flipped))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        RectangleF destRect = new RectangleF(0, 0, result.Width, result.Height);

                        if (flipH && flipV)
                        {
                            // Both flips
                            g.DrawImage(result,
                                new PointF[] {
                            new PointF(result.Width, result.Height),
                            new PointF(0, result.Height),
                            new PointF(result.Width, 0)
                                },
                                destRect, GraphicsUnit.Pixel);
                        }
                        else if (flipH)
                        {
                            // Horizontal flip
                            g.DrawImage(result,
                                new PointF[] {
                            new PointF(result.Width, 0),
                            new PointF(0, 0),
                            new PointF(result.Width, result.Height)
                                },
                                destRect, GraphicsUnit.Pixel);
                        }
                        else if (flipV)
                        {
                            // Vertical flip
                            g.DrawImage(result,
                                new PointF[] {
                            new PointF(0, result.Height),
                            new PointF(result.Width, result.Height),
                            new PointF(0, 0)
                                },
                                destRect, GraphicsUnit.Pixel);
                        }
                    }
                    result.Dispose();
                    result = flipped;
                }

                // 5. Apply zoom transformation (LAST)
                if (zoomFactor != 1.0f)
                {
                    Bitmap zoomed = ApplyZoom(result, zoomFactor, zoomCenter);
                    result.Dispose();
                    result = zoomed;
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Transformation error: {ex.Message}");
                result.Dispose();
                return (Bitmap)originalFrame.Clone();
            }
        }
        private Bitmap ApplyImageAdjustments(Bitmap image, int brightness, int contrast, int saturation, int sharpness)
        {
            Bitmap adjusted = (Bitmap)image.Clone();

            try
            {
                // Apply brightness (-1 to 1 range, where 0 is neutral)
                if (brightness != 50)
                {
                    float brightnessValue = (brightness - 50) / 50f; // Converts 0-100 to -1 to +1
                    adjusted = ApplyColorMatrix(adjusted, new float[][] {
                new[] {1f, 0, 0, 0, 0},
                new[] {0, 1f, 0, 0, 0},
                new[] {0, 0, 1f, 0, 0},
                new[] {0, 0, 0, 1f, 0},
                new[] {brightnessValue, brightnessValue, brightnessValue, 0, 1f}
            });
                }

                // Apply contrast (0.5-1.5 range, where 1 is neutral)
                if (contrast != 50)
                {
                    float contrastValue = 1 + ((contrast - 50) / 50f); // Converts 0-100 to 0.5-1.5
                    float adjust = (-0.5f * contrastValue) + 0.5f;
                    adjusted = ApplyColorMatrix(adjusted, new float[][] {
                new[] {contrastValue, 0, 0, 0, 0},
                new[] {0, contrastValue, 0, 0, 0},
                new[] {0, 0, contrastValue, 0, 0},
                new[] {0, 0, 0, 1f, 0},
                new[] {adjust, adjust, adjust, 0, 1f}
            });
                }

                // Apply saturation (0-2 range, where 1 is neutral)
                if (saturation != 50)
                {
                    float saturationValue = 1 + ((saturation - 50) / 50f); // Converts 0-100 to 0-2
                    float rWeight = 0.299f;
                    float gWeight = 0.587f;
                    float bWeight = 0.114f;

                    adjusted = ApplyColorMatrix(adjusted, new float[][] {
                new[] {rWeight*(1-saturationValue)+saturationValue, rWeight*(1-saturationValue), rWeight*(1-saturationValue), 0, 0},
                new[] {gWeight*(1-saturationValue), gWeight*(1-saturationValue)+saturationValue, gWeight*(1-saturationValue), 0, 0},
                new[] {bWeight*(1-saturationValue), bWeight*(1-saturationValue), bWeight*(1-saturationValue)+saturationValue, 0, 0},
                new[] {0, 0, 0, 1f, 0},
                new[] {0, 0, 0, 0, 1f}
            });
                }

                return adjusted;
            }
            catch
            {
                adjusted.Dispose();
                throw;
            }
        }
        private Bitmap ApplyColorMatrix(Bitmap image, float[][] colorMatrix)
        {
            Bitmap adjusted = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(adjusted))
            using (ImageAttributes attributes = new ImageAttributes())
            {
                attributes.SetColorMatrix(new ColorMatrix(colorMatrix));
                g.DrawImage(image,
                    new Rectangle(0, 0, image.Width, image.Height),
                    0, 0, image.Width, image.Height,
                    GraphicsUnit.Pixel, attributes);
            }

            image.Dispose();
            return adjusted;
        }

        private void UpdateStreamDisplay(Bitmap frame)
        {
            if (streampbox1.InvokeRequired)
            {
                streampbox1.Invoke(new Action(() =>
                {
                    UpdatePictureBox(frame);
                }));
            }
            else
            {
                UpdatePictureBox(frame);
            }
        }

        private void UpdatePictureBox(Bitmap frame)
        {
            if (streampbox1.InvokeRequired)
            {
                streampbox1.Invoke(new Action(() => UpdatePictureBox(frame)));
                return;
            }

            // Dispose previous image if exists
            if (streampbox1.Image != null)
            {
                var oldImage = streampbox1.Image;
                streampbox1.Image = null;
                oldImage.Dispose();
            }


            streampbox1.SizeMode = PictureBoxSizeMode.StretchImage;
            streampbox1.Image = frame;
        }
       

        private void onoffbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Normal stop case
                if (_streaming)
                {
                    StopStreaming();
                    fpslb2.Text = "0";
                    return;
                }

                // Start streaming case
                if (capture == null)
                {
                    capture = new Capture(GetCameraIndex(currentCameraName));

                    // Test connection
                    using (var testFrame = capture.QueryFrame())
                    {
                        if (testFrame == null || testFrame.IsEmpty)
                        {
                            throw new Exception("Camera not responding - no frame received");
                        }
                    }

                    cameraWidth = capture.Width;
                    cameraHeight = capture.Height;
                    getcwidth.Text = cameraWidth.ToString();
                    getchight.Text = cameraHeight.ToString();
                }

                _streaming = true;
                _frameTimer.Start();

                // Update UI
                this.Invoke((MethodInvoker)delegate {
                    onoffbtn.Text = "Stop Streaming";
                    onoffbtn.ForeColor = Color.Black;
                    onoffbtn.BackColor = Color.Red;
                    onoffindentify.Text = "CAMERA IS ON";
                    onoffindentify.BackColor = Color.Green;
                    streampbox1.Image = null;
                    cameraWidth = capture.Width;
                    cameraHeight = capture.Height;
                    getcwidth.Text = cameraWidth.ToString();
                    getchight.Text = cameraHeight.ToString();
                });

                errormassage("Camera started successfully");
            }
            catch (Exception ex)
            {              
                if (!_streaming)
                {
                    this.Invoke((MethodInvoker)delegate {
                        MessageBox.Show($"Failed to start camera: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                    errormassage($"Camera start failed: {ex.Message}");
                }

                _streaming = false;
                capture?.Dispose();
                capture = null;

                SetConnectionStatus(false);
            }
        }

        private void webcamera_FormClosing(object sender, FormClosingEventArgs e)
        {
      
            SaveCurrentCameraSettings(currentCameraName);
            SaveAppState();
            cameraCheckTimer?.Stop();
            cameraCheckTimer?.Dispose();

            // Clean up resources
            if (capture != null)
            {
                capture.Dispose();
                capture = null;
            }

        }
        
        private void StopStreaming()
        {
            if (_streaming)
            {
                try
                {
                    if (_frameTimer != null)
                    {
                        _frameTimer.Stop();
                    }

                    _streaming = false;

                    // Clear the picture box safely
                    if (streampbox1.Image != null)
                    {
                        var oldImage = streampbox1.Image;
                        streampbox1.Image = null;
                        oldImage.Dispose();
                    }

                    // Update UI
                    if (onoffbtn.InvokeRequired)
                    {
                        onoffbtn.Invoke((MethodInvoker)delegate {
                            onoffbtn.Text ="Start Streaming";
                            onoffbtn.ForeColor = Color.Black;
                            onoffbtn.BackColor = Color.Green;
                            onoffindentify.Text = "CAMERA IS OFF";
                            onoffindentify.ForeColor = Color.White;
                            onoffindentify.BackColor = Color.Red;
                        });
                    }
                    else
                    {
                        onoffbtn.Text = "Start Streaming";
                        onoffbtn.ForeColor = Color.Black;
                        onoffbtn.BackColor = Color.Green;
                        onoffindentify.Text = "CAMERA IS OFF";
                        onoffindentify.ForeColor = Color.White;
                        onoffindentify.BackColor = Color.Red;
                    }

                    errormassage("Camera is off");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error stopping stream: {ex.Message}");
                    errormassage($"Error stopping stream: {ex.Message}");
                }
            }
        }

        private void capturebtn_Click(object sender, EventArgs e)
        {
            if (streampbox1.Image == null)
            {
                MessageBox.Show("No image to save. Please capture an image first.");
                errormassage("No image to save. Please capture an image first.");
                return;
            }

            string dateTimeString = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string filename = $"captured_image_{dateTimeString}.jpg";
            string defaultSavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string fullPath = Path.Combine(defaultSavePath, filename);

            try
            {
                streampbox1.Image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving picture: {ex.Message}");
                errormassage($"Error saving picture: {ex.Message}");
            }
        }
        
        private void rotatestreambtn_Click(object sender, EventArgs e)
        {
            rotationAngleStream += 90;
            if (rotationAngleStream >= 360)
            {
                rotationAngleStream = 0;
            }
            Debug.WriteLine($"Rotation button clicked. New rotation angle: {rotationAngleStream}");

            switch (rotationAngleStream)
            {
                case 0:
                    rotatestreambtn.Text = "Rotate Stream";
                    rotatestreambtn.ForeColor = Color.Black;
                    rotatestreambtn.BackColor = Color.Gainsboro;
                    break;
                case 90:
                    rotatestreambtn.Text = "Rotate Stream (90°)";
                    rotatestreambtn.ForeColor = Color.White;
                    rotatestreambtn.BackColor = Color.Blue;
                    break;
                case 180:
                    rotatestreambtn.Text = "Rotate Stream (180°)";
                    rotatestreambtn.ForeColor = Color.Black;
                    rotatestreambtn.BackColor = Color.Yellow;
                    break;
                case 270:
                    rotatestreambtn.Text = "Rotate Stream (270°)";
                    rotatestreambtn.ForeColor = Color.White;
                    rotatestreambtn.BackColor = Color.DarkGreen;
                    break;
            }
        }
        private void SaveAppState()
        {
            try
            {
                string appDataPath = Application.StartupPath;  
                Directory.CreateDirectory(appDataPath);
                string settingsPath = Path.Combine(appDataPath, "settings.xml");

                // Save settings for ALL cameras
                var collection = new CameraSettingsCollection
                {
                    Entries = _cameraSettings.Select(kvp => new CameraSettingEntry
                    {
                        CameraName = kvp.Key,
                        Settings = new CameraSettings
                        {                 
                            FlipHorizontal = kvp.Value.FlipHorizontal,
                            FlipVertical = kvp.Value.FlipVertical,
                            RotationAngleStream = kvp.Value.RotationAngleStream,
                            RoiRect = kvp.Value.RoiRect,
                            HStartPercent = kvp.Value.HStartPercent,
                            HEndPercent = kvp.Value.HEndPercent,
                            VStartPercent = kvp.Value.VStartPercent,
                            VEndPercent = kvp.Value.VEndPercent,
                            CameraWidth = kvp.Value.CameraWidth,
                            CameraHeight = kvp.Value.CameraHeight,
                            Brightness = kvp.Value.Brightness,
                            Contrast = kvp.Value.Contrast,
                            Saturation = kvp.Value.Saturation,
                            Sharpness = kvp.Value.Sharpness,
                            ZoomLevel = kvp.Value.ZoomLevel,
                            ZoomFactor = kvp.Value.ZoomFactor,
                            ZoomCenterX = kvp.Value.ZoomCenterX,
                            ZoomCenterY = kvp.Value.ZoomCenterY
                        }
                    }).ToList()
                };

                var serializer = new XmlSerializer(typeof(CameraSettingsCollection));
                using (var writer = new StreamWriter(settingsPath))
                {
                    serializer.Serialize(writer, collection);
                }

                Debug.WriteLine("=== Saved Settings ===");
                foreach (var entry in collection.Entries)
                {
                    Debug.WriteLine($"Camera: {entry.CameraName}");
                    Debug.WriteLine($"- Brightness: {entry.Settings.BrightnessValue}");
                    Debug.WriteLine($"- Flip H: {entry.Settings.FlipHorizontal}, V: {entry.Settings.FlipVertical}");
                    Debug.WriteLine($"- Rotation: {entry.Settings.RotationAngleStream}°");
                    Debug.WriteLine($"- ROI: {entry.Settings.RoiRect}");
                    Debug.WriteLine($"- ROI Percentages: H({entry.Settings.HStartPercent}-{entry.Settings.HEndPercent}) V({entry.Settings.VStartPercent}-{entry.Settings.VEndPercent})");
                    Debug.WriteLine($"- Dimensions: {entry.Settings.CameraWidth}x{entry.Settings.CameraHeight}");
                    Debug.WriteLine("-------------------");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving application state: {ex.Message}");
                errormassage($"Error saving application state: {ex.Message}");
            }
        }

        private void LoadAppState()
        {
            try
            {
                string appDataPath = Application.StartupPath;
                string settingsPath = Path.Combine(appDataPath, "settings.xml");

                if (File.Exists(settingsPath))
                {
                    var serializer = new XmlSerializer(typeof(CameraSettingsCollection));
                    using (var reader = new StreamReader(settingsPath))
                    {
                        var collection = (CameraSettingsCollection)serializer.Deserialize(reader);

                        // Clear existing settings and load new ones
                        _cameraSettings.Clear();
                        foreach (var entry in collection.Entries)
                        {
                            if (entry.Settings.GetType().GetProperty("FlipStateStreamOne") != null)
                            {
                                var flipOne = (FlipState)entry.Settings.GetType().GetProperty("FlipStateStreamOne").GetValue(entry.Settings);
                                entry.Settings.FlipHorizontal = flipOne == FlipState.Horizontal || flipOne == FlipState.Both;
                                entry.Settings.FlipVertical = flipOne == FlipState.Vertical || flipOne == FlipState.Both;
                            }

                            _cameraSettings[entry.CameraName] = entry.Settings;
                        }
                    }

                    // Load current camera's settings
                    if (_cameraSettings.TryGetValue(currentCameraName, out var currentSettings))
                    {
                        // Update internal variables
                        brightness = currentSettings.Brightness;
                        contrast = currentSettings.Contrast;
                        saturation = currentSettings.Saturation;
                        sharpness = currentSettings.Sharpness;
                        zoomFactor = currentSettings.ZoomFactor;
                        zoomCenter = new PointF(currentSettings.ZoomCenterX, currentSettings.ZoomCenterY);
                        rotationAngleStream = currentSettings.RotationAngleStream;
                        roiRect = currentSettings.RoiRect;
                       

                        // Update UI controls
                        this.Invoke((MethodInvoker)delegate {
                            UpdateUIControls(currentSettings);
                            UpdateRotationButtonText();
                            UpdateFlipButtons();
                            UpdateRoiTextBoxes();
                        });

                        // Update camera dimensions display
                        getcwidth.Text = currentSettings.CameraWidth.ToString();
                        getchight.Text = currentSettings.CameraHeight.ToString();
                    }
                    else
                    { 
                        InitializeDefaultSettings();
                    }
                }
                else
                {
                    InitializeDefaultSettings();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading app state: {ex.Message}");
                errormassage($"Error loading settings: {ex.Message}");
                InitializeDefaultSettings();
            }
            UpdateFlipButtons();
        }
        private void UpdateUIControls(CameraSettings settings)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<CameraSettings>(UpdateUIControls), settings);
                return;
            }

            try
            {
                // Update scrollbars and validate ranges
                brighscroll.Value = Math.Max(brighscroll.Minimum, Math.Min(settings.Brightness, brighscroll.Maximum));
                contrastscroll.Value = Math.Max(contrastscroll.Minimum, Math.Min(settings.Contrast, contrastscroll.Maximum));
                saturationscroll.Value = Math.Max(saturationscroll.Minimum, Math.Min(settings.Saturation, saturationscroll.Maximum));
                zoombar.Value = Math.Max(zoombar.Minimum, Math.Min(settings.ZoomLevel, zoombar.Maximum));

                // Update labels
                brightness0lb.Text = brighscroll.Value.ToString();
                contrast0lb.Text = contrastscroll.Value.ToString();
                saturation0lb.Text = saturationscroll.Value.ToString();
                zoom0lb.Text = zoombar.Value.ToString();

                // Update flip checkboxes
                fliph2.Checked = settings.FlipHorizontal;
                flip1v.Checked = settings.FlipVertical;

                // Update ROI fields with validation
                hstarttb.Text = Math.Max(0, Math.Min(100, settings.HStartPercent)).ToString("0.##");
                hendtb.Text = Math.Max(0, Math.Min(100, settings.HEndPercent)).ToString("0.##");
                vstarttb.Text = Math.Max(0, Math.Min(100, settings.VStartPercent)).ToString("0.##");
                vendtb.Text = Math.Max(0, Math.Min(100, settings.VEndPercent)).ToString("0.##");

                // Update ROI button state
                setroi.BackColor = settings.RoiRect != Rectangle.Empty ? Color.LightGreen : SystemColors.Control;
                setroi.Text = settings.RoiRect != Rectangle.Empty ? "ROI Active" : "Set ROI";

                // Update camera dimensions display
                if (getcwidth != null && getchight != null)
                {
                    getcwidth.Text = settings.CameraWidth.ToString();
                    getchight.Text = settings.CameraHeight.ToString();
                }

                // Force immediate update if streaming
                if (_streaming)
                {
                    _frameTimer.Stop();
                    _frameTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating UI controls: {ex.Message}");
                errormassage($"UI update error: {ex.Message}");
            }
        }

        private void UpdateRoiTextBoxes()
        {
            if (_cameraSettings.TryGetValue(currentCameraName, out var settings))
            {
                hstarttb.Text = settings.HStartPercent.ToString("0.##");
                hendtb.Text = settings.HEndPercent.ToString("0.##");
                vstarttb.Text = settings.VStartPercent.ToString("0.##");
                vendtb.Text = settings.VEndPercent.ToString("0.##");
            }
        }      

        private void saveparameter_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate ROI values first
                if (!double.TryParse(hstarttb.Text, out double hStart) ||
                    !double.TryParse(hendtb.Text, out double hEnd) ||
                    !double.TryParse(vstarttb.Text, out double vStart) ||
                    !double.TryParse(vendtb.Text, out double vEnd))
                {
                    return;
                }

                if (hStart < 0 || hStart > 100 || hEnd < 0 || hEnd > 100 ||
                    vStart < 0 || vStart > 100 || vEnd < 0 || vEnd > 100)
                {
                    MessageBox.Show("ROI percentages must be between 0 and 100", "Invalid ROI Values",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save settings for CURRENT camera only
                SaveCurrentCameraSettings(currentCameraName);
                SaveAppState();

                MessageBox.Show("All parameters saved successfully!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving parameters: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                errormassage($"Error saving parameters: {ex.Message}");
            }
        }

        private void cameracollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ignore if no selection or same camera profile selected
            if (cameracollection.SelectedItem == null ||
                currentCameraName == cameracollection.SelectedItem.ToString())
            {
                return;
            }
            try
            {
                // 1. Save current settings before switching profiles
                SaveCurrentCameraSettings(currentCameraName);

                // 2. Store previous profile name for reference
                string previousProfile = currentCameraName;

                // 3. Update current profile name
                currentCameraName = cameracollection.SelectedItem.ToString();

                // 4. Initialize default settings if new profile doesn't exist
                if (!_cameraSettings.ContainsKey(currentCameraName))
                {
                    _cameraSettings[currentCameraName] = new CameraSettings
                    {
                        Brightness = 50,
                        Contrast = 50,
                        Saturation = 50,
                        Sharpness = 50,
                        ZoomLevel = 0,
                        ZoomFactor = 1.0f,
                        ZoomCenterX = 0,
                        ZoomCenterY = 0,
                        FlipHorizontal = false,
                        FlipVertical = false,
                        RotationAngleStream = 0,
                        RoiRect = Rectangle.Empty,
                        PreviousRoiRect = Rectangle.Empty,
                        CameraWidth = 0,   
                        CameraHeight = 0,  
                        HStartPercent = 0,
                        HEndPercent = 0,
                        VStartPercent = 0,
                        VEndPercent = 0
                    };
                }

                // 5. Load settings for the new profile
                LoadCameraSettings(currentCameraName);

                // 6. Calculate zoom bar value safely
                int zoomBarValue = 0;
                if (MAX_ZOOM_FACTOR > 1.0f && zoomFactor >= 1.0f)
                {
                    zoomBarValue = (int)(((zoomFactor - 1.0f) / (MAX_ZOOM_FACTOR - 1.0f)) * 100);
                    zoomBarValue = Math.Max(0, Math.Min(100, zoomBarValue));
                }

                // 7. Update UI controls on the main thread
                this.Invoke((MethodInvoker)delegate
                {
                    // Update scroll bars
                    brighscroll.Value = brightness;
                    contrastscroll.Value = contrast;
                    saturationscroll.Value = saturation;
                    zoombar.Value = zoomBarValue;

                    // Update labels
                    brightness0lb.Text = brightness.ToString();
                    contrast0lb.Text = contrast.ToString();
                    saturation0lb.Text = saturation.ToString();
                    zoom0lb.Text = zoomBarValue.ToString();

                    // Update flip buttons
                    fliph2.Checked = _cameraSettings[currentCameraName].FlipHorizontal;
                    flip1v.Checked = _cameraSettings[currentCameraName].FlipVertical;
                    UpdateFlipButtons();

                    // Update rotation button
                    UpdateRotationButtonText();

                    // Update ROI controls
                    UpdateRoiTextBoxes();
                    setroi.BackColor = roiRect != Rectangle.Empty ? Color.LightGreen : SystemColors.Control;
                    setroi.Text = roiRect != Rectangle.Empty ? "ROI Active" : "Set ROI";

                });

                // 8. Force redraw if streaming
                if (_streaming)
                {
                    _frameTimer.Stop();
                    _frameTimer.Start();
                }
                errormassage($"Switched to camera profile: {currentCameraName}");
                SaveAppState();
            }
            catch (Exception ex)
            {
                cameracollection.SelectedItem = currentCameraName;

                string errorMessage = $"Error switching to profile '{cameracollection.SelectedItem}': {ex.Message}";
                MessageBox.Show(errorMessage, "Profile Switch Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                errormassage(errorMessage);

                try
                {
                    LoadCameraSettings(currentCameraName);
                    if (_streaming)
                    {
                        _frameTimer.Stop();
                        _frameTimer.Start();
                    }
                }
                catch (Exception recoveryEx)
                {
                    errormassage($"Recovery failed: {recoveryEx.Message}");
                }
            }
        }

        private int GetCameraIndex(string cameraName)
        {
            switch (cameraName)
            {
                case "camera1": return 0;
                case "camera2": return 0;
                case "camera3": return 0;
                case "camera4": return 0;
                default: return 0;
            }
        }
        private void SaveCurrentCameraSettings(string cameraName)
        {
            if (string.IsNullOrEmpty(cameraName)) return;

            try
            {
                // Get or create settings for the current camera
                if (!_cameraSettings.TryGetValue(cameraName, out var settings))
                {
                    settings = new CameraSettings();
                    _cameraSettings[cameraName] = settings;
                }

                settings.Brightness = Math.Max(brighscroll.Minimum, Math.Min(brighscroll.Value, brighscroll.Maximum));
                settings.Contrast = Math.Max(contrastscroll.Minimum, Math.Min(contrastscroll.Value, contrastscroll.Maximum));
                settings.Saturation = Math.Max(saturationscroll.Minimum, Math.Min(saturationscroll.Value, saturationscroll.Maximum));
                settings.Sharpness =sharpness;
                settings.ZoomLevel = Math.Max(zoombar.Minimum, Math.Min(zoombar.Value, zoombar.Maximum));

                settings.ZoomFactor = zoomFactor;
                settings.ZoomCenterX = zoomCenter.X;
                settings.ZoomCenterY = zoomCenter.Y;

                settings.FlipHorizontal = fliph2.Checked;
                settings.FlipVertical = flip1v.Checked;

                settings.RotationAngleStream = rotationAngleStream;

                settings.RoiRect = roiRect;
                settings.PreviousRoiRect = previousRoiRect;

                if (double.TryParse(hstarttb.Text, out double hStart))
                    settings.HStartPercent = Math.Max(0, Math.Min(100, hStart));
                if (double.TryParse(hendtb.Text, out double hEnd))
                    settings.HEndPercent = Math.Max(0, Math.Min(100, hEnd));
                if (double.TryParse(vstarttb.Text, out double vStart))
                    settings.VStartPercent = Math.Max(0, Math.Min(100, vStart));
                if (double.TryParse(vendtb.Text, out double vEnd))
                    settings.VEndPercent = Math.Max(0, Math.Min(100, vEnd));

                settings.CameraWidth = cameraWidth;
                settings.CameraHeight = cameraHeight;

              
                Debug.WriteLine($"Settings saved for {cameraName}:");
                Debug.WriteLine($"- Brightness: {settings.Brightness}, Contrast: {settings.Contrast}");
                Debug.WriteLine($"- Saturation: {settings.Saturation}, Zoom: {settings.ZoomLevel}");
                Debug.WriteLine($"- Flip H: {settings.FlipHorizontal}, V: {settings.FlipVertical}");
                Debug.WriteLine($"- Rotation: {settings.RotationAngleStream}°");
                Debug.WriteLine($"- ROI: {settings.RoiRect}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving camera settings: {ex.Message}");
                errormassage($"Error saving settings for {cameraName}: {ex.Message}");
            }
        }
        private void InitializeDefaultSettings()
        {
            try
            {
                var defaultSettings = new CameraSettings
                {
                    Brightness = 50,
                    Contrast = 50,
                    Saturation = 50,
                    Sharpness = 50,
                    ZoomLevel = 0,
                    ZoomFactor = 1.0f,
                    ZoomCenterX = 0,
                    ZoomCenterY = 0,
                    FlipHorizontal = false,
                    FlipVertical = false,
                    RotationAngleStream = 0,
                    RoiRect = Rectangle.Empty,
                    PreviousRoiRect = Rectangle.Empty,
                    HStartPercent = 0,
                    HEndPercent = 0,
                    VStartPercent = 0,
                    VEndPercent = 0,
                    CameraWidth = cameraWidth,
                    CameraHeight = cameraHeight,
                    BrightnessValue = 0
                };

                // Initialize all cameras with default settings if they don't exist
                var cameraNames = new[] { "camera1", "camera2", "camera3", "camera4" };
                foreach (var name in cameraNames)
                {
                    if (!_cameraSettings.ContainsKey(name))
                    {
                        _cameraSettings[name] = defaultSettings;
                    }
                }

                // Update current camera's settings
                if (_cameraSettings.TryGetValue(currentCameraName, out var currentSettings))
                {
                    // Update internal variables
                    brightness = currentSettings.Brightness;
                    contrast = currentSettings.Contrast;
                    saturation = currentSettings.Saturation;
                    sharpness = currentSettings.Sharpness;
                    zoomFactor = currentSettings.ZoomFactor;
                    zoomCenter = new PointF(currentSettings.ZoomCenterX, currentSettings.ZoomCenterY);
                    rotationAngleStream = currentSettings.RotationAngleStream;
                    roiRect = currentSettings.RoiRect;
                    previousRoiRect = currentSettings.PreviousRoiRect;
             

                    // Update UI
                    this.Invoke((MethodInvoker)delegate {
                        UpdateUIControls(currentSettings);
                        UpdateRotationButtonText();
                        UpdateFlipButtons();
                        UpdateRoiTextBoxes();
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing default settings: {ex.Message}");
                errormassage($"Error initializing default settings: {ex.Message}");
            }
        }
        
        private void UpdateRotationButtonText()
        {
            switch (rotationAngleStream)
            {
                case 0:
                    rotatestreambtn.Text = "Rotate Stream";
                    rotatestreambtn.ForeColor = Color.Black;
                    rotatestreambtn.BackColor = NormalButtonColor;
                    break;
                case 90:
                    rotatestreambtn.Text = "Rotate Stream (90°)";
                    rotatestreambtn.ForeColor = Color.White;
                    rotatestreambtn.BackColor = Color.Blue;
                    break;
                case 180:
                    rotatestreambtn.Text = "Rotate Stream (180°)";
                    rotatestreambtn.ForeColor = Color.Black;
                    rotatestreambtn.BackColor = Color.Yellow;
                    break;
                case 270:
                    rotatestreambtn.Text = "Rotate Stream (270°)";
                    rotatestreambtn.ForeColor = Color.White;
                    rotatestreambtn.BackColor = Color.DarkGreen;
                    break;
            }
        }
        private void LoadCameraSettings(string cameraName)
        {
            if (_cameraSettings.TryGetValue(cameraName, out var settings))
            {
                // Image adjustments
                brighscroll.Value = settings.Brightness;
                contrastscroll.Value = settings.Contrast;
                saturationscroll.Value = settings.Saturation;
                zoombar.Value = settings.ZoomLevel;

                brightness = settings.Brightness;
                contrast = settings.Contrast;
                saturation = settings.Saturation;
                sharpness = settings.Sharpness;
                zoomFactor = settings.ZoomFactor;
                zoomCenter = new PointF(settings.ZoomCenterX, settings.ZoomCenterY);

                // Update labels
                brightness0lb.Text = settings.Brightness.ToString();
                contrast0lb.Text = settings.Contrast.ToString();
                saturation0lb.Text = settings.Saturation.ToString();
                zoom0lb.Text = settings.ZoomLevel.ToString();

                // Camera properties
            
                rotationAngleStream = settings.RotationAngleStream;
                roiRect = settings.RoiRect;
              

                // Update UI elements
                fliph2.Checked = settings.FlipHorizontal;
                flip1v.Checked = settings.FlipVertical;
                UpdateFlipButtons();
                UpdateRotationButtonText();
                UpdateRoiTextBoxes();

                // Update camera dimensions display
                getcwidth.Text = settings.CameraWidth.ToString();
                getchight.Text = settings.CameraHeight.ToString();
            }
            else
            {
                // Initialize default values for new profile
                brighscroll.Value = 50;
                contrastscroll.Value = 50;
                saturationscroll.Value = 50;
                zoombar.Value = 0;

                brightness0lb.Text = "50";
                contrast0lb.Text = "50";
                saturation0lb.Text = "50";
                zoom0lb.Text = "0";

                brightness = 50;
                contrast = 50;
                saturation = 50;
                sharpness = 50;
                zoomFactor = 1.0f;
                zoomCenter = PointF.Empty;
                rotationAngleStream = 0;
                roiRect = Rectangle.Empty;
                previousRoiRect = Rectangle.Empty;

                fliph2.Checked = false;
                flip1v.Checked = false;

                UpdateFlipButtons();
                UpdateRotationButtonText();
                UpdateRoiTextBoxes();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (infobtn.BackColor == SelectedButtonColor)
            {
                MessageBox.Show("You are already in the Info section!", "Info",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                errormassage("You are already in the Info section!");
                return;
            }

            ResetNavigationButtons();
            infobtn.BackColor = SelectedButtonColor;

            panelInfo.Visible = true;
            clearlogbtn.Visible = true;
            savelogbtn.Visible = true;
            txtErrorDisplay.Visible = true;
            errorloglb.Visible = true;

            camerasettinglb.Visible = false;
            rotatestreambtn.Visible = false;
            saveparameter.Visible = false;
            switchcamera.Visible = false;
            cameracollection.Visible = false;

            label1.Visible = false;
            startroi.Visible = false;
            endroi.Visible = false;
            roi1.Visible = false;
            hstarttb.Visible = false;
            hendtb.Visible = false;
            roi2.Visible = false;
            vstarttb.Visible = false;
            vendtb.Visible = false;
            setroi.Visible = false;

            outcameralb.Visible = false;
            cwidth.Visible = false;
            cwdescription.Visible = false;
            cheight.Visible = false;
            chdescription.Visible = false;
            getcwidth.Visible = false;
            getchight.Visible = false;
            capturebtn.Visible = false;
            onoffbtn.Visible = false;

            adjustlb.Visible = false;
            saturationscroll.Visible = false;
            brighscroll.Visible = false;
            saturationlb.Visible = false;
            brightlb.Visible = false;
            contrastlb.Visible = false;
            contrastscroll.Visible = false;
            zoombar.Visible = false;
            zoombar.Visible = false;
            zoomlb.Visible = false;
            adjustclrear.Visible = false;

            brightsvalue1.Visible = false;
            brightevalue.Visible = false;
            saturationsvalue1.Visible = false;
            saturationevalue.Visible = false;
            contrastsvalue1.Visible = false;
            contrastevalue.Visible = false;
            zoomsvalue1.Visible = false;
            zoomevalue.Visible = false;

            brightness0lb.Visible = false;
            saturation0lb.Visible = false;
            contrast0lb.Visible = false;
            zoom0lb.Visible = false;
            fliph2.Visible = false;
            flip1v.Visible = false;
            cameraselectbox.Visible = false;
            cameras.Visible = false;
            fpslb.Visible = false;
            fpslb2.Visible = false;

            UpdateErrorDisplay();

            txtErrorDisplay.Text = $"=== Today's Errors ({DateTime.Today:yyyy-MM-dd}) ===\r\n\r\n" + txtErrorDisplay.Text;
        }

        private void setupbtn_Click(object sender, EventArgs e)
        {
            if (setupbtn.BackColor == SelectedButtonColor)
            {
                MessageBox.Show("You are already in Setup!", "Setup",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                errormassage("You are already in Setup!");
                return;
            }
            using (password1 passwordForm = new password1())
            {
                DialogResult result = passwordForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (passwordForm.EnteredPassword == "123")  
                    {
                        label1.Visible = true;
                        startroi.Visible = true;
                        endroi.Visible = true;
                        roi1.Visible = true;
                        hstarttb.Visible = true;
                        hendtb.Visible = true;
                        roi2.Visible = true;
                        vstarttb.Visible = true;
                        vendtb.Visible = true;
                        setroi.Visible = true;

                        camerasettinglb.Visible = true;
                        rotatestreambtn.Visible = true;
                        saveparameter.Visible = true;
                        switchcamera.Visible = true;
                        cameracollection.Visible = true;

                        errorloglb.Visible = false;
                        clearlogbtn.Visible = false;
                        txtErrorDisplay.Visible = false;
                        savelogbtn.Visible = false;

                        outcameralb.Visible = false;
                        cwidth.Visible = false;
                        cwdescription.Visible = false;
                        cheight.Visible = false;
                        chdescription.Visible = false;
                        getcwidth.Visible = false;
                        getchight.Visible = false;
                        capturebtn.Visible = false;
                        onoffbtn.Visible = false;

                        adjustlb.Visible = true;
                        saturationscroll.Visible = true;
                        brighscroll.Visible = true;
                        saturationlb.Visible = true;
                        brightlb.Visible = true;
                        contrastlb.Visible = true;
                        contrastscroll.Visible = true;
                        zoombar.Visible = true;
                        zoombar.Visible = true;
                        adjustclrear.Visible = true;

                        brightsvalue1.Visible = true;
                        brightevalue.Visible = true;
                        saturationsvalue1.Visible = true;
                        saturationevalue.Visible = true;
                        contrastsvalue1.Visible = true;
                        contrastevalue.Visible = true;
                        zoomsvalue1.Visible = true;
                        zoomevalue.Visible = true;
                        zoomlb.Visible = true;

                        brightness0lb.Visible = true;
                        saturation0lb.Visible = true;
                        contrast0lb.Visible = true;
                        zoom0lb.Visible = true;

                        fliph2.Visible = true;
                        flip1v.Visible = true;

                        cameraselectbox.Visible = false;
                        cameras.Visible = false;
                        fpslb.Visible = false;
                        fpslb2.Visible = false;

                        ResetNavigationButtons();
                        setupbtn.BackColor = SelectedButtonColor;

                    }
                    else 
                    {
                        MessageBox.Show("Incorrect password. Setup cannot proceed.", "Error",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        errormassage("Incorrect password. Setup cannot proceed.");
                    }
                }
            }
        }

        private void CheckCameraConnection()
        {
            try
            {
                using (var testCapture = new Capture(GetCameraIndex(currentCameraName)))
                using (var testFrame = testCapture.QueryFrame())
                {
                    if (testFrame == null)
                    {
                        string errorMsg = "Camera connected but not responding (no frame)";
                        SetConnectionStatus(false, true);
                        errormassage(errorMsg);
                    }
                    else
                    {
                        SetConnectionStatus(true);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Camera connection failed: {ex.Message}";
                SetConnectionStatus(false, true);
                errormassage(errorMsg);
            }
        }

        private void SetConnectionStatus(bool isConnected, bool immediateDisconnect = false)
        {
            if (crtorwrongpb.InvokeRequired || error_or_no.InvokeRequired ||
                errormsg.InvokeRequired || whaterror.InvokeRequired)
            {
                this.Invoke(new Action<bool, bool>(SetConnectionStatus), isConnected, immediateDisconnect);
                return;
            }

            if (isConnected && availableCameras.Count > 0)
            {
                crtorwrongpb.BackColor = Color.Green;
                error_or_no.Text = "READY";
                error_or_no.ForeColor = Color.White;
                error_or_no.BackColor = Color.Green;
                errormsg.Text = "• Camera connected successfully";
                errormsg.ForeColor = Color.LightSeaGreen;
                whaterror.Text = "• System operational";
                whaterror.ForeColor = Color.LightSeaGreen;
                onoffbtn.Enabled = true;
            }
            else
            {
                crtorwrongpb.BackColor = Color.Red;
                error_or_no.Text = "ERROR";
                error_or_no.ForeColor = Color.White;
                error_or_no.BackColor = Color.Red;

                errormsg.Text = "• Camera connection failed";
                errormsg.ForeColor = Color.Red;

                whaterror.Text = immediateDisconnect
                    ? "• Please check: USB cable"
                    : "• No cameras available";
                whaterror.ForeColor = Color.Red;

                onoffbtn.Enabled = false;

                if (immediateDisconnect)
                {
                    errormassage("Camera was suddenly disconnected - please check USB cable");
                }
            }
        }

        private void setroi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(hstarttb.Text, out double hStart) ||
                    !double.TryParse(hendtb.Text, out double hEnd) ||
                    !double.TryParse(vstarttb.Text, out double vStart) ||
                    !double.TryParse(vendtb.Text, out double vEnd))
                {
                    MessageBox.Show("Please enter valid numeric values for all ROI fields.");
                    return;
                }

                // Validate percentages
                if (hStart < 0 || hStart > 100 || hEnd < 0 || hEnd > 100 ||
                    vStart < 0 || vStart > 100 || vEnd < 0 || vEnd > 100)
                {
                    MessageBox.Show("Percentage values must be between 0 and 100");
                    return;
                }

                if (hStart >= hEnd || vStart >= vEnd)
                {
                    MessageBox.Show("Start values must be less than end values");
                    return;
                }

                // Calculate pixel coordinates
                int startX = (int)(hStart / 100 * cameraWidth);
                int endX = (int)(hEnd / 100 * cameraWidth);
                int startY = (int)(vStart / 100 * cameraHeight);
                int endY = (int)(vEnd / 100 * cameraHeight);

                roiRect = new Rectangle(startX, startY, endX - startX, endY - startY);

                // Update UI feedback
                setroi.BackColor = Color.LightGreen;
                setroi.Text = "ROI Active";

                // Force immediate update
                if (_streaming)
                {
                    _frameTimer.Stop();
                    _frameTimer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting ROI: {ex.Message}");
            }
        }

        private void clearlogbtn_Click(object sender, EventArgs e)
        {
            errorLogBuffer.Clear();
            txtErrorDisplay.Clear();
        }

        private void all_Click(object sender, EventArgs e)
        {
            if (all.BackColor == SelectedButtonColor)
            {
                return;
            }
    
            ResetNavigationButtons();

            all.BackColor = SelectedButtonColor;

            outcameralb.Visible = true;
            cwidth.Visible = true;
            cwdescription.Visible = true;
            cheight.Visible = true;
            chdescription.Visible = true;

            label1.Visible = false;
            startroi.Visible = false;
            endroi.Visible = false;
            roi1.Visible = false;
            hstarttb.Visible = false;
            hendtb.Visible = false;
            roi2.Visible = false;
            vstarttb.Visible = false;
            vendtb.Visible = false;
            setroi.Visible = false;

            camerasettinglb.Visible = false;
            rotatestreambtn.Visible = false;
            saveparameter.Visible = false;
            switchcamera.Visible = false;
            cameracollection.Visible = false;

            errorloglb.Visible = false;
            clearlogbtn.Visible = false;
            txtErrorDisplay.Visible = false;
            savelogbtn.Visible = false;

            capturebtn.Visible = true;
            onoffbtn.Visible = true;
            getcwidth.Visible = true;
            getchight.Visible = true;

            adjustlb.Visible = false;
            saturationscroll.Visible = false;
            brighscroll.Visible = false;
            saturationlb.Visible = false;
            brightlb.Visible = false;
            contrastlb.Visible = false;
            contrastscroll.Visible = false;
            zoombar.Visible = false;
            zoomlb.Visible = false;
            adjustclrear.Visible = false;

            brightsvalue1.Visible = false;
            brightevalue.Visible = false;
            saturationsvalue1.Visible = false;
            saturationevalue.Visible = false;
            contrastsvalue1.Visible = false;
            contrastevalue.Visible = false;
            zoomsvalue1.Visible = false;
            zoomevalue.Visible = false;

            brightness0lb.Visible = false;
            saturation0lb.Visible = false;
            contrast0lb.Visible = false;
            zoom0lb.Visible = false;

            fliph2.Visible = false;
            flip1v.Visible = false;

            cameraselectbox.Visible = true;
            cameras.Visible = true;
            fpslb.Visible = true;
            fpslb2.Visible = true;
        }
       
        private void adjustclrear_Click(object sender, EventArgs e)
        {
            try
            {
                brightness = 50;
                contrast = 50;
                saturation = 50;
                sharpness = 50;
                zoomFactor = 1.0f;
                zoomCenter = PointF.Empty;

                if (_cameraSettings.TryGetValue(currentCameraName, out var settings))
                {
                    settings.FlipHorizontal = false;
                    settings.FlipVertical = false;
                    settings.Brightness = 50;
                    settings.Contrast = 50;
                    settings.Saturation = 50;
                    settings.Sharpness = 50;
                    settings.ZoomFactor = 1.0f;
                    settings.ZoomCenterX = 0;
                    settings.ZoomCenterY = 0;
                }
                rotationAngleStream = 0;

                if (_cameraSettings.TryGetValue(currentCameraName, out var currentSettings))
                {
                    if (currentSettings.RoiRect != Rectangle.Empty)
                    {
                        currentSettings.PreviousRoiRect = currentSettings.RoiRect;
                        previousRoiRect = currentSettings.PreviousRoiRect;
                    }
                    currentSettings.RoiRect = Rectangle.Empty;
                    roiRect = Rectangle.Empty;

                    currentSettings.HStartPercent = 0;
                    currentSettings.HEndPercent = 0;
                    currentSettings.VStartPercent = 0;
                    currentSettings.VEndPercent = 0;
                }

                flip1v.Checked = false;
                fliph2.Checked = false;

                brighscroll.Value = 50;
                contrastscroll.Value = 50;
                saturationscroll.Value = 50;
                zoombar.Value = 0;

                brightness0lb.Text = "50";
                contrast0lb.Text = "50";
                saturation0lb.Text = "50";
                zoom0lb.Text = "0";

                hstarttb.Clear();
                hendtb.Clear();
                vstarttb.Clear();
                vendtb.Clear();

                setroi.BackColor = SystemColors.Control;
                setroi.Text = "Set ROI";

                UpdateFlipButtons();
                UpdateRotationButtonText();

                SaveCurrentCameraSettings(currentCameraName);
                SaveAppState();

                if (_streaming)
                {
                    _frameTimer.Stop();
                    _frameTimer.Start();
                }
                errormassage($"Reset all adjustments for camera: {currentCameraName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resetting adjustments: {ex.Message}");
                errormassage($"Adjustment reset failed: {ex.Message}");
            }
        }
        private void closebtn_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Are you sure you want to close the application?",
               "Confirm Exit",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning,
               MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void fliph2_Click(object sender, EventArgs e)
        {
            if (!_cameraSettings.TryGetValue(currentCameraName, out var settings))
            {
                settings = new CameraSettings();
                _cameraSettings[currentCameraName] = settings;
            }

            settings.FlipHorizontal = !settings.FlipHorizontal;
            UpdateFlipButtons();

            SaveCurrentCameraSettings(currentCameraName);
            SaveAppState();
        }

        private void flip1v_Click(object sender, EventArgs e)
        {
            if (!_cameraSettings.TryGetValue(currentCameraName, out var settings))
            {
                settings = new CameraSettings();
                _cameraSettings[currentCameraName] = settings;
            }

            settings.FlipVertical = !settings.FlipVertical;
            UpdateFlipButtons();
            SaveCurrentCameraSettings(currentCameraName);
            SaveAppState();
        }
        private void UpdateFlipButtons()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateFlipButtons));
                return;
            }

            if (_cameraSettings.TryGetValue(currentCameraName, out var settings))
            {
                fliph2.Checked = settings.FlipHorizontal;
                flip1v.Checked = settings.FlipVertical;

                fliph2.BackColor = settings.FlipHorizontal ? Color.LightGreen : NormalButtonColor;
                flip1v.BackColor = settings.FlipVertical ? Color.LightGreen : NormalButtonColor;
            }
            else
            {
                fliph2.Checked = false;
                flip1v.Checked = false;
                fliph2.BackColor = NormalButtonColor;
                flip1v.BackColor = NormalButtonColor;
            }
        }

        private void PopulatePhysicalCameras()
        {
            var newCameras = new List<string>();
            bool camerasChanged = false;

            try
            {
                var devices = new List<DirectShowLib.DsDevice>(DirectShowLib.DsDevice.GetDevicesOfCat(DirectShowLib.FilterCategory.VideoInputDevice));

                for (int i = 0; i < devices.Count; i++)
                {
                    newCameras.Add(devices[i].Name);
                }

                // Fallback for non-DirectShow cameras
                if (newCameras.Count == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            using (var cap = new Capture(i))
                            {
                                var frame = cap.QueryFrame();
                                if (frame != null)
                                {
                                    newCameras.Add($"Camera {i}");
                                }
                            }
                        }
                        catch { }
                    }
                }

                // Check if camera list changed
                camerasChanged = !newCameras.SequenceEqual(availableCameras);
                availableCameras = newCameras;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Camera detection error: {ex.Message}");
            }

            if (camerasChanged)
            {
                this.Invoke((MethodInvoker)delegate {
                    string previouslySelected = cameraselectbox.SelectedItem?.ToString();

                    cameraselectbox.BeginUpdate();
                    cameraselectbox.Items.Clear();

                    foreach (var cam in availableCameras)
                    {
                        cameraselectbox.Items.Add(cam);
                    }

                    // Try to maintain selection if the camera still exists
                    if (!string.IsNullOrEmpty(previouslySelected))
                    {
                        int index = availableCameras.IndexOf(previouslySelected);
                        if (index >= 0)
                        {
                            cameraselectbox.SelectedIndex = index;
                        }
                        else if (cameraselectbox.Items.Count > 0)
                        {
                            cameraselectbox.SelectedIndex = 0;
                        }
                    }
                    else if (cameraselectbox.Items.Count > 0)
                    {
                        cameraselectbox.SelectedIndex = 0;
                    }

                    cameraselectbox.EndUpdate();
                });
            }
        }
        private void HandleLastCameraDisconnected()
        {
            try
            {
                // Remember streaming state
                _wasStreamingBeforeDisconnect = _streaming;

                StopStreaming();
                 
                if (streampbox1.Image != null)
                {
                    streampbox1.Image.Dispose();
                    streampbox1.Image = null;
                }

                MessageBox.Show("All cameras have been disconnected. Streaming will automatically resume when a camera is reconnected.",
                               "Camera Disconnected",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);

                SetConnectionStatus(false);
                onoffbtn.Enabled = false;
                getcwidth.Text = "0";
                getchight.Text = "0";
                fpslb2.Text = "0";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling camera disconnect: {ex.Message}");
            }
        }

        private void cameraselectbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cameraselectbox.SelectedItem == null) return;

            try
            {
                bool wasStreaming = _streaming;
                if (_streaming)
                {
                    StopStreaming();
                }

                // Clean up previous capture
                if (capture != null)
                {
                    capture.Dispose();
                    capture = null;
                }

                // Initialize new capture device
                int selectedIndex = cameraselectbox.SelectedIndex;
                capture = new Capture(selectedIndex);

                
                cameraWidth = capture.Width;
                cameraHeight = capture.Height;
                getcwidth.Text = cameraWidth.ToString();
                getchight.Text = cameraHeight.ToString();

                // Update settings for the current profile with new camera dimensions
                if (_cameraSettings.TryGetValue(currentCameraName, out var settings))
                {
                    settings.CameraWidth = cameraWidth;
                    settings.CameraHeight = cameraHeight;
                }

                // Restart stream if it was running
                if (wasStreaming)
                {
                    _streaming = true;
                    _frameTimer.Start();

                    this.Invoke((MethodInvoker)delegate {
                        onoffbtn.Text = "Stop Streaming";
                        onoffbtn.ForeColor = Color.Black;
                        onoffbtn.BackColor = Color.Red;
                        onoffindentify.Text = "CAMERA IS ON";
                        onoffindentify.BackColor = Color.Green;
                    });
                }

                errormassage($"Physical camera changed to: {cameraselectbox.SelectedItem}");
            }
            catch (Exception ex)
            {
                errormassage($"Physical camera switch error: {ex.Message}");

                // Attempt fallback to first available camera
                if (cameraselectbox.Items.Count > 0)
                {
                    cameraselectbox.SelectedIndex = 0;
                }
                else
                {
                    SetConnectionStatus(false);
                }
            }
        }
        private Bitmap RotateImage(Bitmap b, int angle)
        {      
            if (angle % 360 == 0) return (Bitmap)b.Clone();
            Bitmap rotated;
            if (angle % 180 == 90)
            {
                rotated = new Bitmap(b.Height, b.Width);
            }
            else
            {
                rotated = new Bitmap(b.Width, b.Height);
            }

            using (Graphics g = Graphics.FromImage(rotated))
            {
                // Set up rotation
                g.TranslateTransform(rotated.Width / 2f, rotated.Height / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-b.Width / 2f, -b.Height / 2f);

                // Draw with high quality
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(b, new Point(0, 0));
            }

            return rotated;
        }
        private void InitializeCameraDetection()
        {
            cameraCheckTimer = new System.Windows.Forms.Timer(); 
            cameraCheckTimer.Interval = 1000;
            cameraCheckTimer.Tick += CheckForCameraChanges;
            cameraCheckTimer.Start();

            CheckForCameraChanges(null, EventArgs.Empty);
        }

        private void CheckForCameraChanges(object sender, EventArgs e)
        {
            try
            {
                int currentCount = CountAvailableCameras();
                bool cameraDisconnected = currentCount < lastCameraCount;
                bool cameraReconnected = currentCount > lastCameraCount;

                if (currentCount != lastCameraCount)
                {
                    lastCameraCount = currentCount;
                    this.Invoke((MethodInvoker)delegate {
                        string previouslySelected = cameraselectbox.SelectedItem?.ToString();
                        PopulatePhysicalCameras();

                        if (cameraDisconnected && currentCount == 0)
                        {
                            HandleLastCameraDisconnected();
                        }
                        else if (cameraReconnected && currentCount > 0)
                        {
                            HandleCameraReconnected(previouslySelected);
                        }

                        if (cameraselectbox.SelectedItem == null && cameraselectbox.Items.Count > 0)
                        {
                            cameraselectbox.SelectedIndex = 0;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Camera detection error: {ex.Message}");
            }
        }

        private void HandleCameraReconnected(string previouslySelectedCamera)
        {
            try
            {
                // Try to select the previously used camera if available
                if (!string.IsNullOrEmpty(previouslySelectedCamera))
                {
                    int index = availableCameras.IndexOf(previouslySelectedCamera);
                    if (index >= 0)
                    {
                        cameraselectbox.SelectedIndex = index;
                    }
                    else if (cameraselectbox.Items.Count > 0)
                    {
                        cameraselectbox.SelectedIndex = 0;
                    }
                }

                // Automatically restart streaming if it was active before disconnection
                if (!_streaming && _wasStreamingBeforeDisconnect)
                {
                    _wasStreamingBeforeDisconnect = false;
                    onoffbtn.PerformClick();
                }

                SetConnectionStatus(true);
                errormassage("Camera reconnected and streaming resumed");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling camera reconnection: {ex.Message}");
                errormassage($"Camera reconnection error: {ex.Message}");
            }
        }

        private int CountAvailableCameras()
        {
            try
            {
                // Try DirectShow first
                var devices = new List<DirectShowLib.DsDevice>(
                    DirectShowLib.DsDevice.GetDevicesOfCat(DirectShowLib.FilterCategory.VideoInputDevice));
                if (devices.Count > 0) return devices.Count;

                // Fallback method
                int count = 0;
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        using (var cap = new Capture(i))
                        {
                            var frame = cap.QueryFrame();
                            if (frame != null) count++;
                        }
                    }
                    catch { }
                }
                return count;
            }
            catch
            {
                return 0;
            }
        }     
    }
}