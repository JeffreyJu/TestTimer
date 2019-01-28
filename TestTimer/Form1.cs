using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace TestTimer
{
    public partial class Form1 : Form
    {
       
      List<BrushWithName> m_list = new List<BrushWithName>();
      Brush m_currentBrush;

      int count;

      int fontSize0 = 20;
      int fontSize1 = 24;
         
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();


            initBackBrush();

            System.Timers.Timer timer1 = new System.Timers.Timer(3000);
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timeOut);
            timer1.AutoReset = true;
            timer1.Enabled = true;    
    
   
        }

        public void initBackBrush()
        {
  
            PropertyInfo[] props = typeof(Brushes).GetProperties();

            foreach (var prop in props)
            {
                BrushWithName entity = new BrushWithName();
                entity.Name = prop.Name;
                entity.Value = (Brush)prop.GetValue(null, null);
                m_list.Add(entity);
            }

        }


        public void timeOut(object source, System.Timers.ElapsedEventArgs e)
        {
            if(count >= m_list.Count())
            {
                count = 0;
            }
            m_currentBrush = m_list[count].Value;
            count++;
        }
       
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        protected override void OnPaint(PaintEventArgs e)
        {
      
            Graphics g = e.Graphics; 
            //Graphics g = this.CreateGraphics();

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            if (m_currentBrush == null)
                m_currentBrush = m_list[0].Value;
            g.FillRectangle(m_currentBrush, rect);

            string strTime = DateTime.Now.ToString("HH:mm:ss.fff");
            g.DrawString(strTime.Substring(0,8), new Font("楷体",150), new SolidBrush(Color.Red), new PointF(100.0f, this.Height/2 -180));
            g.DrawString(strTime.Substring(8), new Font("楷体", 180), new SolidBrush(Color.Red), new PointF(this.Width / 2 + 100, this.Height / 2));
            this.Invalidate();
        }

        class BrushWithName
        {
            public Brush Value;
            public String Name;
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }
    }
}
