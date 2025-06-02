
namespace webcamproject
{
    partial class webcamera
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(webcamera));
            this.capturebtn = new System.Windows.Forms.Button();
            this.onoffbtn = new System.Windows.Forms.Button();
            this.headingbox = new System.Windows.Forms.GroupBox();
            this.kmllogo = new System.Windows.Forms.PictureBox();
            this.cnamelb = new System.Windows.Forms.Label();
            this.vertionlb = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.errorbox = new System.Windows.Forms.GroupBox();
            this.all = new System.Windows.Forms.Button();
            this.onoffindentify = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.setupbtn = new System.Windows.Forms.Button();
            this.infobtn = new System.Windows.Forms.Button();
            this.homebox = new System.Windows.Forms.GroupBox();
            this.whaterror = new System.Windows.Forms.Label();
            this.errormsg = new System.Windows.Forms.Label();
            this.error_or_no = new System.Windows.Forms.Label();
            this.crtorwrongpb = new System.Windows.Forms.PictureBox();
            this.setroi = new System.Windows.Forms.Button();
            this.vendtb = new System.Windows.Forms.TextBox();
            this.hendtb = new System.Windows.Forms.TextBox();
            this.vstarttb = new System.Windows.Forms.TextBox();
            this.hstarttb = new System.Windows.Forms.TextBox();
            this.startroi = new System.Windows.Forms.Label();
            this.roi2 = new System.Windows.Forms.Label();
            this.endroi = new System.Windows.Forms.Label();
            this.roi1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorloglb = new System.Windows.Forms.Label();
            this.cameracollection = new System.Windows.Forms.ComboBox();
            this.saveparameter = new System.Windows.Forms.Button();
            this.rotatestreambtn = new System.Windows.Forms.Button();
            this.camerasettinglb = new System.Windows.Forms.Label();
            this.switchcamera = new System.Windows.Forms.Label();
            this.txtErrorDisplay = new System.Windows.Forms.TextBox();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.fpslb2 = new System.Windows.Forms.Label();
            this.fpslb = new System.Windows.Forms.Label();
            this.cameraselectbox = new System.Windows.Forms.ComboBox();
            this.fliph2 = new System.Windows.Forms.CheckBox();
            this.flip1v = new System.Windows.Forms.CheckBox();
            this.brightsvalue1 = new System.Windows.Forms.Label();
            this.saturationsvalue1 = new System.Windows.Forms.Label();
            this.contrastsvalue1 = new System.Windows.Forms.Label();
            this.zoomsvalue1 = new System.Windows.Forms.Label();
            this.saturation0lb = new System.Windows.Forms.Label();
            this.contrast0lb = new System.Windows.Forms.Label();
            this.zoom0lb = new System.Windows.Forms.Label();
            this.brightness0lb = new System.Windows.Forms.Label();
            this.zoomevalue = new System.Windows.Forms.Label();
            this.contrastevalue = new System.Windows.Forms.Label();
            this.saturationevalue = new System.Windows.Forms.Label();
            this.brightevalue = new System.Windows.Forms.Label();
            this.adjustclrear = new System.Windows.Forms.Button();
            this.zoombar = new System.Windows.Forms.TrackBar();
            this.zoomlb = new System.Windows.Forms.Label();
            this.contrastscroll = new System.Windows.Forms.TrackBar();
            this.contrastlb = new System.Windows.Forms.Label();
            this.saturationscroll = new System.Windows.Forms.TrackBar();
            this.brighscroll = new System.Windows.Forms.TrackBar();
            this.saturationlb = new System.Windows.Forms.Label();
            this.brightlb = new System.Windows.Forms.Label();
            this.adjustlb = new System.Windows.Forms.Label();
            this.getcwidth = new System.Windows.Forms.Label();
            this.getchight = new System.Windows.Forms.Label();
            this.chdescription = new System.Windows.Forms.Label();
            this.cheight = new System.Windows.Forms.Label();
            this.cwdescription = new System.Windows.Forms.Label();
            this.cwidth = new System.Windows.Forms.Label();
            this.outcameralb = new System.Windows.Forms.Label();
            this.savelogbtn = new System.Windows.Forms.Button();
            this.clearlogbtn = new System.Windows.Forms.Button();
            this.streampbox1 = new System.Windows.Forms.PictureBox();
            this.cameras = new System.Windows.Forms.Label();
            this.headingbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kmllogo)).BeginInit();
            this.errorbox.SuspendLayout();
            this.homebox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crtorwrongpb)).BeginInit();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoombar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastscroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationscroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brighscroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.streampbox1)).BeginInit();
            this.SuspendLayout();
            // 
            // capturebtn
            // 
            this.capturebtn.BackColor = System.Drawing.Color.Ivory;
            this.capturebtn.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capturebtn.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.capturebtn.Location = new System.Drawing.Point(264, 463);
            this.capturebtn.Name = "capturebtn";
            this.capturebtn.Size = new System.Drawing.Size(99, 42);
            this.capturebtn.TabIndex = 1;
            this.capturebtn.Text = "SAVE";
            this.capturebtn.UseVisualStyleBackColor = false;
            this.capturebtn.Click += new System.EventHandler(this.capturebtn_Click);
            // 
            // onoffbtn
            // 
            this.onoffbtn.BackColor = System.Drawing.Color.Ivory;
            this.onoffbtn.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onoffbtn.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.onoffbtn.Location = new System.Drawing.Point(36, 463);
            this.onoffbtn.Name = "onoffbtn";
            this.onoffbtn.Size = new System.Drawing.Size(99, 42);
            this.onoffbtn.TabIndex = 0;
            this.onoffbtn.Text = "CAMERA ON/OFF";
            this.onoffbtn.UseVisualStyleBackColor = false;
            this.onoffbtn.Click += new System.EventHandler(this.onoffbtn_Click);
            // 
            // headingbox
            // 
            this.headingbox.Controls.Add(this.kmllogo);
            this.headingbox.Controls.Add(this.cnamelb);
            this.headingbox.Controls.Add(this.vertionlb);
            this.headingbox.Location = new System.Drawing.Point(2, -5);
            this.headingbox.Name = "headingbox";
            this.headingbox.Size = new System.Drawing.Size(537, 85);
            this.headingbox.TabIndex = 8;
            this.headingbox.TabStop = false;
            // 
            // kmllogo
            // 
            this.kmllogo.Image = ((System.Drawing.Image)(resources.GetObject("kmllogo.Image")));
            this.kmllogo.Location = new System.Drawing.Point(10, 14);
            this.kmllogo.Name = "kmllogo";
            this.kmllogo.Size = new System.Drawing.Size(271, 64);
            this.kmllogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.kmllogo.TabIndex = 18;
            this.kmllogo.TabStop = false;
            // 
            // cnamelb
            // 
            this.cnamelb.AutoSize = true;
            this.cnamelb.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnamelb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.cnamelb.Location = new System.Drawing.Point(295, 27);
            this.cnamelb.Name = "cnamelb";
            this.cnamelb.Size = new System.Drawing.Size(237, 22);
            this.cnamelb.TabIndex = 12;
            this.cnamelb.Text = "KML SENSORS (REDEFINED)";
            // 
            // vertionlb
            // 
            this.vertionlb.AutoSize = true;
            this.vertionlb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vertionlb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.vertionlb.Location = new System.Drawing.Point(418, 54);
            this.vertionlb.Name = "vertionlb";
            this.vertionlb.Size = new System.Drawing.Size(103, 15);
            this.vertionlb.TabIndex = 11;
            this.vertionlb.Text = " Version 1.0.07";
            // 
            // errorbox
            // 
            this.errorbox.Controls.Add(this.all);
            this.errorbox.Controls.Add(this.onoffindentify);
            this.errorbox.Controls.Add(this.closebtn);
            this.errorbox.Controls.Add(this.setupbtn);
            this.errorbox.Controls.Add(this.infobtn);
            this.errorbox.Location = new System.Drawing.Point(2, 545);
            this.errorbox.Name = "errorbox";
            this.errorbox.Size = new System.Drawing.Size(537, 84);
            this.errorbox.TabIndex = 9;
            this.errorbox.TabStop = false;
            // 
            // all
            // 
            this.all.BackColor = System.Drawing.Color.Ivory;
            this.all.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.all.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.all.Image = ((System.Drawing.Image)(resources.GetObject("all.Image")));
            this.all.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.all.Location = new System.Drawing.Point(219, 10);
            this.all.Name = "all";
            this.all.Size = new System.Drawing.Size(74, 69);
            this.all.TabIndex = 28;
            this.all.Text = "HOME";
            this.all.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.all.UseVisualStyleBackColor = false;
            this.all.Click += new System.EventHandler(this.all_Click);
            // 
            // onoffindentify
            // 
            this.onoffindentify.AutoSize = true;
            this.onoffindentify.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onoffindentify.Location = new System.Drawing.Point(21, 27);
            this.onoffindentify.Name = "onoffindentify";
            this.onoffindentify.Padding = new System.Windows.Forms.Padding(25, 11, 25, 11);
            this.onoffindentify.Size = new System.Drawing.Size(70, 40);
            this.onoffindentify.TabIndex = 2;
            this.onoffindentify.Text = "///";
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.Ivory;
            this.closebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.closebtn.Image = ((System.Drawing.Image)(resources.GetObject("closebtn.Image")));
            this.closebtn.Location = new System.Drawing.Point(457, 10);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(74, 69);
            this.closebtn.TabIndex = 14;
            this.closebtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click_1);
            // 
            // setupbtn
            // 
            this.setupbtn.BackColor = System.Drawing.Color.Ivory;
            this.setupbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setupbtn.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.setupbtn.Image = ((System.Drawing.Image)(resources.GetObject("setupbtn.Image")));
            this.setupbtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.setupbtn.Location = new System.Drawing.Point(299, 10);
            this.setupbtn.Name = "setupbtn";
            this.setupbtn.Size = new System.Drawing.Size(74, 69);
            this.setupbtn.TabIndex = 12;
            this.setupbtn.Text = "SETUP";
            this.setupbtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.setupbtn.UseVisualStyleBackColor = false;
            this.setupbtn.Click += new System.EventHandler(this.setupbtn_Click);
            // 
            // infobtn
            // 
            this.infobtn.BackColor = System.Drawing.Color.Ivory;
            this.infobtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infobtn.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.infobtn.Image = ((System.Drawing.Image)(resources.GetObject("infobtn.Image")));
            this.infobtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.infobtn.Location = new System.Drawing.Point(377, 10);
            this.infobtn.Name = "infobtn";
            this.infobtn.Size = new System.Drawing.Size(74, 69);
            this.infobtn.TabIndex = 13;
            this.infobtn.Text = "INFO";
            this.infobtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.infobtn.UseVisualStyleBackColor = false;
            this.infobtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // homebox
            // 
            this.homebox.Controls.Add(this.whaterror);
            this.homebox.Controls.Add(this.errormsg);
            this.homebox.Controls.Add(this.error_or_no);
            this.homebox.Controls.Add(this.crtorwrongpb);
            this.homebox.Location = new System.Drawing.Point(2, 460);
            this.homebox.Name = "homebox";
            this.homebox.Size = new System.Drawing.Size(537, 79);
            this.homebox.TabIndex = 10;
            this.homebox.TabStop = false;
            // 
            // whaterror
            // 
            this.whaterror.AutoSize = true;
            this.whaterror.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whaterror.Location = new System.Drawing.Point(243, 52);
            this.whaterror.Name = "whaterror";
            this.whaterror.Size = new System.Drawing.Size(20, 18);
            this.whaterror.TabIndex = 12;
            this.whaterror.Text = "///";
            // 
            // errormsg
            // 
            this.errormsg.AutoSize = true;
            this.errormsg.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errormsg.Location = new System.Drawing.Point(243, 20);
            this.errormsg.Name = "errormsg";
            this.errormsg.Size = new System.Drawing.Size(20, 18);
            this.errormsg.TabIndex = 12;
            this.errormsg.Text = "///";
            // 
            // error_or_no
            // 
            this.error_or_no.AutoSize = true;
            this.error_or_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error_or_no.Location = new System.Drawing.Point(117, 16);
            this.error_or_no.MinimumSize = new System.Drawing.Size(20, 0);
            this.error_or_no.Name = "error_or_no";
            this.error_or_no.Padding = new System.Windows.Forms.Padding(15);
            this.error_or_no.Size = new System.Drawing.Size(30, 54);
            this.error_or_no.TabIndex = 12;
            // 
            // crtorwrongpb
            // 
            this.crtorwrongpb.Image = ((System.Drawing.Image)(resources.GetObject("crtorwrongpb.Image")));
            this.crtorwrongpb.Location = new System.Drawing.Point(25, 16);
            this.crtorwrongpb.Name = "crtorwrongpb";
            this.crtorwrongpb.Size = new System.Drawing.Size(86, 54);
            this.crtorwrongpb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.crtorwrongpb.TabIndex = 12;
            this.crtorwrongpb.TabStop = false;
            // 
            // setroi
            // 
            this.setroi.Font = new System.Drawing.Font("Modern No. 20", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setroi.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.setroi.Location = new System.Drawing.Point(103, 262);
            this.setroi.Name = "setroi";
            this.setroi.Size = new System.Drawing.Size(187, 38);
            this.setroi.TabIndex = 9;
            this.setroi.Text = "SET ROI";
            this.setroi.UseVisualStyleBackColor = true;
            this.setroi.Click += new System.EventHandler(this.setroi_Click);
            // 
            // vendtb
            // 
            this.vendtb.Location = new System.Drawing.Point(200, 236);
            this.vendtb.Name = "vendtb";
            this.vendtb.Size = new System.Drawing.Size(87, 20);
            this.vendtb.TabIndex = 7;
            // 
            // hendtb
            // 
            this.hendtb.Location = new System.Drawing.Point(200, 207);
            this.hendtb.Name = "hendtb";
            this.hendtb.Size = new System.Drawing.Size(87, 20);
            this.hendtb.TabIndex = 6;
            // 
            // vstarttb
            // 
            this.vstarttb.Location = new System.Drawing.Point(106, 236);
            this.vstarttb.Name = "vstarttb";
            this.vstarttb.Size = new System.Drawing.Size(88, 20);
            this.vstarttb.TabIndex = 5;
            // 
            // hstarttb
            // 
            this.hstarttb.Location = new System.Drawing.Point(106, 207);
            this.hstarttb.Name = "hstarttb";
            this.hstarttb.Size = new System.Drawing.Size(88, 20);
            this.hstarttb.TabIndex = 4;
            // 
            // startroi
            // 
            this.startroi.AutoSize = true;
            this.startroi.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startroi.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.startroi.Location = new System.Drawing.Point(122, 187);
            this.startroi.Name = "startroi";
            this.startroi.Size = new System.Drawing.Size(64, 17);
            this.startroi.TabIndex = 3;
            this.startroi.Text = "START ";
            // 
            // roi2
            // 
            this.roi2.AutoSize = true;
            this.roi2.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roi2.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.roi2.Location = new System.Drawing.Point(17, 239);
            this.roi2.Name = "roi2";
            this.roi2.Size = new System.Drawing.Size(85, 17);
            this.roi2.TabIndex = 2;
            this.roi2.Text = "VRET ROI";
            // 
            // endroi
            // 
            this.endroi.AutoSize = true;
            this.endroi.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endroi.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.endroi.Location = new System.Drawing.Point(212, 187);
            this.endroi.Name = "endroi";
            this.endroi.Size = new System.Drawing.Size(47, 17);
            this.endroi.TabIndex = 1;
            this.endroi.Text = "END ";
            // 
            // roi1
            // 
            this.roi1.AutoSize = true;
            this.roi1.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roi1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.roi1.Location = new System.Drawing.Point(17, 207);
            this.roi1.Name = "roi1";
            this.roi1.Size = new System.Drawing.Size(86, 17);
            this.roi1.TabIndex = 0;
            this.roi1.Text = "HORZ ROI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(16, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "..ROI SETTINGS..";
            // 
            // errorloglb
            // 
            this.errorloglb.AutoSize = true;
            this.errorloglb.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorloglb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.errorloglb.Location = new System.Drawing.Point(697, 3);
            this.errorloglb.Name = "errorloglb";
            this.errorloglb.Size = new System.Drawing.Size(159, 24);
            this.errorloglb.TabIndex = 15;
            this.errorloglb.Text = " ERROR LOG :";
            // 
            // cameracollection
            // 
            this.cameracollection.BackColor = System.Drawing.Color.LightSeaGreen;
            this.cameracollection.FormattingEnabled = true;
            this.cameracollection.Location = new System.Drawing.Point(304, 3);
            this.cameracollection.Name = "cameracollection";
            this.cameracollection.Size = new System.Drawing.Size(119, 21);
            this.cameracollection.TabIndex = 7;
            // 
            // saveparameter
            // 
            this.saveparameter.BackColor = System.Drawing.Color.LightSeaGreen;
            this.saveparameter.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveparameter.ForeColor = System.Drawing.Color.White;
            this.saveparameter.Location = new System.Drawing.Point(200, 534);
            this.saveparameter.Name = "saveparameter";
            this.saveparameter.Size = new System.Drawing.Size(184, 36);
            this.saveparameter.TabIndex = 6;
            this.saveparameter.Text = "SAVE PARAMETER";
            this.saveparameter.UseVisualStyleBackColor = false;
            this.saveparameter.Click += new System.EventHandler(this.saveparameter_Click);
            // 
            // rotatestreambtn
            // 
            this.rotatestreambtn.BackColor = System.Drawing.Color.Ivory;
            this.rotatestreambtn.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotatestreambtn.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.rotatestreambtn.Location = new System.Drawing.Point(255, 78);
            this.rotatestreambtn.Name = "rotatestreambtn";
            this.rotatestreambtn.Size = new System.Drawing.Size(121, 44);
            this.rotatestreambtn.TabIndex = 6;
            this.rotatestreambtn.Text = "ROTATE CAMERA";
            this.rotatestreambtn.UseVisualStyleBackColor = false;
            this.rotatestreambtn.Click += new System.EventHandler(this.rotatestreambtn_Click);
            // 
            // camerasettinglb
            // 
            this.camerasettinglb.AutoSize = true;
            this.camerasettinglb.BackColor = System.Drawing.Color.LightSeaGreen;
            this.camerasettinglb.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.camerasettinglb.ForeColor = System.Drawing.Color.Ivory;
            this.camerasettinglb.Location = new System.Drawing.Point(16, 29);
            this.camerasettinglb.Name = "camerasettinglb";
            this.camerasettinglb.Size = new System.Drawing.Size(243, 24);
            this.camerasettinglb.TabIndex = 8;
            this.camerasettinglb.Text = "..CAMERA SETTINGS..";
            // 
            // switchcamera
            // 
            this.switchcamera.AutoSize = true;
            this.switchcamera.Font = new System.Drawing.Font("Modern No. 20", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switchcamera.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.switchcamera.Location = new System.Drawing.Point(856, 13);
            this.switchcamera.Name = "switchcamera";
            this.switchcamera.Size = new System.Drawing.Size(125, 14);
            this.switchcamera.TabIndex = 13;
            this.switchcamera.Text = "SELECT  SETTINGS";
            // 
            // txtErrorDisplay
            // 
            this.txtErrorDisplay.BackColor = System.Drawing.Color.White;
            this.txtErrorDisplay.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtErrorDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrorDisplay.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtErrorDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtErrorDisplay.Location = new System.Drawing.Point(0, 0);
            this.txtErrorDisplay.Multiline = true;
            this.txtErrorDisplay.Name = "txtErrorDisplay";
            this.txtErrorDisplay.ReadOnly = true;
            this.txtErrorDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtErrorDisplay.Size = new System.Drawing.Size(449, 570);
            this.txtErrorDisplay.TabIndex = 0;
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.fpslb2);
            this.panelInfo.Controls.Add(this.fpslb);
            this.panelInfo.Controls.Add(this.cameraselectbox);
            this.panelInfo.Controls.Add(this.fliph2);
            this.panelInfo.Controls.Add(this.flip1v);
            this.panelInfo.Controls.Add(this.brightsvalue1);
            this.panelInfo.Controls.Add(this.saturationsvalue1);
            this.panelInfo.Controls.Add(this.contrastsvalue1);
            this.panelInfo.Controls.Add(this.zoomsvalue1);
            this.panelInfo.Controls.Add(this.saturation0lb);
            this.panelInfo.Controls.Add(this.contrast0lb);
            this.panelInfo.Controls.Add(this.zoom0lb);
            this.panelInfo.Controls.Add(this.saveparameter);
            this.panelInfo.Controls.Add(this.brightness0lb);
            this.panelInfo.Controls.Add(this.zoomevalue);
            this.panelInfo.Controls.Add(this.contrastevalue);
            this.panelInfo.Controls.Add(this.saturationevalue);
            this.panelInfo.Controls.Add(this.brightevalue);
            this.panelInfo.Controls.Add(this.adjustclrear);
            this.panelInfo.Controls.Add(this.zoombar);
            this.panelInfo.Controls.Add(this.zoomlb);
            this.panelInfo.Controls.Add(this.contrastscroll);
            this.panelInfo.Controls.Add(this.contrastlb);
            this.panelInfo.Controls.Add(this.saturationscroll);
            this.panelInfo.Controls.Add(this.brighscroll);
            this.panelInfo.Controls.Add(this.saturationlb);
            this.panelInfo.Controls.Add(this.brightlb);
            this.panelInfo.Controls.Add(this.adjustlb);
            this.panelInfo.Controls.Add(this.getcwidth);
            this.panelInfo.Controls.Add(this.capturebtn);
            this.panelInfo.Controls.Add(this.onoffbtn);
            this.panelInfo.Controls.Add(this.getchight);
            this.panelInfo.Controls.Add(this.label1);
            this.panelInfo.Controls.Add(this.setroi);
            this.panelInfo.Controls.Add(this.chdescription);
            this.panelInfo.Controls.Add(this.cheight);
            this.panelInfo.Controls.Add(this.cwdescription);
            this.panelInfo.Controls.Add(this.cwidth);
            this.panelInfo.Controls.Add(this.cameracollection);
            this.panelInfo.Controls.Add(this.outcameralb);
            this.panelInfo.Controls.Add(this.vendtb);
            this.panelInfo.Controls.Add(this.rotatestreambtn);
            this.panelInfo.Controls.Add(this.hendtb);
            this.panelInfo.Controls.Add(this.camerasettinglb);
            this.panelInfo.Controls.Add(this.roi1);
            this.panelInfo.Controls.Add(this.vstarttb);
            this.panelInfo.Controls.Add(this.endroi);
            this.panelInfo.Controls.Add(this.hstarttb);
            this.panelInfo.Controls.Add(this.roi2);
            this.panelInfo.Controls.Add(this.startroi);
            this.panelInfo.Controls.Add(this.txtErrorDisplay);
            this.panelInfo.Location = new System.Drawing.Point(555, 27);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(449, 570);
            this.panelInfo.TabIndex = 16;
            // 
            // fpslb2
            // 
            this.fpslb2.AutoSize = true;
            this.fpslb2.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fpslb2.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.fpslb2.Location = new System.Drawing.Point(264, 303);
            this.fpslb2.Name = "fpslb2";
            this.fpslb2.Size = new System.Drawing.Size(16, 16);
            this.fpslb2.TabIndex = 58;
            this.fpslb2.Text = " .";
            // 
            // fpslb
            // 
            this.fpslb.AutoSize = true;
            this.fpslb.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fpslb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.fpslb.Location = new System.Drawing.Point(103, 303);
            this.fpslb.Name = "fpslb";
            this.fpslb.Size = new System.Drawing.Size(52, 17);
            this.fpslb.TabIndex = 57;
            this.fpslb.Text = " FPS :";
            // 
            // cameraselectbox
            // 
            this.cameraselectbox.BackColor = System.Drawing.Color.LightSeaGreen;
            this.cameraselectbox.FormattingEnabled = true;
            this.cameraselectbox.Location = new System.Drawing.Point(3, 1);
            this.cameraselectbox.Name = "cameraselectbox";
            this.cameraselectbox.Size = new System.Drawing.Size(140, 21);
            this.cameraselectbox.TabIndex = 18;
            // 
            // fliph2
            // 
            this.fliph2.AutoSize = true;
            this.fliph2.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fliph2.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.fliph2.Location = new System.Drawing.Point(151, 92);
            this.fliph2.Name = "fliph2";
            this.fliph2.Size = new System.Drawing.Size(81, 19);
            this.fliph2.TabIndex = 56;
            this.fliph2.Text = " FLIP X";
            this.fliph2.UseVisualStyleBackColor = true;
            // 
            // flip1v
            // 
            this.flip1v.AutoSize = true;
            this.flip1v.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flip1v.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.flip1v.Location = new System.Drawing.Point(44, 92);
            this.flip1v.Name = "flip1v";
            this.flip1v.Size = new System.Drawing.Size(80, 19);
            this.flip1v.TabIndex = 55;
            this.flip1v.Text = " FLIP Y";
            this.flip1v.UseVisualStyleBackColor = true;
            // 
            // brightsvalue1
            // 
            this.brightsvalue1.AutoSize = true;
            this.brightsvalue1.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brightsvalue1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.brightsvalue1.Location = new System.Drawing.Point(17, 428);
            this.brightsvalue1.Name = "brightsvalue1";
            this.brightsvalue1.Size = new System.Drawing.Size(17, 16);
            this.brightsvalue1.TabIndex = 54;
            this.brightsvalue1.Text = "0";
            // 
            // saturationsvalue1
            // 
            this.saturationsvalue1.AutoSize = true;
            this.saturationsvalue1.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saturationsvalue1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.saturationsvalue1.Location = new System.Drawing.Point(228, 428);
            this.saturationsvalue1.Name = "saturationsvalue1";
            this.saturationsvalue1.Size = new System.Drawing.Size(17, 16);
            this.saturationsvalue1.TabIndex = 53;
            this.saturationsvalue1.Text = "0";
            // 
            // contrastsvalue1
            // 
            this.contrastsvalue1.AutoSize = true;
            this.contrastsvalue1.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contrastsvalue1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.contrastsvalue1.Location = new System.Drawing.Point(17, 500);
            this.contrastsvalue1.Name = "contrastsvalue1";
            this.contrastsvalue1.Size = new System.Drawing.Size(17, 16);
            this.contrastsvalue1.TabIndex = 52;
            this.contrastsvalue1.Text = "0";
            // 
            // zoomsvalue1
            // 
            this.zoomsvalue1.AutoSize = true;
            this.zoomsvalue1.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomsvalue1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.zoomsvalue1.Location = new System.Drawing.Point(228, 510);
            this.zoomsvalue1.Name = "zoomsvalue1";
            this.zoomsvalue1.Size = new System.Drawing.Size(17, 16);
            this.zoomsvalue1.TabIndex = 51;
            this.zoomsvalue1.Text = "0";
            // 
            // saturation0lb
            // 
            this.saturation0lb.AutoSize = true;
            this.saturation0lb.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saturation0lb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.saturation0lb.Location = new System.Drawing.Point(371, 406);
            this.saturation0lb.Name = "saturation0lb";
            this.saturation0lb.Size = new System.Drawing.Size(17, 16);
            this.saturation0lb.TabIndex = 49;
            this.saturation0lb.Text = "0";
            // 
            // contrast0lb
            // 
            this.contrast0lb.AutoSize = true;
            this.contrast0lb.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contrast0lb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.contrast0lb.Location = new System.Drawing.Point(159, 477);
            this.contrast0lb.Name = "contrast0lb";
            this.contrast0lb.Size = new System.Drawing.Size(17, 16);
            this.contrast0lb.TabIndex = 48;
            this.contrast0lb.Text = "0";
            // 
            // zoom0lb
            // 
            this.zoom0lb.AutoSize = true;
            this.zoom0lb.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoom0lb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.zoom0lb.Location = new System.Drawing.Point(373, 478);
            this.zoom0lb.Name = "zoom0lb";
            this.zoom0lb.Size = new System.Drawing.Size(17, 16);
            this.zoom0lb.TabIndex = 47;
            this.zoom0lb.Text = "0";
            // 
            // brightness0lb
            // 
            this.brightness0lb.AutoSize = true;
            this.brightness0lb.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brightness0lb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.brightness0lb.Location = new System.Drawing.Point(159, 404);
            this.brightness0lb.Name = "brightness0lb";
            this.brightness0lb.Size = new System.Drawing.Size(17, 16);
            this.brightness0lb.TabIndex = 45;
            this.brightness0lb.Text = "0";
            // 
            // zoomevalue
            // 
            this.zoomevalue.AutoSize = true;
            this.zoomevalue.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomevalue.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.zoomevalue.Location = new System.Drawing.Point(342, 510);
            this.zoomevalue.Name = "zoomevalue";
            this.zoomevalue.Size = new System.Drawing.Size(35, 16);
            this.zoomevalue.TabIndex = 44;
            this.zoomevalue.Text = "100";
            // 
            // contrastevalue
            // 
            this.contrastevalue.AutoSize = true;
            this.contrastevalue.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contrastevalue.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.contrastevalue.Location = new System.Drawing.Point(132, 504);
            this.contrastevalue.Name = "contrastevalue";
            this.contrastevalue.Size = new System.Drawing.Size(35, 16);
            this.contrastevalue.TabIndex = 43;
            this.contrastevalue.Text = "100";
            // 
            // saturationevalue
            // 
            this.saturationevalue.AutoSize = true;
            this.saturationevalue.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saturationevalue.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.saturationevalue.Location = new System.Drawing.Point(342, 426);
            this.saturationevalue.Name = "saturationevalue";
            this.saturationevalue.Size = new System.Drawing.Size(35, 16);
            this.saturationevalue.TabIndex = 42;
            this.saturationevalue.Text = "100";
            // 
            // brightevalue
            // 
            this.brightevalue.AutoSize = true;
            this.brightevalue.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brightevalue.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.brightevalue.Location = new System.Drawing.Point(132, 428);
            this.brightevalue.Name = "brightevalue";
            this.brightevalue.Size = new System.Drawing.Size(35, 16);
            this.brightevalue.TabIndex = 41;
            this.brightevalue.Text = "100";
            // 
            // adjustclrear
            // 
            this.adjustclrear.BackColor = System.Drawing.Color.LightSeaGreen;
            this.adjustclrear.Font = new System.Drawing.Font("Modern No. 20", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adjustclrear.ForeColor = System.Drawing.Color.White;
            this.adjustclrear.Location = new System.Drawing.Point(3, 534);
            this.adjustclrear.Name = "adjustclrear";
            this.adjustclrear.Size = new System.Drawing.Size(191, 35);
            this.adjustclrear.TabIndex = 34;
            this.adjustclrear.Text = "RESET ADJUSTMENT";
            this.adjustclrear.UseVisualStyleBackColor = false;
            this.adjustclrear.Click += new System.EventHandler(this.adjustclrear_Click);
            // 
            // zoombar
            // 
            this.zoombar.Location = new System.Drawing.Point(221, 477);
            this.zoombar.Name = "zoombar";
            this.zoombar.Size = new System.Drawing.Size(155, 45);
            this.zoombar.TabIndex = 33;
            // 
            // zoomlb
            // 
            this.zoomlb.AutoSize = true;
            this.zoomlb.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomlb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.zoomlb.Location = new System.Drawing.Point(228, 454);
            this.zoomlb.Name = "zoomlb";
            this.zoomlb.Size = new System.Drawing.Size(62, 17);
            this.zoomlb.TabIndex = 32;
            this.zoomlb.Text = "ZOOM :";
            // 
            // contrastscroll
            // 
            this.contrastscroll.Location = new System.Drawing.Point(7, 475);
            this.contrastscroll.Name = "contrastscroll";
            this.contrastscroll.Size = new System.Drawing.Size(155, 45);
            this.contrastscroll.TabIndex = 30;
            // 
            // contrastlb
            // 
            this.contrastlb.AutoSize = true;
            this.contrastlb.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contrastlb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.contrastlb.Location = new System.Drawing.Point(14, 453);
            this.contrastlb.Name = "contrastlb";
            this.contrastlb.Size = new System.Drawing.Size(99, 17);
            this.contrastlb.TabIndex = 28;
            this.contrastlb.Text = "CONTRAST :";
            // 
            // saturationscroll
            // 
            this.saturationscroll.Location = new System.Drawing.Point(221, 404);
            this.saturationscroll.Name = "saturationscroll";
            this.saturationscroll.Size = new System.Drawing.Size(155, 45);
            this.saturationscroll.TabIndex = 27;
            // 
            // brighscroll
            // 
            this.brighscroll.Location = new System.Drawing.Point(7, 402);
            this.brighscroll.Name = "brighscroll";
            this.brighscroll.Size = new System.Drawing.Size(155, 45);
            this.brighscroll.TabIndex = 26;
            // 
            // saturationlb
            // 
            this.saturationlb.AutoSize = true;
            this.saturationlb.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saturationlb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.saturationlb.Location = new System.Drawing.Point(228, 383);
            this.saturationlb.Name = "saturationlb";
            this.saturationlb.Size = new System.Drawing.Size(120, 17);
            this.saturationlb.TabIndex = 25;
            this.saturationlb.Text = "SATURATION :";
            // 
            // brightlb
            // 
            this.brightlb.AutoSize = true;
            this.brightlb.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brightlb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.brightlb.Location = new System.Drawing.Point(7, 382);
            this.brightlb.Name = "brightlb";
            this.brightlb.Size = new System.Drawing.Size(126, 17);
            this.brightlb.TabIndex = 18;
            this.brightlb.Text = " BRIGHTNESS :";
            // 
            // adjustlb
            // 
            this.adjustlb.AutoSize = true;
            this.adjustlb.BackColor = System.Drawing.Color.LightSeaGreen;
            this.adjustlb.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adjustlb.ForeColor = System.Drawing.SystemColors.Info;
            this.adjustlb.Location = new System.Drawing.Point(16, 343);
            this.adjustlb.Name = "adjustlb";
            this.adjustlb.Size = new System.Drawing.Size(299, 24);
            this.adjustlb.TabIndex = 24;
            this.adjustlb.Text = "..ADJUSTMENT SETTINGS..";
            // 
            // getcwidth
            // 
            this.getcwidth.AutoSize = true;
            this.getcwidth.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getcwidth.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.getcwidth.Location = new System.Drawing.Point(264, 172);
            this.getcwidth.Name = "getcwidth";
            this.getcwidth.Size = new System.Drawing.Size(16, 16);
            this.getcwidth.TabIndex = 23;
            this.getcwidth.Text = " .";
            // 
            // getchight
            // 
            this.getchight.AutoSize = true;
            this.getchight.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getchight.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.getchight.Location = new System.Drawing.Point(264, 237);
            this.getchight.Name = "getchight";
            this.getchight.Size = new System.Drawing.Size(16, 16);
            this.getchight.TabIndex = 22;
            this.getchight.Text = " .";
            // 
            // chdescription
            // 
            this.chdescription.AutoSize = true;
            this.chdescription.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chdescription.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.chdescription.Location = new System.Drawing.Point(41, 172);
            this.chdescription.Name = "chdescription";
            this.chdescription.Size = new System.Drawing.Size(12, 16);
            this.chdescription.TabIndex = 22;
            this.chdescription.Text = " ";
            // 
            // cheight
            // 
            this.cheight.AutoSize = true;
            this.cheight.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cheight.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.cheight.Location = new System.Drawing.Point(100, 236);
            this.cheight.Name = "cheight";
            this.cheight.Size = new System.Drawing.Size(159, 17);
            this.cheight.TabIndex = 21;
            this.cheight.Text = " CAMERA HEIGHT :";
            // 
            // cwdescription
            // 
            this.cwdescription.AutoSize = true;
            this.cwdescription.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cwdescription.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.cwdescription.Location = new System.Drawing.Point(41, 106);
            this.cwdescription.Name = "cwdescription";
            this.cwdescription.Size = new System.Drawing.Size(12, 16);
            this.cwdescription.TabIndex = 20;
            this.cwdescription.Text = " ";
            // 
            // cwidth
            // 
            this.cwidth.AutoSize = true;
            this.cwidth.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cwidth.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.cwidth.Location = new System.Drawing.Point(103, 170);
            this.cwidth.Name = "cwidth";
            this.cwidth.Size = new System.Drawing.Size(152, 17);
            this.cwidth.TabIndex = 19;
            this.cwidth.Text = " CAMERA WIDTH :";
            // 
            // outcameralb
            // 
            this.outcameralb.AutoSize = true;
            this.outcameralb.BackColor = System.Drawing.Color.LightSeaGreen;
            this.outcameralb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.outcameralb.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outcameralb.ForeColor = System.Drawing.Color.Beige;
            this.outcameralb.Location = new System.Drawing.Point(24, 79);
            this.outcameralb.Name = "outcameralb";
            this.outcameralb.Size = new System.Drawing.Size(352, 27);
            this.outcameralb.TabIndex = 18;
            this.outcameralb.Text = "OUR CAMERA RESOLUTION";
            // 
            // savelogbtn
            // 
            this.savelogbtn.BackColor = System.Drawing.Color.Ivory;
            this.savelogbtn.Font = new System.Drawing.Font("Modern No. 20", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savelogbtn.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.savelogbtn.Image = ((System.Drawing.Image)(resources.GetObject("savelogbtn.Image")));
            this.savelogbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.savelogbtn.Location = new System.Drawing.Point(900, 603);
            this.savelogbtn.Name = "savelogbtn";
            this.savelogbtn.Size = new System.Drawing.Size(104, 29);
            this.savelogbtn.TabIndex = 15;
            this.savelogbtn.Text = "SAVE LOG";
            this.savelogbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.savelogbtn.UseVisualStyleBackColor = false;
            // 
            // clearlogbtn
            // 
            this.clearlogbtn.BackColor = System.Drawing.Color.Ivory;
            this.clearlogbtn.Font = new System.Drawing.Font("Modern No. 20", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearlogbtn.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.clearlogbtn.Image = ((System.Drawing.Image)(resources.GetObject("clearlogbtn.Image")));
            this.clearlogbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.clearlogbtn.Location = new System.Drawing.Point(554, 603);
            this.clearlogbtn.Name = "clearlogbtn";
            this.clearlogbtn.Size = new System.Drawing.Size(117, 29);
            this.clearlogbtn.TabIndex = 17;
            this.clearlogbtn.Text = "CLEAR LOG";
            this.clearlogbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.clearlogbtn.UseVisualStyleBackColor = false;
            this.clearlogbtn.Click += new System.EventHandler(this.clearlogbtn_Click);
            // 
            // streampbox1
            // 
            this.streampbox1.BackColor = System.Drawing.Color.Black;
            this.streampbox1.Location = new System.Drawing.Point(2, 86);
            this.streampbox1.Name = "streampbox1";
            this.streampbox1.Size = new System.Drawing.Size(537, 383);
            this.streampbox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.streampbox1.TabIndex = 0;
            this.streampbox1.TabStop = false;
            // 
            // cameras
            // 
            this.cameras.AutoSize = true;
            this.cameras.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameras.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.cameras.Location = new System.Drawing.Point(559, 9);
            this.cameras.Name = "cameras";
            this.cameras.Size = new System.Drawing.Size(107, 17);
            this.cameras.TabIndex = 57;
            this.cameras.Text = "select cameras";
            // 
            // webcamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(1009, 644);
            this.ControlBox = false;
            this.Controls.Add(this.cameras);
            this.Controls.Add(this.savelogbtn);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.errorloglb);
            this.Controls.Add(this.homebox);
            this.Controls.Add(this.errorbox);
            this.Controls.Add(this.headingbox);
            this.Controls.Add(this.streampbox1);
            this.Controls.Add(this.switchcamera);
            this.Controls.Add(this.clearlogbtn);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Name = "webcamera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "webcamera";
            this.Load += new System.EventHandler(this.webcamera_Load);
            this.headingbox.ResumeLayout(false);
            this.headingbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kmllogo)).EndInit();
            this.errorbox.ResumeLayout(false);
            this.errorbox.PerformLayout();
            this.homebox.ResumeLayout(false);
            this.homebox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crtorwrongpb)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoombar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastscroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationscroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brighscroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.streampbox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox streampbox1;
        private System.Windows.Forms.Button onoffbtn;
        private System.Windows.Forms.Button capturebtn;
        private System.Windows.Forms.GroupBox headingbox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label cnamelb;
        private System.Windows.Forms.Label vertionlb;
        private System.Windows.Forms.GroupBox errorbox;
        private System.Windows.Forms.GroupBox homebox;
        private System.Windows.Forms.Button setupbtn;
        private System.Windows.Forms.Button infobtn;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.PictureBox crtorwrongpb;
        private System.Windows.Forms.Label error_or_no;
        private System.Windows.Forms.Label errormsg;
        private System.Windows.Forms.Label whaterror;
        private System.Windows.Forms.Button setroi;
        private System.Windows.Forms.TextBox vendtb;
        private System.Windows.Forms.TextBox hendtb;
        private System.Windows.Forms.TextBox vstarttb;
        private System.Windows.Forms.TextBox hstarttb;
        private System.Windows.Forms.Label startroi;
        private System.Windows.Forms.Label roi2;
        private System.Windows.Forms.Label endroi;
        private System.Windows.Forms.Label roi1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label errorloglb;
        private System.Windows.Forms.Button clearlogbtn;
        private System.Windows.Forms.Button savelogbtn;
        private System.Windows.Forms.Label onoffindentify;
        private System.Windows.Forms.ComboBox cameracollection;
        private System.Windows.Forms.Button saveparameter;
        private System.Windows.Forms.Button rotatestreambtn;
        private System.Windows.Forms.Label camerasettinglb;
        private System.Windows.Forms.Label switchcamera;
        private System.Windows.Forms.TextBox txtErrorDisplay;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label outcameralb;
        private System.Windows.Forms.Label cwidth;
        private System.Windows.Forms.Label cwdescription;
        private System.Windows.Forms.Label cheight;
        private System.Windows.Forms.Label chdescription;
        private System.Windows.Forms.Button all;
        private System.Windows.Forms.PictureBox kmllogo;
        private System.Windows.Forms.Label getchight;
        private System.Windows.Forms.Label getcwidth;
        private System.Windows.Forms.Label adjustlb;
        private System.Windows.Forms.TrackBar saturationscroll;
        private System.Windows.Forms.TrackBar brighscroll;
        private System.Windows.Forms.Label saturationlb;
        private System.Windows.Forms.Label brightlb;
        private System.Windows.Forms.TrackBar contrastscroll;
        private System.Windows.Forms.Label contrastlb;
        private System.Windows.Forms.TrackBar zoombar;
        private System.Windows.Forms.Label zoomlb;
        private System.Windows.Forms.Button adjustclrear;
        private System.Windows.Forms.Label zoomevalue;
        private System.Windows.Forms.Label contrastevalue;
        private System.Windows.Forms.Label saturationevalue;
        private System.Windows.Forms.Label brightevalue;
        private System.Windows.Forms.Label saturation0lb;
        private System.Windows.Forms.Label contrast0lb;
        private System.Windows.Forms.Label zoom0lb;
        private System.Windows.Forms.Label brightness0lb;
        private System.Windows.Forms.Label brightsvalue1;
        private System.Windows.Forms.Label saturationsvalue1;
        private System.Windows.Forms.Label contrastsvalue1;
        private System.Windows.Forms.Label zoomsvalue1;
        private System.Windows.Forms.CheckBox fliph2;
        private System.Windows.Forms.CheckBox flip1v;
        private System.Windows.Forms.ComboBox cameraselectbox;
        private System.Windows.Forms.Label cameras;
        private System.Windows.Forms.Label fpslb2;
        private System.Windows.Forms.Label fpslb;
    }
}

